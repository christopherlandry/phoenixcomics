using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public static class WebLoader
{
    const string serverUrl = "https://secret-mesa-30987.herokuapp.com/";
    public static string currentTitle = "";
    public static int currentIssue = 1;
    public static AssetBundle bundle;

    public delegate void bundleCallback();
    //load an asset bundle by name. Async, b = callback method on load
    public static IEnumerator LoadBundle(bundleCallback b)
    {
        if (bundle) bundle.Unload(true); //clear currently open issue
        //bundles/{title}{issue} //should this be be title/issue?
        var req = UnityWebRequestAssetBundle.GetAssetBundle(serverUrl + "bundles/" + currentTitle + currentIssue.ToString());
        yield return req.SendWebRequest();
        if (req.result != UnityWebRequest.Result.Success) Debug.LogError(req.error);
        else
        {
            bundle = DownloadHandlerAssetBundle.GetContent(req);
            if (bundle != null) b();
            else Debug.Log("ERROR: failed to load bundle " + currentTitle + currentIssue.ToString());
        }
    }

    public delegate void titleCallback(List<String> titles);
    //load a list of issues for the current title
    public static IEnumerator LoadTitles(titleCallback c)
    {
        //loadTitles
        using (UnityWebRequest req = UnityWebRequest.Get(serverUrl + "loadTitles")) //TODO
        {
            yield return req.SendWebRequest();
            if (req.result != UnityWebRequest.Result.Success) Debug.LogError(req.error);
            else
            {
                Debug.Log("received " + req.downloadHandler.text);
                c(ParseTitleList(req.downloadHandler.text));
            }
        }
    }

    public delegate void imageCallback(Texture2D image);
    public static IEnumerator LoadImage(string uri, imageCallback c)
    {
        using (UnityWebRequest req = UnityWebRequestTexture.GetTexture(serverUrl + "images/" + uri))
        {
            yield return req.SendWebRequest();
            if (req.result != UnityWebRequest.Result.Success) Debug.LogError(req.error);
            else
            {
                c(((DownloadHandlerTexture)req.downloadHandler).texture);
            }
        }
    }


    static List<String> ParseTitleList(string response)
    {
        return new List<String>(response.Split(',')); //TODO probably not valid
    }

    public delegate void issueCallback(int i);
    //load a list of issues for the current title
    public static IEnumerator LoadIssues(issueCallback c)
    {
        //loadIssues/{title}
        using (UnityWebRequest req = UnityWebRequest.Get(serverUrl + "loadIssues/" + currentTitle))
        {
            yield return req.SendWebRequest();
            if (req.result != UnityWebRequest.Result.Success) Debug.LogError(req.error);
            else
            {
                Debug.Log("received " + req.downloadHandler.text);
                c(ParseIssueList(req.downloadHandler.text));
            }
        }
    }

    static int ParseIssueList(string response)
    {
        return Int32.Parse(response); //TODO this maybe should return a list of named issues?
    }
}
