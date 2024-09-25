using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorMan : MonoBehaviour
{
    public static ColorMan instance;

    public Color[] Colors;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

}
