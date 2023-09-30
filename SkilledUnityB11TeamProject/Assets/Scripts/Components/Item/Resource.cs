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

	private int nowCapacity;

    private void Awake()
    {
		if (transform.parent.TryGetComponent(out ResourceSpawner sp))
		{
			spawner = sp;
		}
	}

    private void OnEnable()
    {
		nowCapacity = capacity;
    }

    private void OnDisable()
    {
		spawner.Respawn();
	}

    public void Gather()
	{
		for (int i = 0; i < quantityPerHit; i++)
		{
			if (nowCapacity <= 0) { break; }
			nowCapacity -= 1;
			//Inventory.instance.AddItem(itemToGive);
			GameManager.Instance.inventory.AddItem(itemToGive);
			particle.Play();
		}

		if (nowCapacity <= 0)
			gameObject.SetActive(false);
	}
}
