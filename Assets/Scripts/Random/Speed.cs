using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speed : MonoBehaviour
{
   public Rigidbody2D rb;
   private float gravityScale = 2f;
  
    private void Start()
    {
        GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            rb.gravityScale = gravityScale;
        }
    }
}
