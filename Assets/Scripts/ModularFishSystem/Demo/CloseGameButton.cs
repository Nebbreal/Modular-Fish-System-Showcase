using System;
using UnityEngine;

namespace ModularFishSystem.Demo
{
    public class CloseGameButton : MonoBehaviour
    {
        public void Start()
        {
            #if UNITY_WEBGL
                gameObject.SetActive(false);
            #endif
        }

        public void OnCloseGame()
        {
            Application.Quit();
        }
    }
}