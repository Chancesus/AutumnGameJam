using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    [SerializeField] AudioClip menuMusic;
    [SerializeField] AudioClip levelMusic;

    public static LevelManager instance;

    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
        {
            Destroy(instance);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
       AudioManager.Instance.PlayMusic(menuMusic);
    }


    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        AudioManager.Instance.PlayMusic(levelMusic);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
