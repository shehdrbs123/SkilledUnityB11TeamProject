using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;

    private void Start()
    {
        if (!GameObject.FindWithTag("Player"))
        {
            Instantiate(playerPrefab, transform.position, transform.rotation);
        }
    }
}
