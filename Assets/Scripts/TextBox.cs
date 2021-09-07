using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class TextBox : MonoBehaviour
{
    //const int maxChars = 185;
    //string displayBuffer = "";

    public GameObject leftBubble;
    public GameObject rightBubble;

    private void Update()
    {
       /* if (Input.GetKeyUp(KeyCode.Space))
        {
            if(displayBuffer.Length > 0) showMore();
        }*/
    }
    //button for continue showing text / space / whatever
    /*void showMore()
    {
        if (displayBuffer.Length > maxChars)
        {
            var remainder = displayBuffer.Substring(maxChars);
            SetText(displayBuffer.Substring(0, maxChars));
            displayBuffer = remainder;
        } else
        {
            SetText(displayBuffer);
            displayBuffer = "";
        }
    }*/

    //set text. no more than maxChars! private.
    /*void SetText(string text)
    {
        this.GetComponentInChildren<TextMeshProUGUI>().text = text;
    }*/

    //add some text to be displayed by the box
    public void DisplayText(string text, StagePosition direction = StagePosition.OFFSTAGE)
    {
        leftBubble.SetActive(false);
        rightBubble.SetActive(false);
        switch (direction)
        {
            case StagePosition.STAGE_LEFT:
                leftBubble.SetActive(true);
                break;
            case StagePosition.STAGE_RIGHT:
                rightBubble.SetActive(true);
                break;
            case StagePosition.OFFSTAGE:
                text = "????: " + text; //todo should we sometimes know who is speaking from offstage?
                break;
        }
        this.GetComponentInChildren<TextMeshProUGUI>().text = text.Trim();
        //displayBuffer += text;
        
    }
}
