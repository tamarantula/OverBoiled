using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
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
