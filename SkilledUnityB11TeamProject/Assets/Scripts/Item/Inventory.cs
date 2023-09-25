using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static UnityEditor.Progress;

public class ItemSlot
{
	public ItemData item;
	public int quantity;
}
public class Inventory : MonoBehaviour
{
	public ItemSlotUI[] uiSlot;
	public ItemSlot[] slots;

	public GameObject inventoryWindow;
	public Transform dropPosition;

	[Header("Selected Item")]
	private ItemSlot selectedItem;
	private int selectedItemIndex;
	public TextMeshProUGUI selectedItemName;
	public TextMeshProUGUI selectedItemDescription;
	public TextMeshProUGUI selectedItemStatNames;
	public TextMeshProUGUI selectedItemStatValues;
	public GameObject useButton;
	public GameObject dropButton;

	private int curEquipIndex;
	private PlayerController controller;
	[Header("Events")]
	public UnityEvent onOpenInventory;
	public UnityEvent onCloseInventory;

	public static Inventory instance;


	// Start is called before the first frame update
	private void Awake()
	{
		instance = this;
		controller = GetComponent<PlayerController>();
	}

	private void Start()
	{
		inventoryWindow.SetActive(false);
		slots = new ItemSlot[uiSlot.Length];

		for (int i = 0; i < slots.Length; i++)
		{
			slots[i] = new ItemSlot();
			uiSlot[i].index = i;
			uiSlot[i].Clear();

		}
		ClearSelectedItemWindow();
	}

	public void Toggle()
	{
		if (inventoryWindow.activeInHierarchy)
		{
			inventoryWindow.SetActive(false);
			onCloseInventory?.Invoke();
			controller.ToggleCursor(false);
		}
		else
		{
			inventoryWindow.SetActive(true);
			onOpenInventory?.Invoke();
			controller.ToggleCursor(true);
		}
	}

	public void OnInventoryButton(InputAction.CallbackContext callbackContext)
	{
		if (callbackContext.phase == InputActionPhase.Started)
		{
			Toggle();
		}
	}

	public bool IsOpen()
	{
		return inventoryWindow.activeInHierarchy;
	}

	public void AddItem(ItemData item) //이거를 이용해서 아이템 채취시 얻기
	{
		if (item.canStack)
		{
			ItemSlot slotToStakTo = GetItemStack(item);
			if (slotToStakTo != null)
			{
				slotToStakTo.quantity++;
				UpdateUI();
				return;
			}
		}

		ItemSlot emptySlot = GetEmptySlot();

		if (emptySlot != null)
		{
			emptySlot.item = item;
			emptySlot.quantity = 1;
			UpdateUI();
			return;
		}
	}

	void UpdateUI()
	{
		for (int i = 0; i < slots.Length; i++)
		{
			if (slots[i].item != null)
				uiSlot[i].Set(slots[i]);
			else
				uiSlot[i].Clear();
		}
	}

	ItemSlot GetItemStack(ItemData item) //슬롯을 찾아오는 함수
	{
		for (int i = 0; i < slots.Length; i++)
		{
			if (slots[i].item == item && slots[i].quantity < item.maxStackAmount)
				return slots[i];
		}
		return null;
	}

	ItemSlot GetEmptySlot() //아이템이 비어있다면
	{
		for (int i = 0; i < slots.Length; i++)
		{
			if (slots[i].item == null)
				return slots[i];
		}
		return null;
	}

	public void SelectItem(int index)
	{
		if (slots[index].item == null)
			return;

		selectedItem = slots[index];
		selectedItemIndex = index;

		selectedItemName.text = selectedItem.item.itemName;
		selectedItemDescription.text = selectedItem.item.description;

		selectedItemStatNames.text = string.Empty;
		selectedItemStatValues.text = string.Empty;

		//for (int i = 0; i < selectedItem.item.consumables.Length; i++)
		//{
		//	selectedItemStatNames.text += selectedItem.item.consumables[i].type.ToString() + "\n";
		//	selectedItemStatValues.text += selectedItem.item.consumables[i].value.ToString() + "\n";
		//}

		useButton.SetActive(selectedItem.item.type == ItemType.Consumable);
		dropButton.SetActive(true);
	}

	private void ClearSelectedItemWindow()
	{
		selectedItem = null;
		selectedItemName.text = string.Empty;
		selectedItemDescription.text = string.Empty;

		selectedItemStatNames.text = string.Empty;
		selectedItemStatValues.text = string.Empty;

		useButton.SetActive(false);
		dropButton.SetActive(false);
	}

	//public void OnUseButton()
	//{
	//	if (selectedItem.item.type == ItemType.Consumable)
	//	{
	//		for (int i = 0; i < selectedItem.item.consumables.Length; i++)
	//		{
	//			switch (selectedItem.item.consumables[i].type)
	//			{
	//				case ConsumableType.Health:
	//					condition.Heal(selectedItem.item.consumables[i].value); break;
	//				case ConsumableType.Hunger:
	//					condition.Eat(selectedItem.item.consumables[i].value); break;
	//			}
	//		}
	//	}
	//	RemoveSelectedItem();
	//}

	public void OnDropButton()
	{
		RemoveSelectedItem();
	}

	//private void RemoveSelectedItem()
	//{
	//	selectedItem.quantity--;

	//	if (selectedItem.quantity <= 0)
	//	{
	//		if (uiSlot[selectedItemIndex].equipped)
	//		{
	//			UnEquip(selectedItemIndex);
	//		}

	//		selectedItem.item = null;
	//		ClearSelectedItemWindow();
	//	}
	//	UpdateUI();
	//}

	public void RemoveItem(ItemData item)
	{

	}

	public bool HasItems(ItemData item, int quantity)
	{
		return false;
	}
}