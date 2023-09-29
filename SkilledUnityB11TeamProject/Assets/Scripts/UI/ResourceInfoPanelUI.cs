using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceInfoPanelUI : MonoBehaviour
{
    [SerializeField] private Transform contents;

    private ResourceInfoUI[] resourceInfoUis;

    private void Awake()
    {
        resourceInfoUis = contents.GetComponentsInChildren<ResourceInfoUI>();
    }

    public void Init(GridScriptableObject data)
    {
        for(int i=0;i<data.resoureces.Length;++i)
        {
            resourceInfoUis[i].gameObject.SetActive(true);
            resourceInfoUis[i].Init(data.resoureces[i],data.resourecsCount[i]);
        }
    }

    public void OnDisable()
    {
        Array.ForEach(resourceInfoUis,(x) => x.gameObject.SetActive(false));
    }
}
