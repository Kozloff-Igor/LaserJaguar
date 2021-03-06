﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlobalVariables : MonoBehaviour
{
    public static GlobalVariables instance = null;

    public List<Dish> Dishes;
    public DishData[] DishData;
    public string[] features;
    public Dish dishPrefab;
    public Transform canvas;
    public int tier = 1;
    public GameObject ButtonNewspaper;
    public Image Newspaper;
    public Sprite News;

    public GameObject FinalComics;

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

    public void ChangeNews()
    {
        Newspaper.sprite = News;
    }
}
