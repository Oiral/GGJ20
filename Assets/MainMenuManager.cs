using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    float timeElapsed;


    private void Update()
    {
        if (timeElapsed > 0.5f)
        {

            if (Input.GetButtonDown("Interact0") || Input.GetButtonDown("Interact1"))
            {
                //Load the scene
                SceneManager.LoadScene(1);
                return;
            }
        }
        else
        {
            timeElapsed += Time.deltaTime;
        }
    }
}
