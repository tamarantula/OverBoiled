using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerController : InteractableController
{
    protected GameObject childObject;
    protected GameObject containerDummy;
    protected bool isItemContainer; 
    public string containerName;

    protected void Start(){
        isItemContainer = true;
        containerDummy = transform.Find("ContainerContents").gameObject;
        foreach (Transform child in containerDummy.transform)
        {
            if(childObject != null){
                // fucked up during init, blow up the world.
                Destroy(this.gameObject);
            }
            childObject = child.gameObject;
            isItemContainer = childObject.tag != "InteractableObject";
        }
    }

    public override void OnPlayerInteract(PlayerController p){
        if(isItemContainer) this.OnPlayerInteractItem(p);
        else childObject.GetComponent<InteractableController>().OnPlayerInteract(p);
    }

    public ContainerController GetContainer(){
        if(!isItemContainer && childObject.GetComponent<ContainerController>()!=null){
            return childObject.GetComponent<ContainerController>().GetContainer();
        }else{
            return this;
        }
    }

    public GameObject GetCurrentItem(){
        if(childObject!=null && childObject.GetComponent<ItemController>()!=null){
            return childObject;
        }else{
            return null;
        }
    }

    public void placeNewItem(GameObject newObj){
        newObj.transform.parent=this.containerDummy.transform;
        this.childObject = newObj;
        this.childObject.transform.localPosition = new Vector3(0,0.6f,0);
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
            if(!this.OnItemRemoved(this.childObject))
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
            return i.GetComponent<ItemController>().onPlaceItem(this.gameObject);
        }
    }

    private bool OnItemRemoved(GameObject i){
        if(i.GetComponent<ItemController>() == null){
            return true;
        }else{
            return i.GetComponent<ItemController>().onRemoveItem(this.gameObject);
        }
    }
}
