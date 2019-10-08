using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneBehavior : MonoBehaviour {

    //Settings
    public int key_check;
    public string event_name = "";

    private EventManager em;
    private Collider2D collision;
    private bool playing;

	//Init Event
	void Start () {
        playing = false;
		em = GetComponent<EventManager>();
        collision = GetComponent<Collider2D>();

        if (key_check >= 0){
            if (GameManager.instance.save.getKey(key_check)){
                Destroy(gameObject);
            }
        }
	}
	
	//Update Event
	void Update () {
        if (!playing){
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null){
                if (player.GetComponent<PlayerBehavior>().can_move){
                    if (collision.bounds.Contains(new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z))){
                        playing = true;
                        GameManager.instance.save.setKey(key_check, true);
                        em.playEvent(event_name);
                    }
                }
            }
        }
        else {
		    if (!em.isActive){
                Destroy(gameObject);
            }
        }
	}
}
