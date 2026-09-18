using UnityEngine;

namespace ModularFishSystem.Demo
{
    public class UIPanelAnimationController : MonoBehaviour
    {
        private static readonly int IsClosed = Animator.StringToHash("IsClosed");

        [SerializeField]
        private Animator animator;

        public void ToggleUIPanelState()
        {
            animator.SetBool(IsClosed, !animator.GetBool(IsClosed));
        }
    }
}
