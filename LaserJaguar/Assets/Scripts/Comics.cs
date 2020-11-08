using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class Comics : MonoBehaviour
{
    public Sprite[] sprites;
    private int indx;
    void Start()
    {

    }

    public void onClick()
    {
        indx++;
        if (indx < sprites.Length)
        {
            GetComponent<Image>().sprite = sprites[indx];
        }
    }
}
