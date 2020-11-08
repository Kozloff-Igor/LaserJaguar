using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cat : MonoBehaviour
{
    public Sprite[] sprites;

    private Image image;



    public int Count
    {
        get => count;
        set
        {
            count = Mathf.Clamp(value, 0, 4);
            image.sprite = sprites[count];
        }
    }
    private int count;

    private void Start()
    {
        image = GetComponent<Image>();
    }
}
