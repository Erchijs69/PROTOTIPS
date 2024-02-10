
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{

   public PlayerMovement movement;
   

   void OnCollisionEnter2D (Collision2D collisionInfo)
   {
        if (collisionInfo.collider.tag == "Obstacle")
        {
            movement.enabled = false;
           
            FindObjectOfType<GameManager>().EndGame();
            
        }
   }
}
