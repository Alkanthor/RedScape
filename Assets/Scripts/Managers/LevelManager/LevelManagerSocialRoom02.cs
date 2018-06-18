using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManagerSocialRoom02 : MonoBehaviour {

    public static LevelManagerSocialRoom02 Instance;


    [SerializeField]
    public UnityEvents.UnityEventString SendGeneratedCode;

    public int _cipherKey;

    private int[] _originalCode;
    public string _originalCodeString;

    private int[] _cipheredCode;
    public string _cipheredCodeString;

    public int CodeLength = 5;

    void Awake()
    {
        //Check if instance already exists
        if (Instance == null)
        {
            //if not, set instance to this
            Instance = this;
        }
        //If instance already exists and it's not this:
        else if (Instance != this)
        {
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);
        }

        if (SendGeneratedCode == null)
        {
            SendGeneratedCode = new UnityEvents.UnityEventString();
        }
    }

    // Use this for initialization
    void Start () {
        InitCode();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void InitCode()
    {
        _cipherKey = Random.Range(1, 9);

        _originalCodeString = "";
        _originalCode = new int[CodeLength];
        _cipheredCodeString = "";
        _cipheredCode = new int[CodeLength];

        for (int i = 0; i < CodeLength; ++i)
        {
            _originalCode[i] = Random.Range(1, 9);
            _originalCodeString += _originalCode[i];
            _cipheredCode[i] = (_originalCode[i] + _cipherKey) % 10;
            _cipheredCodeString += _cipheredCode[i];
        }
        SendGeneratedCode.Invoke(_originalCodeString);
        Debug.Log("Generated code is " + _originalCodeString);
        Debug.Log("Ciphered code is " + _cipheredCodeString);

    }
}
