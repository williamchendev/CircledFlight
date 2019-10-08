
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugLayer : MonoBehaviour {

    [SerializeField] private float z;
	
	void LateUpdate () {
		transform.position = new Vector3(transform.position.x, transform.position.y, z);
	}
}
