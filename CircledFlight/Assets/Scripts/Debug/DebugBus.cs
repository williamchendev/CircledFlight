using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugBus : MonoBehaviour {

    float lerpTime = 260f;
    float currentLerpTime;

    void Update () {
        //Lerp
        currentLerpTime += Time.deltaTime;
        if (currentLerpTime > lerpTime) {
            currentLerpTime = lerpTime;
        }
        float t = currentLerpTime / lerpTime;
        t = 1f - Mathf.Cos(t * Mathf.PI * 0.5f);

        if (GameManager.instance.save.getKey(11)){
            transform.position = new Vector3(Mathf.Lerp(transform.position.x, -18, t), transform.position.y, -3);
        }
		else if (GameManager.instance.save.getKey(10)){
            transform.position = new Vector3(Mathf.Lerp(transform.position.x, 0, t), transform.position.y, -3);
        }
	}
}
