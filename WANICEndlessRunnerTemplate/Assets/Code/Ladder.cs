//------------------------------------------------------------------------------
//
// File Name:	Ladder.cs
// Author(s):	Alex Dzius
// Project:		Endless Runner
// Course:		WANIC VGP
//
//
//------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Checks if player has hit ladder, and if so, move player accordingly
        if(collision.gameObject.CompareTag("Player"))
        {
            if(collision.rigidbody.velocity.y < 0)
            {
                collision.transform.position = new Vector2(collision.transform.position.x, 4.5f);
            }
            else
            {
                collision.transform.position = new Vector2(collision.transform.position.x, 11.5f);
            }
        }
    }
}
