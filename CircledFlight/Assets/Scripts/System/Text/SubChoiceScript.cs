using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubChoiceScript : MonoBehaviour {

	//Text
    private int choice;
    [SerializeField] private string text;

    //Components
    private Text text_obj;
    private GameObject hBox;
    private GameObject vBox;
    private GameObject[] corners;

    //Settings
    private float spd;
    private float offset;
    private float sin_offset;
    private float sin_val;
    private float texttimer;

    private float width;
    private float height;

    private float alpha;

    private int audio_counter;
    private Vector2 cam_shift;

    private bool destroy;
    private ChoiceScript boss;
    private EventManager em;

    private int string_length;

	//Init Variables
    void Awake () {
        //Get Text Object
        text_obj = transform.GetChild(0).GetComponent<Text>();
        hBox = transform.GetChild(1).gameObject;
        vBox = transform.GetChild(2).gameObject;
        vBox.GetComponent<SpriteRenderer>().enabled = false;
        corners = new GameObject[4];
        for (int i = 0; i < 4; i++){
            corners[i] = transform.GetChild(i + 3).gameObject;
            corners[i].GetComponent<SpriteRenderer>().enabled = false;
        }

        spd = 25f;
        
        //Settings
        offset = 6f;
        sin_offset = 4f;
        sin_val = Random.Range(0f, 1f);
        texttimer = 0;

        width = 0f;
        height = 0f;

        audio_counter = 0;
        cam_shift = new Vector2(Camera.main.transform.position.x - transform.position.x, Camera.main.transform.position.y - transform.position.y);
        cam_shift = new Vector2(cam_shift.x - (cam_shift.x % 0.03125f), cam_shift.y - (cam_shift.y % 0.03125f));

        destroy = false;
        gameObject.tag = "Choice";

        string_length = 0;
	}

	public void Start () {
        //Set Text Box Settings
        alpha = 0;
		text_obj.color = Color.white;
        text_obj.color = new Color(text_obj.color.r, text_obj.color.g, text_obj.color.b, alpha);

        //Camera
        transform.parent = GameManager.instance.canvas.gameObject.transform;
	}
	
	//Update Event
	void LateUpdate () {
        //Set Positions
        Vector3 real_position = new Vector3(Camera.main.transform.position.x + cam_shift.x, Camera.main.transform.position.y - cam_shift.y, -1f * GameManager.instance.canvas.transform.childCount);
        transform.position = real_position;

		//Draw Sin
        sin_val += 0.009f;
		if (sin_val > 1f){
            sin_val = 0f;
        }
        float draw_sin = (Mathf.Sin(sin_val * 2 * Mathf.PI) + 1) / 2f;

        //Check Destroy
        if (!destroy){
            if (boss.destroyself){
                destroy = true;
                if (text_obj != null){
                    Destroy(text_obj.gameObject);
                }
            }
        }

        //Text Calculations
        if (!destroy){
            alpha += 0.02f;
            if (string_length < text.Length){
                if (texttimer < 1){
                    texttimer += spd * Time.deltaTime;
                }
                else {
                    texttimer = 0;
                    string_length++;

                    audio_counter--;
                    if (audio_counter <= 0) {
                        //Play Type Writer Audio Clip
                        int rand_aud = Random.Range(0, 10);
                        string aud_path = "TypeWriterSFX/TypeSFX" + rand_aud;
                        
                        GameManager.instance.playSound(aud_path);
                        
                        audio_counter = 2;
                    }
                }
            }

            text_obj.text = text.Substring(0, string_length);
        }

        //Check Click
        Vector2 vA = new Vector2(corners[0].transform.position.x, corners[0].transform.position.y);
        Vector2 vB = new Vector2(corners[2].transform.position.x, corners[2].transform.position.y);
        Vector3 v3 = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        bool inside = false;
        if (v3.x > vA.x && v3.x < vB.x) {
            if (v3.y < vA.y && v3.y > vB.y) {
                inside = true;
            }
        }

        if (inside && boss.canHighlight()){
            if (Input.GetMouseButtonDown(0)){
                if (!destroy){
                    destroy = true;
                    if (text_obj != null){
                        Destroy(text_obj.gameObject);
                    }
                    em.setChoice(choice);
                    boss.destroyself = true;
                }
            }

            if (!destroy) {
                text_obj.text = "<b>" + text.Substring(0, string_length) + "</b>";
            }
        }
        else {
            alpha = Mathf.Clamp(alpha, 0, 0.6f);
        }

        //Box Dimensions
        if (!destroy){
            //Alpha
            text_obj.color = new Color(text_obj.color.r, text_obj.color.g, text_obj.color.b, alpha);

            width = Mathf.Clamp(text_obj.preferredWidth, 0, text_obj.gameObject.GetComponent<RectTransform>().sizeDelta.x) + offset;
            height = text_obj.preferredHeight + offset;
            width = Mathf.Round(width) + Mathf.Round(draw_sin * sin_offset);
            height = Mathf.Round(height) + Mathf.Round(draw_sin * sin_offset);
        }
        else {
            float shrink_spd = 8f;
            width = Mathf.Clamp(Mathf.Round(width - shrink_spd), 8, width);
            height = Mathf.Clamp(Mathf.Round(height - shrink_spd), 20, height);
        }       

        hBox.transform.localScale = new Vector3(width, height, 1);
        vBox.transform.localScale = new Vector3(width - 10, height, 1);

        //Corner placement
        corners[0].transform.position = new Vector3(transform.position.x - ((width / 32) / 2), transform.position.y + ((height / 32) / 2), transform.position.z);
        corners[1].transform.position = new Vector3(transform.position.x + ((width / 32) / 2), transform.position.y + ((height / 32) / 2), transform.position.z);
        corners[2].transform.position = new Vector3(transform.position.x + ((width / 32) / 2), transform.position.y - ((height / 32) / 2), transform.position.z);
        corners[3].transform.position = new Vector3(transform.position.x - ((width / 32) / 2), transform.position.y - ((height / 32) / 2), transform.position.z);

        //Destroy
        if (destroy){
            if (width < 10){
                Destroy(gameObject);
            }
        }
	}

    //Get and Set Methods
    public string textContent {
        set {
            text = value;
        }
    }

    public ChoiceScript choicescript {
        set {
            boss = value;
        }
    }

    public EventManager setEvent {
        set {
            em = value;
        }
    }

    public int choiceContent {
        set {
            choice = value;
        }
    }

}
