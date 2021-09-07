using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitlePanel : MonoBehaviour
{
    public GameObject buttonPrefab;
    public GameObject titlePanel;
    public GameObject loadingSplash;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WebLoader.LoadTitles(DisplayTitles));
    }

    public void DisplayTitles(List<string> titles)
    {
        titlePanel.SetActive(true); //show title panel after load
        loadingSplash.SetActive(false);

        foreach(string title in titles)
        {
            var b = Instantiate(buttonPrefab);
            b.GetComponent<SceneButton>().sceneName = title;
            //StartCoroutine(WebLoader.LoadImage(title + ".png", showTitle));
            //b.GetComponentInChildren<Text>().text = title;
            b.transform.SetParent(titlePanel.transform);
        }
    }
    /*public void showTitle(Texture2D img)
    {
        var foo = new Button();
        foo.image = new Image();
        foo.image.sprite = Sprite.Create(img,
            new Rect(0, 0, img.width, img.height), //take entire sprite
            new Vector2(0.5f, 0.5f), //anchor point center
                                     //100.0f);
            100.0f * Mathf.Min(((float)foo.width / (float)Screen.width), ((float)bg.height / (float)Screen.height))); //scaling

    }*/

    // Update is called once per frame
    void Update()
    {
        
    }
}
