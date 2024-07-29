using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueItemPopulator : MonoBehaviour
{
    private QueueItem queueItem;

    public void populate(int index, QueueItem qi){
        this.queueItem = qi;
        this.transform.localPosition = new Vector3(-50,-50-100*index,0);
        
        var main_item_resource = (GameObject)Resources.Load("UIItemPrefabs/"+qi.result_item.name,typeof(GameObject));
        
        var main_item = Instantiate(main_item_resource,this.transform.GetChild(1));
        main_item.transform.localPosition = new Vector3(20,0,0);
        main_item.GetComponent<RectTransform>().sizeDelta = new Vector2(20, 25);


        var in_resource = (GameObject)Resources.Load("UIItemPrefabs/"+qi.input_item.name,typeof(GameObject));
        var in_obj = Instantiate(in_resource,this.transform.GetChild(1));
        in_obj.transform.localPosition = new Vector3(-10,0,0);
        in_obj.GetComponent<RectTransform>().sizeDelta = new Vector2(15,20);
        
        RecipeOption opt = qi.input_item.GetComponent<CraftableItemController>().recipeOptions[qi.item_path];
        int si=0;
        int sw=80/(opt.steps.Length+1);
        foreach(RecipeStep s in opt.steps){
            var sub_resource = (GameObject)Resources.Load("UIItemPrefabs/"+qi.result_item.name,typeof(GameObject));
            var sub_item = Instantiate(sub_resource,this.transform.GetChild(0));
            sub_item.transform.localPosition = new Vector3(-(sw+10)*si+30,0,0);
            sub_item.GetComponent<RectTransform>().sizeDelta = new Vector2(sw*0.8f, sw);
            si+=1;
        }
    }
}
