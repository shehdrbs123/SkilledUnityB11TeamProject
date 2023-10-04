using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    private GameManager gameManager;

    [Header("아이템")] 
    [SerializeField] private ItemData item;
    [SerializeField] private int quantity;
    private void Start()
    {
        gameManager = GameManager.Instance;
    }

    public void ItemAdd()
    {
        GameObject player = gameManager.GetPlayer();
        Inventory inven = player.GetComponent<Inventory>();

        for (int i = 0; i < quantity; ++i)
        {
            inven.AddItem(item);
        }
    }
}
