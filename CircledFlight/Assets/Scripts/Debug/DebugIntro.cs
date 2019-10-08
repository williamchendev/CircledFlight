
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugIntro : MonoBehaviour {

    //Settings
    private SpriteRenderer sr;
    private EventManager em;
    float lerpTime = 20f;
    float currentLerpTime;

    private int counter;
    private bool fade;
    private bool intro_text1;
    private bool intro_text2;
    private bool talk;
    private Vector2 cam_pos;

	//Init Event
	void Awake () {
        //Settings
		sr = GetComponent<SpriteRenderer>();
        sr.enabled = true; //true
        em = GetComponent<EventManager>();

        Camera.main.GetComponent<CameraBehavior>().enabled = false;
        Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y + 3f, Camera.main.transform.position.z);
        cam_pos = Camera.main.transform.position;
        counter = 60;
        fade = true; //true
        talk = false; //false
        intro_text1 = true; //true
        intro_text2 = true; //true
	}
	
	//Update Event
	void Update () {
        if (counter > 0){
            counter--;
            return;
        }

		if (fade){
            float alpha = sr.color.a;
            
            //Lerp
            currentLerpTime += Time.deltaTime;
            if (currentLerpTime > lerpTime) {
                currentLerpTime = lerpTime;
            }
            float t = currentLerpTime / lerpTime;
            t = 1f - Mathf.Cos(t * Mathf.PI * 0.5f);

            //Move Camera
            cam_pos = new Vector3(cam_pos.x, Mathf.Lerp(cam_pos.y, 0, t), Camera.main.transform.position.z);
            Camera.main.transform.position = new Vector3(cam_pos.x, cam_pos.y - (cam_pos.y % 0.03125f), Camera.main.transform.position.z);
            
            //Change Alpha
            if (alpha > 0){
                alpha -= 0.0027f;
            }
            else {
                fade = false;
            }
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, Mathf.Clamp(alpha, 0, 1));
        }
        else {
            if (!em.isActive){
                if (talk){
                    talk = false;

                    if (GameObject.FindGameObjectWithTag("Player") == null){
                        GameObject[] npcs = GameObject.FindGameObjectsWithTag("NPC");
                        for (int i = 0; i < npcs.Length; i++){
                            if (npcs[i].GetComponent<NPCBehavior>().hashid == "HannahSit"){
                                Destroy(npcs[i]);
                                break;
                            }
                        }
                        GameObject pl = Instantiate(Resources.Load("pPlayer") as GameObject, new Vector3(1.4809f, -2.877639f, 499f), transform.rotation);
                        pl.GetComponent<SpriteRenderer>().flipX = true;
                        Camera.main.GetComponent<CameraBehavior>().enabled = true;
                        Camera.main.GetComponent<CameraBehavior>().setFollow(pl);
                    }
                }

                if (intro_text1){
                    intro_text1 = false;
                    talk = true;
                    em.playEvent("IntroA");
                }
                else if (intro_text2){
                    if (GameManager.instance.save.getKey(2)){
                        GameObject[] npcs = GameObject.FindGameObjectsWithTag("NPC");
                        for (int i = 0; i < npcs.Length; i++){
                            if (npcs[i].GetComponent<NPCBehavior>().hashid == "WillSit"){
                                Destroy(npcs[i]);
                                break;
                            }
                        }

                        Instantiate(Resources.Load("pWill") as GameObject, new Vector3(-0.95f, -2.75f, 400f), transform.rotation);
                        npcs = GameObject.FindGameObjectsWithTag("NPC");
                        for (int i = 0; i < npcs.Length; i++){
                            if (npcs[i].GetComponent<NPCBehavior>().hashid == "Will"){
                                npcs[i].GetComponent<NPCFollowBehavior>().setFollow(GameObject.FindGameObjectWithTag("Player"));
                                break;
                            }
                        }

                        intro_text2 = false;
                        em.playEvent("IntroB");
                    }
                }
            }
        }
	}
}
