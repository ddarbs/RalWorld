using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMuteButton : MonoBehaviour
{
    [SerializeField] private AudioSource MusicAudioSource, SFXAudioSource, DialogueAudioSource;

    public void Button_OnClick()
    {
        MusicAudioSource.mute = !MusicAudioSource.mute;
        SFXAudioSource.mute = !SFXAudioSource.mute;
        DialogueAudioSource.mute = !DialogueAudioSource.mute;
    }
}
