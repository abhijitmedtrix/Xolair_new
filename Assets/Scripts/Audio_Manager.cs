using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio_Manager : MonoBehaviour {

    // Use this for initialization
    [SerializeField]
    private AudioSource audioSource;
	void Start () {
		
	}

    // Update is called once per frame
    public void Audio_play(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }
}
