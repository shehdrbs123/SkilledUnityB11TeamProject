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

	[Header("Selected Item")]
	private ItemSlot selectedItem;
	private int selectedItemIndex;
	public TextMeshProUGUI selectedItemName;
	public TextMeshProUGUI selectedItemDescription;
	public GameObject useButton;
	public GameObject equipButton;
	public GameObject unEquipButton;
	public GameObject dropButton;

	private ItemData pickaxe;
	private ItemData hammer;
	private int curEquipIndex;
	private PlayerMovement controller;
	[Header("Events")]
	public UnityEvent onOpenInventory;
	public UnityEvent onCloseInventory;
	// Start is called before the first frame update

	private void Awake()
	{
		controller = GetComponent<PlayerMovement>();
	}

	private void Start()
	{
		GameManager.Instance.inventory = this;
		pickaxe = GameManager.Instance._itemManager.Pickax;
		hammer = GameManager.Instance._itemManager.Hammer;
		inventoryWindow.SetActive(false);
		slots = new ItemSlot[uiSlot.Length];

		for (int i = 0; i < slots.Length; i++)
		{
			slots[i] = new ItemSlot();
			uiSlot[i].index = i;
			uiSlot[i].Clear();

		}
		ClearSelectedItemWindow();
		AddItem(pickaxe);
		AddItem(hammer);
	}

	public void Toggle()
	{
		if (inventoryWindow.activeInHierarchy)
		{
			GameManager.Instance._uiManager.RemoveUICount(gameObject);
			inventoryWindow.SetActive(false);
			onCloseInventory?.Invoke();
		}
		else
		{
			GameManager.Instance._uiManager.AddUICount(gameObject);
			inventoryWindow.SetActive(true);
			onOpenInventory?.Invoke();
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

	public void AddItem(ItemData item) 
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

	ItemSlot GetItemStack(ItemData item) 
	{
		for (int i = 0; i < slots.Length; i++)
		{
			if (slots[i].item == item && slots[i].quantity < item.maxStackAmount)
				return slots[i];
		}
		return null;
	}

	ItemSlot GetEmptySlot() 
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

		useButton.SetActive(selectedItem.item.type == ItemType.Consumable);
		equipButton.SetActive(selectedItem.item.type == ItemType.Equipable && !uiSlot[index].equipped);
		unEquipButton.SetActive(selectedItem.item.type == ItemType.Equipable && uiSlot[index].equipped);
		dropButton.SetActive(selectedItem.item.type != ItemType.Equipable);
	}

	private void ClearSelectedItemWindow()
	{
		selectedItem = null;
		selectedItemName.text = string.Empty;
		selectedItemDescription.text = string.Empty;

		equipButton.SetActive(false);
		dropButton.SetActive(false);
		useButton.SetActive(false);
		unEquipButton.SetActive(false);
	}

	public void OnUseButton()
	{
		if (selectedItem.item.type == ItemType.Consumable)
		{
			for (int i = 0; i < selectedItem.item.consumables.Length; i++)
			{
				switch (selectedItem.item.consumables[i].type)
				{
					case ConsumableType.Thirsty:
						//condition.Heal(selectedItem.item.consumables[i].value); 
						break;
					case ConsumableType.Hunger:
						//condition.Eat(selectedItem.item.consumables[i].value);
						break;
				}
			}
		}
		RemoveSelectedItem();
	}

	public void OnPickaxEquipButton()
	{
		if (uiSlot[0].equipped)
		{
			UnEquip(0);
		}

		uiSlot[0].equipped = true;
		uiSlot[1].equipped = false;
		curEquipIndex = 0;
		GameManager.Instance._equipManager.EquipNew(pickaxe);
		UpdateUI();

		SelectItem(0);
	}
	public void OnHammerEquipButton()
	{
		if (uiSlot[1].equipped)
		{
			UnEquip(1);
		}
		uiSlot[0].equipped = false;
		uiSlot[1].equipped = true;
		curEquipIndex = 1;
		GameManager.Instance._equipManager.EquipNew(hammer);
		UpdateUI();

		SelectItem(1);
	}
	public void OnEquipButton()
	{
		if (uiSlot[curEquipIndex].equipped)
		{
			UnEquip(curEquipIndex);
		}

		uiSlot[selectedItemIndex].equipped = true;
		
		GameManager.Instance._equipManager.EquipNew(selectedItem.item);
		UpdateUI();

		SelectItem(selectedItemIndex);
	}

	void UnEquip(int index)
	{
		uiSlot[index].equipped = false;
		GameManager.Instance._equipManager.UnEquip();
		UpdateUI();

		if(selectedItemIndex == index)
		{
			SelectItem(index);
		}
	}

	public void OnUnEquipButton()
	{
		UnEquip(selectedItemIndex);
	}
	public void OnDropButton()
	{
		RemoveSelectedItem();
	}

	private void RemoveSelectedItem()
	{
		selectedItem.quantity--;

		if (selectedItem.quantity <= 0)
		{
			if (uiSlot[selectedItemIndex].equipped)
			{
				UnEquip(selectedItemIndex);
			}

			selectedItem.item = null;
			ClearSelectedItemWindow();
		}
		UpdateUI();
	}

	public void RemoveItem(ItemData item, int quantity)
	{
		ItemSlot test = GetItemStack(item);
		test.quantity -= quantity;
		if (test.quantity <= 0)
		{
			test.quantity = 0;
			test.item = null;
		}

		UpdateUI();
	}

	public bool HasItems(ItemData item, int quantity)
	{
		ItemSlot test = GetItemStack(item);
		if (test != null && test.quantity >= quantity)
		{
			return true;
		}
		return false;
	}
}