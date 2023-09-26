using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
	Resource,
	Equipable,
	Consumable
}
public enum ConsumableType
{
    Hunger,
	Thirst
}

[CreateAssetMenu(fileName = "Item", menuName = "New Item")]
public class ItemData : ScriptableObject
{
	[Header("Info")]
	public string itemName;
	public string description;
	public ItemType type;
	public Sprite icon;

	[Header("Stacking")]
	public bool canStack;
	public int maxStackAmount;

	//[Header("Consumable")] 소모형 아이템이 생성되면 시작

	[Header("Equip")]
	public GameObject equipPrefab;
	
}
