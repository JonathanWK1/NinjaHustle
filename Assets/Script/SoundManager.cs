using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    AudioSource bgmSource;
    [SerializeField]
    AudioSource sfxSource;

    [SerializeField]
    List<AudioClip> sfxClipList;

    public void PlaySFX(string name)
    {
        AudioClip clip = sfxClipList.Where(x => x.name == name).FirstOrDefault();
        sfxSource.clip = clip;
        sfxSource.Play();
    }

}
