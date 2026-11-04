using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(
    fileName = "HandTouchSoundSettings",
    menuName = "GTAG/Hand Touch Sound Settings"
)]
public class HandTouchSoundSettings : ScriptableObject
{
    [System.Serializable]
    public class TagSound
    {
        public string tag;
        public AudioClip sound;
    }

    public List<TagSound> tagSounds = new List<TagSound>();
    public float volume = 1f;
    public float cooldown = 0.15f;
}
