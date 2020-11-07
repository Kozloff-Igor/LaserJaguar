using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dish : MonoBehaviour
{
    public string Name;
    public DishType type;
    public string[] features;
    public List<string> visibleFeatures = new List<string>();

}

public enum DishType
{
    First,
    Second,
    Drink
}
