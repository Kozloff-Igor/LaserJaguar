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

        if (result)
        {
            first.AddVisibleFeatures(currentClient.requiredFeaturesForFirst);
            second.AddVisibleFeatures(currentClient.requiredFeaturesForSecond);
            drink.AddVisibleFeatures(currentClient.requiredFeaturesForDrink);
        }
        Debug.Log(result);
    }

    bool CheckDish(string[] reqFeatures, Dish dish)
    {
        return reqFeatures.Count(f => dish.features.Contains(f)) == reqFeatures.Length;

    }
}
