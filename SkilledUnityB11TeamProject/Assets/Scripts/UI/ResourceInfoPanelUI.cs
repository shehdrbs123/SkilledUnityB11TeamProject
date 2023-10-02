using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class ResourceInfoPanelUI : MonoBehaviour
{
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text InfoText;
    [SerializeField] private Transform contents;
    
    private ResourceInfoUI[] resourceInfoUis;

    private void Awake()
    {
        resourceInfoUis = contents.GetComponentsInChildren<ResourceInfoUI>();
    }

    public void Init(GridScriptableObject data)
    {
        nameText.text = data.GetName();
        InfoText.text = data.GetDataInfo();
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
