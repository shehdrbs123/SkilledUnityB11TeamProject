using System;using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ConditionType{
    Mental,Hunger,Thirsty,Health
}
[Serializable]
public class Condition
{
    public ConditionType type;
    public float curValue;
    public float maxValue;
    public float startValue;
    public float decayRate;
    public float regenRate;

    public event Action OnValueChanged;

    private float decayPerSeconds;
    private float regenPerSeconds;

    public void Init()
    {
        curValue = startValue;
        decayPerSeconds = decayRate * Time.deltaTime;
        regenPerSeconds = regenRate * Time.deltaTime;
    }
    public void Update()
    {
        Decay();
        Regen();
        OnValueChanged?.Invoke();
    }

    private void Regen()
    {
        Add(regenPerSeconds);
    }

    private void Decay()
    {
        Add(-decayPerSeconds);
    }

    public void Add(float amount)
    {
        curValue = Mathf.Clamp(curValue + amount,0, maxValue);
    }

    public float GetPercentage()
    {
        return curValue / maxValue;
    }
}
public class Conditions : MonoBehaviour
{
    [Header("능력치 선택")]
    [SerializeField] private Condition[] _listConditions;
    private Dictionary<ConditionType, Condition> conditions = new Dictionary<ConditionType, Condition>();

    private void Awake()
    {
        conditions = new Dictionary<ConditionType, Condition>();
        foreach (var condition in _listConditions)
        {
            conditions.Add(condition.type,condition);
            condition.Init();
        }
    }

    private void Update()
    {
        foreach (var condition in _listConditions)
        {
            condition.Update();
        }
    }

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

    public void AddCondition(ConditionType type, int amount)
    {
        Condition con = GetCondition(type);
        con.Add(amount);
    }
}
