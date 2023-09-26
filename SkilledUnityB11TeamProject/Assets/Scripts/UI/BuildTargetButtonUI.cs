using UnityEngine;
using UnityEngine.UI;

public class BuildTargetButtonUI : MonoBehaviour
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
