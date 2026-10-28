using UnityEngine;

public class Freezer : MonoBehaviour
{

    [SerializeField] Animator animator;
    bool isOpen = false;

    public void Toggle()
    {
        isOpen = !isOpen;
        animator.enabled = true;
        animator.SetBool("isOpen", isOpen);

    }
}
