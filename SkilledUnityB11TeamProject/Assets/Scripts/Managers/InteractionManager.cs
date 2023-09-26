using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public interface IInteractable //ItemObjec에 구현돼있음
{
	string GetInteractPrompt();
	void OnInteract();
}
public class InteractionManager : MonoBehaviour
{
    public float checkRate = 0.05f;
    private float lastCheckTime;
    public float maxCheckDistance;
    public LayerMask layerMask;

    private GameObject curInteractGameObject;
    private IInteractable curInteractable;

    public TextMeshProUGUI promptText;
    private Camera camera;
    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
    }

	// Update is called once per frame
	void Update()
	{
		// 실시간으로 현재 바라보고 있는 정보를 curInteractable에 저장
		if (Time.time - lastCheckTime > checkRate)
		{
			lastCheckTime = Time.time;

			Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
			RaycastHit hit;

			if (Physics.Raycast(ray, out hit, maxCheckDistance, layerMask))
			{
				if (hit.collider.gameObject != curInteractGameObject)
				{
					curInteractGameObject = hit.collider.gameObject;
					curInteractable = hit.collider.GetComponent<IInteractable>();
				}
			}
			else
			{
				curInteractGameObject = null;
				curInteractable = null;
			}
		}
	}

	private void SetPromptText()
	{
		promptText.gameObject.SetActive(true);
		promptText.text = string.Format("<b>[E]</b> {0}", curInteractable.GetInteractPrompt());
	}

	public void OnInteractInput(InputAction.CallbackContext callbackContext)
	{
		//현재 아이템들은 모두 ItemObject를 가지고 있다. ItemObject는 IInteractable를 상속받고 있다.
		//그래서 바라보고 있는 게임오브젝트의 ItemObjec가 IInteractable을 상속받아 만든 함수를
		//이곳에서 실행하고 있다.
		if (callbackContext.phase == InputActionPhase.Started && curInteractable != null)
		{
			curInteractable.OnInteract();
			curInteractGameObject = null;
			curInteractable = null;
			promptText.gameObject.SetActive(false);
		}
	}
}


