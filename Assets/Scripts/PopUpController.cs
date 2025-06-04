using UnityEngine;

public class PopUpController : MonoBehaviour
{
    [SerializeField] private float pressureToHidePopUp = 5f;
    [SerializeField] private float popUpCooldown = 1000f;
    private float popUpCooldownRemaining;

    private void OnEnable()
    {
        popUpCooldownRemaining = popUpCooldown;
    }

    private void Update()
    {
        popUpCooldownRemaining -= Time.deltaTime * 1000; // seconds to milliseconds
    }

    public void HandleHidingPopUp(float pressure)
    {
        if (pressure > pressureToHidePopUp && popUpCooldownRemaining < 0f)
        {
            gameObject.SetActive(false);
        }
    }
}
