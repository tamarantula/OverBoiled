using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftableItemController : ItemController
{
    public RecipeStep[] recipeSteps;

    private int currentStep;
    private int currentStepStarted;

    void Update()
    {
        
    }

    public bool CanPlaceItem(GameObject newparent){
        return true;
    }
    public bool CanRemoveItem(GameObject oldparent){
        return true;
    }
}
