using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] Animator animator;
    bool isOpen;

    public void Toggle()
    {
        if (ShiftManager.Instance.isClockedIn == true) return;

        isOpen = !isOpen;
        animator.SetBool("isOpen", isOpen);
    }

    void Update()
    {
        if (ShiftManager.Instance.isClockedIn)
        {
            isOpen = true;
            animator.SetBool("isOpen", isOpen);
        }
    }
}
