using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class BaseUI : MonoBehaviour
{
    [SerializeField] protected bool isIgnoreInput;
    
    protected static UIManager _uiManager;
     
    protected virtual void Awake()
    {
        _uiManager = GameManager.Instance._uiManager;
    }

    protected void OnEnable()
    {
        if(_uiManager && isIgnoreInput)
            _uiManager.EnablePanel(gameObject);
    }

    protected void OnDisable()
    {
        if(_uiManager && isIgnoreInput)
            _uiManager.DisablePanel(gameObject);
    }
}
