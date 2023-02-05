using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pulse : MonoBehaviour
{
    public float opacity = 0f;
    public float pulseSpeed = 1f;
    public bool increase = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UpdatePulse();
    }

    void UpdatePulse()
    {
        if (increase)
        {
            opacity += pulseSpeed * Time.deltaTime;
            if (opacity >= 1)
            {
                increase = false;
            }
        }
        else
        {
            opacity -= pulseSpeed * Time.deltaTime;
            if (opacity <= 0)
            {
                increase = true;
            }
        }
        Color newColor = GetComponent<SpriteRenderer>().color;
        newColor.a = opacity;
        GetComponent<SpriteRenderer>().color = newColor;
    }
}
