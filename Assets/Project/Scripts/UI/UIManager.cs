using UnityEngine;
using UnityEngine.SceneManagement;

namespace Project.Scripts.UI
{
    public class UIManager : MonoBehaviour
    {
        public void BackToMenu()
        {
            SceneManager.LoadScene(Constants.MenuSceneName);
        }

        public void PlayAgain()
        {
            SceneManager.LoadScene(Constants.GameSceneName);
        }
    }
}