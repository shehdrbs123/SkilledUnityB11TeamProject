using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ConditionType
{
    Hunger,
    Thirsty,
    Mental,
}

[System.Serializable]
public class Condition
{
    public ConditionType type;
    [HideInInspector] public float curValue;
    public float maxValue;
    public float startValue;
    public float decayRate;
    public float regenRate;
    public Image uiBar;

    public void Initalize()
    {
        curValue = startValue;
    }

    public void Change(float amount)
    {
        curValue = Mathf.Clamp(curValue + amount, 0f, 100f);
    }

    public float GetPercentage()
    {
        return curValue / maxValue;
    }

    public void Update()
    {
        Change(-1 * decayRate * Time.deltaTime);
        // uiBar.fillAmount = GetPercentage();
    }
}

public class ConditionManager : MonoBehaviour
{
    [Header("Game Stat")]
    public Condition hunger;
    public Condition thirsty;
    public Condition mental;

    [Header("Life")]
    public int battery;

    [SerializeField] private GameObject conditionCanvas;

    private List<GameObject> batteries = new List<GameObject>();

    private void Start()
    {
        hunger.Initalize();
        thirsty.Initalize();
        mental.Initalize();
        battery = 4;

        for (int i = 0; i < battery; i++)
        {
            GameObject obj = GameManager.Instance.prefabManager.SpawnFromPool(PoolType.Battery);
            obj.transform.position += new Vector3(0, 0, -3 * i);
            obj.SetActive(true);
            batteries.Add(obj);
        }

        Instantiate(conditionCanvas);
    }

    private void Update()
    {
        hunger.Update();
        thirsty.Update();

        if (hunger.GetPercentage() <= 0.00f || thirsty.GetPercentage() <= 0.00f)
        {
            mental.Update();
        }

        if (mental.GetPercentage() <= 0.00f)
        {
            GameOver();
        }
    }

    public void GetDamaged()
    {
        if (battery > 0)
        {
            battery -= 1;
            StartCoroutine(CoDisapear(batteries[battery]));
        }
        else
        {
            GameOver();
        }
    }

    private IEnumerator CoDisapear(GameObject battery)
    {
        while (battery.transform.position.y > -4.5f)
        {
            battery.transform.Translate(Vector3.down * 0.05f);
            yield return null;
        }

        battery.SetActive(false);
    }

    private void GameOver()
    {
        Debug.Log("GAME OVER");
    }
}
