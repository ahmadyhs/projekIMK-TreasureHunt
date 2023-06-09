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
    public TextMeshProUGUI checkpointText; // Reference to the checkpoint text
    public float checkpointTextDuration = 3f; // Duration in seconds to show the checkpoint text
    public Transform respawnPosition;
    public SceneReference targetScene;
    private Vector3 checkpointPosition;
    private bool isRespawning = false;

    public AudioSource checkpointSoundEffect;
    public AudioSource trapSoundEffect;

    private void Start()
    {
        remainingLives = maxLives;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        startingPosition = player.transform.position;
        checkpointText.gameObject.SetActive(false); // Initially set the checkpoint text to inactive
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("OUB"))
        {
            ReduceLife();
            Invoke("ResetRespawn", 5f);
        }
        else if (other.CompareTag("Flag"))
        {
            SetCheckpoint(other.transform.position);
        }
        else if (other.CompareTag("Trap"))
        {
            if (trapSoundEffect != null)
            {
                trapSoundEffect.Play();
            }
            ReduceLife();
        }
    }

    private void ReduceLife()
    {
        if (!isRespawning)
        {
            isRespawning = true;
            remainingLives--;

            if (remainingLives <= 0)
            {
                PlayerPrefs.SetString("CoinCollected", counter.text);
                GetComponent<Timer>().StopTimerAndStoreTime();
                SceneManager.LoadScene(targetScene);
            }
            else
            {
                lives.text = remainingLives.ToString();
                PlayerPrefs.SetInt("PreviousSceneIndex", SceneManager.GetActiveScene().buildIndex);
                RespawnPlayer();
            }
        }
    }

    private void SetCheckpoint(Vector3 position)
    {
        checkpointPosition = position;
        Debug.Log("Checkpoint set at position: " + checkpointPosition);
        StartCoroutine(ShowCheckpointText());

        // Play the checkpoint sound effect
        if (checkpointSoundEffect != null)
        {
            checkpointSoundEffect.Play();
        }
    }

    private IEnumerator ShowCheckpointText()
    {
        checkpointText.gameObject.SetActive(true); // Set the checkpoint text to active
        yield return new WaitForSeconds(checkpointTextDuration);
        checkpointText.gameObject.SetActive(false); // Set the checkpoint text to inactive after the duration
    }

    private void RespawnPlayer()
    {
        Debug.Log("Respawn");
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Rigidbody playerRigidbody = player.GetComponent<Rigidbody>();

        playerRigidbody.velocity = Vector3.zero;
        playerRigidbody.angularVelocity = Vector3.zero;

        Vector3 respawnPosition = Vector3.zero;

        if (checkpointPosition != Vector3.zero)
        {
            respawnPosition = checkpointPosition;
        }
        else
        {
            respawnPosition = startingPosition;
        }

        // Adjust the respawn position to be slightly above the starting position
        float respawnOffset = 15.5f;
        respawnPosition += Vector3.up * respawnOffset;

        player.transform.position = respawnPosition;

        Invoke("ResetRespawn", 0.5f);
    }

    private void ResetRespawn()
    {
        isRespawning = false;
    }
}
