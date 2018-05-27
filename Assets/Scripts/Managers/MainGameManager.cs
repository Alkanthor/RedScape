using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameManager : MonoBehaviour {

    public static MainGameManager Instance;

    public bool IsSaveAllowed;
    public bool AdjustPlayerPositionAtStart;


    private bool _playerManagerInitialized;
    public bool PlayerManagerInitialized
    {
        get { return _playerManagerInitialized; }
        set
        {
            _playerManagerInitialized = value;
            TryStartGame();
        }
    }

    private bool _sceneManagerInitialized;
    public bool SceneManagerInitialized
    {
        get { return _sceneManagerInitialized; }
        set
        {
            _sceneManagerInitialized = value;
            TryStartGame();
        }
    }

    public bool AllManagersInitialized
    {
        get { return SceneManagerInitialized && PlayerManagerInitialized; }
    }
    private void TryStartGame()
    {
        if(AllManagersInitialized)
        {
            StartGame();
        }

    }

    private void StartGame()
    {
        var loadGameLevel = PlayerPrefs.GetInt(UnityStrings.PREF_LOAD_GAME, -1);
        if(loadGameLevel < 0)
        {
            InitGame(AdjustPlayerPositionAtStart);
        }
        else
        {
            LoadGame(loadGameLevel, AdjustPlayerPositionAtStart);
        }
    }

    private void InitGame(bool adjustPlayerPosition = false)
    {
        GameSceneManager.Instance.InitScene(adjustPlayerPosition);
    }
    private void LoadGame(int level, bool adjustPlayerPosition = false)
    {
        GameSceneManager.Instance.LoadScene(level, adjustPlayerPosition);
    }

    private void SaveGame()
    {

    }
    void Awake()
    {
        //Check if instance already exists
        if (Instance == null)
        {
            //if not, set instance to this
            Instance = this;
            DontDestroyOnLoad(this);
        }
        //If instance already exists and it's not this:
        else if (Instance != this)
        {
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);
        }

    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnApplicationQuit()
    {
        if(IsSaveAllowed)
        {
            SaveGame();
        }
    }
}
