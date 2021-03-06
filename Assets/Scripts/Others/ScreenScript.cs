﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ScreenScript : MonoBehaviour
{
    public int screenTreshhold;

    private int actualScreen;

    private int screenCount;

    private RawImage boardImage;

    private Button nextBtn;

    private Button backBtn;

    private Button startBtn;

    private Object[] textures;

    private LevelManagerPrisonCell00 levelManager;


    // Use this for initialization
    void Start()
    {

        actualScreen = 1;
        textures = Resources.LoadAll("GUI/Prison_Board", typeof(Texture));
        screenCount = screenTreshhold;

        boardImage = GameObject.Find("InfoBoardImage").GetComponent<RawImage>();
        boardImage.texture = (Texture)textures[0];
        nextBtn = GameObject.Find("NextBtn").GetComponent<Button>();
        backBtn = GameObject.Find("BackBtn").GetComponent<Button>();
        startBtn = GameObject.Find("StartGameBtn").GetComponent<Button>();


    }

    // Update is called once per frame
    void Update()
    {
        backBtn.interactable = actualScreen != 1;
        nextBtn.interactable = actualScreen != screenCount;
    }

    public void NextScreen()
    {
        Debug.Log("Next");
        if (actualScreen < screenCount)
        {
            actualScreen++;
        }
        boardImage.texture = (Texture)textures[actualScreen - 1];
    }

    public void PrevScreen()
    {
        Debug.Log("Prev");
        if (actualScreen > 1)
        {
            actualScreen--;
        }
        boardImage.texture = (Texture)textures[actualScreen - 1];
    }

    public void StartGame()
    {
        LevelManagerPrisonCell00.Instance.GameStarted = true;
        startBtn.interactable = false;
        startBtn.GetComponentInChildren<Text>().text = "ESCAPE PRISON!";
        screenCount = textures.Length;
        actualScreen = screenTreshhold + 1;
        boardImage.texture = (Texture)textures[actualScreen - 1];
    }
}
