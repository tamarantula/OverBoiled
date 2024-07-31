using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchController : ItemController
{

    float seconds_to_next_check = 0.2f;
    float shadow_destroy_distance = 1f;
    public GameObject heheprefab;

    AudioSource audioData = null;

    void Start(){
        audioData = GetComponent<AudioSource>();
        audioData.Play();
        audioData.Pause();
    }

    void Update(){
        if(seconds_to_next_check>0){
            seconds_to_next_check-=Time.deltaTime;
            return;
        }
        seconds_to_next_check=0.2f;
        ShadowEntityController[] secs = (ShadowEntityController[])GameObject.FindObjectsOfType(typeof(ShadowEntityController));
        foreach (var sec in secs)
        {
            if(shadow_destroy_distance>Vector3.Distance(new Vector3(transform.position.x,0,transform.position.z),new Vector3(sec.transform.position.x,0,sec.transform.position.z))){
                Destroy(sec.gameObject);
                Instantiate(heheprefab);
            }
        }
    }

    public override bool onPlaceItem(GameObject newparent){
        ContainerController newContainer = newparent.GetComponent<ContainerController>().GetContainer();
        
        if(audioData != null) audioData.Pause();
        return newContainer.containerName == "table";
    }
    public override bool onRemoveItem(GameObject oldparent){
        if(audioData != null) audioData.UnPause();
        return true;
    }

}
