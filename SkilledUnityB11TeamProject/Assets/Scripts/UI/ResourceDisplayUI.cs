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

	private readonly int animOnCollect = Animator.StringToHash("OnCollect");

	private void Awake()
	{
		animator = GetComponent<Animator>();	
		GameManager.Instance.ResourceDisplayUI = this;
	}

	//public void ShowGetResource(ItemData item, string anim)
 //   {
	//	resourceTxt.text = item.itemName;
	//	resourceDisplayImg.SetActive(true);
	//	animator.Play(anim, -1, 0f);
	//}

	public void ShowGetResource(ItemData item)
	{
		resourceTxt.text = item.itemName;
		resourceDisplayImg.SetActive(true);
		animator.SetTrigger(animOnCollect);
	}
}
