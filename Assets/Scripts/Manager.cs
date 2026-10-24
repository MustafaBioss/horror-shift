using UnityEngine;
using System.Collections;

public class Manager : MonoBehaviour
{

    public static Manager Instance;

    public Transform spawnPoint, orderArea, exitPoint;
    public Seat[] seats;
    [SerializeField] private GameObject NPCPrefab;
    [SerializeField] private Vector2 spawnDelay = new Vector2(10f, 25f);

    void Awake()
    {
        Instance = this;
    }

    public void StartSpawning()
    {
        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            Debug.Log("Waiting to spawn customer.");
            yield return new WaitForSeconds(Random.Range(spawnDelay.x, spawnDelay.y));
            Debug.Log("Spawning customer.");
            Instantiate(NPCPrefab, spawnPoint.position, spawnPoint.rotation);
        }
    }

    public Seat TakeSeat()
    {
        foreach (var s in seats)
        {
            if (!s.occupied)
            {
                s.occupied = true;
                return s;
            }
        }

        return null;
    }
}
