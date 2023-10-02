using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    [SerializeField]private AudioClip OpenUISound;
    [SerializeField]private AudioClip CloseUISound;
    private Dictionary<string, GameObject> _uiPrefabs;
    private Dictionary<string, GameObject> _uiInstances;
    private HashSet<GameObject> _uiCounter;

    private List<InputAction> _inputs;
    private void Awake()
    {
        _uiCounter = new HashSet<GameObject>();
        _uiInstances = new Dictionary<string, GameObject>();
        _uiPrefabs = new Dictionary<string, GameObject>();
        
        var a = Resources.LoadAll<GameObject>(Path.Combine("UI"));
        
        foreach (var value in a)
        {
            _uiPrefabs.Add(value.name,value);
        }
    }

    private void Start()
    {
        CheckInputAction();
    }

    public GameObject GetUI(string name)
    {
        GameObject ui;
        if (!_uiInstances.TryGetValue(name, out ui))
        {
            if (_uiPrefabs.TryGetValue(name,out GameObject obj))
            {
                ui = Instantiate(obj);
                _uiInstances.Add(name,ui);
            }
            else
            {
                Debug.Log($"{name}은 uiManager에 등록되지 않은 프리팹입니다");
            }
        }

        return ui;
    }

    public GameObject GetUIClone(string name)
    {
        GameObject ui=null;
        if (_uiPrefabs.TryGetValue(name,out GameObject obj))
            ui = Instantiate(obj);
        else
            Debug.Log($"{name}은 uiManager에 등록되지 않은 프리팹입니다");

        return ui;
    }

    public void EnablePanel(GameObject o)
    {
        AddUICount(o);
    }
    
    public void AddUICount(GameObject o)
    {
        if (_uiCounter == null)
            _uiCounter = new HashSet<GameObject>();
        _uiCounter.Add(o);
        SoundManager.PlayClip(OpenUISound,GameManager.Instance.GetPlayer().transform.position);
        CheckInputAction();
    }

    public void DisablePanel(GameObject o)
    {
        RemoveUICount(o);
    }
    
    public void RemoveUICount(GameObject o)
    {
        _uiCounter.Remove(o);
        SoundManager.PlayClip(CloseUISound,GameManager.Instance.GetPlayer().transform.position);
        CheckInputAction();
    }

    private void CheckInputAction()
    {
        if (_inputs == null)
        {
            _inputs = new List<InputAction>(10);
        }
        GameObject player = GameManager.Instance.GetPlayer();
        if (player && _inputs.Count<=0)
        {
            PlayerInput playerInput = player.GetComponent<PlayerInput>();
            _inputs.Add(playerInput.actions.FindAction("Move"));
            _inputs.Add(playerInput.actions.FindAction("Look"));
            _inputs.Add(playerInput.actions.FindAction("Fire1"));
            _inputs.Add(playerInput.actions.FindAction("Fire2"));
            _inputs.Add(playerInput.actions.FindAction("Interact"));
        }

        IgnoreInput(_uiCounter.Count>0);
    }

    private void IgnoreInput(bool ignore)
    {
        if (ignore)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            foreach (var input in _inputs)
                input.Disable();
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            foreach (var input in _inputs)
                input.Enable();
        }
    }
    
    public IEnumerator LerpAdjustRect(RectTransform rect, float scaleX,float scaleY, float changingTime,Action callback=null)
    {
        float currentTime = 0;
        Vector3 targetScale = new Vector3(scaleX, scaleY,0f);
        float deltaScale = Time.deltaTime / changingTime;
        
        while (Mathf.Abs(rect.localScale.x - targetScale.x)> 0.01f)
        {
            rect.localScale = Vector2.Lerp(rect.localScale, targetScale, deltaScale);
            yield return null;
        }
        rect.localScale = targetScale;
        callback?.Invoke();
    }

    public static void PopupText(string text)
    {
        UIManager uiManager = GameManager.Instance._uiManager;
        GameObject popupTextPanel = uiManager.GetUI("MindTextUI");
        CanvasGroup popupTextCanG = popupTextPanel.GetComponent<CanvasGroup>(); 
        TMP_Text popupText = popupTextPanel.transform.GetChild(0).GetComponent<TMP_Text>();
        popupTextCanG.alpha = 1;
        popupText.text = text;
        uiManager.StartCoroutine(uiManager.LerpAdjustAlpha(popupTextCanG, 0));
    }

    public IEnumerator LerpAdjustAlpha(CanvasGroup group, float toAlpha)
    {
        while (Mathf.Abs(group.alpha - toAlpha) > 0.01f)
        {
            group.alpha = Mathf.Lerp(group.alpha, toAlpha, 0.01f);
            yield return null;
        }

        group.alpha = toAlpha;
    }
}
