using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemProgressController : ContainerController
{
    private bool hasItem;

    public string container_name;
    protected void Start(){
        base.Start();
        isItemTable = true;
    }
}
