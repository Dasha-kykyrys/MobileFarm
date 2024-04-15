using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mainmenu : MonoBehaviour
{
    public void Scenes(int numberScenes)
    {
        SceneManager.LoadScene(numberScenes);
    }

    public void Clear()
    {
        DataBase.ClearDataBase();
    }

    public void Exit()
    {
        Application.Quit();
    }
}
