using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour {

    public static GameSceneManager Instance;

    [SerializeField]
    private List<string> _sceneNames;

    [SerializeField]
    private int _startSceneIndex;

    private int _sceneIndex = -1;
    private int _previousLoadedSceneIndex = -1;

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
        LoadScene(_sceneIndex);
    }


    private void Update()
    {
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
        UnloadScene(_previousLoadedSceneIndex);
    }
    
    public void LoadScene(int index)
    {
        if(index < _sceneNames.Count && index >= 0)
        {
            if (_previousLoadedSceneIndex >= 0) _previousLoadedSceneIndex = _sceneIndex;
            _sceneIndex = index;
            SceneManager.LoadSceneAsync(_sceneNames[_sceneIndex], LoadSceneMode.Additive);

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

    public void LoadNextScene()
    {
        if(_sceneIndex + 1 < _sceneNames.Count)
        {
            _previousLoadedSceneIndex = _sceneIndex;
            _sceneIndex++;
            SceneManager.LoadSceneAsync(_sceneNames[_sceneIndex], LoadSceneMode.Additive);
        }

    }

    public void LoadPreviousScene()
    {
        if (_sceneIndex - 1 >= 0)
        {
            _previousLoadedSceneIndex = _sceneIndex;
            _sceneIndex--;
            SceneManager.LoadSceneAsync(_sceneNames[_sceneIndex], LoadSceneMode.Additive);
        }
    }


}
