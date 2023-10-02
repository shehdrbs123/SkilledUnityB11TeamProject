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
			capacity = spawner.InitCapacity();
		}
	}

    private void OnEnable()
    {
		nowCapacity = capacity;
    }

    public void Gather()
	{
		for (int i = 0; i < quantityPerHit; i++)
		{
			if (nowCapacity <= 0)
			{
				break;
			}

			nowCapacity -= 1;

			GameManager.Instance.inventory.AddItem(itemToGive);
			particle.Play();
		}

		if (nowCapacity <= 0)
        {
            if (spawner != null)
            {
                gameObject.SetActive(false);
                spawner.Respawn(this.gameObject);
            }
			else
            {
				Destroy(this.gameObject);
            }
        }

    }
}
