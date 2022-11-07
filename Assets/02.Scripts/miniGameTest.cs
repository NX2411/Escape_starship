using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class miniGameTest : MonoBehaviour
{


    public void buttonClick()
    {
        SceneManager.LoadScene("minigameTest", LoadSceneMode.Additive);
    }

    public void closeMinigame()
    {
        SceneManager.UnloadSceneAsync("minigameTest");
    }
}
