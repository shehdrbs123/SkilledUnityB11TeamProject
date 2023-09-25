using System;using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ConditionType{
    Mental,Hunger,Thirsty,Health
}

public class Condition
{
    public float curValue;
    public float MaxValue;
    public float StartValue;
    public float decayRate;
    public float regenRate;

    public event Action OnValueChanged;

    public void Add(float amount)
    {
        curValue = Mathf.Clamp(curValue + amount,0, MaxValue);
        OnValueChanged?.Invoke();
    }

    public float GetPercentage()
    {
        return curValue / MaxValue;
    }
}
public class Conditions : MonoBehaviour
{
    public Dictionary<ConditionType, Condition> conditions = new Dictionary<ConditionType, Condition>();

    public Condition GetCondition(ConditionType type)
    {
        Condition value = null;
        
        if (!conditions.TryGetValue(type, out value))
        {
#if UNITY_EDITOR
            Debug.Log($"{type.ToString()}은 {name}에 없는 Condition입니다");
#endif
        }
        return value;
    }
}
