using UnityEngine;

public class ShiftManager : MonoBehaviour
{

    public static ShiftManager Instance;
    public bool isClockedIn = false;

    void Awake()
    {
        Instance = this;
    }

    public void Clock()
    {
        isClockedIn = true;
        Manager.Instance.StartSpawning();
        Debug.Log("Player clocked in and started spawning.");
    }
}
