using UnityEngine;
using System.Collections;

public class LightFlicker : MonoBehaviour
{

    [SerializeField] Light flickerLight;
    [SerializeField] bool flicker = true;
    [SerializeField] Light[] allLights;
    [SerializeField] GameObject flashlight;

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

    void Update()
    {
        if (Manager.Instance.customersServed > 1)
        {
            flashlight.GetComponent<Interactable>().canInteract = true;
            flicker = false;
            foreach (Light light in allLights)
                light.enabled = false;
        }
    }

    IEnumerator Flicker()
    {
        while (true)
        {
            while (!flicker)
                yield return null;
            
            flickerLight.enabled = false;
            yield return new WaitForSeconds(Random.Range(0.1f, 0.5f));
            flickerLight.enabled = true;
            yield return new WaitForSeconds(Random.Range(0.1f, 1.5f));
        }
    }
}
