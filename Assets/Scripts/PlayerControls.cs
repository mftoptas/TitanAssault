using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [Header("General Settings")]
    [Tooltip("How fast ship moves up and down based upon player input.")]
    [SerializeField] float controlSpeed = 30f;
    [Tooltip("How far player moves horizontally.")]
    [SerializeField] float xRange = 10f;
    [Tooltip("How far player moves vertically.")]
    [SerializeField] float yRange = 7f;

    [Header("Laser gun array")]
    [Tooltip("Add all player lasers here.")]
    [SerializeField] GameObject[] lasers;

    [Header("Screen position based tuning")]
    [SerializeField] float positionPitchFactor = -2f;
    [SerializeField] float positionYawFactor = 2f;

    [Header("Player input based tuning")]
    [SerializeField] float controlPitchFactor = -15f;
    [SerializeField] float controlRollFactor = -20f;

    float xThrow, yThrow;

    // Update is called once per frame
    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
        ProcessFiring();
    }

    void ProcessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor; // Pitch's value due to position
        float pitchDueToControlThrow = yThrow * controlPitchFactor; // Pitch's value due to throw

        float pitch =  pitchDueToPosition + pitchDueToControlThrow; // Pitch's value due to position&throw
        float yaw = transform.localPosition.x * positionYawFactor; // Yaw's value due to position
        float roll = xThrow * controlRollFactor; // Roll's value due to throw

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll); // It rotates in a moment when game starts.
    }

    void ProcessTranslation()
    {
        xThrow = Input.GetAxis("Horizontal"); // Returns the value of the virtual axis identified by axisName.
        yThrow = Input.GetAxis("Vertical"); // Returns the value of the virtual axis identified by axisName.

        float xOffset = xThrow * Time.deltaTime * controlSpeed;
        float rawXPos = transform.localPosition.x + xOffset; // transform.localPosition = Position of the transform relative to the parent transform.
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange); // Clamps the given value between the given minimum float and maximum float values. Returns the given value if it is within the minimum and maximum range

        float yOffset = yThrow * Time.deltaTime * controlSpeed;
        float rawYPos = transform.localPosition.y + yOffset; // transform.localPosition = Position of the transform relative to the parent transform.
        float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange); // Clamps the given value between the given minimum float and maximum float values. Returns the given value if it is within the minimum and maximum range.

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }
    void ProcessFiring()
    {
        if (Input.GetButton("Fire1"))
        {
            SetLasersActive(true);
        }
        else
        {
            SetLasersActive(false);
        }
    }
    
    void SetLasersActive(bool isActive)
    {
        foreach (GameObject laser in lasers)
        {
            var emissionModule = laser.GetComponent<ParticleSystem>().emission; // Get the emission module
            emissionModule.enabled = isActive;
        }
    }
}
