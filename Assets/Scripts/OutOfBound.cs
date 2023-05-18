using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class OutOfBound : MonoBehaviour
{
        public int maxLives = 3;
        private int remainingLives;

        public Transform respawnPosition;
        public SceneReference targetScene;
        private void Start()
        {
            remainingLives = maxLives;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("OUB"))
            {
            Debug.Log("kena");
                ReduceLife();
            }
        }

        private void ReduceLife()
        {
            remainingLives--;

            if (remainingLives <= 0)
            {
            SceneManager.LoadScene(targetScene);
            }
            else
            {
                RespawnPlayer();
            }
        }

        private void RespawnPlayer()
        {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Rigidbody playerRigidbody = player.GetComponent<Rigidbody>();

        playerRigidbody.velocity = Vector3.zero; // Reset velocity
        playerRigidbody.angularVelocity = Vector3.zero; // Reset angular velocity

        player.transform.position = respawnPosition.position;
    }
    }

