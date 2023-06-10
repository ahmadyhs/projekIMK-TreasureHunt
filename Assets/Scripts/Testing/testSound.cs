using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testSound : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource audioSource;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void playSound()
    {
        audioSource.Play();
        Debug.Log("sound played");
    }
}
