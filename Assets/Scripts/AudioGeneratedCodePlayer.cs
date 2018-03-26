using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class SendCodeEvent : UnityEvent<string> { }

public class AudioGeneratedCodePlayer : MonoBehaviour
{
    [SerializeField]
    public SendCodeEvent SendGeneratedCode;

    public bool CanPlay;
    private bool _isPlaying;
    public AudioSource Sound;

    private int[] _code;
    [HideInInspector]
    public string CodeString;
    public int CodeLength = 4;

    public float AudioTimeout = 1;
    // Use this for initialization
    void Start()
    {
        SendGeneratedCode = new SendCodeEvent();
        InitCode();
    }

    private void InitCode()
    {
        CodeString = "";
        _code = new int[CodeLength];
        for (int i = 0; i < CodeLength; ++i)
        {
            _code[i] = Random.Range(1, 9);
            CodeString += _code[i];
        }
        SendGeneratedCode.Invoke(CodeString);
        Debug.Log("Generated code is " + CodeString);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            CanPlay = !CanPlay;       
        }
        if (CanPlay && !_isPlaying)
        {
            StartCoroutine(PlayCode());
        }
        if (!CanPlay)
        {
            _isPlaying = false;
            StopCoroutine(PlayCode());
        }
    }

    IEnumerator PlayCode()
    {

        _isPlaying = true;
        for (int codePosition = 0; codePosition < CodeLength; ++codePosition)
        {
            for (int i = 0; i < _code[codePosition]; ++i)
            {
                Sound.Play();
                // Wait for the audio to have finished
                yield return new WaitForSeconds(Sound.clip.length);
                if (!CanPlay) break;
            }
            if (!CanPlay) break;
            yield return new WaitForSeconds(1 * AudioTimeout);
        }
        yield return new WaitForSeconds(2 * AudioTimeout);
        _isPlaying = false;

    }

    public void StartPlayingCode()
    {
        CanPlay = true;
    }

    public void StopPlayingCode()
    {
        CanPlay = false;
    }
}
