using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SmallGame
{
    public class SceneTransitionManager : MonoBehaviour
    {
        public void StartGame()
        {
            SceneManager.LoadScene(1);
        }
    }
}
