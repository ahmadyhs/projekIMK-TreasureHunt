using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CollectCoin: MonoBehaviour
{
    public TextMeshProUGUI counter;
    public AudioSource coinSoundEffect;
    public int pitchOffsetSteps = 4;
    public float pitchOffsetScale = 0.05f;
    private int count = 0;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            count++;
            counter.text= count.ToString();
            int pitchOffsetStep;
            pitchOffsetStep = (0 + count % (pitchOffsetSteps+1)) - pitchOffsetSteps/2;
            float pitchOffset = pitchOffsetStep * pitchOffsetScale;
            coinSoundEffect.pitch += pitchOffset;
            Debug.Log(coinSoundEffect.pitch + " offset = " + pitchOffset);
            coinSoundEffect.Play();
            PlayerPrefs.SetString("CoinCollected", counter.text);
        }
    }
}
