using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound : MonoBehaviour
{
    public string name;
    
    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume;
    [Range(0.1f, 3f)]
    public float pitch;

    public bool loops;
    
    [HideInInspector]
    public AudioSource source;
}
