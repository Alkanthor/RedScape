using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AudioGeneratedCodePlayer : MonoBehaviour
{
    [SerializeField]
    public UnityEvents.UnityEventString SendGeneratedCode;

    public bool CanPlay;
    private bool _isPlaying;
    public AudioSource Sound;

    private int[] _code;
    [HideInInspector]
    public string CodeString;
    public int CodeLength = 4;

    public float AudioTimeout = 1;

    private Coroutine playCoroutine;
    // Use this for initialization
    void Start()
    {
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

    public void CanPlayCode(bool canPlay)
    {
        CanPlay = canPlay;
        if (CanPlay && !_isPlaying)
        {
            Debug.Log("Radio starts playing code");
            _isPlaying = true;
            playCoroutine = StartCoroutine(PlayCode());
        }
        if (!CanPlay)
        {
            Debug.Log("Radio stops playing code");
            _isPlaying = false;
            if(playCoroutine != null)
            {
                StopCoroutine(playCoroutine);
            }

        }
    }

    IEnumerator PlayCode()
    {
        while(_isPlaying)
        {
            Debug.Log("radio is playing code");
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
        }

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
