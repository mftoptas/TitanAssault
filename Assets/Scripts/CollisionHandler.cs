using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float loadDelay = 1f;
    [SerializeField] ParticleSystem crashVFX;

    void OnTriggerEnter(Collider other)
    {
        StartCrashSequence();
    }

    void StartCrashSequence()
    {
        crashVFX.Play();
        GetComponent<MeshRenderer>().enabled = false; // Disable MeshRenderer so Ship cannot be seen anymore after collision.
        GetComponent<PlayerControls>().enabled = false; // Disable PlayerControls script so Ship cannot be controlled anymore after collision.
        GetComponent<BoxCollider>().enabled = false; // Disable BoxCollider so Ship cannot collide after first collision.
        Invoke("ReloadLevel", loadDelay); // Reload level after 1 second.
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; // Getting current scene.
        SceneManager.LoadScene(currentSceneIndex); // Loading current scene.
    }    
}
