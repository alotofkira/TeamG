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
        if(collision.gameObject.tag == "Floor" || collision.gameObject.tag == "Zombie" || collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
