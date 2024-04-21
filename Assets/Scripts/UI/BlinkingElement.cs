using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkingElement : MonoBehaviour
{
    [SerializeField] Color startColor;
    [SerializeField] Color endColor;
    [Range(0,10)]
    public float speed = 1;

    private Image image;

    void Awake()
    {
        image = GetComponent<Image>();
    }

    void Update()
    {
        image.color = Color.Lerp(startColor, endColor, Mathf.PingPong(Time.time * speed, 1));
    }
}
