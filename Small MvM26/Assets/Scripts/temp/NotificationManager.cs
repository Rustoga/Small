using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace SmallGame
{
    public class NotificationManager : MonoBehaviour
    {
        [SerializeField] TMP_Text _notificationText;
        [SerializeField] float _displayDuration = 5.0f;
        [SerializeField] float _fadeDuration = 1.0f;

        void Start()
        {
            _notificationText.gameObject.SetActive(false);
        }

        public async void ShowNotification(string msg)
        {
            await ShowAndFadeNotification(msg);
        }

        private async Task ShowAndFadeNotification(string msg)
        {
            if (_notificationText != null)
            {

                _notificationText.text = msg;
                _notificationText.gameObject.SetActive(true);
                _notificationText.canvasRenderer.SetAlpha(1.0f);

                await Task.Delay((int)(_displayDuration * 1000));

                float elapsedTime = 0.0f;
                while (elapsedTime < _fadeDuration && _notificationText != null)
                {
                    elapsedTime += Time.deltaTime;
                    float alpha = Mathf.Lerp(1.0f, 0f, elapsedTime / _fadeDuration);
                    _notificationText.canvasRenderer.SetAlpha(alpha);
                    await Task.Yield();
                }
                
                if(_notificationText != null)
                    _notificationText.gameObject.SetActive(false);
            }
        }
    }
}
