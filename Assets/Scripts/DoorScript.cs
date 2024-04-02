using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    [SerializeField] GameObject canvas;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("collision");
        if(collision.tag == "Hitbox")
        {   
            canvas.SetActive(true);
        }
    }
}
