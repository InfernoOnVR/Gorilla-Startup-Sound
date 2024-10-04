using BepInEx;
using UnityEngine;
using System.IO;
using System.Collections;

[BepInPlugin("com.inferno.gorillatag.GorillaCloseSounds", "Gorilla Close Sounds", "1.0.0")]
public class gorillaclosesound : BaseUnityPlugin
{
    private AudioSource Sound;
    private string soundfolder;

    void Close()
    {
        
        soundfolder = Path.Combine(Paths.PluginPath, "CloseSound/");

        if (!Directory.Exists(soundfolder))
        {
            Logger.LogError($"No folder for mp3 files. maybe you deleted it?");
            return;
        }

        string[] mp3Files = Directory.GetFiles(soundfolder, "*.mp3");
        if (mp3Files.Length == 0)
        {
            Logger.LogError("No mp3 in the folder.");
            return;
        }

        string mp3dir = mp3Files[0];

        StartCoroutine(playmp3(mp3dir));
    }

    IEnumerator playmp3(string mp3filePath)
    {
        WWW www = new WWW("file://" + mp3filePath);
        yield return www;

        if (www.error == null)
        {
            Sound = gameObject.AddComponent<AudioSource>();
            Sound.clip = www.GetAudioClip(false, false);
            Sound.Play();
        }
       
    }
}
