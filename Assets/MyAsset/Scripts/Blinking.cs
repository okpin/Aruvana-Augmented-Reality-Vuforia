using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blinking : MonoBehaviour
{
    public Color startcolor;
    public Color endcolor;
    [Range(0,10)]
    public float waktu;

    Renderer render;

    void Awake()
    {
        render = GetComponent<Renderer>();        
    }
    void Update()
    {
        render.material.color = Color.Lerp(startcolor, endcolor, Mathf.PingPong(Time.time * waktu, 1));        
    }



}
