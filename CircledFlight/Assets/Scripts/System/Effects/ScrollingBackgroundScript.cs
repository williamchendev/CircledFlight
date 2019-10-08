using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackgroundScript : MonoBehaviour {

    //Objects
    [SerializeField] private Sprite controlA;
    [SerializeField] private Sprite controlB;
    [SerializeField] private Sprite controlC;
    [SerializeField] private Sprite controlD;

    private List<GameObject> bgA;
    private List<GameObject> bgB;
    private GameObject bgC;
    private List<GameObject> bgD;

	// Use this for initialization
	void Start () {
		bgA = new List<GameObject>();
        bgA.Add(createBackground(controlA, new Vector3(-controlA.bounds.size.x, 0, 9999)));
        bgA.Add(createBackground(controlA, new Vector3(0, 0, 9999)));
        bgA.Add(createBackground(controlA, new Vector3(controlA.bounds.size.x, 0, 9999)));
        bgA.Add(createBackground(controlA, new Vector3(controlA.bounds.size.x * 2, 0, 9999)));
        bgA.Add(createBackground(controlA, new Vector3(controlA.bounds.size.x * 3, 0, 9999)));

        bgB = new List<GameObject>();
        bgB.Add(createBackground(controlB, new Vector3(-controlB.bounds.size.x, 0, 9998)));
        bgB.Add(createBackground(controlB, new Vector3(0, 0, 9998)));
        bgB.Add(createBackground(controlB, new Vector3(controlB.bounds.size.x, 0, 9998)));
        bgB.Add(createBackground(controlB, new Vector3(controlB.bounds.size.x * 2, 0, 9998)));
        bgB.Add(createBackground(controlB, new Vector3(controlB.bounds.size.x * 3, 0, 9998)));

        bgC = createBackground(controlC, new Vector3(Camera.main.transform.position.x, 0, 9997));

        bgD = new List<GameObject>();
        bgD.Add(createBackground(controlD, new Vector3(-controlD.bounds.size.x * 2, 0, 9996)));
        bgD.Add(createBackground(controlD, new Vector3(-controlD.bounds.size.x, 0, 9996)));
        bgD.Add(createBackground(controlD, new Vector3(0, 0, 9996)));
        bgD.Add(createBackground(controlD, new Vector3(controlD.bounds.size.x, 0, 9996)));
        bgD.Add(createBackground(controlD, new Vector3(controlD.bounds.size.x * 2, 0, 9996)));
        bgD.Add(createBackground(controlD, new Vector3(controlD.bounds.size.x * 3, 0, 9996)));
	}
	
	// Update is called once per frame
	void Update () {
		if (bgA[0].transform.position.x <= (controlA.bounds.size.x * -2f)){
            for (int k = 0; k < bgA.Count; k++){
                bgA[k].transform.position = new Vector3(controlA.bounds.size.x * (k - 1), bgA[k].transform.position.y, bgA[k].transform.position.z);
            }
        }
        else {
            for (int k = 0; k < bgA.Count; k++){
                bgA[k].transform.position = new Vector3(bgA[k].transform.position.x - 0.002f, bgA[k].transform.position.y, bgA[k].transform.position.z);
            }
        }

        if (bgB[0].transform.position.x <= (controlB.bounds.size.x * -2f)){
            for (int k = 0; k < bgB.Count; k++){
                bgB[k].transform.position = new Vector3(controlB.bounds.size.x * (k - 1), bgB[k].transform.position.y, bgB[k].transform.position.z);
            }
        }
        else {
            for (int i = 0; i < bgB.Count; i++){
                bgB[i].transform.position = new Vector3(bgB[i].transform.position.x - 0.005f, bgB[i].transform.position.y, bgB[i].transform.position.z);
            }
        }

        bgC.transform.position = new Vector3(Camera.main.transform.position.x, bgC.transform.position.y, bgC.transform.position.z);

        if (bgD[0].transform.position.x >= (controlD.bounds.size.x * -1f)){
            for (int k = 0; k < bgD.Count; k++){
                bgD[k].transform.position = new Vector3(controlD.bounds.size.x * (k - 2), bgD[k].transform.position.y, bgD[k].transform.position.z);
            }
        }
        else {
            for (int i = 0; i < bgD.Count; i++){
                bgD[i].transform.position = new Vector3(bgD[i].transform.position.x + 0.005f, bgD[i].transform.position.y, bgD[i].transform.position.z);
            }
        }
	}

    private GameObject createBackground(Sprite img, Vector3 pos) {
        GameObject obj = new GameObject("background");
        obj.transform.position = pos;
        obj.AddComponent<SpriteRenderer>();
        obj.GetComponent<SpriteRenderer>().sprite = img;
        obj.AddComponent<PixelSnap>();
        obj.transform.parent = transform;
        return obj;
    }
}
