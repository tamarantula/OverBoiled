using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBobber : MonoBehaviour
{
    public float speed = 5f;
    public float height = 2f;

    void Start(){
        height = transform.localPosition.y;
    }
    void Update()
    {
        Vector3 pos = transform.localPosition;
        float newY = Mathf.Sin(Time.time * speed)*0.5f + height;
        transform.localPosition = new Vector3(pos.x, newY, pos.z);
    }
}
