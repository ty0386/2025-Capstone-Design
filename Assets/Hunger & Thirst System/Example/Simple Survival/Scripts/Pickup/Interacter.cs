using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DeepWolf.HungerThirstSystem.Examples
{
    public class Interacter : MonoBehaviour
    {
        [SerializeField] private float _maxPickupRange = 4.0f;
        [SerializeField] private LayerMask _pickableItemLayers;

        [Header("UI")]
        [SerializeField] private Text _pickupText;

        private void Update()
        {
            RaycastHit hitInfo;
            Ray ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width * 0.5f, Screen.height * 0.5f));
            if (Physics.Raycast(ray, out hitInfo, _maxPickupRange, _pickableItemLayers))
            {
                IPickupable pickableItem = hitInfo.collider.GetComponent<IPickupable>();

                // Show text that tells the user to press a button to pickup.
                _pickupText.gameObject.SetActive(true);
                _pickupText.text = string.Format("Press 'LMB' to pickup <color=#EB6721>{0}</color>", pickableItem.DisplayName);

                if (Input.GetMouseButtonDown(0))
                {
                    // Pickup item
                    pickableItem.Pickup(gameObject);
                }
            }
            else
            {
                _pickupText.gameObject.SetActive(false);
            }
        }
    }
}