using UnityEngine;

namespace DeepWolf.HungerThirstSystem.Examples
{
    public class SimpleFPCamera : MonoBehaviour
    {
        [SerializeField] private float _sensivity = 2.0f;
        [SerializeField] private bool _invert;
        private Camera _camera;
        Vector2 _mouseRotation;

        private void Start()
        {
            _camera = GetComponentInChildren<Camera>();
        }

        // Update is called once per frame
        private void Update()
        {
            _mouseRotation.Set(Input.GetAxis("Mouse X"),
                _invert ? Input.GetAxis("Mouse Y") : -Input.GetAxis("Mouse Y"));
            _mouseRotation *= _sensivity;

            transform.Rotate(0, _mouseRotation.x, 0);
            _camera.transform.Rotate(_mouseRotation.y, 0, 0);
        }
    }
}