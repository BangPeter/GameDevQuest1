using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CollisionDetector : MonoBehaviour
{
    [SerializeField] private int damagableLayer;
    [SerializeField] private BoxCollider2D hitbox;

    private float knockback = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == damagableLayer)
        {
            //Debug.Log("hit = " + collision.gameObject.layer);

            //float direction = hitbox.transform.localScale.x;
            float direction = hitbox.GetComponentInParent<Rigidbody2D>().transform.localScale.x;

            collision.attachedRigidbody.velocity = new Vector2(knockback * direction, 0);
            
        }
    }
}
