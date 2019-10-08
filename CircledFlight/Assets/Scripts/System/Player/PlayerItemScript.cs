using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class PlayerItemScript : MonoBehaviour {

	//Settings
    private SpriteRenderer sr;
    private Animator anim;

    void Awake() {
        sr = GetComponent<SpriteRenderer>();
        sr.enabled = false;
        anim = GetComponent<Animator>();
    }

    void Update() {
        Vector3 v3 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(v3.x, v3.y, -6f);
    }

    public void changeItem(int item){
        //Return null if item is empty
        sr.enabled = false;
        if (item == -1){
            return;
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
            }
        }
    }

}
