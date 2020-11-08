using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Client : MonoBehaviour
{
    public string Name;
    public string DialogueName;
    public bool isBoss;
    public Sprite Good;

    public string NextMonologue;
    public DishData dish1;
    public DishData dish2;
    public DishData drink;

    public string[] requiredFeaturesForFirst;
    public string[] requiredFeaturesForSecond;
    public string[] requiredFeaturesForDrink;

    public void MakeGood()
    {
        if (Good)
            GetComponentInChildren<Image>().sprite = Good;
    }

}
