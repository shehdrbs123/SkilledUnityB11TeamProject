# ReadMe

### 목차

1. 게임 개요 및 개발 기간
2. 시연 영상
3. 기능 명세서
4. 기획 설계
5. 클래스 설명
6. 개발 후기
7. 사용 에셋

---
<br>
<br>

# 1. 게임 개요 및 개발 기간

- **게임명** : `Safe Home`
- **설명** : [내일 배움 캠프 8기 Unity] Chapter 3-2 게임개발 숙련 팀 프로젝트
- **장르** : 3D 생존 타워 디펜스 게임
- **개요** : 외딴 행성에서 몬스터로부터 집을 지키며 생존하자 !
- **조작법:**
    - 이동: [WASD]
    - 인벤토리 : [TAB]
    - 점프 : [SPACE]
    - 곡괭이 : [1] / 망치: [2]
    - 자원 채취 및 건물 제작 : [Left Click]
    - 건물 제작 탭 열기 : [Right Click]
- **개발 환경** : Unity 2022.3.2f1
- **타겟 플랫폼** : PC
- **개발 기간** `2023.09.23 ~ 2023.10.03`

---


<br>
<br>

# 2. 시연 영상
[🔗 **영상 보러가기**]()

---

<br>
<br>

# 3. 기능 명세서

진행도 | 기능명 | 기능 소개 | 담당자 |
--|--|--|--|
★★★ | 플레이어 | 게임 내를 돌아다니며 자원을 채취하고 포탑을 건설하는 주체 | 노동균
★★★★★★★★ | 매니저 클래스 | 게임 내의 정보와 플레이를 관리하고 조정해주는 모듈 | 노동균, 김민상, 강성호
★★★★★ | UI | 플레이어의 기본 컨디션 정보, 미니맵 등 Main HUD에 관련된 기능 | 노동균, 김민상, 강성호
★★★★ | 인벤토리 | 플레이어의 인벤토리로써 필요한 아이템과 관련된 기능 | 강성호
★★★★★★ | 건설 | 건설을 하기 위한 시스템 | 노동균
★★★★★★ | 포탑 | 타워 디펜스 기물인 포탑 개발 | 노동균
★★★★ | 적/AI | 적을 배치, 특정 위치로 이동하는 AI와 밸런싱 | 김민상
★★★★★★ | 자원/지형 | 자원, 맵 디자인에 관련된 기능 | 김민상, 강성호
★★ | 유틸리티 | 게임 개발에 추가적인 도움이 되는 기능 개발 | 노동균
★★★★★★ | 사운드 | 게임 내 필요한 사운드 작업 | 노동균
★★★★ | 씬 | 게임의 주요 씬 제작 | 김민상, 강성호

