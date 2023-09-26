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
		//아이템 상호작용이 일어나면 실제보이는 아이템은 삭제시키고 클릭을 당한 게임오브젝트의
		//정보를 인벤토리 클래스의 AddItem함수를 이용하여 슬롯에 추가시킨다.
		Inventory.instance.AddItem(item);
		Destroy(gameObject);
	}
}
