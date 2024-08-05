using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueAudio : MonoBehaviour
{
    [SerializeField] private AudioClip[] AlphabetClips;

    [SerializeField] private AudioSource DialogueAudioSource;

    private static DialogueAudio Instance;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("[ERROR] Should not be more than one LetterAudio", this);
        }
    }
    
    public static void RequestDialogueSound(char _c)
    {
        int _Index = char.ToUpper(_c) - 65;
        
        if (_Index < 0 || _Index >= Instance.AlphabetClips.Length)
        {
            return;
        }
        
        Instance.DialogueAudioSource.PlayOneShot(Instance.AlphabetClips[_Index]);
    }
}
