using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition : MonoBehaviour {

	protected bool finished;
    protected Vector3 position;

    void Awake() {
        finished = false;
        position = transform.position;
    }

    public bool end {
        get {
            return finished;
        }
    }

}
