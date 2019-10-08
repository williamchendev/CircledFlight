using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainDebug : MonoBehaviour {

    [SerializeField] private float y_offset = 0f;

	void Update () {
        transform.position = new Vector3(Camera.main.transform.position.x, transform.position.y + y_offset, 2.5f);
	}

}
