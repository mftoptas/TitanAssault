using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject deathFX; // We'll use game object because this is a game object we're looking to drop in the world, instantiate and drop in the world.
    [SerializeField] GameObject hitVFX;
    [SerializeField] int scorePerHit = 15;
    [SerializeField] int hitPoints = 4;

    ScoreBoard scoreBoard; // Created a variable scoreBoard of type ScoreBoard class.
    GameObject parentGameObject;

    void Start()
    {
        scoreBoard = FindObjectOfType<ScoreBoard>(); // Returns the first active loaded object of type.
        // Find object by type looks through our entire project and says the very first scoreboard you find, that's the scoreboard i'am referring to.
        parentGameObject = GameObject.FindWithTag("SpawnAtRuntime");
        AddRigidbody();
    }

    void AddRigidbody()
    {
        Rigidbody rb = gameObject.AddComponent<Rigidbody>(); // Add rigidbody to enemy object.
        rb.useGravity = false; // Enable the gravity so when game starts enemy object would not fall.
    }

    void OnParticleCollision(GameObject other)
    {
        ProcessHit();
        if (hitPoints < 1)
        {
        KillEnemy();
        }
    }

    void ProcessHit()
    {
        GameObject vfx = Instantiate(hitVFX, transform.position, Quaternion.identity); // Instantiate: Clones the object original and returns the clone.
        // Quaternion.identity means i don't need any rotation, leave it as is.
        vfx.transform.parent = parentGameObject.transform; // transform.parent is going to be the parent variable that i've just gone and define and serialised and pointed to.
        hitPoints--;
    }

    void KillEnemy()
    {
        scoreBoard.IncreaseScore(scorePerHit); // Increase the score when enemy is killed.
        GameObject fx = Instantiate(deathFX, transform.position, Quaternion.identity); // Instantiate: Clones the object original and returns the clone.
        // Quaternion.identity means i don't need any rotation, leave it as is.
        fx.transform.parent = parentGameObject.transform; // transform.parent is going to be the parent variable that i've just gone and define and serialised and pointed to.
        Destroy(gameObject);
    }
}
