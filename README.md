# 3D-idle-game

## 📖 목차

1. [프로젝트 소개](#프로젝트-소개)
3. [게임구조](#게임구조)
4. [주요기능](#주요기능)
5. [개발기간](#개발기간)
6. [기술스택](#기술스택)
7. [Trouble Shooting](#trouble-shooting)

---
    
## 프로젝트 소개

- 방치형게임의 토대 제작

---

## 주요기능

- 기능 1. 기본 UI 구현
![FullShot](https://github.com/user-attachments/assets/3daaa791-e8ff-4289-a71f-7a9b467d2bb8)
HP, MP, 경험치 바 골드등의 정보를 표시합니다.

<br>

- 기능 2. 플레이어 AI 시스템
![monsterAI](https://github.com/user-attachments/assets/0fb0ab83-1253-4166-a6b4-c4ba8a1c17f2)
  - 다음 스테이지로 건너가는 위치 혹은 주변에 가까운 적에게 다가갑니다.
  - NavMesh를 이용합니다.
  - 상태패턴을 이용하여 Idle, AutoMove, Attack상태를 가지고 이동합니다.
<br>

- 기능 3. 게임 내 통화 시스템
- ![Magnetic](https://github.com/user-attachments/assets/d28b4d6e-1bf5-4e2f-a401-62a2b05b80bf)
  - 몬스터를 죽이면 골드가 들어옵니다.
  - 골드와 아이템은 플레이어가 가까이 다가가면 빨려옵니다.

<br>

- 기능 4. 아이템 및 장비 창 UI 구현
![Inventory](https://github.com/user-attachments/assets/fecdd136-73a5-4348-941d-2d068e56e6f4)
  - 인벤토리에서 아이템을 선택하여 효과를 받습니다.
  - 아이템 장착 시 EquipManager에서 무기의 데이터를 변경합니다.
  - 전략패턴을 사용하여 확장성을 고려합니다.
  - 몬스터와 골드, 아이템은 오브젝트 풀에서 관리합니다.


- 기능 5. ScriptableObject를 이용한 데이터 관리
  - 포션, 무기, 플레이어의 기본데이터, 적들의 기본데이터 그리고 스테이지에 관한 SO를 만들어 일를 참조합니다.
<br>

---

## 개발기간

- 2024.11.8(금) ~ 2024.11.14(목)   

---

## 기술스택

- 유니티 2022.3.17f LTS   
- Microsoft Visual Studio 2022   
- GitHub   

---

## Trouble Shooting

<details>
  <summary>애니메이션 중에 무기 콜라이더를 켰다가 끌 때</summary>
    <div markdown="1">
      <ul>
        <li>공격 애니메이션의 Normalize시간을 이용하여 애니메이션 시작할때 콜라이더를 키고 끝날 때 콜라이더를 끄고자 합니다.</li>
        <li>콜라이더가 제대로 켜지거나 꺼지지 못하는 이슈가 발생</li>
        <li>애니메이션에 직접 이벤트를 달고자 했으나 Read-Only 애니메이션이였고, Inspector창에서 추가하고자 했지만 해당 게임오브젝트가 아니라며 오류가 발생</li>
        <li>콤보시스템을 없에고 플래그를 세워 이슈를 해결</li>
      </ul>
    </div>
</details>

<details>
  <summary>상태 전환중의 오류</summary>
    <div markdown="1">
      <ul>
        <li>공격상태가 끝나고 다시 공격상태가 되면서 플래그를 초기화해주고자 하였음</li>
        <li>Exit함수가 제대로 발생하지 않았고 현재 상태와 애니메이션이 일치하지 않게되는 상황이 되어버림</li>
        <li>공격상태로 가지않고 Chasing상태로 변경하였으나 똑같은 이슈가 발생하여 상태들을 정리해 3개만 남겨두게 되었음</li>
      </ul>
    </div>
</details>

<details>
  <summary>애니메이션과 상태패턴을 조정하느라 못한 것들</summary>
    <div markdown="1">
      <ul>
        <li>공격애니메이션과 콜라이더, 상태패턴의과 애니메이션의 동기화때문에 시간을 너무 많이 소모해버린게 가장 큰 아쉬움. 그래도 전략패턴과 이벤트 버스를 사용하고자 시도함.</li>
        <li>스테이지 매니저를 만들어서 스테이지 번호를 입력받으면 해당 스테이지의 stageSO를 꺼내 스테이지를 시작</li>
        <li>이벤트 버스를 만들어놔서 파티클시스템, 사운드이펙트등을 이벤트버스를 이용해 사운드매니저에서 통괄적으로 재생</li>
        <li>Json을 이용해 저장/로드</li>
        <li>타임라인을 이용하여 시네머신의 돌리트랙과 함께 컷신을 만들고, 특정 스킬을 사용하여 버추얼 카메라의 6D Shake를 이용해 화면을 흔드는 효과를 사용할 수 있음</li>
      </ul>
    </div>
</details>
