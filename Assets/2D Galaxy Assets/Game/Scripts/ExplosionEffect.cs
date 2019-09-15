using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionEffect : MonoBehaviour {

    // Use this for initialization
    private AudioSource _audioSource;
	void Start ()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.Play();
        Destroy(this.gameObject, 4f);
	}
	
}
