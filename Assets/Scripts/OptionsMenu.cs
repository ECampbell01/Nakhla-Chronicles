// Contributors: Ethan Campbell
// Created On 1/25/2025
using UnityEngine;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;

    public void SetVolume(float volume) 
    {
        audioMixer.SetFloat("volume", volume);
    }
}
