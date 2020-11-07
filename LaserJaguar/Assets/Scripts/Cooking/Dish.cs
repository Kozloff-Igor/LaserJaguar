using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Dish : MonoBehaviour
{
    public DishData data;
    public Text nameText;
    public Image image;
    public Transform featureGroup;
    public Button full;
    public Button small;

    public string Name { get => data.Name; }
    public DishType type { get => data.type; }
    public string[] features { get => data.features; }

    public List<string> visibleFeatures = new List<string>();

    public void AddVisibleFeatures(string[] newFeatures)
    {
        visibleFeatures = visibleFeatures.Concat(newFeatures.Where(reqF => !visibleFeatures.Contains(reqF))).ToList();
        RefreshFeatures();
    }

    public void Init()
    {
        nameText.text = data.Name;
        // добавить спрайт
        RefreshFeatures();
    }

    public void RefreshFeatures()
    {

        for (var i = 0; i < featureGroup.childCount; i++)
        {
            if (i >= features.Length)
            {
                featureGroup.GetChild(i).gameObject.SetActive(false);
                continue;
            }
            if (i >= visibleFeatures.Count)
            {
                featureGroup.GetChild(i).gameObject.SetActive(true);
                featureGroup.GetChild(i).GetComponent<Text>().text = "?????";
                continue;
            }
            if (i < visibleFeatures.Count)
            {
                featureGroup.GetChild(i).gameObject.SetActive(true);
                featureGroup.GetChild(i).GetComponent<Text>().text = visibleFeatures[i];
            }
        }
    }

    public void SetSize(bool isFull)
    {
        full.gameObject.SetActive(isFull);
        small.gameObject.SetActive(!isFull);
    }
}

public enum DishType
{
    First,
    Second,
    Drink
}
