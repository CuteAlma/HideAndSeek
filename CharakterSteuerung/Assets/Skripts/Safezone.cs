using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Safezone : MonoBehaviour
{
    public Renderer rend;
    public Color safeColor = Color.green;

    // Use this for initialization
    void Start()
    {
        rend = GetComponent<Renderer>();

    }

    // Update is called once per frame
    void Update()
    {
        rend.material.color = safeColor;
    }


}