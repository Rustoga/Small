using UnityEngine;
using SmallGame.Input;
namespace SmallGame
{


    public class Screenshot : MonoBehaviour
    {
        #if UNITY_EDITOR
        InputHandler _input;
        void Start()
        {
            _input = InputHandler.Instance;
            _input.OnScreenShot.AddListener(TakeScreenShot);
        }

        void TakeScreenShot()
        {
            ScreenCapture.CaptureScreenshot("Screenshot.png");
            Debug.Log("Screenshot taken!");
        }
        #endif
    }
}

