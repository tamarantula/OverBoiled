using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    void Update()
    {
        
    }

    public virtual bool onPlaceItem(GameObject newparent){
        return true;
    }
    public virtual bool onRemoveItem(GameObject oldparent){
        return true;
    }
}
