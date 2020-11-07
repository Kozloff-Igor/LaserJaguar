using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Conveyer : MonoBehaviour
{
    public RectTransform firstGroup;
    public RectTransform secondGroup;
    public RectTransform drinkGroup;
    public RectTransform tableGroup;

    private Dictionary<DishType, Dish> table = new Dictionary<DishType, Dish>();

    private List<Dish> firsts;
    private List<Dish> seconds;
    private List<Dish> drinks;
    public void StartCooking(Client client)
    {
        var FirstDishes = GlobalVariables.instance.Dishes.Where(d => d.type == DishType.First);
        var reqFirstDishes = FirstDishes.Where(d => client.requiredFeaturesForFirst.Count(f => d.features.Contains(f)) == client.requiredFeaturesForFirst.Length).ToArray();
        var dish = reqFirstDishes[Random.Range(0, reqFirstDishes.Length)];

        firsts = FirstDishes.Where(d => d != dish).Take(2).ToList();
        firsts.Add(dish);
        PutDishes(firsts, firstGroup);

        var SecondDishes = GlobalVariables.instance.Dishes.Where(d => d.type == DishType.Second);
        var reqSecDishes = SecondDishes.Where(d => client.requiredFeaturesForSecond.Count(f => d.features.Contains(f)) == client.requiredFeaturesForSecond.Length).ToArray();
        var sDish = reqSecDishes[Random.Range(0, reqSecDishes.Length)];

        seconds = SecondDishes.Where(d => d != sDish).Take(2).ToList();
        seconds.Add(sDish);
        PutDishes(seconds, secondGroup);

        var Drinks = GlobalVariables.instance.Dishes.Where(d => d.type == DishType.Drink);
        var reqDrinks = Drinks.Where(d => client.requiredFeaturesForDrink.Count(f => d.features.Contains(f)) == client.requiredFeaturesForDrink.Length).ToArray();
        var drink = reqDrinks[Random.Range(0, reqDrinks.Length)];

        drinks = Drinks.Where(d => d != drink).Take(2).ToList();
        drinks.Add(drink);

        PutDishes(drinks, drinkGroup);
    }

    void PutDishes(List<Dish> dishes, Transform parent)
    {
        for (var i = 0; i < dishes.Count; i++)
        {
            var random = Random.Range(0, dishes.Count);
            var temp = dishes[random];
            dishes[random] = dishes[i];
            dishes[i] = temp;
        }
        foreach (var dish in dishes)
        {
            //var dishObj = GameObject.Instantiate(dish, parent);
            dish.transform.SetParent(parent);
            dish.GetComponent<Button>().onClick.AddListener(() => MoveToTable(dish));
            dish.gameObject.SetActive(true);
        }
    }

    public void MoveToTable(Dish dish)
    {
        if (table.ContainsKey(dish.type)) MoveBack(table[dish.type]);
        table.Add(dish.type, dish);
        dish.transform.SetParent(tableGroup);
        dish.GetComponent<Button>().onClick.RemoveAllListeners();
        dish.GetComponent<Button>().onClick.AddListener(() => MoveBack(dish));
    }

    public void MoveBack(Dish dish)
    {
        table.Remove(dish.type);
        switch (dish.type)
        {
            case DishType.First:
                dish.transform.SetParent(firstGroup);
                break;
            case DishType.Second:
                dish.transform.SetParent(secondGroup);
                break;
            default:
                dish.transform.SetParent(drinkGroup);
                break;
        }
        dish.GetComponent<Button>().onClick.RemoveAllListeners();
        dish.GetComponent<Button>().onClick.AddListener(() => MoveToTable(dish));
    }

    public void CompleteOrder()
    {
        if (!table.ContainsKey(DishType.First) || !table.ContainsKey(DishType.Second) || !table.ContainsKey(DishType.Drink))
            return;
        FindObjectOfType<ClientManager>().CompleteOrder(table[DishType.First], table[DishType.Second], table[DishType.Drink]);
    }
}
