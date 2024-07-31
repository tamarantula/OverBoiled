using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarController : MonoBehaviour
{
    private Slider progressSlider;
    void Start()
    {
        progressSlider = transform.Find("Canvas/Slider").GetComponent<Slider>();
    }
 
    void Update(){
        var mypos = transform.position;
        this.transform.LookAt(new Vector3(mypos.x,mypos.y+100000f,mypos.z));
    }

    public void SetProgress(float progress){
        if(progressSlider != null) progressSlider.value = progress;
    }

    public void SetHidden(bool hidden){
        this.gameObject.SetActive(!hidden);
    }
}
