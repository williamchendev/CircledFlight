using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class InventoryScript : MonoBehaviour {

    //Component
    private SpriteRenderer sr;

    //Item Settings
    private int item;
    private string item_name;
    private GameObject inventory_anim;

    //Draw Settings
    private float angle;
    private float length_val;
    private float length;

    private float spd;
    private float radius;
    private float sin_val;
    private float rot_val;

    private bool active;

	//Init
	void Awake () {
        //Settings
        sr = GetComponent<SpriteRenderer>();

        angle = UnityEngine.Random.Range(0f, 1f) * (2 * Mathf.PI);
        length_val = 0;
        length = 1f;

        spd = UnityEngine.Random.Range(0.8f, 1.2f) * 0.01f;
        radius = 0.07f;
        sin_val = UnityEngine.Random.Range(0f, 1f);
        rot_val = UnityEngine.Random.Range(0f, 1f);

        active = false;
        sr.color = new Color(1f, 1f, 1f, 0f);

        //GUI settings
        inventory_anim = Instantiate((Resources.Load("Prefabs/pInvAnim")) as GameObject, transform.position, transform.rotation);
        inventory_anim.AddComponent<InventoryItemScript>();
        inventory_anim.transform.parent = this.transform;
	}
	
	//Update
	void Update () {
		//Update Draw Values
        sin_val += 0.004f;
        if (sin_val >= 1){
            sin_val = 0;
        }
        float draw_sin = Mathf.Sin(sin_val * 2 * Mathf.PI);
        rot_val += spd;
        if (rot_val > (2 * Mathf.PI)){
            rot_val = 0;
        }

        //Calculate Position
        float alpha = sr.color.a;
        if (active){
            if (length_val < length){
                length_val += length * 0.06f;
            }
            if (alpha < 1){
                alpha += 0.035f;
            }
        }
        else {
            if (length_val > 0){
                length_val -= length * 0.06f;
            }
            if (alpha > 0){
                alpha -= 0.035f;
            }
        }
        length_val = Mathf.Clamp(length_val, 0, length);
        alpha = Mathf.Clamp(alpha, 0, 1);
        sr.color = new Color(1, 1, 1, alpha);
        Vector2 temp_position = new Vector2(transform.parent.transform.position.x + (Mathf.Cos(angle) * length_val), (transform.parent.transform.position.y + 1.7f) + (Mathf.Sin(angle) * length_val));

        //Set Position
        transform.position = new Vector3(temp_position.x + (Mathf.Cos(rot_val) * (radius * draw_sin)), temp_position.y + (Mathf.Sin(rot_val) * (radius * draw_sin)), -5f);
        inventory_anim.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.01f);
        inventory_anim.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, Mathf.Pow(sr.color.a, 2f));
	}

    //Public Methods
    public void setAngle(float new_angle){
        angle = new_angle;
    }

    public bool setActive {
        set {
            active = value;
        }
    }

    public int itemnum {
        get {
            return item;
        }
    }

    public string itemname {
        get {
            return item_name;
        }
    }

    public void setItem(int new_item) {
        item = new_item;
        item_name = inventory_anim.GetComponent<InventoryItemScript>().changeItem(new_item);
    }

}

public class InventoryItemScript : MonoBehaviour {

    //Settings
    private SpriteRenderer sr;
    private Animator anim;

    void Awake() {
        sr = GetComponent<SpriteRenderer>();
        sr.enabled = false;
        anim = GetComponent<Animator>();
    }

    public string changeItem(int item){
        //Return null if item is empty
        sr.enabled = false;
        if (item == -1){
            return null;
        }

        //Get Animation Names
        List<string> animation_names = new List<string>();
        foreach (AnimationClip q in anim.runtimeAnimatorController.animationClips){
            animation_names.Add(q.name);
        }

        for (int i = 0; i < animation_names.Count; i++){
            int number = Int32.Parse(Regex.Match(animation_names[i], @"\d+").Value);
            if (number == item){
                sr.enabled = true;
                anim.Play(animation_names[i]);
                string return_string = Regex.Replace(animation_names[i], @"\d", "");
                return return_string.Replace("_", " ");
            }
        }
        return null;
    }

}