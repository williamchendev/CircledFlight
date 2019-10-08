using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : InteractableBehavior {

	//NPC
    [SerializeField] private int door_id;
    [SerializeField] private string scene_name;
    [SerializeField] private bool highlight = true;

    //Settings
    private int[] player_items;
    private int facing;
    private bool transition;

	// Use this for initialization
	protected override void init() {
        //Interactable
        base.init();

        //Settings
		gameObject.tag = "Door";
        transition = false;
	}
	
	//Update Event
	protected override void step() {
        //Interactable
		if (highlight){
            base.step();
        }

        //Item Behavior Event
        if (action){
            if (!transition){
                transition = true;
                GameManager.instance.transition(scene_name, door_id, "TransitionA", "TransitionB");
            }
        }
	}

    public int door {
        get {
            return door_id;
        }
    }

    //Debug Editor
    void OnDrawGizmosSelected() {
        if (Application.isEditor){
            Gizmos.color = Color.red;
            Gizmos.DrawLine(new Vector3(position.x - 0.1f, position.y, 0), new Vector3(position.x + 0.1f, position.y, 0));
            Gizmos.DrawLine(new Vector3(position.x, position.y - 0.1f, 0), new Vector3(position.x, position.y + 0.1f, 0));
            Gizmos.DrawCube(new Vector3(position.x, position.y, 0), new Vector3(0.05f, 0.05f, 0.05f));
        }
    }

}
