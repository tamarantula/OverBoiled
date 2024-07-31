using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeheScript : MonoBehaviour
{
    public float secondsToDestroy=5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(secondsToDestroy>0){
            secondsToDestroy-=Time.deltaTime;
        }else{
            Destroy(this.gameObject);
        }
    }
}
