using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeepWolf.HungerThirstSystem.Examples
{
    [RequireComponent(typeof(CharacterController))]
    public class SimpleMovement : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed = 5.0f;

        /// <summary>
        /// The extra amount that is going to be added to the hunger increase amount, when moving.
        /// </summary>
        [Tooltip("The extra amount that is going to be added to the hunger increase amount.")]
        [SerializeField] private float _extraHungerIncreaseAmount;

        /// <summary>
        /// The extra amount that is going to be added to the thirst increase amount, when moving.
        /// </summary>
        [Tooltip("The extra amount that is going to be added to the thirst increase amount.")]
        [SerializeField] private float _extraThirstIncreaseAmount;

        private Vector3 _moveDirection;
        private CharacterController _characterController;
        private bool _isMoving;

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
        }

        private void Update()
        {
            _moveDirection.Set(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
            if (_moveDirection.sqrMagnitude > 0.1f)
            {
                _moveDirection = _moveDirection.normalized;
                if (!_isMoving)
                {
                    _isMoving = true;
                    OnMoveStarted();
                }
            }
            else
            {
                if (_isMoving)
                {
                    _isMoving = false;
                    OnMoveStopped();
                }
            }

            _moveDirection *= _moveSpeed;
            _moveDirection = transform.TransformDirection(_moveDirection);

            _characterController.SimpleMove(_moveDirection);
        }

        /// <summary>
        /// Triggered when start moving
        /// </summary>
        private void OnMoveStarted()
        {
            HungerThirst hungerThirst = GetComponent<HungerThirst>();
            // Add extra hunger increase amount
            hungerThirst.AddExtraHungerIncreaseAmount(_extraHungerIncreaseAmount);
            // Add extra thirst increase amount
            hungerThirst.AddExtraThirstIncreaseAmount(_extraThirstIncreaseAmount);
        }

        /// <summary>
        /// Triggered when stop moving
        /// </summary>
        private void OnMoveStopped()
        {
            HungerThirst hungerThirst = GetComponent<HungerThirst>();
            // Remove the extra hunger increase amount
            hungerThirst.ReduceExtraHungerIncreaseAmount(_extraHungerIncreaseAmount);
            // Remove the extra thirst increase amount
            hungerThirst.ReduceExtraThirstIncreaseAmount(_extraThirstIncreaseAmount);
        }
    }
}