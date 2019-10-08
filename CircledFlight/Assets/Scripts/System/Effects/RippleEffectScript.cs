using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RippleEffectScript : MonoBehaviour {

	private Vector3 pos;
    private float size;

	void Awake () {
		pos = transform.position;
        size = Random.Range(0f, 1f);
        transform.position = new Vector3(pos.x + Random.Range(-3f, 3f), pos.y + Random.Range(0.2f, -1f), pos.z);
	}
	
	// Update is called once per frame
	void Update () {
        size += 0.03f;
		if (size >= 1){
            size = 0;
            transform.position = new Vector3(pos.x + Random.Range(-4f, 4f), pos.y + Random.Range(0.5f, -1.3f), pos.z);
        }

        transform.localScale = new Vector3(size * 0.2f , size * 0.03f, 1);
	}

}
