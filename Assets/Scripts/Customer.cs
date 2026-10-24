using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class Customer : MonoBehaviour
{

    NavMeshAgent agent;
    Seat seat;
    public bool orderTaken, foodServed, ateFood;

    [SerializeField] private Vector2 eatingTime = new Vector2(15f, 30f);

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
        Debug.Log("Customer spawned and going to order area.");

        yield return GoTo(diner.orderArea.position);
        Debug.Log("Customer reached order area and waiting for order to be taken.");

        yield return new WaitUntil(() => orderTaken);
        Debug.Log("Customer's order has been taken.");

        while ((seat = diner.TakeSeat()) == null) // waiting time till a seat is available
        {
            yield return new WaitForSeconds(1f);
        }
        Debug.Log("Customer found a seat and going to it.");

        yield return GoTo(seat.transform.position);
        seated = true;
        Debug.Log("Customer seated and waiting for food to be served.");

        yield return new WaitUntil(() => foodServed);
        Debug.Log("Customer's food has been served and eating.");

        yield return new WaitForSeconds(Random.Range(eatingTime.x, eatingTime.y)); // eating time.
        ateFood = true;
        Debug.Log("Customer Ate");

        seat.occupied = false;
        yield return GoTo(diner.exitPoint.position);
        Destroy(gameObject);
        Debug.Log("Customer Left And Destroyed");
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
}
