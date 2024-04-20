using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Animation;
using UnityEngine;
using UnityEngine.InputSystem;

public class Switch : MonoBehaviour
{
    bool currentState;

    public SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (sprite.color == Color.white)
            {
                sprite.color = Color.black;
            }
            else sprite.color = Color.white;
        }
    }
}
