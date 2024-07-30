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
        PickupBowl,
        PlaceInMortar,
        PlaceInCauldron,
        PlaceInDistillery,
        SubmitToPlate
    }

    public TutorialStage curstage = TutorialStage.PickupBowl;

    

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
        DoNextStage();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void DoNextStage(){
        foreach (GameObject highlight in highlights)
        {
            Destroy(highlight);
        }
        switch (curstage)
        {
            default: break;
            case TutorialStage.Movement: break;
            case TutorialStage.PickupBowl: 
                foreach(GameObject go in bowls){
                    HighlightObject(go);
                }
                break;
        }
    }

    public GameObject arrowPrefab;
    List<GameObject> highlights;
    void HighlightObject(GameObject go){
        
        
    }
}
