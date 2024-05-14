using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class CoinManager : MonoBehaviour
{
  public int coinCount;
  public TextMeshProUGUI coinText;
  
  
  void Start()
  {
    coinCount = PlayerPrefs.GetInt("Money", coinCount);
  }

  void Update()
  {
    coinText.text = "Dabloons  " + coinCount;
    PlayerPrefs.SetInt("Money" , coinCount);
    if(SceneManager.GetActiveScene().name == "MainMenu")
    {
      coinCount = 0;
    }
  }
}
  
 
