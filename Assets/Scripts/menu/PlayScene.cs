using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayScene : MonoBehaviour
{
    public void Play()
    {
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        var loading = SceneManager.LoadSceneAsync("SceneRunner");
        while (!loading.isDone)
            yield return null;
    }   
}
