
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceScript : MonoBehaviour {

    //Text
	[SerializeField] private string text;
    [SerializeField] private Color color;

    //Components
    private Text text_obj;
    private GameObject hBox;

    private SubChoiceScript[] choices;

    //Settings
    private Vector3 position;

    private float spd;
    private float offset;
    private float sin_offset;
    private float sin_val;
    private float texttimer;

    private float width;
    private float height;

    private bool destroy;

    private int highlighted;

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

        destroy = false;

        highlighted = 0;
	}

	public void Start () {
        //Draw Sin
        sin_val += 0.009f;
		if (sin_val > 1f){
            sin_val = 0f;
        }
        float draw_sin = (Mathf.Sin(sin_val * 2 * Mathf.PI) + 1) / 2f;

        //Set Text Box Settings
        text_obj.text = text;
		text_obj.color = color;
        hBox.transform.localScale = new Vector3(width, height, 1);
        hBox.transform.localPosition = new Vector3(((text_obj.gameObject.GetComponent<RectTransform>().sizeDelta.x) / -2) + (width / 2) - (Mathf.Round(draw_sin * sin_offset) / 2) - (offset / 2), 0f, 0f);

        //Camera
        transform.parent = GameManager.instance.canvas.gameObject.transform;
        position = new Vector3(transform.position.x, transform.position.y, -1f * GameManager.instance.canvas.transform.childCount);
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

        //Check Click
        if (Input.GetMouseButtonDown(0)){
            if (text_obj.text.Length < text.Length){
                text_obj.text = text;
            }
        }

        //Box Dimensions
        if (!destroy){
            width = Mathf.Clamp(text_obj.preferredWidth, 0, text_obj.gameObject.GetComponent<RectTransform>().sizeDelta.x) + offset;
            height = text_obj.preferredHeight + offset;
            width = Mathf.Round(width) + Mathf.Round(draw_sin * sin_offset);
            height = Mathf.Round(height) + Mathf.Round(draw_sin * sin_offset);

            hBox.transform.localScale = new Vector3(width, height, 1);
            hBox.transform.localPosition = new Vector3(((text_obj.gameObject.GetComponent<RectTransform>().sizeDelta.x) / -2) + (width / 2) - (Mathf.Round(draw_sin * sin_offset) / 2) - (offset / 2), 0f, 0f);
        }
        else {
            if (text_obj != null){
                Destroy(text_obj.gameObject);
            }
            float shrink_spd = 8f;
            width = Mathf.Clamp(Mathf.Round(width - shrink_spd), 8, width);
            height = Mathf.Clamp(Mathf.Round(height - shrink_spd), 20, height);

            hBox.transform.localScale = new Vector3(width, height, 1);
        }       

        //Destroy
        if (destroy){
            if (width < 10){
                Destroy(gameObject);
            }
        }

        //Reset Highlight Counter
        highlighted = 0;
	}

    public bool canHighlight() {
        if (highlighted < 1){
            highlighted++;
            return true;
        }
        highlighted++;
        return false;
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

    public bool destroyself {
        set {
            destroy = value;
        }
        get {
            return destroy;
        }
    }

}
