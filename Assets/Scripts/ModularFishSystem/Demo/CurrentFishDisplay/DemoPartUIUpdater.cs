using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace ModularFishSystem.Demo.CurrentFishDisplay
{
    public class DemoPartUIUpdater : MonoBehaviour
    {
        public static DemoPartUIUpdater Instance;
        [NonSerialized]
        public UnityEvent<string, string> OnPartUIUpdateEvent;
    
        public void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void OnEnable()
        {
            if (OnPartUIUpdateEvent == null)
            {
                OnPartUIUpdateEvent = new UnityEvent<string, string>();
            }
        
            OnPartUIUpdateEvent.AddListener(OnPartUIUpdate);
        }

        public void OnDisable()
        {
            OnPartUIUpdateEvent.RemoveListener(OnPartUIUpdate);
        }
    
        private void OnPartUIUpdate(string partTag, string partName)
        {
            if (partTag == "Base")
            {
                partName = partName.Replace("(Clone)", "");
            }
        
            GameObject uiObject = GameObject.FindWithTag(partTag).GetComponentInChildren<FishPartUIField>().gameObject;
            TextMeshProUGUI partText = uiObject.GetComponent<TextMeshProUGUI>();
            partText.text = partName;
        }
    }
}
