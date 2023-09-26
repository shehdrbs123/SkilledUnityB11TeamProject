using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlotUI : MonoBehaviour
{
	public Button button;
	public Image icon;
	public TextMeshProUGUI quantityText;
	private ItemSlot curSlot;
	private Outline outline;

	public int index;
	public bool equipped;

	private void Awake()
	{
		outline = GetComponent<Outline>();
	}

	private void OnEnable()
	{
		outline.enabled = equipped;
	}

	public void Set(ItemSlot slot) //아이템 값을 전달 후 세팅
	{
		curSlot = slot;
		icon.gameObject.SetActive(true);
		icon.sprite = slot.item.icon;
		quantityText.text = slot.quantity > 1 ? slot.quantity.ToString() : string.Empty;

		if (outline != null)
		{
			outline.enabled = equipped;
		}
	}

	public void Clear()
	{
		curSlot = null;
		icon.gameObject.SetActive(false);
		quantityText.text = string.Empty;
	}

	public void OnButtonClick() // 인벤토리에서 내가 아이템 목록을 클릭했을 때 정보가 나옴
	{
		Inventory.instance.SelectItem(index);
	}
}
