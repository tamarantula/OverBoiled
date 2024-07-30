using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlController : InteractableController
{
    public GameObject supply_object;

    void Start(){
        for(var i=0;i<10;i++){
            var new_obj = Instantiate(supply_object,transform);
            new_obj.transform.localPosition = new Vector3(Random.Range(-0.2f,0.2f),0.3f,Random.Range(-0.2f,0.2f));
            new_obj.transform.rotation = Random.rotation;
            Destroy(new_obj.GetComponent<ItemController>());
        }
    }

    public override void OnPlayerInteract(PlayerController p){
        if(p.carrying == null){
            var new_obj = Instantiate(supply_object);
            new_obj.name=supply_object.name;
            new_obj.transform.parent = p.gameObject.transform;
            p.carrying = new_obj;
            p.carrying.transform.localPosition = new Vector3(0,0.6f,1);
        }
    }
}
