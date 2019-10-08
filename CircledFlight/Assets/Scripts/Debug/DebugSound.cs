using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugSound : MonoBehaviour {

	//Settings
    [SerializeField] private string music_track;
    [SerializeField] private string ambience_track;

    //Play Music
	void Start () {
		if (music_track != null){
            GameManager.instance.playSoundLoop(music_track);
        }
        if (ambience_track != null){
            GameManager.instance.playSoundAmbient(ambience_track);
        }
	}

}
