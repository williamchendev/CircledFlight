using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashDebug : MonoBehaviour {

    [SerializeField] private float y_offset = 0f;

	private int counter;

    void Start() {
        counter = 0;
    }

    void Update () {
        counter--;
        if (counter <= 0){
		    transform.position = new Vector3(Camera.main.transform.position.x + Random.Range(-7.5f, 7.5f), Camera.main.transform.position.y + Random.Range(-2f, -4f) + y_offset, transform.position.z);
            counter = Random.Range(5, 7);
        }
	}

}
