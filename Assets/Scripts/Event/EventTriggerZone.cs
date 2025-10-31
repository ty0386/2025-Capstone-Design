using UnityEngine;
using UnityEngine.Events;
public class EventTriggerZone : MonoBehaviour
{
    public UnityEvent OnPlayerEnter; //인스펙터에서 연결할 이벤트를 노출

    private bool hasTriggered = false; //이벤트는 한번만 실행되도록 제어

    private void OnTriggerEnter2D(Collider2D other)
    {
        //들어온 오브젝트의 태그가 "Player"인지 확인
        //아직 이벤트가 발동된 적이 없는지확인
        if (other.CompareTag("Player") && !hasTriggered)
        {

            hasTriggered = true;


            OnPlayerEnter.Invoke();

            Debug.Log("이벤트 발동 확인용");
        }
    }
}

/*
Hierarchy 창에서 DialogueZone 오브젝트를 선택
Event Trigger Zone (Script) 컴포넌트 하단에 On Player Enter 슬롯 생성됨
리스트를 추가함으로서, UI 등의 오브젝트와 상호작용 가능, 관련 기능은 추가할 예정
No Function 드롭다운 메뉴를 클릭하고, 실행할 스크립트와 함수를 선택

추가로 확인용 로그는 잘 작동함, 필요 없으면 지워도 됨
*/
