using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
	public ItemData itemToGive; 
	public int quantityPerHit = 1;

	public ParticleSystem particle;
	private ResourceSpawner spawner;

	private int capacity;

    private void Awake()
    {
		if (transform.parent.TryGetComponent(out ResourceSpawner sp))
		{
			spawner = sp;
		}
		else
        {
			gameObject.SetActive(false);
        }
	}

    private void OnEnable()
    {
		capacity = spawner.InitCapacity();
    }

    public void Gather()
	{
		for (int i = 0; i < quantityPerHit; i++)
		{
			if (capacity <= 0)
			{
				break;
			}

			capacity -= 1;
			GameManager.Instance.inventory.AddItem(itemToGive);
			particle.Play();
		}

		if (capacity <= 0)
        {
			gameObject.SetActive(false);
			spawner.Respawn(this.gameObject);
		}

    }
}