[🔗 기능 명세서 세부 내용](https://www.notion.so/19976f6180574e709cb0f75d4f5c333d)


---

<br>
<br>

# 4. 기획 설계

- **장비**
    | 아이템 | 기능 |
    | --- | --- |
    | 곡괭이 | 자원을 채취할 수 있는 도구 |
    | 망치 | 포탑을 제작할 수 있는 도구 |

- **음식**
    | 아이템 | 기능 | 미네랄 | 오일락 | 물 | 우주결정 |
    | -- | -- | -- | -- | -- | -- |
    | 깨끗한 물 | 목마름+20 | 0 | 0 | 2 | 1 |
    | 주스 | 배고픔+5, 목마름+15 | 5 | 0 | 2 | 3 |
    | 만능 포션 | 목마름+100 | 16 | 8 | 3 | 5 |
    | 술 | 멘탈+20 | 50 | 20 | 10 | 10 |
    | 고기 | 배고픔+10 | 2 | 1 |0 | 0 |
    | 블루베리 파이 | 배고픔+20 | 4 | 1 | 0 | 2 |
    | 치즈 정식 | 배고픔+35 | 6 | 2 | 0 | 3 |
    | 포도 | 배고픔+25, 목마름+10 | 7 | 4 | 0 | 4 |

- **포탑**
    | 포탑 | 공격력 | 인식 범위 | 공격속도 | 미네랄 | 오일락 | 우주결정 | |
    | -- | -- | -- | -- | -- | -- | -- | -- |
    | 기본 포탑 | 1 | 10 | 0.4 | 3 | 1 | 0 |
    | 강화 포탑 | 4 | 20 | 2 | 4 | 1 | 2 |
    | 미사일 | 15 | 30 | 4 | 7 | 3 | 10 | 3(Splash), 5, 10 |
    | 캐터필터 | 10 | 15 | 12 | 10 | 5 | 20 | 1/1s, 5, 20 |
    | 곡사포 | 200 | 50 | 15 | 50 | 10 | 200 | 200(splash), 20, 200 |

- **몬스터**
    | 컬러 | 체력 | 이동속도 | 드롭 우주결정 |
    | -- | -- | -- | -- |
    | 그린 | 5 | 5 | 1 |
    | 그레이 | 10 | 5 | 2 |
    | 카키 | 5 | 10 | 2 |
    | 브라운 | 20 | 10 | 3 |
    | 퍼플 | 30 | 10 | 5 |
    | 레드 | 20 | 15 | 5 |
    | 블랙 | 50 | 15 | 15 |
    | 크리스탈 | 150 | 7 | 30 |
    | ??? | 300 | 5 | 100 |

- **몬스터 웨이브**
    | 웨이브 | 등장 몬스터 | 총 마리수 |
    | -- | -- | -- |
    | 1 | 그린 - 20, 그레이 - 5, 카키 - 5 | 30 |
    | 2 | 그린 - 20, 그레이 - 10, 카키 - 10, 브라운 - 10 | 50 |
    | 3 | 브라운 - 15, 퍼플 - 10, 레드 - 10, 블랙 - 5 | 50 |
    | 4 | 브라운 - 20, 퍼플 - 20, 레드 - 20, 블랙 - 5, 크리스탈 - 5 | 70 |
    | 5 | 브라운 - 20, 퍼플 - 30, 레드 - 30, 블랙 - 10, 크리스탈 - 10 | 100 |

- **채집 자원**
    | 자원 이름 | 획득 방법 |
    | -- | -- |
    | 미네랄 | 미네랄 광석 채취 |
    | 오일락 | 오일락 광석 채취 |
    | 물 | 호숫가에서 채취 |
    | 우주 결정 | 몬스터 처치 |

---
<br>
<br>

# 5. 클래스 설명

- **플레이어**
    
    | 클래스 | 기능 |
    | -- | -- |
    |[PlayerMovement](SkilledUnityB11TeamProject/Assets/Scripts/Components/Player/PlayerMovement.cs)|플레이어의 움직임에 관련된 기능|
    |[InputController](SkilledUnityB11TeamProject/Assets/Scripts/Components/Player/InputController.cs)|외부 입력을 직접적으로 받아 연결된 event에 전달하는 모듈|
    |[Equip](SkilledUnityB11TeamProject/Assets/Scripts/Components/Player/Equip.cs)|장착되는 무기의 베이스가 추상 클래스
    |[EquipTool](SkilledUnityB11TeamProject/Assets/Scripts/Components/Player/EquipTool.cs)|장착되는 무기에서 기본적으로 추가되는 클래스, 곡괭이에서 사용|
    |BuildHammer|장착되는 무기 중 건설해머에 추가되는 클래스, 건설 UI를 띄워주는 역할|

- **매니저 클래스**
    
    | 클래스 | 기능 |
    | -- | -- |
    | [PrefabManager](SkilledUnityB11TeamProject/Assets/Scripts/Managers/PrefabManager.cs) | 오브젝트 풀링
    | [DayManager](SkilledUnityB11TeamProject/Assets/scripts/Managers/DayManager.cs) | 낮과 밤 구현
    | [ConditionManager](SkilledUnityB11TeamProject/Assets/Scripts/Managers/ConditionManager.cs) | 플레이어의 컨디션과 게임 종료 플래그 체크
    | [MonsterSpawnManager](SkilledUnityB11TeamProject/Assets/Scripts/Managers/MonsterSpawnManager.cs) | 구성된 몬스터 웨이브 정보대로 몬스터 소환
    | [ItemManager](SkilledUnityB11TeamProject/Assets/Scripts/Managers/ItemManager.cs) | 아이템의 데이터 관리
    | [InteractionManager](SkilledUnityB11TeamProject/Assets/Scripts/Managers/InteractionManager.cs) | 오브젝트의 상호작용
    | [EquipManager](SkilledUnityB11TeamProject/Assets/Scripts/Managers/EquipManager.cs) | 플레이어의 장비 장착 관리 및 공격 기능
    

- **UI**
    
    | 클래스 | 기능 |
    | -- | -- |
    | [ResourceDisplayUI](SkilledUnityB11TeamProject/Assets/scripts/UI/ResourceDisplayUI.cs) | 아이템 획득 UI 팝업 관리
    | [ItemSlotUI](SkilledUnityB11TeamProject/Assets/Scripts/UI/ItemSlotUI.cs) | 아이템 슬롯에 들어있는 아이템 정보를 관리
    | [EscBtnUI](SkilledUnityB11TeamProject/Assets/Scripts/UI/EscBtnUI.cs)| Esc UI 활성화, 계속하기, 다시하기 버튼

- **아이템**
    
    | 클래스 | 기능 |
    | -- | -- |
    | [Inventory](SkilledUnityB11TeamProject/Assets/Scripts/UI/Inventory.cs) |  인벤토리 On/Off, 인벤토리 아이템 추가, 아이템 사용, 버리기, 장착 기능
    | [ItemData](SkilledUnityB11TeamProject/Assets/Scriptables/Scripts/ItemData.cs) | 아이템마다 정보를 부여해주는 기능
    | [ItemObject](SkilledUnityB11TeamProject/Assets/Scripts/Components/Item/ItemObject.cs) | 아이템 오브젝트의 상호작용 정보를 전달해주는 기능

- **포탑**
    
    | 클래스 | 기능 |
    | -- | -- |
    |[TurretAIBase](SkilledUnityB11TeamProject/Assets/Scripts/Components/Turrets/TurretAIBase.cs)|모든 터렛이 기본적으로 가져야할 내용을 담은 추상 클래스|
    |[TurretAI](SkilledUnityB11TeamProject/Assets/Scripts/Components/Turrets/TurretsAI.cs)|기본 포탑(총을 쏘는 포탑)을 구현한 포탑 AI|
    |[TurretThrowerAI](SkilledUnityB11TeamProject/Assets/Scripts/Components/Turrets/TurretThrowerAI.cs)|폭탄들을 발사하는 포탑들을 구현한 AI|

- **포탑 폭탄**
-   | 클래스 | 기능 |
    | -- | -- |
    |[Bullets](SkilledUnityB11TeamProject/Assets/Scripts/Components/Bullets/Bullets.cs)|Thrower에 힘을 받아 날아가는 물리적 폭탄|
    |[DamageRange](SkilledUnityB11TeamProject/Assets/Scripts/Components/Bullets/DamageRange.cs)|폭탄이 터진 후 데미지를 입히는 영역을 만드는 컴포넌트|
    |[FireBomb](SkilledUnityB11TeamProject/Assets/Scripts/Components/Bullets/FireBomb.cs)|Bullet과 같으나, 터졌을 때 DamageRange를 생성하는 폭탄|
    |[Follower](SkilledUnityB11TeamProject/Assets/Scripts/Components/Bullets/Follower.cs)|폭탄에 반유도 기능을 추가해주는 컴포넌트|
    

- **제작/건설**
- 
    | 클래스 | 기능 |
    | -- | -- |
    |[CraftManager](SkilledUnityB11TeamProject/Assets/Scripts/Managers/CraftManager.cs)|Craft를 위한 레시피 데이터를 가져오고 공유해주는 모듈|
    |[GridPanelUI](SkilledUnityB11TeamProject/Assets/Scripts/UI/GridPanelUI.cs)|모든 제작/건설 패널에서 사용되는 UI의 기능을 모은 컴포넌트|
    |[GridButtonUI](SkilledUnityB11TeamProject/Assets/Scripts/UI/GridButtonUI.cs)|제작/건설패널에서 제작할 것들을 선택할 수 있는 버튼|
    |[GridScriptableObject](SkilledUnityB11TeamProject/Assets/Scriptables/Scripts/GridScriptableObject.cs)|제작/건설 패널에서 기본적으로 사용하는 이미지, 리소스의 수 등을 보관하는 ScriptableObject|


- **건설**
    
    | 클래스 | 기능 |
    | -- | -- |
    |[BuildManager](SkilledUnityB11TeamProject/Assets/Scripts/Managers/BuildManager.cs)|건설 레시피를 가져오고, 건설모드시 PlayerInput을 받아 건설 동작을 할 수 있도록하는 매니져|
    |[BuildTargetButtonUI](SkilledUnityB11TeamProject/Assets/Scripts/UI/BuildTargetButtonUI.cs)|GridButtonUI를 상속받아 건설에 쓰이는 ScriptableObject를 보관,표시,동작하는 스크립트 |
    |[BuildDataSO](SkilledUnityB11TeamProject/Assets/Scriptables/Scripts/BuildDataSO.cs)|GridScriptableObject를 상속받고, 건설 시 완성될 오브젝트를 포함하는 Scriptable Object|

- **제작**
    
    | 클래스 | 기능 |
    | -- | -- |
    |[CraftManager](SkilledUnityB11TeamProject/Assets/Scripts/Managers/CraftManager.cs)|Craft를 위한 레시피 데이터를 가져오고 공유해주는 모듈|
    |[CraftButtonUI](SkilledUnityB11TeamProject/Assets/Scripts/UI/CraftButtonUI.cs)|GridButtonUI를 상속받고, 제작에 쓰이는 ScriptableObject를 보관,표시,동작하는 스크립트|
    |[CraftDataSO](SkilledUnityB11TeamProject/Assets/Scriptables/Scripts/CraftDataSO.cs)|GridScriptableObject를 상속받고, 제작 시 완성될 Item을 포함하는 Scriptable Object|
    


- **적/AI**
    
    | 클래스 | 기능 |
    | -- | -- |
    | [MonsterDataSO](SkilledUnityB11TeamProject/Assets/Scriptables/Scripts/MonsterDataSO.cs) | 몬스터 데이터를 스크립터블오브젝트로 저장해 재사용
    | [Monster](SkilledUnityB11TeamProject/Assets/Scripts/Components/Monster.cs) | 네이게이션 에이전트로 몬스터 AI 구성 및 관리
    

- **자원/지형**
    
    | 클래스 | 기능 |
    | -- | -- |
    | [ResourceRandomSpawner](SkilledUnityB11TeamProject/Assets/Scripts/Components/ResourceRandomSpawner.cs) | 정해진 자원을 무작위로 배치하는 기능
    | [Resource](SkilledUnityB11TeamProject/Assets/Scripts/Components/Item/Resource.cs) | 자원의 정보전달과 채취효과 기능


- **유틸리티**
    
    | 클래스 | 기능 |
    | -- | -- |
    |[TestScript](SkilledUnityB11TeamProject/Assets/Scripts/Test/TestScript.cs)|아이템 추가, 밤낮변경, 일자 변경 등 테스트를 위한 기능이 포함된 모듈|
    |[TextureMaker](SkilledUnityB11TeamProject/Assets/Scripts/TextureMaker/Editor/TextureMakeES.cs)|별도의 씬 내에서 256x256크기에 맞춰 보이는 모습대로 카메라를 찍고, PNG 파일로 변환해주는 모듈|

- **사운드**
    
    | 클래스 | 기능 |
    | -- | -- |
    |[SoundManager](SkilledUnityB11TeamProject/Assets/Scripts/Managers/SoundManager.cs)|AudioClip을 전달받아 보유중인 AudioSource들로 효과음, 배경음을 재생해주는 모듈|

---

# 6. 후기

| 팀원명 | 후기 |
| -- | -- |
| 노동균 | 처음에 인원 수의 문제로 걱정이 들었습니다 그렇지만 끝으로 올 수록 든든하신 민상님, 성호님이 계서서 거의 완벽한 게임을 만든 것 같아 정말 좋았습니다. 저희 게임은 생각보다 할 일이 많고, 난이도도 어느정도 있어서 재미있습니다. 한번씩 해보셨으면 좋겠습니다. |
| 강성호 | 시간이 부족하여 애니메이션 동작이나 효과 같은 세심한 부분을 신경쓰지 못한 것이 아쉬웠습니다. 이번 프로젝트에서 아쉬웠던 점을 정리하여 다음 프로젝트에선 꼭 구현하고 싶습니다. 그리고 이 게임은 저희 팀의 많은 노력이 담겨있기 때문에 꼭 엔딩을 봐주셨으면 좋겠습니다! |
| 김민상 | 3명이서 팀 프로젝트를 시작하게 되었지만, 모두가 2인분이상 해주셔서 좋은 결과 나왔다고 생각합니다. 고생하신 팀원 분들께 감사드립니다. 저희 게임 재밌으니까 모두 한 번씩 해주세요. |


# 7. 사용 에셋
- [집 모델](https://assetstore.unity.com/packages/3d/environments/sci-fi/sci-fi-styled-modular-pack-82913)
- [주변 환경](https://assetstore.unity.com/packages/3d/environments/sci-fi/free-demo-of-low-poly-space-alien-worlds-3d-asset-pack-258683)
- [스카이 박스](https://assetstore.unity.com/packages/2d/textures-materials/sky/customizable-skybox-174576)
- [거미 몬스터](https://assetstore.unity.com/packages/3d/characters/creatures/meshtint-free-polygonal-metalon-151383)
- [인간 몬스터](https://assetstore.unity.com/publishers/40676)
- [터렛 1](https://assetstore.unity.com/packages/3d/props/weapons/fatty-poly-turret-part-2-free-159989)
- [터렛 2](https://assetstore.unity.com/packages/3d/props/weapons/fatty-poly-turret-free-155251#content)
- [도구 1](https://assetstore.unity.com/packages/3d/props/tools/free-tools-kit-155875)
- [도구 2](https://assetstore.unity.com/packages/3d/props/tools/sci-fi-tool-pickaxe-32216)
- [특수 효과](https://assetstore.unity.com/packages/vfx/particles/fire-explosions/low-poly-fire-244190)
- [사운드 : 총소리](https://assetstore.unity.com/packages/audio/sound-fx/sci-fi-guns-sfx-pack-181144)
- [사운드 : 효과음](https://assetstore.unity.com/packages/audio/sound-fx/sci-fi-sfx-package-184029)
- [사운드 : 도구](https://assetstore.unity.com/packages/audio/sound-fx/weapons/bow-and-hammer-sound-effects-163948)
- [사운드 : 발소리](https://assetstore.unity.com/packages/audio/sound-fx/foley/footsteps-essentials-189879)
- [사운드 : BGM](https://assetstore.unity.com/packages/audio/music/space-threat-free-action-music-205935)
- [UI : Sci-Fi](https://assetstore.unity.com/packages/2d/gui/sci-fi-gui-skin-15606)
- [UI : 아이콘](https://game-icons.net/1x1/delapouite/house.html#download)
