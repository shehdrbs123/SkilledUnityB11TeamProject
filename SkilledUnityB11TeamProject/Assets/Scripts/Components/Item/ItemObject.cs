using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour, IInteractable
{
	public ItemData item;

	public string GetInteractPrompt()
	{
		return string.Format("Pickup {0}", item.itemName);
	}

	public void OnInteract()
	{
		GameManager.Instance.ResourceDisplayUI.resourceTxt.text = item.itemName;
		GameManager.Instance.ResourceDisplayUI.resourceDisplayImg.SetActive(true);
		GameManager.Instance.ResourceDisplayUI.animator.Play("ResourceDisplay", -1, 0f);
		GameManager.Instance.inventory.AddItem(item);
	}
}

