using NUnit.Framework.Internal;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField]  GameObject nextStateGO;
    [SerializeField]  GameObject Menu;
    [SerializeField]  GameObject Settings;
    public void PlayButton()
    {
        nextStateGO.SetActive(true);
        gameObject.SetActive(false);
    }
    public void GoToSettings()
    {
        Settings.SetActive(true);
        Menu.SetActive(false);
    }
    public void BackToMenu()
    {
        Menu.SetActive(true);
        Settings.SetActive(false);
    }
    public void doExitGame() {
    Application.Quit();
    }


}
