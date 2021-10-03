using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButton : MonoBehaviour
{
    public void ClickButton()
    {
        SceneManager.LoadScene(0);
    }
}
