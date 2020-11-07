using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Dish : MonoBehaviour
{
    public DishData data;

    public string Name { get => data.Name; }
    public DishType type { get => data.type; }
    public string[] features { get => data.features; }

    public List<string> visibleFeatures = new List<string>();

    public void AddVisibleFeatures(string[] newFeatures)
    {
        visibleFeatures = visibleFeatures.Concat(newFeatures.Where(reqF => !visibleFeatures.Contains(reqF))).ToList();
    }

}

public enum DishType
{
    First,
    Second,
    Drink
}
