using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Manager : MonoBehaviour
{

    public static Manager Instance;

    public Transform spawnPoint, exitPoint;
    public Transform[] orderAreas, seats;
    [SerializeField] private GameObject NPCPrefab;
    public bool spawn = true;
    [SerializeField] private Vector2 spawnDelay = new Vector2(10f, 25f);

    Queue<Transform> freeSeats, freeOrderAreas;

    void Awake()
    {
        Instance = this;
        freeSeats = new Queue<Transform>(seats);
        freeOrderAreas = new Queue<Transform>(orderAreas);
    }

    public void StartSpawning()
    {
        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        while (spawn)
        {
            yield return new WaitForSeconds(Random.Range(spawnDelay.x, spawnDelay.y));
            
            if (freeOrderAreas.Count > 0)
            {
                Instantiate(NPCPrefab, spawnPoint.position, spawnPoint.rotation);
            }
        }
    }

    public Transform TakeSeat() => freeSeats.Count > 0 ? freeSeats.Dequeue() : null;
    public void ReleaseSeat(Transform seat) => freeSeats.Enqueue(seat);

    public Transform TakeOrderArea() => freeOrderAreas.Count > 0 ? freeOrderAreas.Dequeue() : null;
    public void ReleaseOrderArea(Transform area) => freeOrderAreas.Enqueue(area);
}
