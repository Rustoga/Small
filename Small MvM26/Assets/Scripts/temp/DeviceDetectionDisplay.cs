using System.Threading.Tasks;
using UnityEngine;

namespace SmallGame
{
    public class DetectedDeviceHelper : MonoBehaviour
    {
        [SerializeField] NotificationManager _notificationManager;

        async void Start() {
            await Task.Delay(2000); 
            _notificationManager.ShowNotification("Notification Test!");
        }
    }
}
