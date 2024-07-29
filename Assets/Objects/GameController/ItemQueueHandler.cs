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

    private float seconds_since_last_gen = 30;

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
                    expiry_time=Time.time+Random.Range(new_task.min_time,new_task.max_time)
                });
            }
        }
    }    

    public bool trySubmit(GameObject item){
        Debug.Log("Attempt to submit "+item.name);
        return false;
    }
}

[System.Serializable]
public class QueueItem{
    public GameObject input_item;
    public int item_path;

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