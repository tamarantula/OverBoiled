using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 6f;
    public float reach = 1f;

    private Quaternion lookingDirection;
    private Color lastlookingColor;
    private GameObject lookingAt;
    List<GameObject> nearbyInteractables = new List<GameObject>();

    public GameObject carrying;

    void OnTriggerEnter(Collider col){
        if(col.gameObject.tag == "InteractableObject"){
            nearbyInteractables.Add(col.gameObject);
        }
    }
    void OnTriggerExit(Collider col){
        if(col.gameObject.tag == "InteractableObject"){
            nearbyInteractables.Remove(col.gameObject);
        }
    }


    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if(direction.magnitude >= 0.1f)
        {
            transform.rotation = Quaternion.LookRotation(direction);
            lookingDirection = transform.rotation;
            controller.Move(direction * speed * Time.deltaTime);
            float leastDiff=100;
            GameObject lastlookingAt = lookingAt;
            lookingAt = null;
            // find the object we're looking at
            foreach(GameObject g in nearbyInteractables){
                Vector3 diff = g.transform.position-transform.position;
                diff.y=0;
                Quaternion angle = Quaternion.LookRotation(diff);
                float reqlookangle = angle.eulerAngles.y;
                float eulerangle = Mathf.Abs(reqlookangle-lookingDirection.eulerAngles.y);
                if(eulerangle > 180) eulerangle = 360-eulerangle;

                if(eulerangle<leastDiff){
                    leastDiff=eulerangle;
                    lookingAt=g;
                }
            }
            if(lastlookingAt != lookingAt){
                if(lastlookingAt != null) OnStopLookingAt(lastlookingAt);
                if(lookingAt != null) OnStartLookingAt(lookingAt);
            }
        }



        if(lookingAt != null){
            Debug.DrawLine(transform.position,lookingAt.transform.position,Color.green);
            if(Input.GetKeyDown(KeyCode.E))
            {
                lookingAt.GetComponent<InteractableController>().OnPlayerInteract(this);
                // call the onInteract of the worldobject we're looking at
            }
        }

        // highlight the object we're looking at

    }


    void OnStartLookingAt(GameObject obj){
        lastlookingColor = obj.GetComponent<Renderer>().material.color;
        obj.GetComponent<Renderer>().material.color=Color.yellow;
    }

    void OnStopLookingAt(GameObject obj){
        obj.GetComponent<Renderer>().material.color=lastlookingColor;
    }
}
