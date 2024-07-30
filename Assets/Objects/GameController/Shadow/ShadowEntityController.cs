using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowEntityController : MonoBehaviour
{
    public GameObject target;

    public float seconds_to_next_check = 1;

    public float grab_distance = 0.5f;

    void Update()
    {
        if(target == null){
            if(seconds_to_next_check>0){
                seconds_to_next_check -= Time.deltaTime;
                return;
            }
            seconds_to_next_check=2;
            ItemController[] ics = (ItemController[])GameObject.FindObjectsOfType(typeof(CraftableItemController));
            if(ics.Length == 0){
                return;
            }
            target = ics[Random.Range(0,ics.Length)].gameObject;
            seconds_to_next_check=0;
        }else{
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, 0.5f * Time.deltaTime);
            var pos = transform.position;
            pos.y=0.5f;
            transform.position=pos;
            transform.rotation = Quaternion.LookRotation(target.transform.position-transform.position);
            if(grab_distance>Vector3.Distance(transform.position,target.transform.position)){
                Destroy(target);
                Destroy(this.gameObject);
                // Play HEHEHEHE sound
            }
        }
    }
}
