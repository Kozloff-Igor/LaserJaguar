using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ClientManager : MonoBehaviour
{
    public Client currentClient;

    public void StartOrder()
    {
        FindObjectOfType<Conveyer>().StartCooking(currentClient);
        DialogueManager.Internal.DialogueStart("Example", currentClient.requiredFeaturesForFirst);
    }

    public void CompleteOrder(Dish first, Dish second, Dish drink)
    {
        var result = CheckDish(currentClient.requiredFeaturesForFirst, first)
            && CheckDish(currentClient.requiredFeaturesForSecond, second)
             && CheckDish(currentClient.requiredFeaturesForDrink, drink);
        Debug.Log(result);
    }

    bool CheckDish(string[] reqFeatures, Dish dish)
    {
        var isGood = reqFeatures.Count(f => dish.features.Contains(f)) == reqFeatures.Length;
        if (isGood)
        {
            dish.visibleFeatures = dish.visibleFeatures.Concat(reqFeatures.Where(reqF => !dish.visibleFeatures.Contains(reqF))).ToList();
        }
        return isGood;
    }
}
