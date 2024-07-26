using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerController : InteractableController
{
    protected GameObject childObject;
    protected GameObject containerDummy;
    protected bool isItemTable; 

    protected void Start(){
        isItemTable = true;
        containerDummy = transform.Find("ContainerContents").gameObject;
        foreach (Transform child in containerDummy.transform)
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
            if(!this.OnItemPlaced(p.carrying))
                return;
            p.carrying.gameObject.transform.parent = this.containerDummy.transform;
            this.childObject = p.carrying;
            this.childObject.transform.localPosition = new Vector3(0,0.6f,0);
            p.carrying = null;
        }else if(p.carrying == null && this.childObject != null){
            if(!this.OnItemPlaced(this.childObject))
                return;
            this.childObject.transform.parent = p.gameObject.transform;
            p.carrying = this.childObject;
            p.carrying.transform.localPosition = new Vector3(0,0.6f,1);
            this.childObject=null;
        }
    }

    private bool OnItemPlaced(GameObject i){
        if(i.GetComponent<ItemController>() == null){
            return true;
        }else{
            return i.GetComponent<ItemController>().CanPlaceItem(this.gameObject);
        }
    }

    private bool OnItemRemoved(GameObject i){
        if(i.GetComponent<ItemController>() == null){
            return true;
        }else{
            return i.GetComponent<ItemController>().CanRemoveItem(this.gameObject);
        }
    }
}
