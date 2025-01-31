using UnityEngine;

[CreateAssetMenu(fileName = "AudioClipRefs", menuName = "ScriptableObjects/AudioClipRefsSO")]
public class AudioClipRefsSO : ScriptableObject
{
    public AudioClip[] chop;
    public AudioClip[] deliveryFail;
    public AudioClip[] deliverySuccess;
    public AudioClip[] objectDrop;
    public AudioClip[] objectPickup;
    public AudioClip[] footstep;
    public AudioClip stoveSizzle;
    public AudioClip[] trash;
    public AudioClip[] warning;
}
