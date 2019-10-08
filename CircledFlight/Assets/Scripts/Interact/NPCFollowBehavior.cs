using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCFollowBehavior : NPCBehavior {

	//NPC
    [SerializeField] private string[] event_cycle_eventnames = new string[0];

    [Header("Follow Settings")]
    
    [SerializeField] private GameObject follow;
    [SerializeField] private float move_spd;
    [SerializeField] private float range_width;
    [SerializeField] private float range_height;
    [SerializeField] private string idle_anim;
    [SerializeField] private string walk_anim;

    //Animation
    private Animator anim;

    //Settings
    private int counter;
    private bool range_shrink;

	//Init
	protected override void init() {
        //Interactable
        base.init();
        
        if (event_cycle_eventnames.Length != 0){
            event_cycle = true;
            event_names = event_cycle_eventnames;
        }

        //Movement Settings
        canmove = true;
        spd = move_spd;

        //Animation
        anim = GetComponent<Animator>();

        //Settings
        counter = 0;
        range_shrink = false;
	}

    //Step
    protected override void step() {
        //Movement
        if (!cutscene){
            if (canmove){
                if (follow != null){
                    counter--;
                    if (counter <= 0){
                        Vector2 follow_pos;
                        Vector2 ellipse_dim;
                        if (range_shrink){
                            follow_pos = new Vector2(follow.transform.position.x, follow.transform.position.y);
                            ellipse_dim = new Vector2(range_width * 0.7f, range_height * 0.7f);
                        }
                        else {
                            follow_pos = new Vector2(follow.transform.position.x, follow.transform.position.y);
                            ellipse_dim = new Vector2(range_width, range_height);
                        }

                        bool follow_range = inRange(follow_pos, ellipse_dim);
                        if (!follow_range){
                            moveToFollow(follow_pos);
                            if (!range_shrink) {
                                range_shrink = true;
                            }
                        }
                        else {
                            if (moving){
                                moveToFollow(new Vector2(transform.position.x, transform.position.y));
                                moving = false;
                            }

                            if (range_shrink) {
                                range_shrink = false;
                            }
                        }

                        counter = 3;
                    }
                }
            }
        }

        //Set Talk Position
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        position = new Vector2(player.transform.position.x, player.transform.position.y);

        base.step();

        //Animation
        if (moving){
            anim.Play(walk_anim);
        }
        else {
            anim.Play(idle_anim);
        }
    }

    //Functions & Methods
    private bool inRange(Vector2 position, Vector2 ellipse_dimensions) {
        float xComponent = Mathf.Pow(position.x - transform.position.x, 2) / Mathf.Pow(ellipse_dimensions.x, 2); 
        float yComponent = Mathf.Pow(position.y - transform.position.y, 2) / Mathf.Pow(ellipse_dimensions.y, 2); 
        float value = xComponent + yComponent;

        if (value < 1) {
            return true;
        }
        return false;
    }

    public void moveToFollow(Vector2 v2){
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
        moving = true;
    }

    public void setFollow(GameObject new_follow) {
        follow = new_follow;
    }

    //Debug
    void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        if (path_array != null){
            for (int i = 0; i < path_array.Length; i++){
                Gizmos.DrawSphere(new Vector3(path_array[i].x, path_array[i].y, transform.position.z), 0.05f);
            }
        }

        //Large Radius
        Gizmos.color = Color.blue;
        float el_x = transform.position.x + range_width * Mathf.Cos(0);
        float el_y = transform.position.y + range_height * Mathf.Sin(0);
        for (int i = 15; i <= 360; i += 15){
            float el_rad = Mathf.Deg2Rad * i;
            float el_x2 = transform.position.x + range_width * Mathf.Cos(el_rad);
            float el_y2 = transform.position.y + range_height * Mathf.Sin(el_rad);

            Gizmos.DrawLine(new Vector3(el_x, el_y, transform.position.z), new Vector3(el_x2, el_y2, transform.position.z + 1));

            el_x = el_x2;
            el_y = el_y2;
        }

        //Smaller Radius
        Gizmos.color = Color.red;
        el_x = transform.position.x + (range_width * 0.7f) * Mathf.Cos(0);
        el_y = transform.position.y + (range_height * 0.7f) * Mathf.Sin(0);
        for (int i = 15; i <= 360; i += 15){
            float el_rad = Mathf.Deg2Rad * i;
            float el_x2 = transform.position.x + (range_width * 0.7f) * Mathf.Cos(el_rad);
            float el_y2 = transform.position.y + (range_height * 0.7f) * Mathf.Sin(el_rad);

            Gizmos.DrawLine(new Vector3(el_x, el_y, transform.position.z), new Vector3(el_x2, el_y2, transform.position.z + 1));

            el_x = el_x2;
            el_y = el_y2;
        }
    }

}