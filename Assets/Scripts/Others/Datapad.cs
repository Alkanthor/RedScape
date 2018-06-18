﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class Datapad : MonoBehaviour
{
    public int recordIndex;

    public TextAsset DatapadTextFile;
    private GameObject authorText;
    private GameObject dateText;
    private GameObject contentText;

    private static List<string> datapads = new List<string>()
    {
        "1;26.2.2118;Jerry Hallee;Fraud, drunk drive and contempt of court.Not really my fault, but I admit I shouldn’t have called judge’s mom worldwide.This time I won’t get out that easily.",
"2;7.3.2118;Gaston Winslett;Go to Mars, they said.It will be fun, they said. You know, what is NOT fun? When your coworker keeps forgetting to turn off the machine. Yesterday it warped my only book somewhere to the surface. This is just great. Now I’ll never know, what happened to John Carter.",
 "3;2.3.2118;Paul K.Fry;Dang it.I forgot my wife’s birthday. I really forget a lot of stuff.Too bad I cannot write everything down; the deposit box password, for instance.But I still recorded it, without it being so RADIATING, ha-ha.I’m funny genius.",
"4;1.3.2118;Isac Raynor;Gaston is somehow upset last few days.His best friend was send to prison.Well, there’s nothing he can do about it, I guess.Anyway, I’m almost done assembling the RC rover.This will cheer him up.",
"5;7.3.2118;Chell Freeman;Now this is odd.The teleporting device was found running again.Gaston claims, it’s Fry’s fault, but it was me, who supervised the testing and I’m confident I turned it off. The anomaly it creates could be dangerous, even though only small objects can pass through. Also, the portable teleported is missing.Our superiors will be furious.",
 "6;8.3.2118;Jack Sheppard;The machine has gone mad and keeps creating random portals or something. It took one of us outside. There was nothing we could do. What is more, the red code was announced and the lead scientist locked himself in the labs with ID card.We are stuck here.",
"7;8.3.2118;Gaston Winslett;Now it’s not the time for writing my thoughts… or for reading them. I must stop the device immediately.",
"8;6.3.2118;Stephen Bell;Martians want to steal my research! Now I can be sure.I suspect Sheppard is the one who took the portable device.That’s it. I’ll blow up the whole facility rather than let them win.Without ID card they will be unable to reach the unstable core in time.Nobody can be trusted."
    };



    // Use this for initialization
    void Start()
    {



        string line = datapads[recordIndex-1];

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
