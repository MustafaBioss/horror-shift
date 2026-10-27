using UnityEngine;
using System.Collections;

public class LightFlicker : MonoBehaviour
{

    [SerializeField] Light light;
    [SerializeField] bool flicker = true;

    // Update is called once per frame
    void OnEnable()
    {
        StartCoroutine(Flicker());
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            flicker = false;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            flicker = true;
    }

    IEnumerator Flicker()
    {
        while (true)
        {
            while (!flicker)
            {
                light.enabled = true;
                yield return null;
            }
            light.enabled = false;
            yield return new WaitForSeconds(Random.Range(0.1f, 0.5f));
            light.enabled = true;
            yield return new WaitForSeconds(Random.Range(0.1f, 1.5f));
        }
    }
}
