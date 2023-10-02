using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "CraftData", menuName = "Scriptable/Craft Recipe")]
public class CraftDataSO : GridScriptableObject
{
    public ItemData ResultItem;
    public override string GetDataInfo()
    {
        string resultString;
        sb.Append("이름 : ").Append(ResultItem.itemName).Append('\n');
        sb.Append("설명 : ").Append(ResultItem.description).Append('\n');
        sb.Append("효과 : ");

        for (int i = 0; i < ResultItem.consumables.Length;++i)
        {
            sb.Append(GetComsumableType(ResultItem.consumables[i].type)).Append(" ")
                .Append(ResultItem.consumables[i].value).Append(' ');
        }
        
        resultString = sb.ToString();
        sb.Clear();
        return resultString;
    }

    private string GetComsumableType(ConsumableType type)
    {
        switch (type)
        {
            case ConsumableType.Hunger :
                return "배고픔";
            case ConsumableType.Thirsty :
                return "목마름";
            case ConsumableType.Mental :
                return "멘탈";
        }

        return string.Empty;
    }
}
