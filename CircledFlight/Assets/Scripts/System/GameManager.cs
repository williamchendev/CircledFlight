
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    //Settings
    private static GameManager manager;
    private Canvas uicanvas;

    //Save System
    private SaveFile save_file;

    //Audio Management
    private List<AudioSource> aus;
    private AudioSource aus_loop;
    private AudioSource aus_ambient;
    private float volume;
    private float audiofade;
    
    //Player Scene Management
    private bool init_scene;
    private int[] player_inventory;
    private bool player_facing;
    private int player_door;

    //Transition Management
    private bool transitioning;
    private Transition transitionA;
    private string transitionB;
    private string transition_scene;

    //Pause Menu
    private bool pause_check;
    private bool pause_delay;
    private GameObject pause;

	//Init Manager
	void Awake () {
        //Set Resolution
        //Screen.SetResolution(1920, 1080, true, 60);

		//Create Save
        save_file = new SaveFile();

        //Create Manager
        bool exists = (GameObject.Find("GameManager") != true);
        if (exists){
            gameObject.name = "GameManager";
            DontDestroyOnLoad(this.gameObject);
            manager = gameObject.GetComponent<GameManager>();
        }
        else {
            Destroy(this.gameObject);
            return;
        }

        //Audio Manager
        aus = new List<AudioSource>();
        aus_loop = gameObject.AddComponent<AudioSource>();
        aus_loop.loop = true;
        aus_ambient = gameObject.AddComponent<AudioSource>();
        aus_ambient.loop = true;
        volume = 0.7f;
        audiofade = 0f;

        //Transition Manager
        transitioning = false;
        transitionB = null;
        transition_scene = null;

        //Settings
        uicanvas = GetComponentInChildren<Canvas>();
        init_scene = false;

        //Pause Menu
        pause_check = false;
        pause_delay = false;
        pause = Instantiate(Resources.Load("UI/pUIMenu") as GameObject, transform.position, transform.rotation);
        pause.transform.parent = transform;
        pause.transform.GetChild(0).position = new Vector3(Camera.main.transform.position.x - 7f, Camera.main.transform.position.y + 3.75f, -8f);
        pause.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.6f);
        pause.transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        pause.transform.GetChild(1).transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        pause.transform.GetChild(2).gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
	}

    //Scene Load Event
    void OnEnable () {
      SceneManager.sceneLoaded += OnSceneLoaded;
    }
 
    void OnDisable () {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
 
    private void OnSceneLoaded (Scene scene, LoadSceneMode mode) {
        if (init_scene){
            //Init Player
            Vector2 player_position = Vector2.zero;
            GameObject[] doors = GameObject.FindGameObjectsWithTag("Door");
            if (doors.Length > 0){
                for (int d = 0; d < doors.Length; d++){
                    if (doors[d].GetComponent<DoorScript>().door == player_door){
                        player_position = doors[d].GetComponent<InteractableBehavior>().getPosition();
                        break;
                    }
                }
            }

            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player == null){
                player = Instantiate((Resources.Load("pPlayer")) as GameObject, new Vector3(player_position.x, player_position.y, 0f), (((Resources.Load("pPlayer")) as GameObject).transform.rotation));
            }
            for (int i = 0; i < player_inventory.Length; i++){
                player.GetComponent<PlayerBehavior>().addItem(player_inventory[i]);
            }
            player.transform.position = new Vector3(player_position.x, player_position.y, 0f);
            player.GetComponent<SpriteRenderer>().flipX = player_facing;
            player.GetComponent<PlayerBehavior>().hideItem();

            //Find Will and put him next to Hannah
            GameObject[] npcs = GameObject.FindGameObjectsWithTag("NPC");
            for (int i = 0; i < npcs.Length; i++){
                if (npcs[i].GetComponent<NPCBehavior>().hashid == "Will"){
                    if (npcs[i].GetComponent<NPCFollowBehavior>() != null){
                        npcs[i].transform.position = new Vector3(player_position.x, player_position.y + 0.0001f, 0);
                        npcs[i].GetComponent<NPCFollowBehavior>().setFollow(player.gameObject);
                        npcs[i].GetComponent<SpriteRenderer>().flipX = player_facing;
                    }
                    break;
                }
            }

            //Center Camera
            if (Camera.main.GetComponent<CameraBehavior>() != null){
                Camera.main.GetComponent<CameraBehavior>().recenterCam();
            }

            //Init Transition
            if (transitionB != null){
                Instantiate((Resources.Load("Prefabs/transitions/p" + transitionB)) as GameObject, new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z + 1.5f), transform.rotation);
                transitioning = false;
                transitionB = null;
            }

            //End Init
            init_scene = false;
        }

        uicanvas.worldCamera = Camera.main;
    }

    //Update Event
    void Update () {
        //Transition Checks
        if (transitioning) {
            if (transitionA.end){
                transitioning = false;
                changeScene(transition_scene);
            }
            audiofade = Mathf.Lerp(audiofade, 0, Mathf.Pow(Time.deltaTime * 0.8f, 0.8f));
            if (audiofade < 0.15f){
                audiofade = 0;
                aus_loop.Pause();
                aus_ambient.Stop();
                for (int a = 0; a < aus.Count; a++){
                    AudioSource aus_remove = aus[a];
                    aus.Remove(aus_remove);
                    Destroy(aus_remove);
                }
            }
        }
        else {
            audiofade = Mathf.Lerp(audiofade, 1, Mathf.Pow(Time.deltaTime * 0.8f, 1.05f));
            if (audiofade > 0.99f){
                audiofade = 1;
            }
        }

        //Pause Game
        if (Input.GetMouseButtonDown(0)){
            Vector3 v2 = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
            if (Vector2.Distance(v2, new Vector2(Camera.main.transform.position.x - 7f, Camera.main.transform.position.y + 3.75f)) < 0.35f){
                if (!pause_check){
                    pause_check = true;
                }
                else {
                    pause_delay = true;
                }
            }
            else if (Vector2.Distance(v2, new Vector2(Camera.main.transform.position.x - 3.9f, Camera.main.transform.position.y + 3.75f)) < 0.25f) {
                if (pause_check){
                    Application.Quit();
                }
            }
            else {
                if (Mathf.Abs(v2.x - (Camera.main.transform.position.x - 4.9f)) < 0.5){
                    if (Mathf.Abs(v2.y - (Camera.main.transform.position.y + 3.81f)) < 0.25){
                        int vol_change = (int) (Mathf.Round(((v2.x - (Camera.main.transform.position.x - 4.9f)) * 1.05f) * 10)) + 5;
                        volume = Mathf.Clamp((vol_change / 10.0f), 0, 1);
                    }
                }
            }
        }

        //Audio Volume
        if (Input.GetKeyDown(KeyCode.UpArrow)){
            volume += 0.1f;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow)){
            volume -= 0.1f;
        }
        volume = Mathf.Clamp(volume, 0, 1);
        aus_loop.volume = volume * audiofade * 0.5f;
        aus_ambient.volume = (volume * audiofade) * 0.15f;
        for (int a = 0; a < aus.Count; a++){
            aus[a].volume = volume * audiofade;
            if (!aus[a].isPlaying){
                AudioSource aus_remove = aus[a];
                aus.Remove(aus_remove);
                Destroy(aus_remove);
            }
        }
	}

    //Pause Functions
    void LateUpdate () {
        //Pause Menu
        pause.transform.GetChild(0).position = new Vector3(Camera.main.transform.position.x - 7f, Camera.main.transform.position.y + 3.75f, -8f);
        pause.transform.GetChild(1).position = new Vector3(Camera.main.transform.position.x - 6f, Camera.main.transform.position.y + 3.78f, -8f);
        pause.transform.GetChild(1).transform.GetChild(0).transform.position = new Vector3(Camera.main.transform.position.x - 4.9f, Camera.main.transform.position.y + 3.81f, -8f);
        pause.transform.GetChild(2).position = new Vector3(Camera.main.transform.position.x - 3.9f, Camera.main.transform.position.y + 3.75f, -8f);

        if (pause_delay){
            pause_delay = false;
            pause_check = false;
        }

        int vol_string = (int) Mathf.Round(volume * 10);
        pause.transform.GetChild(1).transform.GetChild(0).gameObject.GetComponent<Animator>().Play("volume" + vol_string);

        if (pause_check){
            pause.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1f);
            pause.transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, Mathf.Lerp(pause.transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().color.a, 1, Time.deltaTime * 5f));
            pause.transform.GetChild(1).transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, Mathf.Lerp(pause.transform.GetChild(1).transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color.a, 1, Time.deltaTime * 5f));
            pause.transform.GetChild(2).gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, Mathf.Lerp(pause.transform.GetChild(2).gameObject.GetComponent<SpriteRenderer>().color.a, 1, Time.deltaTime * 5f));
        }
        else {
            pause.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.6f);
            pause.transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, Mathf.Lerp(pause.transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().color.a, 0, Time.deltaTime * 5f));
            pause.transform.GetChild(1).transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, Mathf.Lerp(pause.transform.GetChild(1).transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color.a, 0, Time.deltaTime * 5f));
            pause.transform.GetChild(2).gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, Mathf.Lerp(pause.transform.GetChild(2).gameObject.GetComponent<SpriteRenderer>().color.a, 0, Time.deltaTime * 5f));
        }
    }

    public bool getPause() {
        //Check if paused
        return pause_check;
    }

    //Scene Management
    public void changeScene (string name){
        init_scene = true;
        SceneManager.LoadScene(name, LoadSceneMode.Single);
    }

    public void transition(string scene_name, int door_num, string transition_nameA, string transition_nameB) {
        transitioning = true;
        transitionA = Instantiate((Resources.Load("Prefabs/transitions/p" + transition_nameA)) as GameObject, new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z + 1.5f), transform.rotation).GetComponent<Transition>();
        transitionB = transition_nameB;
        transition_scene = scene_name;

        PlayerBehavior player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehavior>();
        player.cleanItem();
        player_inventory = player.allItem();
        player_facing = player.gameObject.GetComponent<SpriteRenderer>().flipX;
        player_door = door_num;
        player.can_move = false;
    }

    //Audio Management
    public void playSound(string soundname){
        AudioSource new_aus = gameObject.AddComponent<AudioSource>();
        new_aus.volume = volume * audiofade;
        new_aus.clip = Resources.Load("AudioClip/" + soundname) as AudioClip;
        new_aus.Play();
        aus.Add(new_aus);
    }

    public void playSoundLoop(string soundname){
        //Check if same track
        if ((Resources.Load("AudioClip/" + soundname) as AudioClip) == aus_loop.clip){
            aus_loop.UnPause();
            return;
        }

        //Play new track
        if (aus_loop.isPlaying){
            aus_loop.Stop();
        }
        aus_loop.clip = Resources.Load("AudioClip/" + soundname) as AudioClip;
        aus_loop.Play();
        aus_loop.volume = volume * audiofade * 0.5f;
    }

    public void playSoundAmbient(string soundname){
        if (aus_ambient.isPlaying){
            aus_ambient.Stop();
        }
        aus_ambient.clip = Resources.Load("AudioClip/" + soundname) as AudioClip;
        aus_ambient.Play();
        aus_ambient.volume = (volume * audiofade) * 0.15f;
    }

    //Save Management
    public SaveFile save {
        get {
            return save_file;
        }
    }

    //Instance Methods
    public static GameManager instance {
        get {
            return manager;
        }
    }

    public Canvas canvas {
        get {
            return uicanvas;
        }
    }
}

