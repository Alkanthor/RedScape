using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class Datapad : MonoBehaviour {
    public int recordIndex;

    public TextAsset DatapadTextFile;
    private GameObject authorText;
    private GameObject dateText;
    private GameObject contentText;




    // Use this for initialization
    void Start() {
        DatapadTextFile = Resources.Load<TextAsset>("datapads");

        
        string line = File.ReadAllLines(Application.dataPath + "/Resources/datapads.txt", Encoding.GetEncoding("iso-8859-1"))[recordIndex-1]; // 0-based
        line = line.Replace("\u0092", "'");

        authorText = gameObject.transform.Find("Canvas/author_text").gameObject;
        dateText = transform.Find("Canvas/date_text").gameObject;
        contentText = transform.Find("Canvas/content_text").gameObject;

        string[] parts = line.Split(';');
        authorText.GetComponent<Text>().text = parts[1];
        dateText.GetComponent<Text>().text = parts[2];
        contentText.GetComponent<Text>().text = parts[3];


        //Debug.Log(line);

    }
}
