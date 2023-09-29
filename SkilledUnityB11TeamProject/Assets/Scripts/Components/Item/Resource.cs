using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
	public ItemData itemToGive; 
	public int quantityPerHit = 1;
	public int capacity;

	public ParticleSystem particle;
	private ResourceSpawner spawner;

    private void Awake()
    {
		if (transform.parent.TryGetComponent(out ResourceSpawner sp))
		{
			spawner = sp;
		}
	}

    private void OnDisable()
    {
		spawner.Respawn();
	}

    public void Gather()
	{
		for (int i = 0; i < quantityPerHit; i++)
		{
			if (capacity <= 0) { break; }
			capacity -= 1;
			//Inventory.instance.AddItem(itemToGive);
			GameManager.Instance.inventory.AddItem(itemToGive);
		}

		if (capacity <= 0)
			gameObject.SetActive(false);
	}
}
