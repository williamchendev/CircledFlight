using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainEffectScript : MonoBehaviour {

	void Update () {
        transform.position = new Vector3(Camera.main.transform.position.x, transform.position.y, 2.5f);
	}
}
