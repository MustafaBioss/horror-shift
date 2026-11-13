using UnityEngine;

public class ExclamationSpin : MonoBehaviour
{

    [SerializeField] float spinSpeed = 90f;
    [SerializeField] float bounceHeight = 0.1f;
    [SerializeField] float bounceSpeed = 2f;

    Vector3 startPos;

    void Start()
    {
        startPos = transform.localPosition;
    }

    void Update()
    {
        transform.Rotate(0f, spinSpeed * Time.deltaTime, 0f, Space.World);

        float offset = Mathf.Sin(Time.time * bounceSpeed) * bounceHeight;
        transform.localPosition = startPos + new Vector3(0f, offset, 0f);
    }
}
