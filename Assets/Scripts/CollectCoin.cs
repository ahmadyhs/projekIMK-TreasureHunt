using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CollectCoin: MonoBehaviour
{
    public TextMeshProUGUI counter;
    private int count = 0;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            count++;
            counter.text= count.ToString();
            PlayerPrefs.SetString("CoinCollected", counter.text);
        }
    }
}
