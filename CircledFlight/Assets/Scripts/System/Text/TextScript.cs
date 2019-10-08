using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextScript : MonoBehaviour {

    //Text
    [SerializeField] private string text;
    [SerializeField] private Color color;

    //Components
    private Text text_obj;
    private GameObject hBox;

    //Settings
    private Vector3 position;

    private float spd;
    private float offset;
    private float sin_offset;
    private float sin_val;
    private float texttimer;

    private float width;
    private float height;

    private int audio_counter;

    private bool destroy;

    private bool bolding;
    private int string_length;

	//Init Variables
    void Awake () {
        //Get Text Object
        text_obj = transform.GetChild(0).GetComponent<Text>();
        hBox = transform.GetChild(1).gameObject;

        spd = 15f;
		color = Color.white;
        
        //Settings
        offset = 9f;
        sin_offset = 6f;
        sin_val = 0;
        texttimer = 0;

        width = 0f;
        height = 0f;

        audio_counter = 0;

        destroy = false;

        bolding = false;
        string_length = 0;
	}

	public void Start () {
        //Set Text Box Settings
		text_obj.color = color;

		//Draw Sin
        sin_val += 0.009f;
		if (sin_val > 1f){
            sin_val = 0f;
        }
        float draw_sin = (Mathf.Sin(sin_val * 2 * Mathf.PI) + 1) / 2f;

        //Text Calculations
        if (!destroy){
            if (text_obj.text.Length < text.Length){
                if (texttimer < 1){
                    texttimer += spd * Time.deltaTime;
                }
                else {
                    texttimer = 0;
                    text_obj.text = text.Substring(0, text_obj.text.Length + 1);
                }
            }
        }

        //Camera
        transform.parent = GameManager.instance.canvas.gameObject.transform;
        position = new Vector3(transform.position.x, transform.position.y, -1f * GameManager.instance.canvas.transform.childCount);

        //Set Text Details
        Vector3 real_position = new Vector3(position.x, position.y, position.z);
        real_position = new Vector3(real_position.x - (real_position.x % 0.03125f), real_position.y - (real_position.y % 0.03125f), real_position.z);
        transform.position = real_position;

        //Box Dimensions
        width = Mathf.Clamp(text_obj.preferredWidth, 0, text_obj.gameObject.GetComponent<RectTransform>().sizeDelta.x) + offset;
        height = text_obj.preferredHeight + offset;
        width = Mathf.Round(width) + Mathf.Round(draw_sin * sin_offset);
        height = Mathf.Round(height) + Mathf.Round(draw_sin * sin_offset);  

        hBox.transform.localScale = new Vector3(width, height, 1);
        hBox.transform.localPosition = new Vector3(((text_obj.gameObject.GetComponent<RectTransform>().sizeDelta.x) / -2) + (width / 2) - (Mathf.Round(draw_sin * sin_offset) / 2) - (offset / 2), 0f, 0f);
	}
	
	//Update Event
	void Update () {
        //Set Positions
        Vector3 real_position = new Vector3(position.x, position.y, position.z);
        real_position = new Vector3(real_position.x - (real_position.x % 0.03125f), real_position.y - (real_position.y % 0.03125f), real_position.z);
        transform.position = real_position;

		//Draw Sin
        sin_val += 0.009f;
		if (sin_val > 1f){
            sin_val = 0f;
        }
        float draw_sin = (Mathf.Sin(sin_val * 2 * Mathf.PI) + 1) / 2f;

        //Text Calculations
        if (!destroy){
            if (string_length < text.Length){
                if (texttimer < 1){
                    texttimer += spd;
                }
                else {
                    if (bolding) {
                        if (string_length < text.Length - 4) {
                            if (text.Substring(string_length, 4) == "</b>") {
                                bolding = false;
                                string_length += 4;
                            }
                        }
                    }
                    else {
                        if (string_length < text.Length - 3) {
                            if (text.Substring(string_length, 3) == "<b>") {
                                bolding = true;
                                string_length += 3;
                            }
                        }
                    }

                    texttimer = 0;
                    string_length++;

                    if (bolding) {
                        text_obj.text = text.Substring(0, string_length) + "</b>";
                    }
                    else {
                        text_obj.text = text.Substring(0, string_length);
                    }

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
        }

        //Check Click
        if (Input.GetMouseButtonDown(0)){
            if (!GameManager.instance.getPause()){
                if (string_length < text.Length){
                    text_obj.text = text;
                    string_length = text.Length;
                }
                else {
                    destroy = true;
                    if (text_obj != null){
                        Destroy(text_obj.gameObject);
                    }
                }
            }
        }

        //Box Dimensions
        if (!destroy){
            width = Mathf.Clamp(text_obj.preferredWidth, 0, text_obj.gameObject.GetComponent<RectTransform>().sizeDelta.x) + offset;
            height = text_obj.preferredHeight + offset;
            width = Mathf.Round(width) + Mathf.Round(draw_sin * sin_offset);
            height = Mathf.Round(height) + Mathf.Round(draw_sin * sin_offset);

            hBox.transform.localScale = new Vector3(width, height - 4, 1);
            hBox.transform.localPosition = new Vector3(((text_obj.gameObject.GetComponent<RectTransform>().sizeDelta.x) / -2) + (width / 2) - (Mathf.Round(draw_sin * sin_offset) / 2) - (offset / 2), 0f, 0f);
        }
        else {
            float shrink_spd = 8f;
            width = Mathf.Clamp(Mathf.Round(width - shrink_spd), 8, width);
            height = Mathf.Clamp(Mathf.Round(height - shrink_spd), 20, height);

            hBox.transform.localScale = new Vector3(width, height - 10, 1);
        }       

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

    public Color colorContent {
        set {
            color = value;
        }
    }

}
