using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EquipManager : MonoBehaviour
{
	public Equip curEquip;
	public Transform equipParant;
	private PlayerMovement controller;
	
	private void Awake()
	{
		controller = GetComponent<PlayerMovement>();
	}

	private void Start()
	{
		GameManager.Instance._equipManager = this;
	}

	public void OnAttackInput(InputAction.CallbackContext context)
	{
		if(context.phase == InputActionPhase.Performed && curEquip != null && controller.isCanLook())
		{
			curEquip.OnAttackInput();
		}
	}
	public void EquipNew(ItemData item)
	{
		UnEquip();
		curEquip = Instantiate(item.equipPrefab, equipParant).GetComponent<Equip>();
	}

	public void UnEquip()
	{
		if(curEquip != null)
		{
			Destroy(curEquip.gameObject);
			curEquip = null;
		}
	}
}
