using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Conveyer : MonoBehaviour
{
    public RectTransform[] firstGroup;
    public RectTransform[] secondGroup;
    public RectTransform[] drinkGroup;

    //public RectTransform tableGroup;
    public RectTransform tableFirst;
    public RectTransform tableSecond;
    public RectTransform tableDrink;

    private Dictionary<DishType, Dish> table = new Dictionary<DishType, Dish>();

    private List<Dish> firsts = new List<Dish>();
    private List<Dish> seconds = new List<Dish>();
    private List<Dish> drinks = new List<Dish>();
    public void StartCooking(Client client)
    {
        ClearConveyer();
        var tier = GlobalVariables.instance.tier;
        var FirstDishes = GlobalVariables.instance.Dishes.Where(d => d.type == DishType.First && d.tier <= tier);
        Dish dish = null;
        if (client.dish1)
        {
            dish = FirstDishes.FirstOrDefault(d => d.data == client.dish1);
        }
        if (!dish)
        {
            var reqFirstDishes = FirstDishes.Where(d => client.requiredFeaturesForFirst.Count(f => d.features.Contains(f)) == client.requiredFeaturesForFirst.Length).ToArray();
            dish = reqFirstDishes[Random.Range(0, reqFirstDishes.Length)];
        }

        firsts = FirstDishes.Where(d => d != dish).Take(2).ToList();
        firsts.Add(dish);
        PutDishes(firsts, firstGroup);

        var SecondDishes = GlobalVariables.instance.Dishes.Where(d => d.type == DishType.Second && d.tier <= tier);
        Dish sDish = null;
        if (client.dish2)
        {
            sDish = FirstDishes.FirstOrDefault(d => d.data == client.dish2);
        }
        if (!sDish)
        {
            var reqSecDishes = SecondDishes.Where(d => client.requiredFeaturesForSecond.Count(f => d.features.Contains(f)) == client.requiredFeaturesForSecond.Length).ToArray();
            sDish = reqSecDishes[Random.Range(0, reqSecDishes.Length)];
        }

        seconds = SecondDishes.Where(d => d != sDish).Take(2).ToList();
        seconds.Add(sDish);
        PutDishes(seconds, secondGroup);

        var Drinks = GlobalVariables.instance.Dishes.Where(d => d.type == DishType.Drink && d.tier <= tier);
        Dish drink = null;
        if (client.drink)
        {
            drink = FirstDishes.FirstOrDefault(d => d.data == client.drink);
        }
        if (!drink)
        {
            var reqDrinks = Drinks.Where(d => client.requiredFeaturesForDrink.Count(f => d.features.Contains(f)) == client.requiredFeaturesForDrink.Length).ToArray();
            drink = reqDrinks[Random.Range(0, reqDrinks.Length)];
        }

        drinks = Drinks.Where(d => d != drink).Take(2).ToList();
        drinks.Add(drink);

        PutDishes(drinks, drinkGroup);
    }

    public void ClearConveyer()
    {
        table.Clear();
        ClearGroup(firsts);
        ClearGroup(seconds);
        ClearGroup(drinks);
    }

    void ClearGroup(List<Dish> group)
    {

        foreach (var dish in group)
        {
            dish.transform.SetParent(GlobalVariables.instance.canvas);
            dish.gameObject.SetActive(false);
        }
    }
    void PutDishes(List<Dish> dishes, Transform[] parent)
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
            dish.index = dishes.IndexOf(dish);
            dish.transform.SetParent(parent[dish.index]);
            dish.full.onClick.AddListener(() => MoveToTable(dish));
            dish.small.onClick.AddListener(() => MoveBack(dish));
            dish.SetSize(true);
            dish.gameObject.SetActive(true);
            dish.SetSize(true);
            dish.Init();
        }
    }

    public void MoveToTable(Dish dish)
    {
        if (table.ContainsKey(dish.type)) MoveBack(table[dish.type]);
        table.Add(dish.type, dish);
        dish.SetSize(false);

        switch (dish.type)
        {
            case DishType.First:
                dish.transform.SetParent(tableFirst);
                break;
            case DishType.Second:
                dish.transform.SetParent(tableSecond);
                break;
            default:
                dish.transform.SetParent(tableDrink);
                break;
        }
        //dish.GetComponent<Button>().onClick.RemoveAllListeners();
        //dish.GetComponent<Button>().onClick.AddListener(() => MoveBack(dish));
    }

    public void MoveBack(Dish dish)
    {
        table.Remove(dish.type);
        dish.SetSize(true);
        switch (dish.type)
        {
            case DishType.First:
                dish.transform.SetParent(firstGroup[dish.index]);
                break;
            case DishType.Second:
                dish.transform.SetParent(secondGroup[dish.index]);
                break;
            default:
                dish.transform.SetParent(drinkGroup[dish.index]);
                break;
        }

        //dish.GetComponent<Button>().onClick.RemoveAllListeners();
        //dish.GetComponent<Button>().onClick.AddListener(() => MoveToTable(dish));
    }

    public void CompleteOrder()
    {
        if (!table.ContainsKey(DishType.First) || !table.ContainsKey(DishType.Second) || !table.ContainsKey(DishType.Drink))
            return;
        FindObjectOfType<ClientManager>().CompleteOrder(table[DishType.First], table[DishType.Second], table[DishType.Drink]);
        ClearConveyer();
    }
}
