using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureMapper : MonoBehaviour
{
    public Texture2D texture;
    public int width = 400;
    public int height = 400;
    public float metersPerPixel = 0.002f; //2 cm





    void Start()
    {
        texture = new Texture2D(width, height);
    }

}