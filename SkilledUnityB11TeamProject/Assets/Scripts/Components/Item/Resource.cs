using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
	public ItemData itemToGive; // 게임매니저에서 받아와야할 부분
	public int quantityPerHit = 1;
	public int capacity;

	public void Gather()
	{
		for (int i = 0; i < quantityPerHit; i++)
		{
			if (capacity <= 0) { break; }
			capacity -= 1;
			Inventory.instance.AddItem(itemToGive);
		}

		if (capacity <= 0)
			Destroy(gameObject);
	}
}
