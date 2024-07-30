using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowSpawnController : MonoBehaviour
{
    public float secondsPerShadow;
    public GameObject shadowPrefab; 

    float secondsSinceLastShadow=5;

    // Update is called once per frame
    void Update()
    {
        if(secondsSinceLastShadow>0){
            secondsSinceLastShadow-=Time.deltaTime;
        }else{
            var shadow = Instantiate(shadowPrefab);
            var axis = Random.Range(0,2);
            var direction = Random.Range(0,2)==0?-1:1;
            var distance = 20;
            shadow.transform.position = new Vector3(distance*direction*axis,0,distance*direction*(1-axis));
            secondsSinceLastShadow=secondsPerShadow;
        }
    }
}
