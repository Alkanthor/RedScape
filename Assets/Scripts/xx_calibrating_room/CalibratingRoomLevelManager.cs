using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CalibratingRoomLevelManager : MonoBehaviour {

    enum MenuMode { Nothing, NewGame, ContinueGame, ExitGame }

    public static CalibratingRoomLevelManager Instance;

    [SerializeField]
    private GameObject Teleport;

    private MenuMode _menuMode = MenuMode.Nothing;

    private void Start()
    {
        Teleport.SetActive(false);
    }

    public void PrepareTeleportNewGame()
    {
        Teleport.SetActive(true);
        _menuMode = MenuMode.NewGame;
    }

    public void PrepareTeleportContinueGame()
    {
        Teleport.SetActive(false);
        _menuMode = MenuMode.ContinueGame;
    }

    public void TeleportPlayer()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    private void OnValidate()
    {
        if (Instance != null)
            GameObject.Destroy(Instance);
        else
            Instance = this;

        //DontDestroyOnLoad(this);
    }
}
