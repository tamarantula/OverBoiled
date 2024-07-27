using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftableItemController : ItemController
{
    public RecipeOption[] recipeOptions;
    public GameObject progressBarPrefab;

    [SerializeField]
    private int currentOptionNum = -1;

    private RecipeOption currentOption {
        get { return currentOptionNum==-1?null:recipeOptions[currentOptionNum]; }
    }

    [SerializeField]
    private int currentStepNum = -1;
    private RecipeStep currentStep {
        get { return currentStepNum==-1?null:currentOption.steps[currentStepNum]; }
    }
    
    [SerializeField] 
    private float currentStepSecondsOrClicksLeft;

    private ContainerController currentContainer;

    private ProgressBarController progressBar;

    void Start(){
        var bar = Instantiate(progressBarPrefab,this.transform);
        bar.name=progressBarPrefab.name;
        bar.transform.localPosition=new Vector3(0,0.6f,0);
        progressBar = bar.GetComponent<ProgressBarController>();
    }
 
    void Awake(){
        //TODO: get progress bar and current container.
    }

    void Update()
    {
        if(currentContainer!=null&&currentOption!=null&&currentStep.requiredContainer==currentContainer.containerName){
            if(!currentStep.requiredInteract){
                if(currentStepSecondsOrClicksLeft>0){
                    currentStepSecondsOrClicksLeft-=Time.deltaTime;
                }
            }
            if(currentStepSecondsOrClicksLeft<=0){
                completeStep();
                return;
            }
        }

        if(currentOption!=null){
            if(currentStep.secondsOrClicksForProgress>0&&currentStep.secondsOrClicksForProgress>currentStepSecondsOrClicksLeft){
                progressBar.SetHidden(false);
                progressBar.SetProgress(1-currentStepSecondsOrClicksLeft/currentStep.secondsOrClicksForProgress);
            }else{
                progressBar.SetHidden(true);
            }
            
        }
    }

    public override bool onPlaceItem(GameObject newparent){
        ContainerController newContainer = newparent.GetComponent<ContainerController>().GetContainer();
        
        string containerType = newContainer.containerName;
        GameObject existingItem = newContainer.GetCurrentItem();

        
        Debug.Log("Placing "+this.gameObject.name+" on "+containerType+" which has already "+existingItem);

        // No path selected yet. select one based on what is happening first.
        if(currentOption == null){
            for (int i = 0; i < recipeOptions.Length; i++){
                RecipeOption ro = recipeOptions[i];
                if(ro.steps.Length>0&&ro.steps[0].requiredContainer==containerType){
                    currentOptionNum = i;
                    completeStep();
                    break;
                }
            }
        }

        //TODO: check if next step is 'combine' step and if it is then we do stuff here.
        if(existingItem != null && (currentOption == null || currentStep.requiredCombinationItem != null)){
            if(tryCombine(existingItem, false)){
                return false;
            }
        }
        
        if(currentOption == null || containerType == "table") return containerType == "table"; // at this point, if its not figured out yet, we only place on table.
        
        Debug.Log("Found Recipe "+currentOption);
        Debug.Log("Placing on "+containerType+" but need "+currentStep.requiredContainer);

        if(currentStep.requiredContainer == containerType){
            currentContainer=newContainer;
            return true;
        }

        return false;
    }

    //TODO: test me for sure, this is just brain code. no actual implementation or use yet.
    public bool tryCombine(GameObject otherObject,bool changeSelf){
        if(currentOption == null){
            for (int i = 0; i < recipeOptions.Length; i++){
                RecipeOption ro = recipeOptions[i];
                if(ro.steps.Length>0&&ro.steps[0].requiredCombinationItem.name==otherObject.name){
                    currentOptionNum = i;
                    return performCombineStep(otherObject,currentOption.result,changeSelf);
                }
            }
        }else{
            if(currentStep.requiredCombinationItem.name==otherObject.name){
                return performCombineStep(otherObject,currentOption.result,changeSelf);
            }else if(otherObject.GetComponent<CraftableItemController>() != null){
                return otherObject.GetComponent<CraftableItemController>().tryCombine(this.gameObject,true);
            }
        }
        return false;
    }

    public void completeStep(){
        currentStepNum+=1;
        if(currentOption.steps.Length <= currentStepNum){
            completeOption(this.gameObject,currentOption.result);
        }else{
            currentStepSecondsOrClicksLeft=currentStep.secondsOrClicksForProgress;
        }
    }

    public void completeOption(GameObject objectToReplace,GameObject result){
        Debug.Log("Finished recipe, changing to new object");
        var newobj = Instantiate(result, objectToReplace.transform.parent);
        newobj.name=result.name;
        ContainerController currentContainer = this.GetComponentInParent<ContainerController>();
        if(currentContainer != null){
            currentContainer.placeNewItem(newobj);
        }        
        Destroy(this.gameObject);
    }

    public bool performCombineStep(GameObject otherObject,GameObject resultObject, bool changeSelf){
        if(changeSelf){
            completeOption(this.gameObject,resultObject);
        }else{
            completeOption(otherObject,resultObject);
        }
        Destroy(otherObject);

        return true;
    }

    public override bool onRemoveItem(GameObject oldparent){
        ContainerController container = oldparent.GetComponent<ContainerController>().GetContainer();
        
        string containerType = container.containerName;

        if(currentOption!=null&&currentStep.requiredContainer==containerType){
            if(currentStep.requiredInteract){
                if(currentStepSecondsOrClicksLeft>0){
                    currentStepSecondsOrClicksLeft-=1;
                }
            }
            if(currentStepSecondsOrClicksLeft>0){
                return false;
            }
        }
        return true;
    }
}
