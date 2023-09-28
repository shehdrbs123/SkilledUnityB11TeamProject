using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeManager : MonoBehaviour
{
    public int life = 4;
    [SerializeField] private GameObject batteries;
    private List<GameObject> test = new List<GameObject>();

    private PrefabManager prefabManager;

    private void Start()
    {
        prefabManager = GameManager.Instance.prefabManager;

        for (int i = 0; i < 4; i++)
        {
            GameObject obj = prefabManager.SpawnFromPool(PoolType.Battery);
            obj.transform.position += new Vector3(0, 0, -3 * i);
            obj.SetActive(true);
            test.Add(obj);
        }
    }

    public void GetDamaged()
    {
        if (life > 0)
        {
            life -= 1;
            StartCoroutine(CoDisapear(test[life]));
        }
        else
        {
            Debug.Log("GAME OVER");
        }
    }

    private IEnumerator CoDisapear(GameObject obj)
    {
        while (obj.transform.position.y > -4.5f)
        {
            obj.transform.Translate(Vector3.down * 0.05f);
            yield return null;
        }

        obj.SetActive(false);
    }
}
