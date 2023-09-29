using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourceInfoUI : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private TMP_Text text;
    

    public void Init(ItemData data, int count)
    {
        image.sprite = data.icon;
        text.text = count.ToString();
    }
}
