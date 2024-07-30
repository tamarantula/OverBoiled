using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script handles the queue, including adding new tasks and marking tasks as completed.
public class ItemQueueHandler : MonoBehaviour
{
    public bool gen_randomly;
    public float seconds_per_gen;
    public PotentialQueueItem[] potential_items;

    public List<QueueItem> current_queue; 

    private float seconds_since_last_gen = 3;

    private UIController uic;

    void Start(){
        uic = GetComponent<UIController>();
    }

    void Update(){
        if(gen_randomly){
            if(seconds_since_last_gen>0){
                seconds_since_last_gen -= Time.deltaTime;
            }else{
                seconds_since_last_gen = seconds_per_gen;
                var new_task = potential_items[Random.Range(0,potential_items.Length)];
                current_queue.Add(new QueueItem{
                    input_item=new_task.input_item,
                    item_path=new_task.item_path,
                    start_time=Time.time,
                    expiry_time=Time.time+Random.Range(new_task.min_time,new_task.max_time),
                    result_item=new_task.input_item.GetComponent<CraftableItemController>().recipeOptions[new_task.item_path].result
                });
                uic.UpdateUI(current_queue);
            }
        }
    }    

    public bool trySubmit(GameObject item){
        QueueItem queue_to_del = null;
        foreach(QueueItem qi in this.current_queue){
            if(qi.result_item.name == item.name){
                queue_to_del=qi;
                break;
            }
        }
        if(queue_to_del != null){
            current_queue.Remove(queue_to_del);
            uic.UpdateUI(current_queue);
            //TODO: Update score
            return true;
        }
        return false;
    }
}

[System.Serializable]
public class QueueItem{
    public GameObject input_item;
    public int item_path;
    public GameObject result_item;
    public float start_time;
    public float expiry_time;
}

[System.Serializable]
public class PotentialQueueItem{
    public GameObject input_item;
    public int item_path;
    public float min_time = 30;
    public float max_time = 30;
}