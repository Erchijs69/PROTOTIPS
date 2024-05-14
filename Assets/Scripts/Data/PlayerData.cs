using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData : MonoBehaviour
{
  public int coins;

  public PlayerData (CoinManager coinManager)
  {
    coins = coinManager.coinCount;
  }
}