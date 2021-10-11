using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseTab;
    [SerializeField] private BeatManager bm;

    private void Start()
    {
        bm = GameObject.Find("BeatManager").GetComponent<BeatManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }
    
    public void PauseGame()
    {
        pauseTab.SetActive(!pauseTab.activeSelf);
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;

        if (bm.song.isPlaying)
        {
            bm.song.Pause();
        }
        else
        {
            bm.song.UnPause();
        }
    }
    
    public void QuitGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
}
