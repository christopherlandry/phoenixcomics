using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class closeButtonScript : MonoBehaviour
{
    public void Click()
    {
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        var loading = SceneManager.LoadSceneAsync("MainMenu");
        while (!loading.isDone)
            yield return null;
    }
}
