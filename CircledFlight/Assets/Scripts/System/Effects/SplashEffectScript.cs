using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashEffectScript : MonoBehaviour {

    private int counter;

    void Start() {
        counter = 0;
    }

    void Update () {
        counter--;
        if (counter <= 0){
		    transform.position = new Vector3(Camera.main.transform.position.x + Random.Range(-7.5f, 7.5f), Camera.main.transform.position.y + Random.Range(-2f, -4f), transform.position.z);
            counter = Random.Range(5, 7);
        }
	}

}
