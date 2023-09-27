using System;
using UnityEngine;
using UnityEngine.UI;

public class BuildTargetButtonUI : GridButtonUI
{
    [SerializeField] private Image _buildTargetImage;
    private Button button;
    
    public BuildDataSO DataSo
    {
        get
        {
            return _data;
        }
        set
        {
            _data = value;
            UpdateData();
        }
    }
    private BuildDataSO _data;
    private BuildManager _buildManager;

    private void Start()
    {
        _buildManager = GameManager.Instance._buildManager;
    }

    public override void Init(ScriptableObject data, Transform parent)
    {
        BuildTargetButtonUI target = GetComponent<BuildTargetButtonUI>();
        Button button = GetComponent<Button>();

        target.DataSo = data as BuildDataSO;
        button.onClick.AddListener(target.CreateBuild);
            
        transform.SetParent(parent,false);
    }
    
    public void UpdateData()
    {
        SetImage(_data.StructureUISprite);
    }

    public void CreateBuild()
    {
        _buildManager.SetBuildMode(_data);
    }

    private void ButtonEnable(bool enable)
    {
        button.interactable = enable;
    }

    private void SetImage(Sprite sprite)
    {
        _buildTargetImage.sprite = sprite;
    }


    
}
