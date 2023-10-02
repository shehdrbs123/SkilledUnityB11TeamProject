using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourceDisplayUI : MonoBehaviour
{
	PrefabManager prefabManager;

    private void Start()
    {
        prefabManager = GameManager.Instance.prefabManager;
    }

    public void ShowGetResource(ItemData item)
	{
        GameObject obj = prefabManager.SpawnFromPool(PoolType.GetUI);
        obj.SetActive(true);
        obj.GetComponent<GetPopupUI>().Initial(item);
	}
}
