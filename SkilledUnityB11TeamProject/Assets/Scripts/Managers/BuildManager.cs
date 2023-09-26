using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class BuildManager : MonoBehaviour
{
     private BuildDataSO[] buildDatas;

     [SerializeField] private LayerMask groundLayer;
     [SerializeField] private LayerMask StructureLayer;
     [SerializeField] private float range;

     [SerializeField] private float rotateSpeed;
     //빌드모드 플레이
     private InputAction _fire1Action;
     private InputAction _fire2Action;
     private InputAction _ScrollAction;
     private bool isBuildMode = false;
     private Camera _Camera;
     private void Awake()
     {
          buildDatas = Resources.LoadAll<BuildDataSO>("StructureData");
     }

     private void Start()
     {
          
     }


     public BuildDataSO GetBuildData(int idx)
     {
          return buildDatas[idx];
     }

     public int GetBuildDataCount()
     {
          return buildDatas.Length;
     }

     public void SetBuildMode(BuildDataSO data)
     {
          if (_fire1Action == null)
          {
               GameObject player = GameManager.Instance.GetPlayer();
               PlayerInput input = player.GetComponent<PlayerInput>();
               _fire1Action = input.actions.FindAction("Fire1");
               _fire2Action = input.actions.FindAction("Fire2");
               _ScrollAction = input.actions.FindAction("Scroll");
               _Camera = Camera.main;
          }
          isBuildMode = true; 
          
          StartCoroutine(OperateBuild(data));
     }

     private IEnumerator OperateBuild(BuildDataSO data)
     {
          GameObject obj = Instantiate(data.StructurePrefab);
          while (isBuildMode)
          {
               Ray lay = _Camera.ScreenPointToRay(new Vector3(Screen.width * .5f, Screen.height * .5f));
               RaycastHit hit;
               if (Physics.Raycast(lay,out hit, range, groundLayer))
               {
                    Debug.Log("hit");
                    obj.SetActive(true);
                    obj.transform.position = hit.point;
                    
                    if (_fire1Action.IsPressed())
                    {
                         isBuildMode = false;
                    }

                    if (_fire2Action.IsPressed())
                    {
                         isBuildMode = false;
                         Destroy(obj);
                    }

                    if (_ScrollAction.triggered)
                    {
                         Vector2 scrollDirection = _ScrollAction.ReadValue<Vector2>();
                         Vector3 eulerAngle = obj.transform.eulerAngles;
                         if (scrollDirection.y > 0)
                              eulerAngle.y += rotateSpeed*Time.deltaTime;
                         else
                              eulerAngle.y -= rotateSpeed*Time.deltaTime;

                         obj.transform.eulerAngles = eulerAngle;
                    }
               }
               else
               {
                    obj.SetActive(false);
               }
               
               yield return null;
          }
     }
}
