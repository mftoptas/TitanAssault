using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    void Awake()
    {
        int numMusicPlayers = FindObjectsOfType<MusicPlayer>().Length; // Find how many type of music player.

        if (numMusicPlayers >1)
        {
            Destroy(gameObject); // If there already is a music playing destroy the new one.
        }
        else
        {
            DontDestroyOnLoad(gameObject); // Don't destroy if there is not a music playing.
        }
    }
}
