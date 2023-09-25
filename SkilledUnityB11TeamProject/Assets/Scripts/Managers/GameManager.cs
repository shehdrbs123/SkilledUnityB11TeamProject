using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{ 
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject obj = GameObject.FindWithTag("GameObject");
                if (obj == null)
                {
                    obj = new GameObject("GameObject");
                    _instance = obj.AddComponent<GameManager>();
                }
            }

            return _instance;
        }
        private set
        {
            _instance = value;
        }
    }
    private static GameManager _instance;

    private GameObject player;
    public UIManager _uiManager { get; private set; }
    private void Awake()
    {
        _uiManager = GetComponent<UIManager>();
    }

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    public GameObject GetPlayer()
    {
        return player;
    }
}
