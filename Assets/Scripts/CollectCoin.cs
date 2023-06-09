using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CollectCoin: MonoBehaviour
{
    public TextMeshProUGUI counter;
    public AudioSource coinSoundEffect;
    private int count = 0;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            count++;
            counter.text= count.ToString();
            coinSoundEffect.Play();
            PlayerPrefs.SetString("CoinCollected", counter.text);
        }
    }
}
