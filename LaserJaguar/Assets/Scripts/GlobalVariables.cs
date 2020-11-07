using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVariables : MonoBehaviour
{
    public static GlobalVariables instance = null;

    public List<Dish> Dishes;
    public DishData[] DishData;
    public string[] features;
    public Dish dishPrefab;
    public Transform canvas;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
            CreateDishes();
        }
        else if (instance == this)
        {
            Destroy(gameObject);
        }

        //DontDestroyOnLoad(gameObject);
    }

    void CreateDishes()
    {
        foreach (var dishData in DishData)
        {
            var dish = Instantiate(dishPrefab, canvas);
            dish.data = dishData;
            dish.gameObject.SetActive(false);
            Dishes.Add(dish);
        }
    }
}
