using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class OutOfBound : MonoBehaviour
{
        public int maxLives = 3;
        private int remainingLives;
        private Vector3 startingPosition;
        public TextMeshProUGUI counter;
        public TextMeshProUGUI lives;
        public Transform respawnPosition;
        public SceneReference targetScene;
        private void Start()
        {
            remainingLives = maxLives;
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            startingPosition = player.transform.position;
    }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("OUB"))
            {
            Debug.Log("kena");
                ReduceLife();
            }
        if (other.CompareTag("Trap"))
        {
            Debug.Log("trap");
            ReduceLife();
        }
    }

        private void ReduceLife()
        {
            remainingLives--;

            if (remainingLives <= 0)
            {
            PlayerPrefs.SetString("CoinCollected", counter.text);
            GameObject.Find("character").SendMessage("StopTimerAndStoreTime");
            SceneManager.LoadScene(targetScene);
            }
            else
            {
                lives.text = remainingLives.ToString();
                RespawnPlayer();
            }
        }

        private void RespawnPlayer()
        {
        
        Debug.Log("detect");
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Rigidbody playerRigidbody = player.GetComponent<Rigidbody>();

        playerRigidbody.velocity = Vector3.zero; 
        playerRigidbody.angularVelocity = Vector3.zero; 

        player.transform.position = startingPosition;
    }
    }

