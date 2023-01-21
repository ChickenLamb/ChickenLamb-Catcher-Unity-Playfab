using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class AudioService : MonoBehaviour
{
    public static AudioService instance;
    public bool TurnOnVoice;
    public AudioSource bgAudio;
    public AudioSource uiAudio;
    public void InitializeService()
    {
        instance = this;
        this.Log("Initialize Audio Service Completed");
    }

    public void PlayBackgroundMusic(string name, bool isLoop = true)
    {
        if (!TurnOnVoice) return;
        AudioClip audioClip = ResourceService.instance.LoadAudio("ResAudio/" + name, true);
        if (bgAudio.clip == null || bgAudio.clip.name != audioClip.name)
        {
            bgAudio.clip = audioClip;
            bgAudio.loop = isLoop;
            bgAudio.Play();
        }
    }
    public void StopBackgroundMusic()
    {
        if (bgAudio != null)
        {
            bgAudio.Stop();
        }
    }

    public void PlayUIAudio(string name)
    {
        if (!TurnOnVoice) return;
        AudioClip audioClip = ResourceService.instance.LoadAudio("ResAudio/" + name, true);
        if (uiAudio.clip == null || uiAudio.clip.name != audioClip.name)
        {
            uiAudio.clip = audioClip;
            uiAudio.Play();
        }
    }


}

