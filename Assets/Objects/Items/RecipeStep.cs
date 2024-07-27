using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RecipeStep
{
    public string requiredContainer;
    public GameObject requiredCombinationItem;
    public float secondsOrClicksForProgress;
    public bool requiredInteract;
}
