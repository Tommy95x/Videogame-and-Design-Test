using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public static SoundManager  instance { get; private set; }
    public AudioSource playerSource;

	// Use this for initialization
	void Awake () {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
	}

   public void PlayWalk(AudioClip clip)
    {
        playerSource.clip = clip;
        playerSource.Play();
    }


    // Update is called once per frame
    void Update () {
		
	}
}
