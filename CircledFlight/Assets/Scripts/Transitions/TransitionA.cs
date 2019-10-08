using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionA : Transition {

	//Settings
    [SerializeField] private bool direction;

    //Variables
    private int timer;
    private float spd;
    private GameObject tilt;

    //Init
	void Awake () {
        base.finished = false;
        base.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, transform.position.z);
        spd = 0.05f;
        timer = 60;
		if (direction){
            //Comes from the Right Side
            transform.position = new Vector3(position.x + 18f, position.y, position.z);
            transform.localScale = new Vector3(15.5f, 8.5f, 1f);
            
            tilt = new GameObject("TransitionA_tilt", typeof(SpriteRenderer), typeof(PixelSnap));
            tilt.GetComponent<SpriteRenderer>().sprite = GetComponent<SpriteRenderer>().sprite;
            tilt.transform.position = new Vector3(position.x + 11f, position.y, position.z);
            tilt.transform.localScale = new Vector3(4f, 11f, 1f);
            tilt.transform.eulerAngles = new Vector3(0f, 0f, 15);
        }
        else {
            //Comes from the Left Side
            transform.position = new Vector3(position.x - 18f, position.y, position.z);
            transform.localScale = new Vector3(15.5f, 8.5f, 1f);
            
            tilt = new GameObject("TransitionA_tilt", typeof(SpriteRenderer), typeof(PixelSnap));
            tilt.GetComponent<SpriteRenderer>().sprite = GetComponent<SpriteRenderer>().sprite;
            tilt.transform.position = new Vector3(position.x - 11f, position.y, position.z);
            tilt.transform.localScale = new Vector3(4f, 11f, 1f);
            tilt.transform.eulerAngles = new Vector3(0f, 0f, -15);
        }
	}
	
	//Update
	void LateUpdate () {
        //Reset Camera and Y position
        base.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, transform.position.z);
        transform.position = new Vector3(transform.position.x, position.y, transform.position.z);

        //Move Transition Slide
		if (timer > 0){
            timer--;
            spd *= 1.06f;
            if (Mathf.Abs(transform.position.x - position.x) > 0.03f){
                if (direction){
                    transform.position = new Vector3(transform.position.x - spd, transform.position.y, transform.position.z);
                    tilt.transform.position = new Vector3(tilt.transform.position.x - spd, tilt.transform.position.y, tilt.transform.position.z);
                    if (transform.position.x < position.x){
                        transform.position = position;
                    }
                }
                else {
                    transform.position = new Vector3(transform.position.x + spd, transform.position.y, transform.position.z);
                    tilt.transform.position = new Vector3(tilt.transform.position.x + spd, tilt.transform.position.y, tilt.transform.position.z);
                    if (transform.position.x > position.x){
                        transform.position = position;
                    }
                }
            }
        }
        else {
            finished = true;
        }
	}

}
