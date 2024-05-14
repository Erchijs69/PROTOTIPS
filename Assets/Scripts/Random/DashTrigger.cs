using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashTrigger : MonoBehaviour
{
    public GameObject player;
    private CharacterController2D playerController;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            CharacterController2D playerController = other.GetComponent<CharacterController2D>();
            if (playerController != null)
            {
                playerController.canDash = true;
                Destroy(gameObject);
            }
        }
    }
}
