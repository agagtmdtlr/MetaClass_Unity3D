### PlayerController
- 유저의 입력에 대한 반응을 위한 클래스
- 반응이란 ? 애님 재생, 물체 이동 , 화면 전환 등...
- 애님재생과 게임 판전 연관성 

- 하위 요소의 제어만을 담당한다.
- ex) animator, transform , rigidbody , collider
- ex) 히트 박스(collider), 상태전환(animator), 효과음(audio)

### Player , LocalPlayer , RemotePlayer
- 유저 입력에 의해 발생한 이벤트 로직 수행.
- 환경 입력에 의해 발생한 이벤트 로직 
- 다른 유저에 의해 발생한 이벤트 로직