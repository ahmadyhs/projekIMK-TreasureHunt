using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetCoin : MonoBehaviour
{
    public TextMeshProUGUI coin;
    void Start()
    {
        string coinCollected = PlayerPrefs.GetString("CoinCollected");

        coin.text = coinCollected;
    }
}
