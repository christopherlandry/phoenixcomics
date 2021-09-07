using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneButton : MonoBehaviour
{
    public string sceneName; //base name of the thing this plays
    public GameObject panel;

    public void Click()
    {
        print("open " + sceneName);
        var popup = Instantiate(panel);
        popup.transform.SetParent(FindObjectOfType<Canvas>().transform, false);
        popup.GetComponent<PopupPanel>().Open(sceneName);
    }
}
