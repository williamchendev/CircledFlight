using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FountainSplashEffectScript : MonoBehaviour {

	private Vector3 pos;
    private int counter;

	void Start () {
		pos = transform.position;
        counter = 0;
	}
	
	// Update is called once per frame
	void Update () {
        counter--;
		if (counter <= 0){
            counter = 6;
            transform.position = new Vector3(pos.x + Random.Range(-3f, 3f), pos.y + Random.Range(0.2f, -1f), pos.z);
        }
	}
}
