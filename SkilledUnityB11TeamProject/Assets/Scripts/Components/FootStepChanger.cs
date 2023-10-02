using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStepChanger : MonoBehaviour
{
    [SerializeField] private AudioClip[] footstep;
    [SerializeField] private bool isReturn = true;
    private AudioClip[] DefaultStep;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var playerMove = other.GetComponent<PlayerMovement>();
            DefaultStep = playerMove.GetFootStepSounds();
            playerMove.ChangeFootstepSound(footstep);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
        if (other.CompareTag("Player") && isReturn)
        {
            var playerMove = other.GetComponent<PlayerMovement>();
            playerMove.ChangeFootstepSound(DefaultStep);
        }
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            var playerMove = other.gameObject.GetComponent<PlayerMovement>();
            DefaultStep = playerMove.GetFootStepSounds();
            playerMove.ChangeFootstepSound(footstep);
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            var playerMove = other.gameObject.GetComponent<PlayerMovement>();
            playerMove.ChangeFootstepSound(DefaultStep);
        }
    }
}
