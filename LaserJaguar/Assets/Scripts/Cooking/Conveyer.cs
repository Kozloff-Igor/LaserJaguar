using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Conveyer : MonoBehaviour
{
    public RectTransform firstGroup;
    public RectTransform tableGroup;

    private List<Dish> table = new List<Dish>();
    private List<Dish> first;
    public void StartCooking(Client client)
    {
        var FirstDishes = GlobalVariables.instance.Dishes.Where(d => d.type == DishType.First);
        var reqFirstDishes = FirstDishes.Where(d => client.requiredFeaturesForFirst.Count(f => d.features.Contains(f)) == client.requiredFeaturesForFirst.Length).ToArray();
        var dish = reqFirstDishes[Random.Range(0, reqFirstDishes.Length)];

        first = FirstDishes.Where(d => d != dish).Take(2).ToList();
        first.Add(dish);

        var SecondDishes = GlobalVariables.instance.Dishes.Where(d => d.features.Count(f => client.requiredFeaturesForSecond.Contains(f)) > 1 && d.type == DishType.Second).ToArray();
        var Drinks = GlobalVariables.instance.Dishes.Where(d => d.features.Count(f => client.requiredFeaturesForDrink.Contains(f)) > 1 && d.type == DishType.Drink).ToArray();

        PutDishes();
    }

    void PutDishes()
    {
        foreach (var dish in first)
        {
            var dishObj = GameObject.Instantiate(dish, firstGroup);
            dishObj.GetComponent<Button>().onClick.AddListener(() => MoveToTable(dishObj));
        }
    }

    public void MoveToTable(Dish dish)
    {
        table.Add(dish);
        dish.transform.SetParent(tableGroup);
        dish.GetComponent<Button>().onClick.RemoveAllListeners();
        dish.GetComponent<Button>().onClick.AddListener(() => MoveBack(dish));
    }

    public void MoveBack(Dish dish)
    {
        table.Remove(dish);
        switch (dish.type)
        {
            case DishType.First:
                dish.transform.SetParent(firstGroup);
                break;
            default:
                dish.transform.SetParent(firstGroup);
                break;
        }
        dish.GetComponent<Button>().onClick.RemoveAllListeners();
        dish.GetComponent<Button>().onClick.AddListener(() => MoveToTable(dish));
    }
}
