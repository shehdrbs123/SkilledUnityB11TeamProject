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
                GameObject obj = GameObject.FindWithTag("GameManager");
                _instance = obj.GetComponent<GameManager>();
                if (_instance == null)
                {
                    obj = new GameObject("GameManager");
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
    public UIManager _uiManager;
    public BuildManager _buildManager;
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        player = GameObject.FindWithTag("Player");
    }

    public GameObject GetPlayer()
    {
        return player;
    }
}
