using UnityEngine;

namespace SmallGame
{

    public class FloatText : MonoBehaviour
    {
        [SerializeField] float _floatSpeed = 2f;
        [SerializeField] float _floatAmplitude = 5f;

        RectTransform _rectTransform;
        Vector3 _originalPosition;

        void Start()
        {
            _rectTransform = GetComponent<RectTransform>();
            _originalPosition = _rectTransform.anchoredPosition;
        }

        void Update()
        {
            float newY = _originalPosition.y + Mathf.Sin(Time.time * _floatSpeed) * _floatAmplitude;
            _rectTransform.anchoredPosition = new Vector3(_originalPosition.x, newY, _originalPosition.z);
        }
    }

}