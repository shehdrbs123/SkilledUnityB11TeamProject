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

# 2. 시연 영상
[🔗 **영상 보러가기**]()

---

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

# 5. 클래스 설명

- **플레이어**
    
    | 클래스 | 기능 |
    | -- | -- |


- **매니저 클래스**
    
    | 클래스 | 기능 |
    | -- | -- |
    | PrefabManager | 오브젝트 풀링
    | DayManager | 낮과 밤 구현
    | ConditionManager | 플레이어의 컨디션과 게임 종료 플래그 체크
    | MonsterSpawnManager | 구성된 몬스터 웨이브 정보대로 몬스터 소환
    | ItemManager | 아이템의 데이터 관리
    | InteractionManager | 오브젝트의 상호작용
    | EquipManager | 플레이어의 장비 장착 관리 및 공격 기능
    

- **UI**
    
    | 클래스 | 기능 |
    | -- | -- |
    | ResourceDisplayUI | 아이템 획득 UI 팝업 관리
    | ItemSlotUI | 아이템 슬롯에 들어있는 아이템 정보를 관리
    | EscBtnUI| Esc UI 활성화, 계속하기, 다시하기 버튼

- **인벤토리**
    
    | 클래스 | 기능 |
    | -- | -- |
    | Inventory |  인벤토리 On/Off, 인벤토리 아이템 추가, 아이템 사용, 버리기, 장착 기능


- **건설**
    
    | 클래스 | 기능 |
    | -- | -- |


- **포탑**
    
    | 클래스 | 기능 |
    | -- | -- |
    

- **제작**
    
    | 클래스 | 기능 |
    | -- | -- |


- **적/AI**
    
    | 클래스 | 기능 |
    | -- | -- |
    | MonsterDataSO | 몬스터 데이터를 스크립터블오브젝트로 저장해 재사용
    | Monster | 네이게이션 에이전트로 몬스터 AI 구성 및 관리
    

- **자원/지형**
    
    | 클래스 | 기능 |
    | -- | -- |
    | ResourceRandomSpawner | 정해진 자원을 무작위로 배치하는 기능


- **유틸리티**
    
    | 클래스 | 기능 |
    | -- | -- |
    

- **사운드**
    
    | 클래스 | 기능 |
    | -- | -- |


---

# 6. 후기

| 팀원명 | 후기 |
| -- | -- |
| 노동균 | 처음에 인원 수의 문제로 걱정이 들었습니다 그렇지만 끝으로 올 수록 든든하신 민상님, 성호님이 계서서 거의 완벽한 게임을 만든 것 같아 정말 좋았습니다. 저희 게임은 생각보다 할 일이 많고, 난이도도 어느정도 있어서 재미있습니다. 한번씩 해보셨으면 좋겠습니다. |
| 강성호 | 시간이 부족하여 애니메이션 동작이나 효과 같은 세심한 부분을 신경쓰지 못한 것이 아쉬웠다. 이번 프로젝트에서 아쉬웠던 점을 정리하여 다음 프로젝트에선 꼭 구현하고 싶다 |
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
