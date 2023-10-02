using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;


public class BuildManager : GridPanelManager
{

     [SerializeField] private Material CanBuildMaterial;
     [SerializeField] private Material CanNotBuildMaterial;
     [SerializeField] private LayerMask BuildLayer;
     [SerializeField] private LayerMask BuildIgnoreLayer;
     [SerializeField] private LayerMask StructureLayer;
     [SerializeField] private float canBuildRange;
     [SerializeField] private float rotateSpeed;
     [SerializeField]private AudioClip buildSound;
     private BuildDataSO[] buildDatas;
     
     private InputAction _fire1Action;
     private InputAction _fire2Action;
     private InputAction _ScrollAction;
     public bool isBuildMode { get; private set; }
     private Camera _Camera;

     
     private void Awake()
     {
          buildDatas = Resources.LoadAll<BuildDataSO>("StructureData");
     }

     public BuildDataSO GetBuildData(GridPanelType type, int idx)
     {
          return buildDatas[idx];
     }

     public int GetBuildDataCount(GridPanelType type)
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
          GameObject buildObj = Instantiate(data.StructurePrefab);
          Collider buildObjCollider = buildObj.GetComponent<Collider>();
          MeshRenderer[] buildMeshRenderer = buildObj.GetComponentsInChildren<MeshRenderer>();//음영 바꾸기 위해서
          Material defaultMateral = buildMeshRenderer[0].material;
          TurretAIBase buildObjAIBase = buildObj.GetComponent<TurretAIBase>();

          while (isBuildMode)
          {
               Ray lay = _Camera.ScreenPointToRay(new Vector3(Screen.width * .5f, Screen.height * .5f));
               RaycastHit hit;
               
               if (Physics.Raycast(lay,out hit, canBuildRange, BuildLayer) && !((1<<hit.collider.gameObject.layer) == BuildIgnoreLayer) )
               {
                    buildObj.SetActive(true);
                    buildObj.transform.position = hit.point;
                    Collider[] otherStrCollider = Physics.OverlapBox(buildObjCollider.bounds.center, buildObjCollider.bounds.extents, Quaternion.identity,
                         StructureLayer);
                    if (otherStrCollider.Length > 0 || !CheckRightPlace(buildObj))
                    {
                         Array.ForEach(buildMeshRenderer,(x) => x.sharedMaterial = CanNotBuildMaterial);
                    }
                    else
                    {
                         Array.ForEach(buildMeshRenderer,(x) => x.sharedMaterial = CanBuildMaterial);
                         
                         if (_fire1Action.IsPressed())
                         {
                              Array.ForEach(buildMeshRenderer,(x) => x.sharedMaterial = defaultMateral);
                              
                              buildObj.layer = LayerMask.NameToLayer("Structure");
                              buildObj.SetActive(true);
                              
                              buildObjAIBase.RangeRenderer.gameObject.SetActive(false);
                              SoundManager.PlayClip(buildSound,buildObj.transform.position);
                              
                              buildObjAIBase.enabled = true;
                              OnOperated?.Invoke();
                              isBuildMode = false;
                         }

                         if (_fire2Action.IsPressed())
                         {
                              isBuildMode = false;
                              Destroy(buildObj);
                         }

                         if (_ScrollAction.triggered)
                         {
                              Vector2 scrollDirection = _ScrollAction.ReadValue<Vector2>();
                              Vector3 eulerAngle = buildObj.transform.eulerAngles;
                              if (scrollDirection.y > 0)
                                   eulerAngle.y += rotateSpeed*Time.deltaTime;
                              else
                                   eulerAngle.y -= rotateSpeed*Time.deltaTime;

                              buildObj.transform.eulerAngles = eulerAngle;
                         } 
                    }
               }
               else
               {
                    buildObj.SetActive(false);
               }
               
               yield return null;
          }
     }

     public override int GetElementsCount(GridPanelType type)
     {
          return GetBuildDataCount(type);
     }

     public override ScriptableObject GetData(GridPanelType type,int idx)
     {
          return GetBuildData(type,idx);
     }

     private bool CheckRightPlace(GameObject buildObj)
     {
          Collider col = buildObj.GetComponent<Collider>();
          Vector3 extends = col.bounds.extents;
          Ray[] rays = new Ray[4]
          {
               new Ray(buildObj.transform.position + new Vector3(extends.x,0,extends.z) + (Vector3.up * 0.01f), Vector3.down),
               new Ray(buildObj.transform.position + new Vector3(-extends.x,0,extends.z)+(Vector3.up * 0.01f), Vector3.down),
               new Ray(buildObj.transform.position + new Vector3(extends.x,0,-extends.z)+ (Vector3.up * 0.01f), Vector3.down),
               new Ray(buildObj.transform.position + new Vector3(-extends.x,0,-extends.z)+ (Vector3.up * 0.01f), Vector3.down)
          };

          for (int i = 0; i < rays.Length; ++i)
          {
               if (!Physics.Raycast(rays[i], 0.1f, BuildLayer))
               {
                    return false;
               }
          }

          return true;
     }
}
