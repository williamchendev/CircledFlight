using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextFloatScript : MonoBehaviour {

    //Text
    [SerializeField] private string text;
    [SerializeField] private Font font;
    [SerializeField] private Color color;
    [SerializeField] private float max_width;
    [SerializeField] private int timer;

    //Components
    private Text text_obj;
    private Text[] outline;

    //Settings
    private Vector3 position;
    private float spd;
    private float texttimer;

	//Init Variables
    void Awake () {
        //Get Text Object
        text_obj = transform.GetChild(4).GetComponent<Text>();
        if (font == null){
            font = text_obj.font;
        }
        text_obj.font = font;
        text_obj.fontSize = font.fontSize;
        outline = new Text[4];
        for (int i = 0; i < 4; i++){
            outline[i] = transform.GetChild(i).GetComponent<Text>();
            outline[i].font = font;
            outline[i].fontSize = font.fontSize;
        }

        spd = 0.24f;
		color = Color.white;
        max_width = 150;
        
        //Settings
        texttimer = 0;
	}

	void Start () {
        //Set Text Box Settings
		text_obj.color = color;
        text_obj.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(max_width, text_obj.gameObject.GetComponent<RectTransform>().sizeDelta.y);
        for (int i = 0; i < 4; i++){
            outline[i].gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(max_width, text_obj.gameObject.GetComponent<RectTransform>().sizeDelta.y);
            outline[i].color = Color.black;
        }

        //Camera
        transform.parent = GameManager.instance.canvas.gameObject.transform;
        position = new Vector3(transform.position.x, transform.position.y, -1f * GameManager.instance.canvas.transform.childCount);

        //Set Text Details
        Vector3 real_position = new Vector3(position.x, position.y, position.z);
        real_position = new Vector3(real_position.x - (real_position.x % 0.03125f), real_position.y - (real_position.y % 0.03125f), real_position.z);
        transform.position = real_position;
	}

    //Update Event
	void Update () {
        //Calc Positions
        Vector3 real_position = new Vector3(position.x, position.y, position.z);
        real_position = new Vector3(real_position.x - (real_position.x % 0.03125f), real_position.y - (real_position.y % 0.03125f), real_position.z);
        transform.position = real_position;
        text_obj.transform.position = real_position;

        //Set Text & Outline Position
        outline[0].transform.position = new Vector3(real_position.x - (1 / 32f), real_position.y, real_position.z);
        outline[1].transform.position = new Vector3(real_position.x + (1 / 32f), real_position.y, real_position.z);
        outline[2].transform.position = new Vector3(real_position.x, real_position.y - (1 / 32f), real_position.z);
        outline[3].transform.position = new Vector3(real_position.x, real_position.y + (1 / 32f), real_position.z);

        //Text Calculations
        if (text_obj.text.Length < text.Length){
            if (texttimer < 1){
                texttimer += spd;
            }
            else {
                texttimer = 0;
                text_obj.text = text.Substring(0, text_obj.text.Length + 1);
            }
        }

        //Outlines
        for (int i = 0; i < 4; i++){
            outline[i].text = text_obj.text;
        }

        //Destroy
        if (text_obj.text.Length == text.Length){
            if (timer > 0){
                timer--;
                if (timer == 0){
                    Destroy(gameObject);
                }
            }
        }
	}

}
