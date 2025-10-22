using System;
using UnityEngine;
using UnityEngine.UI;

namespace DeepWolf.HungerThirstSystem.Examples
{
    public class GameRuntime : MonoBehaviour
    {
        [SerializeField] private Text _runtime;

        private void Update()
        {
            TimeSpan time = TimeSpan.FromSeconds(Time.timeSinceLevelLoad);
            _runtime.text = string.Format("{0:00}:{1:00}:{2:00}", time.Hours, time.Minutes, time.Seconds);
        }
    }
}