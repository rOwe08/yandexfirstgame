using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    private Dictionary<string, AudioClip> audioClips = new Dictionary<string, AudioClip>();
    public AudioClip clickLetterButtonSound;    
    public AudioClip clickButtonFinalSound;
    public AudioClip activatingLoseWindowSound;
    public AudioClip activatingWinWindowSound;

    private void Start()
    {
        AddSound("clickLetterButtonSound", clickLetterButtonSound);  
        AddSound("clickButtonFinalSound", clickButtonFinalSound);
        AddSound("activatingLoseWindowSound", activatingLoseWindowSound);
        AddSound("activatingWinWindowSound", activatingWinWindowSound);
    }
    public void PlaySound(string key)
    {
        if (audioClips.ContainsKey(key))
        {
            AudioClip clip = audioClips[key];
            AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);
        }
        else
        {
            Debug.LogWarning("Sound with key " + key + " not found!");
        }
    }
    public void AddSound(string key, AudioClip clip)
    {
        if (!audioClips.ContainsKey(key))
        {
            audioClips.Add(key, clip);
        }
        else
        {
            Debug.LogWarning("Sound with key " + key + " already exists!");
        }
    }
}
