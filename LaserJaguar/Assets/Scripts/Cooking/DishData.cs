using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New DishData", menuName = "Dishes", order = 52)]
public class DishData : ScriptableObject
{
    public Sprite sprite;
    public string Name;
    public DishType type;
    public string[] features;
}
