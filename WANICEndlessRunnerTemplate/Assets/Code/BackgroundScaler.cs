//------------------------------------------------------------------------------
//
// File Name:	BackgroundScaler.cs
// Author(s):	Alex Dzius - Tech Lead on Team G in Endless Runner Project
// Project:		Endless Runner
// Course:		WANIC VGP Year 2
//
//
//------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScaler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        float wsh = Camera.main.orthographicSize * 2;
        float wsw = wsh / Screen.height * Screen.width;
        transform.localScale = new Vector3(wsw / sr.sprite.bounds.size.x, wsh / sr.sprite.bounds.size.y, 1);
    }
}
