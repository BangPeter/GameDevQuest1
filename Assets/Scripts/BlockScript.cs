using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockScript : MonoBehaviour
{

    [SerializeField] private Rigidbody2D block;
    private float horizontal = 5.0f;
    //Animate fall even from platforms

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        block.velocity = new Vector2();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.otherCollider.tag == "Hitbox")
        {
            //Find direction of player and add velocity to the block
        }
    }
}
