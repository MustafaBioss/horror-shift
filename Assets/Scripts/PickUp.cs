using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField] Interactable interactable;
    [SerializeField] GameObject item;

    public void PickItem()
    {
        interactable.gameObject.SetActive(false);
        item.SetActive(true);
    }
}
