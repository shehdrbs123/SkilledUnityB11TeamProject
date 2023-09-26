using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
	private Dictionary<string, ItemData> uiPrefabs;

	//[SerializeField] private ItemData mineral;
	//[SerializeField] private ItemData oilRock;
	public ItemData Pickax; 
	private void Awake()
	{
		var a = Resources.LoadAll<ItemData>(Path.Combine("Scriptables", "Item"));

		foreach (var item in a)
		{
			uiPrefabs.Add(item.itemName, item);
		}
	}
}
