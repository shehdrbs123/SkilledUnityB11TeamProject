using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipTool : Equip
{
	public float useStamina;
	public float attackRate;
	private bool attacking;
	public float attackDistance;
	
	[Header("Resource Gathering")]
	public bool doesGatherResources;

	[Header("Combat")]
	public bool doesDealDamage;
	public int damage;

	private Animator animator;
	private Camera camera;

	protected virtual void Awake()
	{
		camera = Camera.main;
		animator = GetComponent<Animator>();
	}

	void OnCanAttack()
	{
		attacking = false;
	}

	public override void OnAttackInput()
	{
		if (!attacking)
		{
			attacking = true;
			animator.SetTrigger("Attack");
			Invoke("OnCanAttack", attackRate);
		}
	}

	public void OnHit()
	{
		
		Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
		RaycastHit hit;
		
		if (Physics.Raycast(ray, out hit, attackDistance))
		{
			if (doesGatherResources && hit.collider.TryGetComponent(out Resource resource) )
			{
				if(GameManager.Instance.ResourceDisplayUI != null)
				{
					resource.particle.Play();
					GameManager.Instance.ResourceDisplayUI.resourceTxt.text = resource.itemToGive.itemName;
					GameManager.Instance.ResourceDisplayUI.resourceDisplayImg.SetActive(true);
					GameManager.Instance.ResourceDisplayUI.animator.SetTrigger("OnCollect");	
				}
				resource.Gather();			
			}
		}
	}

}
