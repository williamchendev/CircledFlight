using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class EventFile {

    [SerializeField] private List<int> event_data;
    [SerializeField] private List<string> event_string;
    [SerializeField] private List<Vector3> event_vector;

    //Init
    public EventFile() {
        //Debug
        event_data = new List<int>();
        event_string = new List<string>();
        event_vector = new List<Vector3>();
    }

    //Return Readable Array of Event Data
    public List<ArrayList> getEvent(){
        List<ArrayList> list = new List<ArrayList>();
        int i = 0;
        while (i < event_data.Count) {
            ArrayList new_event = new ArrayList();
            new_event.Add(event_data[i]);
            if (event_data[i] == 0){
                //Textbox
                new_event.Add(event_string[event_data[i + 1]]); //Text String
                new_event.Add(new Vector2(event_vector[event_data[i + 2]].x, event_vector[event_data[i + 2]].y)); //Text Position
                new_event.Add(event_string[event_data[i + 3]]); //NPC HashID
                i = i + 4;
            }
            else if (event_data[i] == 1){
                //Movement
                new_event.Add(event_string[event_data[i + 1]]); //NPC HashID
                new_event.Add(new Vector2(event_vector[event_data[i + 2]].x, event_vector[event_data[i + 2]].y)); //Move Position
                i = i + 3;
            }
            else if (event_data[i] == 2){
                //Animation
                new_event.Add(event_string[event_data[i + 1]]); //NPC HashID
                new_event.Add(event_string[event_data[i + 2]]); //Animation Name
                i = i + 3;
            }
            else if (event_data[i] == 3){
                //Camera Movement
                new_event.Add(new Vector2(event_vector[event_data[i + 1]].x, event_vector[event_data[i + 1]].y)); //Move Position
                i = i + 2;
            }
            else if (event_data[i] == 4){
                //Transition
                new_event.Add(event_string[event_data[i + 1]]); //Scene Name
                new_event.Add(event_data[i + 2]); //Door Num
                new_event.Add(event_string[event_data[i + 3]]); //TransitionA Name
                new_event.Add(event_string[event_data[i + 4]]); //TransitionB Name
                i = i + 5;
            }
            else if (event_data[i] == 5){
                //Play Music
                new_event.Add(event_string[event_data[i + 1]]); //Track Name
                if (event_data[i + 2] == 0){
                    new_event.Add(false); //Loop Bool
                }
                else {
                    new_event.Add(true); //Loop Bool
                }
                i = i + 3;
            }
            else if (event_data[i] == 6){
                //Save Key
                new_event.Add(event_data[i + 1]); //Key Num
                i = i + 2;
            }
            else if (event_data[i] == 7){
                //End Event
                list.Add(new_event);
                break;
            }
            else if (event_data[i] == 8){
                //Skip Event
                new_event.Add(event_data[i + 1]);
                new_event.Add(event_data[i + 2]);
                i = i + 3;
            }
            else if (event_data[i] == 9){
                //Skip Event
                new_event.Add(event_data[i + 1]);
                new_event.Add(event_string[event_data[i + 2]]);
                new_event.Add(event_string[event_data[i + 3]]);
                new_event.Add(event_string[event_data[i + 4]]);
                new_event.Add(event_string[event_data[i + 5]]);
                new_event.Add(event_data[i + 6]);
                new_event.Add(event_data[i + 7]);
                new_event.Add(event_data[i + 8]);
                new_event.Add(event_string[event_data[i + 9]]);
                i = i + 10;
            }
            list.Add(new_event);
        }
        return list;
    }

    //Event Add Methods 
    public void addText(string text, Vector2 position, string hashid){
        event_data.Add(0);
        event_data.Add(event_string.Count);
        event_string.Add(text);
        event_data.Add(event_vector.Count);
        event_vector.Add(new Vector3(position.x, position.y, 0f));
        event_data.Add(event_string.Count);
        event_string.Add(hashid);
    }

    public void addMovement(string hashid, Vector2 position){
        event_data.Add(1);
        event_data.Add(event_string.Count);
        event_string.Add(hashid);
        event_data.Add(event_vector.Count);
        event_vector.Add(new Vector3(position.x, position.y, 0f));
    }

    public void addAnimation(string hashid, string anim_name){
        event_data.Add(2);
        event_data.Add(event_string.Count);
        event_string.Add(hashid);
        event_data.Add(event_string.Count);
        event_string.Add(anim_name);
    }

    public void addCameraMove(Vector2 position){
        event_data.Add(3);
        event_data.Add(event_vector.Count);
        event_vector.Add(new Vector3(position.x, position.y, 0f));
    }

    public void addTransition(string scene_name, int door_num, string transitionA_name, string transitionB_name){
        event_data.Add(4);
        event_data.Add(event_string.Count);
        event_string.Add(scene_name);
        event_data.Add(door_num);
        event_data.Add(event_string.Count);
        event_string.Add(transitionA_name);
        event_data.Add(event_string.Count);
        event_string.Add(transitionB_name);
    }

    public void addMusic(string track_name, bool loop){
        event_data.Add(5);
        event_data.Add(event_string.Count);
        event_string.Add(track_name);
        if (loop){
            event_data.Add(1);
        }
        else {
            event_data.Add(0);
        }
    }

    public void addKey(int key) {
        event_data.Add(6);
        event_data.Add(key);
    }

    public void addEnd() {
        event_data.Add(7);
    }

    public void addSkip(int key, int skip) {
        event_data.Add(8);
        event_data.Add(key);
        event_data.Add(skip);
    }

    public void addChoice(string hashid, string text, string choiceA, int skipA, string choiceB, int skipB) {
        event_data.Add(9);
        event_data.Add(2);
        event_data.Add(event_string.Count);
        event_string.Add(text);
        event_data.Add(event_string.Count);
        event_string.Add(choiceB);
        event_data.Add(event_string.Count);
        event_string.Add(choiceA);
        event_data.Add(event_string.Count);
        event_string.Add("");
        event_data.Add(skipB);
        event_data.Add(skipA);
        event_data.Add(-1);
        event_data.Add(event_string.Count);
        event_string.Add(hashid);
    }

    public void addChoice(string hashid, string text, string choiceA, int skipA, string choiceB, int skipB, string choiceC, int skipC) {
        event_data.Add(9);
        event_data.Add(3);
        event_data.Add(event_string.Count);
        event_string.Add(text);
        event_data.Add(event_string.Count);
        event_string.Add(choiceC);
        event_data.Add(event_string.Count);
        event_string.Add(choiceB);
        event_data.Add(event_string.Count);
        event_string.Add(choiceA);
        event_data.Add(skipC);
        event_data.Add(skipB);
        event_data.Add(skipA);
        event_data.Add(event_string.Count);
        event_string.Add(hashid);
    }

}
