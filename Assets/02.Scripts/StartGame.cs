using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    public AudioClip audio;
    private AudioSource audioSource;

    private void Start()
    {
        //커서 잠구기
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        audioSource = GetComponent<AudioSource>();
    }

    public void StartPlaying()
    {
        audioSource.PlayOneShot(audio);
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void GoMain()
    {
        audioSource.PlayOneShot(audio);
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);

    }

    public void QuitGame()
    {
        audioSource.PlayOneShot(audio);
        Application.Quit();
    }
}
