using System;
using SmallGame.Input;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


namespace SmallGame
{
    class Directions
    {
        public readonly GameObject Up,Down,Left,Right;
        public Directions(GameObject up, GameObject down,GameObject left,GameObject right)
        {
            Up = up;
            Down = up;
            Left = up;
            Right = up;
        }
    }

    public class UIKeyHilight : MonoBehaviour
    {
        [SerializeField] float _inactiveAlpha = 0.75f;
        [SerializeField]float _activeGrow = 1.15f;
        InputHandler _input;
        float _deadZoneMin => InputSystem.settings.defaultDeadzoneMin;
        [SerializeField] GameObject _upArrow, _downArrow, _leftArrow, _rightArrow;
        GameObject[] directions;
        void Awake()
        {
            directions = new GameObject[] {_upArrow, _downArrow, _leftArrow, _rightArrow};
        }

        void Start()
        {
            _input = InputHandler.Instance;
            _input.OnMovement.AddListener(HilightDirection);

            Array.ForEach(directions, dir => TurnOff(dir));
    
        }

        void HilightDirection(Vector2 direction)
        {
            HighlightButton(direction.y > _deadZoneMin, _upArrow);
            HighlightButton(direction.y < -_deadZoneMin, _downArrow);
            HighlightButton(direction.x > _deadZoneMin, _rightArrow);
            HighlightButton(direction.x < -_deadZoneMin, _leftArrow);
        }

        void HighlightButton(bool selected, GameObject uiButton)
        {
            if (selected)
                TurnOn(uiButton);
            else
                TurnOff(uiButton);
        }
        void TurnOn(GameObject uiButton)
        {
            ChangeAlpha(uiButton, 1);
            ChangeScale(uiButton, _activeGrow);
        }
        void TurnOff(GameObject uiButton)
        {
            ChangeAlpha(uiButton, _inactiveAlpha);
            ChangeScale(uiButton, 1);
        }

        void ChangeAlpha(GameObject obj, float amount)
        {
            if (obj.TryGetComponent<Image>(out Image image))
            {
                var color = image.color;
                color.a = amount;
                image.color = color;
            }
        }

        void ChangeScale(GameObject obj, float amount)
        {
            if (obj.TryGetComponent<RectTransform>(out RectTransform rect))
            {
                rect.localScale = new Vector3(amount, amount, 1);
            }
        }
    }
}
