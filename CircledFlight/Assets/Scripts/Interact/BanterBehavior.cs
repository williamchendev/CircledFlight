using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanterBehavior : InteractableBehavior {

	//Banter
    [SerializeField] private bool walk_over = false;
    [SerializeField] private string event_name = "";

    //Components
    private EventManager em;

	// Use this for initialization
	protected override void init() {
        //Interactable
        base.init();

        //Settings
        em = GetComponent<EventManager>();
		gameObject.tag = "Banter";
	}
	
	//Update Event
	protected override void step() {
        //Interactable
		base.step();

        //Banter Behavior Event
        if (action){
            if (!em.isActive){
                em.playEvent(event_name);
                action = false;
            }
        }
	}

    public override Vector2 getPosition() {
        Vector2 return_position = position;
        if (!walk_over){
            Vector3 player_v3 = GameObject.FindGameObjectWithTag("Player").transform.position;
            return_position = player_v3;
        }
        return return_position;
    }

    //Debug Editor
    void OnDrawGizmosSelected() {
        if (Application.isEditor){
            if (walk_over){
                Gizmos.color = Color.red;
                Gizmos.DrawLine(new Vector3(position.x - 0.1f, position.y, 0), new Vector3(position.x + 0.1f, position.y, 0));
                Gizmos.DrawLine(new Vector3(position.x, position.y - 0.1f, 0), new Vector3(position.x, position.y + 0.1f, 0));
                Gizmos.DrawCube(new Vector3(position.x, position.y, 0), new Vector3(0.05f, 0.05f, 0.05f));
            }
        }
    }

}
