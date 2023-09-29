using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class Condition
{
    public float curValue;
    public float maxValue;
    public float startValue;
    public float decayRate;
    public Image uiBar;

    public void Initalize()
    {
        curValue = startValue;
    }

    public void Change(float amount)
    {
        curValue = Mathf.Clamp(curValue + amount, 0f, 100f);
        uiBar.fillAmount = curValue / maxValue;
    }

    public bool IsZero()
    {
        return curValue <= 0.0f;
    }

    public void Decay()
    {
        Change(-1 * decayRate * Time.deltaTime);
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

    private readonly List<GameObject> batteries = new List<GameObject>();

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
    }

    private void Update()
    {
        hunger.Decay();
        thirsty.Decay();

        if (hunger.IsZero() || thirsty.IsZero())
        {
            mental.Decay();
        }

        if (mental.IsZero())
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
