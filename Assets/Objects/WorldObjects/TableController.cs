using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableController : InteractableController
{
    public GameObject childObject;
    public bool isItemTable; 

    void Start(){
        foreach (Transform child in transform)
        {
            if(childObject != null){
                // fucked up during init, blow up the world.
                Destroy(this);
            }
            childObject = child.gameObject;
            isItemTable = childObject.tag != "InteractableObject";
        }
    }

    public override void OnPlayerInteract(PlayerController p){
        if(isItemTable) this.OnPlayerInteractItem(p);
        else childObject.GetComponent<InteractableController>().OnPlayerInteract(p);
    }

    public void OnPlayerInteractItem(PlayerController p){
        if(this.childObject == null && p.carrying != null){
            p.carrying.gameObject.transform.parent = this.transform;
            this.childObject = p.carrying;
            this.childObject.transform.localPosition = new Vector3(0,0.6f,0);
            p.carrying = null;
        }else if(p.carrying == null && this.childObject != null){
            this.childObject.transform.parent = p.gameObject.transform;
            p.carrying = this.childObject;
            p.carrying.transform.localPosition = new Vector3(0,0.6f,1);
            this.childObject=null;
        }
    }
}
