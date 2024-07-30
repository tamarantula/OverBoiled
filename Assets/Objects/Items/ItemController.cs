using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    void Update()
    {
        
    }

    public virtual bool onPlaceItem(GameObject newparent){
        ContainerController newContainer = newparent.GetComponent<ContainerController>().GetContainer();
        return newContainer.containerName == "table";
    }
    public virtual bool onRemoveItem(GameObject oldparent){
        return true;
    }
    
}
