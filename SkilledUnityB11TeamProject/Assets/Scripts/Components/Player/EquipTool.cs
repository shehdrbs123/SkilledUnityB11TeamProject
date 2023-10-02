using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipTool : Equip
{
	public float useStamina;
	public float attackRate;
	public float attackDistance;
	
	[Header("Resource Gathering")]
	public bool doesGatherResources;

	[Header("Combat")]
	public bool doesDealDamage;
	public int damage;

	[SerializeField]private AudioClip[] swingSound;
	[SerializeField]private AudioClip[] HitSound;
	
	private Animator animator;
	private Camera _cam;
	private bool attacking;
	private bool isHit;

	private readonly int AnimAttack = Animator.StringToHash("Attack");

	protected virtual void Awake()
	{
		_cam = Camera.main;
		animator = GetComponent<Animator>();
	}

	public override void OnAttackInput()
	{
		if (!attacking)
		{
			attacking = true;
			animator.SetTrigger(AnimAttack);
			Invoke(nameof(AttackDelay), attackRate);
		}
	}

	private void AttackDelay()
	{
		attacking = false;
	}

	public void OnHit()
	{
		Ray ray = _cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

		isHit = Physics.Raycast(ray, out RaycastHit hit, attackDistance);
		if (isHit)
		{
			if (doesGatherResources && hit.collider.TryGetComponent(out Resource resource))
			{
				if(GameManager.Instance.ResourceDisplayUI != null)
				{
					resource.particle.Play();
				}
				resource.Gather();			
			}
		}
	}

	public void PlaySound()
	{
		if (isHit)
		{
			SoundManager.PlayRandomClip(HitSound,transform.position);
		}
		else
		{
			SoundManager.PlayRandomClip(swingSound,transform.position);
		}
	}
}
