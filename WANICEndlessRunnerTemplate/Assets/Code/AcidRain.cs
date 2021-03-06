//------------------------------------------------------------------------------
//
// File Name:	AcidRain.cs
// Author(s):	Alex Dzius - Tech Lead on Team G in Endless Runner Project
// Project:		Endless Runner
// Course:		WANIC VGP Year 2
//
//
//------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidRain : MonoBehaviour
{
    private void Update()
    {
        transform.position -= new Vector3(.005f, 0,0);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Floor")
            || collision.gameObject.CompareTag("Obstacle")
            || collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
