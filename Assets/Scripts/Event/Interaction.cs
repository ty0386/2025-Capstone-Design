using UnityEngine;
using UnityEngine.Events;

public class Interaction : MonoBehaviour
{
    public UnityEvent OnInteract;
    private bool isPlayerInRange = false;
    public GameObject interactionPromptUI;


    void Start()
    {
        if (interactionPromptUI != null)
        {
            interactionPromptUI.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            Debug.Log("플레이어 범위 진입");
            //안내 UI 숨기기
            if (interactionPromptUI != null)
            {
                interactionPromptUI.SetActive(true);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            Debug.Log("플레이어 범위 이탈");

            //안내 UI 숨기기
            if (interactionPromptUI != null)
            {
                interactionPromptUI.SetActive(false);
            }
        }
    }

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log("Z키 인식 대화 시작.");

            OnInteract.Invoke();
        }
    }
}
