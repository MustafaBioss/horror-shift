using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] Animator animator;
    bool isOpen;

    public void Toggle()
    {
        isOpen = !isOpen;
        animator.SetBool("isOpen", isOpen);
    }
}
