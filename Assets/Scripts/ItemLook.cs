using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class ItemLook : MonoBehaviour
{
    [SerializeField] float range = 5f;
    [SerializeField] Image pointer;
    [SerializeField] Color defaultColor = Color.white; 
    [SerializeField] Color highlightColor = Color.green;

    Interactable current;

    InputAction interact;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        interact = InputSystem.actions.FindAction("Player/Interact");
    }

    // Update is called once per frame
    void Update()
    {
        current = null;

        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, range) && hit.collider.TryGetComponent(out Interactable interactable)) 
        {
            current = interactable;
            pointer.color = highlightColor;
            Debug.DrawRay(transform.position, transform.forward * range, Color.green);
            Debug.Log(hit.collider);
        }
        else
        {
            pointer.color = defaultColor;
            Debug.DrawRay(transform.position, transform.forward * range, Color.white);
            Debug.Log(hit.collider);
        }

        if (current != null && interact.WasPressedThisFrame())
            current.Interact();
    }
}
