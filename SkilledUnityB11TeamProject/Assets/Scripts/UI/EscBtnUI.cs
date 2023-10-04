using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class EscBtnUI : BaseUI
{
    [SerializeField] private GameObject escWindow;
	public UnityEvent onOpenEsc;
	public UnityEvent onCloseEsc;

	public void OnEscButton(InputAction.CallbackContext callbackContext)
	{
		if (callbackContext.phase == InputActionPhase.Started)
		{
			Toggle();
		}
	}
	public void Toggle()
	{
		if (escWindow.activeInHierarchy)
		{
			Time.timeScale = 1f;
			GameManager.Instance._uiManager.RemoveUICount(gameObject);
			escWindow.SetActive(false);
			onOpenEsc?.Invoke();
		}
		else
		{
			Time.timeScale = 0f;
			GameManager.Instance._uiManager.AddUICount(gameObject);
			escWindow.SetActive(true);
			onCloseEsc?.Invoke();
		}
	}

	public void ContinueBtn()
	{
		Toggle();
	}

	public void RetryBtn()
	{
		SceneManager.LoadScene("MainScene");
	}
}
