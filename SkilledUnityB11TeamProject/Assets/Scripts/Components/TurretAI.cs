using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAI : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log($"{other.name}이 나감");
    }
}
