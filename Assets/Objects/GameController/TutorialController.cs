using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    public GameObject player;
    public List<GameObject> bowls;
    public List<GameObject> mortar_pestles;
    public List<GameObject> cauldrons;
    public List<GameObject> distilleries;
    public List<GameObject> plates;

    public enum TutorialStage {
        Movement,
        Interaction,
        PickupBowl,
        PlaceInMortar,
        PlaceInCauldron,
        PlaceInDistillery,
        SubmitToPlate
    }

    public TutorialStage curstage = TutorialStage.Movement;

    

    // Start is called before the first frame update
    void Start()
    {
        var interactables = GameObject.FindGameObjectsWithTag("InteractableObject");
        foreach(GameObject go in interactables){
            var gname = go.name.ToLower();
            if(gname.Contains("cauldron")){cauldrons.Add(go);}
            else if(gname.Contains("bowl")){bowls.Add(go);}
            else if(gname.Contains("mortarandpestle")){mortar_pestles.Add(go);}
            else if(gname.Contains("distill")){distilleries.Add(go);}
            else if(gname.Contains("plate")){plates.Add(go);}
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    void DoNextStage(){
        switch (curstage)
        {
            default: break;
            case TutorialStage.Movement: break;
        }
    }

    List<GameObject> arrows;
    void DrawArrowAboveObject(){

    }
}
