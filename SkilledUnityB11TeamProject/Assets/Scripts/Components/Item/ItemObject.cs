using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour, IInteractable
{
	public ItemData item;
	public AudioClip interactAudio;
	public string GetInteractPrompt()
	{
		return string.Format("Pickup {0}", item.itemName);
	}

	public void OnInteract()
	{
		GameManager.Instance.inventory.AddItem(item);
		PlayInteractionSound();
	}

	public void PlayInteractionSound()
	{
		SoundManager.PlayClip(interactAudio,GameManager.Instance.GetPlayer().transform.position);
	}
}

