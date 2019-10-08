using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour {

    //Movement Settings
    private PathScript path;
    private float spd;

    private bool canmove;
    private bool cutscene;
    private bool moving;
    private int path_num;
    private Vector2[] path_array;

    //Select Settings
    private InteractableBehavior interact;

    //Inventory
    private int player_item;
    private GameObject playeritem_obj;
    private bool inventory_active;
    private GameObject[] inventory;

    //Animation
    private Animator anim;
    private SpriteRenderer sr;

    //Init Player
	void Awake () {
        //Grid
        path = gameObject.AddComponent(typeof(PathScript)) as PathScript;
        path.Grid = (Grid) GameObject.FindObjectOfType(typeof(Grid));

        //Movement Settings
        canmove = true;
        cutscene = false;
        moving = false;
		spd = 2f;
        path_num = 0;

        //Inventory
        player_item = -1;
        inventory_active = false;
        inventory = new GameObject[6];
        GameObject inventory_obj = (Resources.Load("Prefabs/pInvBlip")) as GameObject;
        for (int i = 0; i < inventory.Length; i++){
            GameObject temp_inv = Instantiate(inventory_obj, transform.position, transform.rotation);
            inventory[i] = temp_inv;
            inventory[i].transform.parent = transform;
            inventory[i].GetComponent<InventoryScript>().setItem(-1);
        }
        playeritem_obj = Instantiate((Resources.Load("Prefabs/pInvPlayer")) as GameObject, transform.position, transform.rotation);
        playeritem_obj.transform.parent = this.transform;

        //Animation
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
	}
	
	//Update Event
	void Update () {
        //Check Game Paused
        if (GameManager.instance.getPause()){
            anim.enabled = false;
            return;
        }
        else {
            anim.enabled = true;
        }

        //Movement
        if (canmove){
            if (Input.GetMouseButtonDown(0)){
                //Get Mouse Point
                Vector3 v3 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                bool position_found = false;
                Vector2 move_v2 = new Vector2(v3.x, v3.y);

                //Reset last Interact
                if (interact != null){
                    interact.selected = false;
                    interact = null;
                }

                //Check Inventory
                Vector2 move_click_v2 = new Vector2(move_v2.x, move_v2.y);
                if (Vector2.Distance(move_v2, new Vector2(transform.position.x, transform.position.y + 1.7f)) < 0.5f){
                    canmove = false;
                    inventory_active = true;
                    for (int q = 0; q < inventory.Length; q++){
                        inventory[q].GetComponent<InventoryScript>().setActive = true;
                        inventory[q].GetComponent<InventoryScript>().setAngle((Mathf.PI + (1.0471975512f * q)) + Random.Range(-0.4f, 0.4f) + 0.52359877559f);
                    }
                    Update();
                    return;
                }
                else {
                    //Check Point Items
                    RaycastHit2D[] click_ray = Physics2D.RaycastAll(new Vector2(v3.x, v3.y), Vector2.zero, Mathf.Infinity);
                    int importance = 10;
                    for (int i = 0; i < click_ray.Length; i++){
                        if (click_ray[i].collider != null){
                            bool higher_importance = false;
                            if (click_ray[i].collider.gameObject.tag == "Item"){
                                if (importance > 0){
                                    importance = 0;
                                    higher_importance = true;
                                }
                            }
                            else if (click_ray[i].collider.gameObject.tag == "NPC"){
                                if (importance > 1){
                                    importance = 1;
                                    higher_importance = true;
                                }
                            }
                            else if (click_ray[i].collider.gameObject.tag == "Banter"){
                                if (importance > 2){
                                    importance = 2;
                                    higher_importance = true;
                                }
                            }
                            else if (click_ray[i].collider.gameObject.tag == "Door"){
                                if (importance > 3){
                                    importance = 3;
                                    higher_importance = true;
                                }
                            }

                            if (higher_importance){
                                interact = click_ray[i].collider.gameObject.GetComponent<InteractableBehavior>();
                                interact.selected = true;
                                move_click_v2 = click_ray[i].collider.gameObject.GetComponent<InteractableBehavior>().getPosition();
                                position_found = true;
                            }
                        }
                    }
                }

                //Find point to move
                if (!position_found){
                    RaycastHit2D space_ray = Physics2D.CircleCast(new Vector2(v3.x, v3.y), (0.5f / 32f), Vector2.zero, Mathf.Infinity, path.Grid.spaceLayer, -Mathf.Infinity, Mathf.Infinity);
                    RaycastHit2D solid_ray = Physics2D.CircleCast(new Vector2(v3.x, v3.y), (0.5f / 32f), Vector2.zero, Mathf.Infinity, path.Grid.solidLayer, -Mathf.Infinity, Mathf.Infinity);
                    if ((space_ray.collider == null) || (solid_ray.collider != null)){
                        move_v2 = path.closestPoint(move_v2);
                    }
                }
                else {
                    move_v2 = move_click_v2;
                }

                //Set move path to point
                path_num = 0;
                path_array = path.getPath(new Vector2(transform.position.x, transform.position.y), move_v2);
                if (path_array == null){
                    path_array = new Vector2[1];
                    path_array[0] = new Vector2(transform.position.x, transform.position.y);
                }
                else {
                    path_array[0] = new Vector2(transform.position.x, transform.position.y);
                }
                moving = true;
            }
        }
        else {
            moving = false;
            if (cutscene){
                moving = true;
            }

            //Inventory Behavior
            if (inventory_active){
                if (Input.GetMouseButtonDown(0)){
                    Vector3 v3 = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                    int inventory_num = 0;
                    float inventory_dis = Vector2.Distance(new Vector2(inventory[0].transform.position.x, inventory[0].transform.position.y), new Vector2(v3.x, v3.y));
                    bool clicked_inventory = false;
                    for (int l = 0; l < inventory.Length; l++){
                        float new_dis = Vector2.Distance(new Vector2(inventory[l].transform.position.x, inventory[l].transform.position.y), new Vector2(v3.x, v3.y));
                        if (new_dis < 0.35f){
                            clicked_inventory = true;
                            if (new_dis < inventory_dis){
                                inventory_num = l;
                                inventory_dis = new_dis;
                            }
                        }
                    }

                    if (clicked_inventory){
                        int temp_swap = player_item;
                        player_item = inventory[inventory_num].GetComponent<InventoryScript>().itemnum;
                        playeritem_obj.GetComponent<PlayerItemScript>().changeItem(player_item);
                        inventory[inventory_num].GetComponent<InventoryScript>().setItem(temp_swap);
                    }
                    else {
                        canmove = true;
                        inventory_active = false;
                        for (int q = 0; q < inventory.Length; q++){
                            inventory[q].GetComponent<InventoryScript>().setActive = false;
                        }
                        if (Vector2.Distance(new Vector2(v3.x, v3.y), new Vector2(transform.position.x, transform.position.y + 1.7f)) >= 0.5f){
                            Update();
                            return;
                        }
                    }
                }
            }
        }

        if (moving){
            Vector2 current_pos = new Vector2(transform.position.x, transform.position.y);
            if (current_pos != path_array[path_array.Length - 1]){
                if (current_pos != path_array[path_num]){
                    current_pos = Vector2.MoveTowards(current_pos, path_array[path_num], spd * Time.deltaTime);
                    transform.position = new Vector3(current_pos.x, current_pos.y, transform.position.z);
                }
                else {
                    path_num++;
                }

                anim.Play("hannah_walk");
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
                if (interact != null){
                    if (interact.selected){
                        interact.action = true;
                        interact.selected = false;
                        interact = null;
                    }
                }
                anim.Play("hannah_idle");
            }
        }
        else {
            anim.Play("hannah_idle");
        }

        transform.position = new Vector3(transform.position.x, transform.position.y, (transform.position.y + 500));
	}

    //Inventory Methods
    public bool useItem(int item) {
        if (player_item == item){
            player_item = -1;
            playeritem_obj.GetComponent<PlayerItemScript>().changeItem(player_item);
            return true;
        }
        return false;
    }

    public void cleanItem() {
        for (int i = 0; i < inventory.Length; i++){
            if (inventory[i].GetComponent<InventoryScript>().itemnum == -1){
                inventory[i].GetComponent<InventoryScript>().setItem(player_item);
                break;
            }
        }
        player_item = -1;
        playeritem_obj.GetComponent<PlayerItemScript>().changeItem(player_item);
    }

    public void hideItem() {
        for (int q = 0; q < inventory.Length; q++){
            inventory[q].GetComponent<InventoryScript>().setActive = false;
        }
        inventory_active = false;
        canmove = true;
    }

    public bool addItem(int item) {
        int empty_space = -1;
        int spaces = 0;
        bool space_found = false;
        if (player_item != -1){
            spaces++;
        }
        for (int i = 0; i < inventory.Length; i++){
            if (inventory[i].GetComponent<InventoryScript>().itemnum == -1){
                if (!space_found){
                    empty_space = i;
                    space_found = true;
                }
            }
            else {
                spaces++;
            }
        }
        if (spaces >= inventory.Length) {
            return false;
        }

        if (empty_space != -1){
            inventory[empty_space].GetComponent<InventoryScript>().setItem(item);

            inventory_active = true;
            for (int q = 0; q < inventory.Length; q++){
                inventory[q].GetComponent<InventoryScript>().setActive = true;
                inventory[q].GetComponent<InventoryScript>().setAngle((Mathf.PI + (1.0471975512f * q)) + Random.Range(-0.4f, 0.4f) + 0.52359877559f);
            }
            canmove = false;
            return true;
        }

        return false;
    }

    public bool removeItem(int item) {
        bool found_space = false;
        for (int i = 0; i < inventory.Length; i++){
            if (inventory[i].GetComponent<InventoryScript>().itemnum == item){
                inventory[i].GetComponent<InventoryScript>().setItem(-1);
                found_space = true;
                break;
            }
        }
        return found_space;
    }

    public int[] allItem() {
        int[] inv_array = new int[inventory.Length];
        for (int i = 0; i < inventory.Length; i++){
            inv_array[i] = inventory[i].GetComponent<InventoryScript>().itemnum;
        }
        return inv_array;
    }

    //Player Settings
    public bool can_move {
        get {
            return canmove;
        }
        set {
            canmove = value;
        }
    }

    public bool inventory_act {
        get {
            return inventory_active;
        }
        set {
            inventory_active = value;
        }
    }

    public bool cut_scene {
        get {
            return cutscene;
        }
    }

    public void moveTo(Vector2 v2) {
        path_num = 0;
        path_array = path.getPath(new Vector2(transform.position.x, transform.position.y), v2);
        if (path_array == null){
            path_array = new Vector2[1];
            path_array[0] = new Vector2(transform.position.x, transform.position.y);
        }
        else {
            path_array[0] = new Vector2(transform.position.x, transform.position.y);
            moving = true;
            cutscene = true;
        }
    }

    //Debug
    void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        if (path_array != null){
            for (int i = 0; i < path_array.Length; i++){
                Gizmos.DrawSphere(new Vector3(path_array[i].x, path_array[i].y, transform.position.z), 0.05f);
            }
        }
    }
}
