using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchController : ItemController
{

    float seconds_to_next_check = 0.2f;
    float shadow_destroy_distance = 1f;
    void Update(){
        if(seconds_to_next_check>0){
            seconds_to_next_check-=Time.deltaTime;
            return;
        }
        seconds_to_next_check=0.2f;
        ShadowEntityController[] secs = (ShadowEntityController[])GameObject.FindObjectsOfType(typeof(ShadowEntityController));
        foreach (var sec in secs)
        {
            if(shadow_destroy_distance>Vector3.Distance(transform.position,sec.transform.position)){
                Destroy(sec.gameObject);
            }
        }
    }

    public virtual bool onPlaceItem(GameObject newparent){
        ContainerController newContainer = newparent.GetComponent<ContainerController>().GetContainer();
        return newContainer.containerName == "table";
    }
    public virtual bool onRemoveItem(GameObject oldparent){
        return true;
    }

}
