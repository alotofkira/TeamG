//------------------------------------------------------------------------------
//
// File Name:	ScrollHorizontal.cs
// Author(s):	Jeremy Kings (j.kings) - Unity Project
//              Nathan Mueller - original Zero Engine project
// Project:		Endless Runner
// Course:		WANIC VGP
//
// Copyright © 2021 DigiPen (USA) Corporation.
//
//------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class ScrollHorizontal : MonoBehaviour
{
    public bool FlipDirection = false;
    public float MoveSpeed = 10.0f;
    public float WrapZoneLeft = -18.0f;
    public float WrapZoneRight = 130.0f;
    public float timer = 0.0f;
    private GameObject holePanel = null;
    void Start()
    {
        holePanel = GameObject.Find("HolePanel");
    }
    // Update is called once per frame
    void Update()
    {
        // Store current position
        Vector3 position = transform.position;

        // Left --> Right, Reset
        if(FlipDirection)
        {
            if (transform.position.x >= WrapZoneRight)
            {
                position.x = WrapZoneLeft;
            }
        }
        // Left <-- Right, Reset
        else
        {
            if (transform.position.x <= WrapZoneLeft)
            {
                position.x = WrapZoneRight + (transform.position.x-WrapZoneLeft);
            }
        }

        // Move
        if (FlipDirection)
        {
            position.x += MoveSpeed * Time.deltaTime;
        }
        else
        {
            position.x -= MoveSpeed * Time.deltaTime;
        }
        // Set new position
        transform.position = position;

        // swawp holeblock at some times -----> google how to swap 2 moving gameobjects
      /*  timer += Time.deltaTime;
        if(timer > 2 && holePanel.transform.position.x > 92 && transform.position.x >= 92 && gameObject.name.Contains("TopPanel"))
        {
            timer = 0;
            var temp = transform.position.x;
            var temp2 = holePanel.transform.position.x;
            transform.position = new Vector3(holePanel.transform.position.x, transform.position.y, 0);
            holePanel.transform.position = new Vector3((temp2-temp), transform.position.y, 0);

        }*/
    }
}
