using UnityEngine;
using UnityEngine.UI;

public class FloatText : MonoBehaviour
{
    [SerializeField] float floatSpeed = 2f;
    [SerializeField] float floatAmplitude = 5f;

    private RectTransform rectTransform;
    private Vector3 originalPosition;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        originalPosition = rectTransform.anchoredPosition;
    }

    void Update()
    {
        // Calculate the new Y position with a sin wave for floating effect
        float newY = originalPosition.y + Mathf.Sin(Time.time * floatSpeed) * floatAmplitude;
        rectTransform.anchoredPosition = new Vector3(originalPosition.x, newY, originalPosition.z);
    }
}
