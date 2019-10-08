using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableBehavior : InteractableInterface {

    //Components
    protected SpriteOutline outline;
    protected SpriteRenderer sr;
    protected Collider2D collider2d;

    //Settings
    [HideInInspector] public bool selected;
    [HideInInspector] public bool action;

	//Init
	protected void Awake () {
        gameObject.AddComponent<PixelSnap>();
        init();
	}
	
	//Update Event
	protected void Update () {
        step();
        transform.position = new Vector3(transform.position.x, transform.position.y, (transform.position.y + 500));
	}

    protected override void init() {
        //Components
        sr = GetComponent<SpriteRenderer>();
        outline = gameObject.AddComponent<SpriteOutline>();
        if (GetComponent<Collider2D>() != null){
            collider2d = GetComponent<Collider2D>();
        }
        else {
            collider2d = (Collider2D) gameObject.AddComponent<PolygonCollider2D>();
        }

        //Outline
        sr.material = (Material) Resources.Load ("Materials/PixelPerfectOutline", typeof(Material));
        outline.OutlineColor = new Color(1, 1, 1, 0);

        //Settings
        selected = false;
        action = false;
    }

    protected override void step(){
        Vector3 v3 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        GameObject player_obj = GameObject.FindGameObjectWithTag("Player");

        if (player_obj != null){
            PlayerBehavior player = player_obj.GetComponent<PlayerBehavior>();

            outline.OutlineColor = new Color(1, 1, 1, 0);
            if (player.can_move){
                if (selected){
                    outline.OutlineColor = new Color(1, 1, 1, 0.8f);
                }
                else {
                    if (collider2d.OverlapPoint(new Vector2(v3.x, v3.y))) {
                        outline.OutlineColor = new Color(1, 1, 1, 0.8f);
                    }
                }
            }
            else {
                if (player.inventory_act){
                    if (collider2d.OverlapPoint(new Vector2(v3.x, v3.y))) {
                        outline.OutlineColor = new Color(1, 1, 1, 0.8f);
                    }
                }
            }
        }
    }

    //Get Position
    public override Vector2 getPosition() {
        return position;
    }

}
