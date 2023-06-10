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
    public GameObject popupPrefab;
    private int count = 0;
    private PopupText pt;
    private int ptCount;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            Vector3 newPos = other.transform.position;
            newPos.y += 2f;
            if(pt == null)
            {
                pt = GameObject.Instantiate(popupPrefab, newPos, popupPrefab.transform.rotation).GetComponent< PopupText >();
                ptCount = 0;
            }
            else
            {
                pt.transform.position = newPos;
                pt.t = 0f;
            }
            ptCount += 1;
            pt.setText("+" + ptCount.ToString());
            (other.gameObject).SetActive(false);
            count++;
            if (counter != null) { 
                counter.text= count.ToString();
                PlayerPrefs.SetString("CoinCollected", counter.text);
            
            }
            int pitchOffsetStep;
            pitchOffsetStep = (0 + count % ((pitchOffsetSteps/2)*2+1)) - pitchOffsetSteps/2;
            float pitchOffset = pitchOffsetStep * pitchOffsetScale;
            coinSoundEffect.pitch += pitchOffset;
            Debug.Log(coinSoundEffect.pitch + " offset = " + pitchOffset);
            coinSoundEffect.Play();
        }
    }
}
