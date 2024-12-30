# Angry Bird 모작 프로젝트
+ "멋쟁이사자처럼 유니티 게임 개발 3기"의 Angry Bird 제작 프로젝트입니다.
+ SlingShot에서 새를 당겨 Stage에 있는 모든 적들을 제거하면 승리합니다.
+ 새의 종류에는 3 종류가 있습니다.
   1. 일반 : SKill이 없는 일반적인 새
   2. Booster : 날라가는 도중에 "Space"키를 누르면 해당 위치에서 가속합니다.
   3. Bomb : 물체와 충돌 시 일정 시간 후에 폭발 범위 안의 물체들에 피해를 줍니다.
+ 게임을 처음 실행 시켰을 때, Local에 User id 정보만을 가지는 UserData.json 파일을 생성합니다.
+ UserData.json에서 읽은 id로 firebase의 DB와 통신해서 플레이어의 각 스테이지의 플레이 기록을 불러옵니다.

# Pre-View
![Angry+Bird+2024-12-30+02-56-23+(1)+(1) (1)](https://github.com/user-attachments/assets/fa93f7de-4a08-4c60-b3e8-57eb0770948e)


# 개발 기간
1. 24/12/22 ~ 24/12/30
2. SlingShot, Drag, 일반 새 구현
3. 비행 궤적 시각화작업, 애니메이션 작업
4. 파괴 가능한 오브젝트와 적 오브젝트 구현
5. Booster 새 구현
6. Bomb 새 구현
7. Title / In-Game UI 구현
8. SoundManager 및 각 Effect Sound 작업
9. DataManager 구현 및 firebase 연동

# 개발자
1. 박종한 (개인 프로젝트)
