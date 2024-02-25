using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        AudioManager.Instance.PlayOneShot(FMODEvents.Instance.MainMenu, transform.position);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            AudioManager.Instance.StopInstance();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
