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
	Thirsty,
	Mental
}
[System.Serializable]
public class ItemDataConsumable
{
	public ConsumableType type;
	public float value;
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

	[Header("Consumable")]
	public ItemDataConsumable[] consumables;

	[Header("Equip")]
	public GameObject equipPrefab;

	[Header("UseAudio")] 
	public AudioClip[] UseAudio;
}
