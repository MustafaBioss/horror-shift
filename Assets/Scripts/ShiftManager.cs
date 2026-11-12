using UnityEngine;

public class ShiftManager : MonoBehaviour
{

    public static ShiftManager Instance;
    public bool isClockedIn = false;
    [SerializeField] GameObject exclamationMark;

    void Awake()
    {
        Instance = this;
    }

    public void Clock()
    {
        if (isClockedIn == false)
        {
            isClockedIn = true;
            Manager.Instance.StartSpawning();
            ObjectiveManager.objective = "Serve The Customers.";
            Destroy(exclamationMark);
            GetComponent<Interactable>().canInteract = false;
        }
    }
}