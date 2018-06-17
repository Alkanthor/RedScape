using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour {

    public static GameSceneManager Instance;

    [SerializeField]
    private List<string> _sceneNames;

    [SerializeField]
    public int _startSceneIndex;

    private int _sceneIndex = -1;
    private int _previousLoadedSceneIndex = -1;
    private bool _loadingScene = false;

    private bool _shouldLoadLevel = false;


    private bool _adjustPlayerPosition = false;

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

    private void Start()
    {
        
        SceneManager.sceneLoaded += OnSceneLoaded;
        _sceneIndex = _startSceneIndex;
        MainGameManager.Instance.SceneManagerInitialized = true;
    }

    private void Update()
    {

        //for debug 
        if(Input.GetKeyDown(KeyCode.B))
        {
            LoadPreviousScene();
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            LoadNextScene();
        }
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        SceneManager.SetActiveScene(arg0);
        UnloadScene(_previousLoadedSceneIndex);
        _loadingScene = false;
        if(_shouldLoadLevel)
        {
            //TODO: load level 
            _shouldLoadLevel = false;
        }
        if(_adjustPlayerPosition)
        {
            var initPoint = GameObject.FindGameObjectWithTag(UnityStrings.TAG_INIT_POINT);
            if(initPoint == null)
            {
                Debug.Log("Init point could not be found...player position not adjusted");
            }
            else
            {
                PlayerManager.Instance.AdjustPlayerPosition(initPoint.transform.position);
            }
            _adjustPlayerPosition = false;
        }
        Debug.Log("Scene " + _sceneNames[_sceneIndex] + " loaded succesfully.");
    }


    public void InitScene(bool adjustPlayerPosition = false)
    {
        LoadScene(_startSceneIndex, adjustPlayerPosition, false);
    }
    public void LoadScene(int index, bool adjustPlayerPosition = false, bool loading = false)
    {
        if(!_loadingScene)
        {
            if (index < _sceneNames.Count && index >= 0)
            {

                _adjustPlayerPosition = adjustPlayerPosition;
                _loadingScene = true;
                _previousLoadedSceneIndex = _sceneIndex;
                _sceneIndex = index;
                _shouldLoadLevel = loading;
                SceneManager.LoadSceneAsync(_sceneNames[_sceneIndex], LoadSceneMode.Additive);

            }
        }

    }

    public void UnloadScene(int index)
    {
        //is within range of it is not the same scene undload (at start we dont have previous scene)
        if (index < _sceneNames.Count && index >= 0 && _sceneIndex != _previousLoadedSceneIndex)
        {
            SceneManager.UnloadSceneAsync(_sceneNames[_previousLoadedSceneIndex]);
        }
    }

    public void LoadNextScene(bool adjustPlayerPosition = false, bool loading = false)
    {
        LoadScene(_sceneIndex + 1, loading);

    }

    public void LoadPreviousScene(bool adjustPlayerPosition = false, bool loading = false)
    {
        LoadScene(_sceneIndex - 1, loading);
    }


    public void SaveScene()
    {
        PlayerPrefs.SetInt(UnityStrings.PREF_LOAD_GAME, _sceneIndex);
        //TODO: save level
    }

}
