
using UnityEngine;

public class WIN : MonoBehaviour
{

  public GameManager gameManager;

  void OnTriggerEnter2D ()
  {
    gameManager.CompleteLevel();
  }
}
