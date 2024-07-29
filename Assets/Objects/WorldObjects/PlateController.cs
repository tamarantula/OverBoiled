using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateController : InteractableController
{
    public GameObject controllerObject;

    private ItemQueueHandler c_iqh;

    void Start(){
        c_iqh = controllerObject.GetComponent<ItemQueueHandler>();
    }

    public override void OnPlayerInteract(PlayerController p){
        if(p.carrying != null){
            if(c_iqh.trySubmit(p.carrying)){
                Destroy(p.carrying);
                p.carrying = null;
            }
        }
    }
}
