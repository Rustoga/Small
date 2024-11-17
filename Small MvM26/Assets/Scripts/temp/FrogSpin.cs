using UnityEngine;

public class FrogSpin : MonoBehaviour
{
    [SerializeField, Range(-200 , 200)]
    float rotationSpeed = -100f;
    void Update()
    {

        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);

    }
}
