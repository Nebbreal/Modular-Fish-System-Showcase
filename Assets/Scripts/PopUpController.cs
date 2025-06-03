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

    public void HandleHidingPopUp(float pressure)
    {
        popUpCooldownRemaining -= Time.deltaTime * 1000; // seconds to milliseconds
        if (pressure > pressureToHidePopUp && popUpCooldownRemaining < 0f)
        {
            gameObject.SetActive(false);
        }
    }
}
