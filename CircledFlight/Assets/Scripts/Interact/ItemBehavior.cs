using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehavior : InteractableBehavior {

	//NPC
    [SerializeField] private int item;
    [SerializeField] private bool this_position = true;
    [SerializeField] private bool destroy_on_collect = true;
    [SerializeField] private string event_name = "";

    //Components
    private EventManager em;

    //Settings
    private bool pick_up_item;

	// Use this for initialization
	protected override void init() {
        //Interactable
        base.init();

        //Settings
        em = GetComponent<EventManager>();
		gameObject.tag = "Item";
        pick_up_item = false;
	}
	
	//Update Event
	protected override void step() {
        //Interactable
		base.step();

        //Item Behavior Event
        if (action){
            if (!em.isActive){
                em.playEvent(event_name);
                action = false;
                pick_up_item = true;
            }
        }

        if (pick_up_item){
            if (!em.isActive){
                pick_up_item = false;
                GameObject player_obj = GameObject.FindGameObjectWithTag("Player");
                if (!player_obj.GetComponent<PlayerBehavior>().addItem(item)){
                        em.playEvent("System/noinventoryspace");
                }
                else {
                    if (destroy_on_collect){
                        GameManager.instance.playSound("InspectItemSFX");
                        Destroy(this.gameObject);
                    }
                }
            }
        }
	}

    public override Vector2 getPosition() {
        Vector2 return_position = position;
        if (this_position){
            return_position = new Vector2(transform.position.x, transform.position.y);
        }
        return return_position;
    }

    //Debug Editor
    void OnDrawGizmosSelected() {
        if (Application.isEditor){
            if (!this_position){
                Gizmos.color = Color.red;
                Gizmos.DrawLine(new Vector3(position.x - 0.1f, position.y, 0), new Vector3(position.x + 0.1f, position.y, 0));
                Gizmos.DrawLine(new Vector3(position.x, position.y - 0.1f, 0), new Vector3(position.x, position.y + 0.1f, 0));
                Gizmos.DrawCube(new Vector3(position.x, position.y, 0), new Vector3(0.05f, 0.05f, 0.05f));
            }
        }
    }

}
