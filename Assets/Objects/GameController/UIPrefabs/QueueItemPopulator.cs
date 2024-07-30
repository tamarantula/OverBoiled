using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QueueItemPopulator : MonoBehaviour
{
    private QueueItem queueItem;

    private Slider progressBar;

    void Start(){
        progressBar = transform.Find("Slider").GetComponent<Slider>();
    }

    void Update(){
        if(this.queueItem != null && Time.time<queueItem.expiry_time){
            progressBar.minValue = queueItem.start_time;
            progressBar.maxValue = queueItem.expiry_time;
            progressBar.value = Time.time;
        }
    }

    public void populate(int index, QueueItem qi){
        this.queueItem = qi;
        this.transform.localPosition = new Vector3(0,-10-140*index,0);


        
        var main_item_resource = (GameObject)Resources.Load("UIItemPrefabs/"+qi.result_item.name,typeof(GameObject));
        
        var main_item = Instantiate(main_item_resource,this.transform.GetChild(1));
        main_item.transform.localPosition = new Vector3(-30,-30,0);
        main_item.GetComponent<RectTransform>().sizeDelta = new Vector2(25, 35);


        var in_resource = (GameObject)Resources.Load("UIItemPrefabs/"+qi.input_item.name,typeof(GameObject));
        var in_obj = Instantiate(in_resource,this.transform.GetChild(1));
        in_obj.transform.localPosition = new Vector3(-80,-35,0);
        in_obj.GetComponent<RectTransform>().sizeDelta = new Vector2(15,20);
        
        RecipeOption opt = qi.input_item.GetComponent<CraftableItemController>().recipeOptions[qi.item_path];
        int si=0;
        int sw=140/(opt.steps.Length+1);
        foreach(RecipeStep s in opt.steps){
            if(s.requiredContainer != null){
                var sub_resource = (GameObject)Resources.Load("UIItemPrefabs/"+s.requiredContainer,typeof(GameObject));
                var sub_item = Instantiate(sub_resource,this.transform.GetChild(0));
                sub_item.transform.localPosition = new Vector3(-120+(sw+10)*si,-50,0);
                sub_item.GetComponent<RectTransform>().sizeDelta = new Vector2(20,40);
            }else if(s.requiredCombinationItem != null){
                //TODO combination!
            }
            si+=1;
        }
    }
}
