using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scaler : MonoBehaviour
{
    private void Start()
    {
        if (gameObject.name.Contains("Zombie"))
        {
            transform.localScale = new Vector3(.3f, .3f);
        }
    }
}
