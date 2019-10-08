using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class EventManager : MonoBehaviour {

    //Settings
    private List<ArrayList> event_data;
    private bool active;
    private int event_type;
    private int event_num;
    private string event_hash;
    private int choice_num;

    //Init
    void Awake() {
        //Settings
        active = false;
        event_type = -1;
        event_num = 0;
    }

    //Event Methods
    public void playEvent(string event_name){
        event_data = loadFile(event_name).getEvent();
        startEvent();
        if (event_data.Count > 0){
            eventHandler(event_data[0]);
        }
        else {
            endEvent();
        }
    }

    private void startEvent() {
        //Player Settings
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null){
            player.GetComponent<PlayerBehavior>().can_move = false;
            player.GetComponent<PlayerBehavior>().inventory_act = false;
            player.GetComponent<PlayerBehavior>().cleanItem();
        }

        //NPC Settings
        GameObject[] npc = GameObject.FindGameObjectsWithTag("NPC");
        for (int i = 0; i < npc.Length; i++){
            npc[i].GetComponent<NPCBehavior>().can_move = false;
        }

        //Event Reader Settings
        active = true;
        event_type = -1;
        event_num = 0;
        event_hash = null;
    }

    private void endEvent() {
        //Player Settings
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null){
            player.GetComponent<PlayerBehavior>().can_move = true;
        }

        //NPC Settings
        GameObject[] npc = GameObject.FindGameObjectsWithTag("NPC");
        for (int i = 0; i < npc.Length; i++){
            npc[i].GetComponent<NPCBehavior>().can_move = true;
        }

        //Event Reader Settings
        active = false;
    }

    private void eventHandler(ArrayList event_array) {
        event_type = (int) event_array[0];
        if (event_type == 0){
            //Text Event
            string text = (string) event_array[1];
            Vector2 v2 = (Vector2) event_array[2];
            string npc_hash = (string) event_array[3];
            Color text_color = Color.white;

            if (npc_hash == "player"){
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                v2 = new Vector2(player.transform.position.x, player.transform.position.y + 3.2f);
            }
            else {
                bool free = false;
                if (npc_hash.Substring(Mathf.Max(0, npc_hash.Length - 4)) == "free"){
                    free = true;
                    npc_hash = npc_hash.Substring(0, npc_hash.Length - 4);
                }

                GameObject[] check = GameObject.FindGameObjectsWithTag("NPC");
                for (int i = 0; i < check.Length; i++){
                    if (check[i].GetComponent<NPCBehavior>().hashid == npc_hash){
                        if (!free){
                            v2 = new Vector2(check[i].transform.position.x, check[i].transform.position.y + 3.2f);
                        }
                        text_color = check[i].GetComponent<NPCBehavior>().textcolor;
                        break;
                    }
                }
            }
            TextScript textbox = Instantiate((Resources.Load("Prefabs/Text/pText")) as GameObject, new Vector3(v2.x, v2.y, 0f), transform.rotation).GetComponent<TextScript>();

            textbox.colorContent = text_color;
            textbox.textContent = text;
            textbox.Start();
        }
        else if (event_type == 1){
            //Movement
            string npc_hash = (string) event_array[1];
            Vector2 v2 = (Vector2) event_array[2];
            event_hash = npc_hash;

            if (npc_hash == "player"){
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                player.GetComponent<PlayerBehavior>().moveTo(v2);
            }
            else {
                GameObject[] check = GameObject.FindGameObjectsWithTag("NPC");
                for (int i = 0; i < check.Length; i++){
                    if (check[i].GetComponent<NPCBehavior>().hashid == npc_hash){
                        check[i].GetComponent<NPCBehavior>().moveTo(v2);
                        break;
                    }
                }
            }
        }
        else if (event_type == 2){
            //Animation
        }
        else if (event_type == 3){
            //Camera
        }
        else if (event_type == 4){
            //Transition
            string scene_name = (string) event_array[1];
            int door_num = (int) event_array[2];
            string transitionA = (string) event_array[3];
            string transitionB = (string) event_array[4];

            GameManager.instance.transition(scene_name, door_num, transitionA, transitionB);
        }
        else if (event_type == 5){
            //Music
            string track_name = (string) event_array[1];
            bool loop_bool = (bool) event_array[2];

            if (loop_bool){
                GameManager.instance.playSoundLoop(track_name);
            }
            else {
                GameManager.instance.playSound(track_name);
            }

            event_num++;
            eventHandler(event_data[event_num]);
        }
        else if (event_type == 6){
            //Keys
            GameManager.instance.save.setKey((int) event_array[1], true);
            event_num++;
            eventHandler(event_data[event_num]);
        }
        else if (event_type == 7){
            //End
            endEvent();
        }
        else if (event_type == 8){
            //Skip
            int key = (int) event_array[1];
            int skip = (int) event_array[2];
            if (skip == 0){
                skip = 1;
            }

            if (GameManager.instance.save.getKey(key)){
                event_num += skip;
            }
            else {
                event_num++;
            }
            eventHandler(event_data[event_num]);
        }
        else if (event_type == 9){
            //Choice Event
            int choice_num = (int) event_array[1];
            string text = (string) event_array[2];
            string npc_hash = (string) event_array[9];
            Color text_color = Color.white;

            Vector2 v2 = Vector2.zero;
            GameObject[] check = GameObject.FindGameObjectsWithTag("NPC");
            for (int i = 0; i < check.Length; i++){
                if (check[i].GetComponent<NPCBehavior>().hashid == npc_hash){
                    v2 = new Vector2(check[i].transform.position.x, check[i].transform.position.y + 3.2f);
                    text_color = check[i].GetComponent<NPCBehavior>().textcolor;
                    break;
                }
            }

            ChoiceScript textbox = Instantiate((Resources.Load("Prefabs/Text/pChoice")) as GameObject, new Vector3(v2.x, v2.y, 0f), transform.rotation).GetComponent<ChoiceScript>();
            textbox.colorContent = text_color;
            textbox.textContent = text;

            if (choice_num == 2) {
                v2 = new Vector2(Camera.main.transform.position.x - 2.5f, Camera.main.transform.position.y - 3.5f);
                for (int i = choice_num - 1; i >= 0; i--){
                    SubChoiceScript choiceA = Instantiate((Resources.Load("Prefabs/Text/pSubChoice")) as GameObject, new Vector3(v2.x + (5f * i), v2.y, 0f), transform.rotation).GetComponent<SubChoiceScript>();
                    choiceA.textContent = (string) event_array[3 + i];
                    choiceA.choiceContent = (int) event_array[6 + i];
                    choiceA.setEvent = this.gameObject.GetComponent<EventManager>();
                    choiceA.choicescript = textbox;
                    choiceA.Start();
                }
            }
            else {
                v2 = new Vector2(Camera.main.transform.position.x - 4f, Camera.main.transform.position.y - 3.5f);
                for (int i = choice_num - 1; i >= 0; i--){
                    SubChoiceScript choiceA = Instantiate((Resources.Load("Prefabs/Text/pSubChoice")) as GameObject, new Vector3(v2.x + (4f * i), v2.y, 0f), transform.rotation).GetComponent<SubChoiceScript>();
                    choiceA.textContent = (string) event_array[3 + i];
                    choiceA.choiceContent = (int) event_array[6 + i];
                    choiceA.setEvent = this.gameObject.GetComponent<EventManager>();
                    choiceA.choicescript = textbox;
                    choiceA.Start();
                }
            }

            textbox.Start();
        }
    }

    //Update Event
    private void Update() {
        if (active){
            if (event_type != -1){
                if (event_type == 0){
                    int canvas_child_count = GameManager.instance.canvas.gameObject.transform.childCount;
                    if (canvas_child_count <= 0){
                        event_type = -1;
                        event_num++;
                        eventHandler(event_data[event_num]);
                    }
                }
                else if (event_type == 1){
                    if (event_hash == "player"){
                        GameObject player = GameObject.FindGameObjectWithTag("Player");
                        if (player.GetComponent<PlayerBehavior>().cut_scene == false){
                            event_hash = null;
                            event_type = -1;
                            event_num++;
                            eventHandler(event_data[event_num]);
                        }
                    }
                    else {
                        GameObject[] check = GameObject.FindGameObjectsWithTag("NPC");
                        for (int i = 0; i < check.Length; i++){
                            if (check[i].GetComponent<NPCBehavior>().hashid == event_hash){
                                if (check[i].GetComponent<NPCBehavior>().cut_scene == false){
                                    event_hash = null;
                                    event_type = -1;
                                    event_num++;
                                    eventHandler(event_data[event_num]);
                                    break;
                                }
                            }
                        }
                    }
                }
                else if (event_type == 3){

                }
                else if (event_type == 9){
                    int canvas_child_count = GameManager.instance.canvas.gameObject.transform.childCount;
                    if (canvas_child_count <= 0){
                        event_type = -1;
                        event_num = event_num + choice_num;
                        eventHandler(event_data[event_num]);
                    }
                }
            }
        }
    }

    //Set Choice
    public void setChoice(int choice) {
        choice_num = choice;
    }

    //Save & Load File Functionality
    public void saveFile(string file_name, EventFile file) {
        string json_save = JsonUtility.ToJson(file, true);
        string path = Application.streamingAssetsPath + "/Events/" + file_name + ".json";
        if (!File.Exists(path)){
            Directory.CreateDirectory(Application.dataPath + "/StreamingAssets/Events/");
        }
        File.WriteAllText (path, json_save);
        #if UNITY_EDITOR
        UnityEditor.AssetDatabase.Refresh();
        #endif
    }

    public EventFile loadFile(string file_name) {
        string path = Application.streamingAssetsPath + "/Events/" + file_name + ".json";
        if (File.Exists(path)){
            string json_save = File.ReadAllText(path);
            return JsonUtility.FromJson<EventFile>(json_save);
        }
        else {
            Debug.LogError("File \"" + file_name + "\" not found");
            return new EventFile();
        }
    }

    //Check Active
    public bool isActive {
        get {
            return active;
        }
    }

}
