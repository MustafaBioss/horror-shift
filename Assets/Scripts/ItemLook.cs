using UnityEngine;

public class ItemLook : MonoBehaviour
{
    [SerializeField] float range = 5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        
        if (Physics.Raycast(transform.position, transform.forward, out hit, range)) 
        {
            if (hit.collider.CompareTag("Item") || hit.collider.CompareTag("Task"))
            {
                Debug.DrawRay(transform.position, transform.forward * hit.distance, Color.green);
                Debug.Log("Looking at item/task: " + hit.collider.name);
            }
            else
                {
                    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * range, Color.blue);
                    Debug.Log("Looking at something else: " + hit.collider.name);
                }
        }
        else
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * range, Color.red);
                Debug.Log("Not looking at anything");
            }
    }
}
