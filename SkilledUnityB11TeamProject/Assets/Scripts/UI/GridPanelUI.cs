using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;


public class GridPanelUI : BaseUI
{
    public string buttonUIName;
    
    [SerializeField] private Transform _contentPanel;
    public Dictionary<string, List<GameObject>> buttonGroupDic;

    protected override void Awake()
    {
        base.Awake();
        buttonGroupDic = new Dictionary<string, List<GameObject>>();
    }
    

    public void Init(GridPanelManager manager, string buttonUIName)
    {
        if (buttonGroupDic.TryGetValue(buttonUIName,out List<GameObject> list ))
        {
            list.ForEach(x => x.SetActive(true));
        }
        else
        {
            List<GameObject> buttons = new List<GameObject>(10);
            buttonGroupDic.Add(buttonUIName,buttons);

            GridPanelManager _manager = manager;
            this.buttonUIName = buttonUIName;
        
            int count = _manager.GetElementsCount();
            for (int i = 0; i < count; ++i)
            {
                GameObject obj = _uiManager.GetUIClone(buttonUIName);
                obj.GetComponent<GridButtonUI>().Init(_manager.GetData(i),_contentPanel,() => gameObject.SetActive(false));
                buttons.Add(obj);
            }            
        }
    }
    

    private void Start()
    {
        if (_uiManager == null)
        {
            Init(GameManager.Instance._buildManager,buttonUIName);
        }
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        if (buttonGroupDic.TryGetValue(buttonUIName, out List<GameObject> buttons))
        {
            buttons.ForEach(x => x.SetActive(false));
        }
    }
}
