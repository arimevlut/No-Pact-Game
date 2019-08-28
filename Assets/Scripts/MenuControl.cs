using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControl : MonoBehaviour
{

    public void Play_Game()
    {
        SceneManager.LoadScene("CharacterSelect");
    }

    public void Play_Alice()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void Play_Bob()
    {
        SceneManager.LoadScene("SampleSceneBob");
    }
}
