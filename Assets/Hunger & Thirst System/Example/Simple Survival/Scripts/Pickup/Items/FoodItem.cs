using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeepWolf.HungerThirstSystem.Examples
{
    public class FoodItem : MonoBehaviour, IPickupable
    {
        /// <summary>
        /// The display name.
        /// </summary>
        [SerializeField] private string _displayName;

        /// <summary>
        /// How much to reduce hunger with.
        /// </summary>
        [Tooltip("How much to reduce hunger with.")]
        [SerializeField] private float _reduceAmount;

        /// <summary>
        /// Gets the display name.
        /// </summary>
        public string DisplayName
        {
            get { return _displayName; }
        }

        /// <summary>
        /// Reduces hunger when picked up
        /// </summary>
        /// <param name="picker">The gameobject that picked up the item.</param>
        public void Pickup(GameObject picker)
        {
            HungerThirst hungerThirst = picker.GetComponent<HungerThirst>();
            if (hungerThirst != null && hungerThirst.UseHunger)
            {
                hungerThirst.ReduceHunger(_reduceAmount);
                Destroy(gameObject);
            }
        }
    }
}