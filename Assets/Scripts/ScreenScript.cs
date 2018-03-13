using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ScreenScript : MonoBehaviour {

    private int actualScreen;

    private int screenCount;

    private RawImage boardImage;

    private Object[] textures;


	// Use this for initialization
	void Start () {
        actualScreen = 1;
        textures = Resources.LoadAll("Textures", typeof(Texture));
        screenCount = textures.Length;

        boardImage = GameObject.Find("InfoBoardImage").GetComponent<RawImage>();
        boardImage.texture = (Texture)textures[1];
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void NextScreen()
    {
        Debug.Log("Next");
        if(actualScreen<screenCount)
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
}
