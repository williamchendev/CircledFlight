using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBehavior : InteractableBehavior {

    //NPC
    [SerializeField] protected string hash = "new_npc_hash";
    [SerializeField] protected Color color = Color.red;
    [SerializeField] protected string event_name = "";

    protected bool event_cycle;
    protected string[] event_names;
    protected int questitemA;
    protected string questevent_nameA;
    protected int questitemB;
    protected string questevent_nameB;
    protected int questitemC;
    protected string questevent_nameC;

    //Components
    protected EventManager em;
    protected PathScript path;

    //Settings
    protected float spd;
    protected bool canmove;
    protected bool moving;
    protected bool cutscene;
    protected int path_num;
    protected Vector2[] path_array;

    //Events
    protected int event_num;

	//Init
	protected override void init() {
        //Interactable
        base.init();

        //Settings
        em = GetComponent<EventManager>();
		gameObject.tag = "NPC";

        //Grid
        path = gameObject.AddComponent(typeof(PathScript)) as PathScript;
        path.Grid = (Grid) GameObject.FindObjectOfType(typeof(Grid));

        //Movement Settings
        canmove = false;
        cutscene = false;
        moving = false;
		spd = 1.8f;
        path_num = 0;

        //Events
        event_num = 0;

        event_cycle = false;
        event_names = new string[0];
        questitemA = -1;
        questevent_nameA = "";
        questitemB = -1;
        questevent_nameB = "";
        questitemC = -1;
        questevent_nameC = "";
	}
	
	//Update Event
	protected override void step() {
        //Interactable
		base.step();

        //NPC Behavior Event
        if (action){
            if (!em.isActive){
                PlayerBehavior player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehavior>();
                if (player.transform.position.x > transform.position.x) {
                    player.GetComponent<SpriteRenderer>().flipX = true;
                }
                else {
                    player.GetComponent<SpriteRenderer>().flipX = false;
                }

                if (player.useItem(-1)){
                    if (event_cycle){
                        if (event_num >= event_names.Length) {
                            event_num = 0;
                        }
                        em.playEvent(event_names[event_num]);
                        event_num++;
                    }
                    else {
                        em.playEvent(event_name);
                    }
                }
                else {
                    if (player.useItem(questitemA)){
                        em.playEvent(questevent_nameA);
                    }
                    else if (player.useItem(questitemB)){
                        em.playEvent(questevent_nameB);
                    }
                    else if (player.useItem(questitemC)){
                        em.playEvent(questevent_nameC);
                    }
                    else {
                        em.playEvent("System/cantuseitemonperson");
                    }
                }
                
                action = false;
            }
        }

        if (!canmove){
            moving = false;
            if (cutscene){
                moving = true;
            }
        }

        //NPC Movement
        if (moving){
            movement();
        }
	}

    //NPC Movement Methods
    public void moveTo(Vector2 v2){
        //Set move path to point
        path_num = 0;
        path_array = path.getPath(new Vector2(transform.position.x, transform.position.y), v2);
        if (path_array == null){
            path_array = new Vector2[1];
            path_array[0] = new Vector2(transform.position.x, transform.position.y);
        }
        else {
            path_array[0] = new Vector2(transform.position.x, transform.position.y);
        }
        cutscene = true;
        moving = true;
    }

    public void movement() {
        Vector2 current_pos = new Vector2(transform.position.x, transform.position.y);
        if (current_pos != path_array[path_array.Length - 1]){
            if (current_pos != path_array[path_num]){
                current_pos = Vector2.MoveTowards(current_pos, path_array[path_num], spd * Time.deltaTime);
                transform.position = new Vector3(current_pos.x, current_pos.y, transform.position.z);
            }
            else {
                path_num++;
                movement();
            }

            if (transform.position.x < path_array[path_num].x){
                sr.flipX = false;
            }
            else if (transform.position.x > path_array[path_num].x){
                sr.flipX = true;
            }
        }
        else {
            moving = false;
            cutscene = false;
        }
    }

    public bool can_move {
        get {
            return canmove;
        }
        set {
            canmove = value;
        }
    }

    public bool cut_scene {
        get {
            return cutscene;
        }
    }

    //Get NPC data
    public string hashid {
        get {
            return hash;
        }
    }

    public Color textcolor {
        get {
            return color;
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
