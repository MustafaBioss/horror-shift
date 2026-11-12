using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;

public class Customer : MonoBehaviour
{

    NavMeshAgent agent;
    Transform seat, orderArea;
    public bool orderTaken, foodServed, ateFood;

    [SerializeField] private Vector2 eatingTime = new Vector2(15f, 30f);

    [SerializeField] string[] menu = {"Donut", "Americano", "Cappucino", "Cake"};
    [SerializeField] List<string> order = new List<string>();

    bool seated;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(Routine());
    }

    IEnumerator Routine()
    {
        var diner = Manager.Instance;

        while ((orderArea = diner.TakeOrderArea()) == null)
            yield return new WaitForSeconds(1f);
        
        yield return GoTo(orderArea.position);

        yield return new WaitUntil(() => orderTaken);
        diner.ReleaseOrderArea(orderArea);
        orderArea = null;

        while ((seat = diner.TakeSeat()) == null) // waiting time till a seat is available
            yield return new WaitForSeconds(1f);
        

        yield return GoTo(seat.transform.position);
        seated = true;

        yield return new WaitUntil(() => foodServed);
        yield return new WaitForSeconds(Random.Range(eatingTime.x, eatingTime.y)); // eating time.
        ateFood = true;

        diner.ReleaseSeat(seat);
        seat = null;
        yield return GoTo(diner.exitPoint.position);
        Destroy(gameObject);
    }

    IEnumerator GoTo(Vector3 target)
    {
        agent.SetDestination(target);
        yield return null;
        while (agent.pathPending || agent.remainingDistance > agent.stoppingDistance + 0.1f)
        {
            yield return null;
        }
    }

    void OnDestroy()
    {
        if (Manager.Instance == null) return;
        if (seat != null) Manager.Instance.ReleaseSeat(seat);
        if (orderArea != null) Manager.Instance.ReleaseOrderArea(orderArea);
    }

    public void PlayerInteract()
    {
        if (!orderTaken) 
        {
            orderTaken = true;
        }
        else if (seated && !foodServed) 
        {
            foodServed = true;
        }
    }
    
    public void placeOrder()
    {
        order.Clear();

        int itemCount = Random.Range(1, 3);
        itemCount = Mathf.Min(itemCount, menu.Length);
        
        var available = new List<string>(menu);

        for (int i = 0; i < itemCount; i++)
        {
            int index = Random.Range(0, available.Count);
            order.Add(available[index]);
            available.RemoveAt(index);
        }
        
        Debug.Log($"Customer ordered {string.Join(", ", order)}");

        orderTaken = true;

        GetComponent<Interactable>().canInteract = false;
    }
}
