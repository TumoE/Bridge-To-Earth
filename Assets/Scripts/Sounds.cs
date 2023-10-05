using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sounds
{
    public enum Sound
    {
        Machete = 0,
        Axe = 1,
        Picaxe = 2,
        Put = 3
    }
    public string name;

    public AudioClip sound;
    public bool isLoop;

    public Sound type;
    [Range(0,1f)] public float volume;
}
