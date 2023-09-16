using UnityEngine;

public class SoundManager
{
    public static SoundManager Instance { get; } = new SoundManager();

    public void Play2DSound(AudioClip clip)
    {
        AudioSource.PlayClipAtPoint(clip, Vector3.zero);
    }
}