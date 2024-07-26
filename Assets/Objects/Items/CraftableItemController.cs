using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftableItemController : ItemController
{
    public 

    void Update()
    {
        
    }

    public bool CanPlaceItem(GameObject newparent){
        return true;
    }
    public bool CanRemoveItem(GameObject oldparent){
        return true;
    }
}
