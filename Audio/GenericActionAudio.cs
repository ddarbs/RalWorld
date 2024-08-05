using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericActionAudio : MonoBehaviour
{
    [SerializeField] private AudioSource ActionAudioSource;
    [SerializeField] private AudioClip GatherClip, BuildClip, ToolBenchClip, ForgeClip;
    [SerializeField] private AudioClip ButtonInteractableClip;

    private static GenericActionAudio Instance;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("[ERROR] Should not be more than one GenericActionAudio", this);
        }
    }

    public static void RequestGatherAudio()
    {
        Instance.ActionAudioSource.PlayOneShot(Instance.GatherClip);
    }
    
    
    public static void RequestBuildAudio()
    {
        Instance.ActionAudioSource.PlayOneShot(Instance.BuildClip);
    }
    
    
    public static void RequestToolBenchAudio()
    {
        Instance.ActionAudioSource.PlayOneShot(Instance.ToolBenchClip);
    }
    
    
    public static void RequestForgeAudio()
    {
        Instance.ActionAudioSource.PlayOneShot(Instance.ForgeClip);
    }

    public static void RequestButtonInteractableAudio()
    {
        Instance.ActionAudioSource.PlayOneShot(Instance.ButtonInteractableClip);
    }
}
