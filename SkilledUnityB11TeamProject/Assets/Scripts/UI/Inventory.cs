using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

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

	private Dictionary<ItemData, int> ItemTotalCount;
	private void Awake()
	{
		controller = GetComponent<PlayerMovement>();
		ItemTotalCount = new Dictionary<ItemData, int>();
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
				ItemTotalCount[item] += 1;
				UpdateUI();
				GameManager.Instance.resourceDisplayUI.ShowGetResource(item);
				return;
			}
		}

		ItemSlot emptySlot = GetEmptySlot();

		if (emptySlot != null)
		{
			emptySlot.item = item;
			emptySlot.quantity = 1;
			if (ItemTotalCount.ContainsKey(item))
				ItemTotalCount[item] += 1;
			else
				ItemTotalCount[item] = 1;
			UpdateUI();
			GameManager.Instance.resourceDisplayUI.ShowGetResource(item);
			return;
		}

		return;
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
					case ConsumableType.Hunger:
						GameManager.Instance._conditionManager.hunger.Change(selectedItem.item.consumables[i].value);
						break;

					case ConsumableType.Thirsty:
						GameManager.Instance._conditionManager.thirsty.Change(selectedItem.item.consumables[i].value);
						break;

					case ConsumableType.Mental:
						GameManager.Instance._conditionManager.mental.Change(selectedItem.item.consumables[i].value);
						break;
				}
				SoundManager.PlayRandomClip(selectedItem.item.UseAudio,transform.position);
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
		SoundManager.PlayRandomClip(pickaxe.UseAudio,transform.position);
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

		SoundManager.PlayRandomClip(hammer.UseAudio,transform.position);
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
		SoundManager.PlayRandomClip(selectedItem.item.UseAudio,transform.position);
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
	

	public void ComsumeItem(ItemData item, int quantity)
	{
		int currentConsumeQuantity = 0;
		List<ItemSlot> removeSlots = new List<ItemSlot>();
		List<ItemSlot> slots = GetAllItemStack(item);
		foreach (var slot in slots)
		{
			if (currentConsumeQuantity >= quantity)
				break;
			removeSlots.Add(slot);
			currentConsumeQuantity += slot.quantity;
		}
			

		foreach (var slot in removeSlots)
		{
			int remain = slot.quantity - quantity;
			if (remain <= 0)
			{
				ItemTotalCount[item] -= slot.quantity;
				slot.quantity = 0;
				slot.item = null;
				quantity = -remain;
			}
			else
			{
				ItemTotalCount[item] -= quantity;
				slot.quantity -= quantity;
			}
		}

		slots = null;
		removeSlots = null;
		
		UpdateUI();
		return;
	}

	public bool HasItems(ItemData item, int quantity)
	{
		if (ItemTotalCount.TryGetValue(item, out int count))
		{
			if (quantity > count)
				return false;
			else
				return true;
		}
		return false;
	}

	private List<ItemSlot> GetAllItemStack(ItemData item)
	{
		List<ItemSlot> stacks = new List<ItemSlot>();
		for (int i = slots.Length-1; i >=0; --i)
		{
			if (slots[i].item == item)
				stacks.Add(slots[i]);
		}

		return stacks;
	}
}