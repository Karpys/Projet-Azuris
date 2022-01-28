using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private int GameScene;

    public void Play()
    {
        SceneManager.LoadScene(GameScene);
    }

    public void ChangeLangue(int langue)
    {
        LanguageSystem.selectedLanguage = langue;
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}
