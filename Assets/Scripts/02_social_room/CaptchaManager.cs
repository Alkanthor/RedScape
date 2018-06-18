using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CaptchaManager : MonoBehaviour {


    [SerializeField]
    private GameObject _inputPanel;
    [SerializeField]
    private GameObject _guessPanel;

    [SerializeField]
    private Text _roundsText;
    [SerializeField]
    private Button _startRoundButton;
    [SerializeField]
    private Text _puzzleText;
    [SerializeField]
    private InputField _input;

    [SerializeField]
    private string[] _symbols;
    [SerializeField]
    private int _initPuzzleLength = 4;
    [SerializeField]
    private int _puzzleStep = 1;

    [SerializeField]
    private float _puzzleInterval = 1;

    private int _puzzleLength;
    [SerializeField]
    private int _totalRounds = 3;

    private int _round;
    private int[] _guessedPuzzle;
    private int[] _generatedPuzzle;

	// Use this for initialization
	void Start () {
        _round = 0;

        InitPuzzleRound();
	}

    private int _pressedCount = 0;
    public void PressedButton(int index)
    {
        Debug.Log("pressed button " + _symbols[index]);
        if(_pressedCount < _puzzleLength)
        {
            _guessedPuzzle[_pressedCount++] = index;
            _input.text += _symbols[index];
        }
    }

    public void ResetText()
    {
        _input.text = "";
        _pressedCount = 0;
    }

    private void InitPuzzleRound()
    {
        _inputPanel.SetActive(false);
        _guessPanel.SetActive(true);
        _startRoundButton.interactable = true;
        _startRoundButton.GetComponent<CanvasGroup>().alpha = 1;
        _pressedCount = 0;
        _round++;
        _roundsText.text = "Round " + _round;
        _puzzleText.text = "";
        if (_round != 1)
        {
            _puzzleLength += _puzzleStep;
        }
        else
        {
            _puzzleLength = _initPuzzleLength;
        }
    }
    public void StartRound()
    {
        _startRoundButton.interactable = false;
        _startRoundButton.GetComponent<CanvasGroup>().alpha = 0;
        GeneratePuzzle();
        StartCoroutine(PlayPuzzle());

    }

    private void GeneratePuzzle()
    {
        _generatedPuzzle = new int[_puzzleLength];
        _guessedPuzzle = new int[_puzzleLength];

        var generatedPuzzleString = "";
        var maxIndex = _symbols.Length;
        for(int i = 0; i < _puzzleLength; ++i)
        {
            _generatedPuzzle[i] = Random.Range(0, maxIndex - 1);
            generatedPuzzleString += _symbols[_generatedPuzzle[i]];
        }
        Debug.Log("Generated puzzle for round " + _round + " is: " + generatedPuzzleString);

    }

    private IEnumerator PlayPuzzle()
    {
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < _puzzleLength; ++i)
        {
            _puzzleText.text = _symbols[_generatedPuzzle[i]];
            yield return new WaitForSeconds(_puzzleInterval);
            _puzzleText.text = "";
            yield return new WaitForSeconds(0.2f);

        }
        _inputPanel.SetActive(true);
        _guessPanel.SetActive(false);
        _input.text = "";
        _puzzleText.text = "";
    }
    public void Confirm()
    {
        if(CheckPuzzleGuess())
        {
            if(_round == _totalRounds)
            {
                this.gameObject.SetActive(false);
                Debug.Log("You win the captcha game");
                ComputerManager.Instance.NextStage();
                return;
            }
        }
        else
        {
            _round = 0;
            
        }
        InitPuzzleRound();

    }

    private bool CheckPuzzleGuess()
    {
        for(int i = 0; i < _puzzleLength; ++i)
        {
            if (_guessedPuzzle[i] != _generatedPuzzle[i]) return false;
        }
        return true;
    }



}
