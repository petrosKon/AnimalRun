using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highscores : MonoBehaviour
{
    const string privateCode = "uvhOuVs8H0uvN4SIDj3GiwmOwsmLQINEOrcDTQh1DCQA";
    const string publicCode = "5eab3d070cf2aa0c28dff73d";
    const string webUrl = "http://dreamlo.com/lb/";

    const string scoreUrl = "http://dreamlo.com/lb/uvhOuVs8H0uvN4SIDj3GiwmOwsmLQINEOrcDTQh1DCQA";

    public Highscore[] highscoresList;
    static Highscores instance;
    DisplayHighscores highscoresDisplay;

    private void Awake()
    {
        instance = this;
        highscoresDisplay = GetComponent<DisplayHighscores>();
    }

    public static void AddNewHighscore(string username,int score)
    {
        instance.StartCoroutine(instance.UploadNewHighscore(username,score));
    }

    IEnumerator UploadNewHighscore(string username, int score)
    {
        WWW www = new WWW(webUrl + privateCode + "/add/" + WWW.EscapeURL(username) + "/" + score);
        yield return www;

        if (string.IsNullOrEmpty(www.error))
        {
            print("upload successful");
            DownloadHighscores();
        }
        else
        {
            print("Error uploading: " + www.error);
        }
    }

    public void DownloadHighscores()
    {
        StartCoroutine(DownloadHighscoresFromDatabase());
    }

    IEnumerator DownloadHighscoresFromDatabase()
    {
        WWW www = new WWW(webUrl + publicCode + "/pipe/");
        yield return www;

        if (string.IsNullOrEmpty(www.error))
        {
            FormatHighscores(www.text);
            if (highscoresDisplay != null)
            {
                highscoresDisplay.OnHighscoresDownloaded(highscoresList);
            }
        }
        else
        {
            print("Error downloading: " + www.error);
        }
    }

    void FormatHighscores(string textStream)
    {
        string[] entries = textStream.Split(new char[] { '\n'},System.StringSplitOptions.RemoveEmptyEntries);
        highscoresList = new Highscore[entries.Length];
        for(int i = 0; i < entries.Length; i++)
        {
            string[] entryInfo = entries[i].Split(new char[] { '|' });
            string username = entryInfo[0];
            int score = int.Parse(entryInfo[1]);
            highscoresList[i] = new Highscore(username, score);
            print(highscoresList[i].username + ": " + highscoresList[i].score);

        }
    }
}

public struct Highscore
{
    public string username;
    public int score;

    public Highscore(string _username,int _score)
    {
        username = _username;
        score = _score;
    }
}
