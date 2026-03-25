using System;
using UnityEngine;

[Serializable] public struct AudioData
{
    public string AudioName;
    public AudioClip AudioClip;
    [Range(0f, 1f)] public float Volume;
    public bool Loop;
}
public class UniversalAudio : MonoBehaviour
{
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioData[] _audioDatas;

    public void StartAudio(string audioName)
    {
        for(int i = 0; i < _audioDatas.Length; i++)
        {
            if(audioName == _audioDatas[i].AudioName)
            {
                _audioSource.clip = _audioDatas[i].AudioClip;
                _audioSource.volume = _audioDatas[i].Volume;
                _audioSource.loop = _audioDatas[i].Loop;
                _audioSource.Play();
                break;
            }
        }
    }
}