[Serializable]
public class SaveFile {

    //Settings
    [SerializeField] private string save_place;
    [SerializeField] private bool[] save_keys;

    public SaveFile() {
        save_place = null;
        save_keys = new bool[100];
        save_keys[0] = true;
    }

    //Key functions
    public bool getKey (int key) {
        return save_keys[key];
    }

    public void setKey (int key, bool val) {
        save_keys[key] = val;
    }

    //File functions
    public void saveFile() {
        string json_save = JsonUtility.ToJson(this, true);
        string path = Application.streamingAssetsPath + "/Saves/Save.json";
        if (!File.Exists(path)){
            Directory.CreateDirectory(Application.streamingAssetsPath + "/Saves");
        }
        File.WriteAllText (path, json_save);
        #if UNITY_EDITOR
        UnityEditor.AssetDatabase.Refresh();
        #endif
    }

    public void loadFile() {
        string path = Application.streamingAssetsPath + "/Saves/Save.json";
        if (File.Exists(path)){
            string json_save = File.ReadAllText(path);
            SaveFile temp_save = JsonUtility.FromJson<SaveFile>(json_save);

            //Transfer over data
            save_place = temp_save.place;
            save_keys = temp_save.keys;
        }
        else {
            Debug.LogError("File not found exception");
        }
    }

    //Get & Set functions
    public string place {
        get {
            return save_place;
        }
        set {
            save_place = value;
        }
    }

    public bool[] keys {
        get {
            return save_keys;
        }
        set {
            save_keys = value;
        }
    }

}