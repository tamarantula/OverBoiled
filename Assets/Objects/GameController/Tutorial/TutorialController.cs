using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    public GameObject player;
    private PlayerController pc;
    public List<GameObject> bowls;
    public List<GameObject> mortar_pestles;
    public List<GameObject> cauldrons;
    public List<GameObject> distilleries;
    public List<GameObject> plates;

    public enum TutorialStage {
        Movement,
        PickupBowl,
        PlaceInMortar,
        PlaceInCauldron,
        PlaceInDistillery,
        SubmitToPlate
    }

    public TutorialStage curstage = TutorialStage.Movement;

    

    // Start is called before the first frame update
    void Start()
    {
        pc = player.GetComponent<PlayerController>();
        var interactables = GameObject.FindGameObjectsWithTag("InteractableObject");
        foreach(GameObject go in interactables){
            var gname = go.name.ToLower();
            if(gname.Contains("cauldron")){cauldrons.Add(go);}
            else if(gname.Contains("bowl")){bowls.Add(go);}
            else if(gname.Contains("mortarandpestle")){mortar_pestles.Add(go);}
            else if(gname.Contains("distill")){distilleries.Add(go);}
            else if(gname.Contains("plate")){plates.Add(go);}
        }
        ShowTutorialStage();
    }

    // Update is called once per frame
    void Update()
    {
        if(curstage == TutorialStage.Movement){
            var ich = GetComponent<ItemQueueHandler>();
            if(ich.current_queue.Count > 0){
                curstage=TutorialStage.PickupBowl;
            }
        }else if(pc.carrying==null && curstage==TutorialStage.SubmitToPlate){
            curstage=TutorialStage.Movement;
        }else if(pc.carrying!=null && curstage > TutorialStage.Movement){
            CraftableItemController cic = pc.carrying.GetComponent<CraftableItemController>();
            if(cic!=null){
                //TODO currently hardcoded... since all items go to M&P. in future get this properly.
                if(cic.currentOption == null){
                    if(pc.carrying.name.Contains("Potion")){
                        //TODO make sure it actually can be submitted.
                        curstage=TutorialStage.SubmitToPlate;
                    }else{
                        curstage=TutorialStage.PlaceInMortar;
                    }
                }else if(cic.currentStep.requiredContainer == "cauldron"){
                    curstage=TutorialStage.PlaceInCauldron;
                }else if(cic.currentStep.requiredContainer == "distillery"){
                    curstage=TutorialStage.PlaceInDistillery;
                }
            }
        }
        ShowTutorialStage();
    }


    TutorialStage lastStage = TutorialStage.Movement;
    void ShowTutorialStage(){
        if(lastStage == curstage) return;
        foreach (GameObject highlight in highlights)
        {
            Destroy(highlight);
        }
        switch (curstage)
        {
            default: break;
            case TutorialStage.Movement: break;
            case TutorialStage.PickupBowl: 
                var ich = GetComponent<ItemQueueHandler>();
                if(ich.current_queue.Count > 0){
                    GameObject desired_object =  ich.current_queue[0].input_item;
                    foreach(GameObject go in bowls){
                        if(go.GetComponent<BowlController>().supply_object.name == desired_object.name)
                            HighlightObject(go);
                    }
                }
                break;
            case TutorialStage.SubmitToPlate: 
                foreach(GameObject go in plates){
                    HighlightObject(go);
                }
                break;
            case TutorialStage.PlaceInCauldron: 
                foreach(GameObject go in cauldrons){
                    HighlightObject(go);
                }
                break;
            case TutorialStage.PlaceInMortar: 
                foreach(GameObject go in mortar_pestles){
                    HighlightObject(go);
                }
                break;
            case TutorialStage.PlaceInDistillery: 
                foreach(GameObject go in distilleries){
                    HighlightObject(go);
                }
                break;
        }
        lastStage = curstage;
    }

    public GameObject arrowPrefab;
    List<GameObject> highlights = new List<GameObject>();
    void HighlightObject(GameObject go){
        var arrow = Instantiate(arrowPrefab,go.transform);
        arrow.transform.localPosition = new Vector3(0,1,0.3f);
        highlights.Add(arrow);
    }
}
