using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourceDisplayUI : MonoBehaviour
{
	public Animator animator;
	public TextMeshProUGUI resourceTxt;
	public GameObject resourceDisplayImg;
	private void Awake()
	{
		animator = GetComponent<Animator>();	
		GameManager.Instance.ResourceDisplayUI = this;
	}
}
