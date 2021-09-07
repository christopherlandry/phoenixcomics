using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class issueButton : MonoBehaviour
{
    public int issue;

    public void Click()
    {
        WebLoader.currentIssue = issue;
        print("open " + WebLoader.currentTitle + WebLoader.currentIssue);
        StartCoroutine(WebLoader.LoadBundle(Launch));
        
    }
    private void Launch()
    {
        FindObjectOfType<PlayScene>().Play();
    }
}
