using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SmallGame
{
    public class SceneTransitionManager : MonoBehaviour
    {
        [SerializeField] SceneAsset testWorld;
        public void StartGame()
        {
            SceneManager.LoadScene(testWorld.name);
        }
    }
}
