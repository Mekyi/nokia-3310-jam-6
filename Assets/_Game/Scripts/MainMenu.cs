using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private bool _banksLoaded = false;
    
    private void Start()
    {
        StartCoroutine(WaitForBanksToLoad());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            AudioManager.Instance.StopInstance();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    private IEnumerator WaitForBanksToLoad()
    {
        while (_banksLoaded == false)
        {
            if (FMODUnity.RuntimeManager.HaveAllBanksLoaded)
            {
                _banksLoaded = true;
            }

            yield return new WaitForSeconds(0.1f);
        }
        
        AudioManager.Instance.PlayOneShot(FMODEvents.Instance.MainMenu, transform.position);
    }
}
