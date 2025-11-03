using UnityEngine;

public class Crosshair : MonoBehaviour
{
    public Camera mainCamera;
    public float followSpeed = 20f;

    private void Start()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;
    }

    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10f; // 카메라와의 거리 보정
        Vector3 worldPos = mainCamera.ScreenToWorldPoint(mousePos);
        worldPos.z = 0;

        // 자연스럽게 따라가도록 보간 (부드럽게 이동)
        transform.position = Vector3.Lerp(transform.position, worldPos, Time.deltaTime * followSpeed);
    }
}


