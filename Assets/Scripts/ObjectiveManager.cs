using UnityEngine;
using UnityEngine.UI;

public class ObjectiveManager : MonoBehaviour
{

    [SerializeField] private TMPro.TextMeshProUGUI objectiveText;
    public static string objective;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        objective = "Clock In At The Diner.";
    }

    // Update is called once per frame
    void Update()
    {
        objectiveText.text = objective;
    }
}
