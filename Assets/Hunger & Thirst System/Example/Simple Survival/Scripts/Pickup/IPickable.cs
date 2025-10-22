using UnityEngine;

namespace DeepWolf.HungerThirstSystem.Examples
{
    public interface IPickupable
    {
        /// <summary>
        /// Gets the display name.
        /// </summary>
        string DisplayName { get; }

        /// <summary>
        /// What happens when picking up the item.
        /// </summary>
        /// <param name="picker">The gameobject that picked up the item.</param>
        void Pickup(GameObject picker);
    }
}