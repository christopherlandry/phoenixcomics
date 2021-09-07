using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupPanel : MonoBehaviour
{
    public GameObject buttonPrefab;
    public GameObject content;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Open(string title)
    {
        WebLoader.currentTitle = title.ToLower();
        StartCoroutine(WebLoader.LoadIssues(displayIssues));
    }

    public void displayIssues(int numIssues)
    {
        
        //create a list of buttons for each issue
        for(int i = 1; i <= numIssues; i++)
        {
            var b = Instantiate(buttonPrefab);
            b.GetComponent<issueButton>().issue = i;
            b.GetComponentInChildren<Text>().text = "Scene " + i.ToString();
            b.transform.SetParent(content.transform);
        }
    }
}
