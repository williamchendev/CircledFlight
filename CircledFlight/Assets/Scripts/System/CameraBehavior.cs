using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour {

    //Settings
    [SerializeField] private Vector2 ClampX = new Vector2(0, 0);
    [SerializeField] private Vector2 ClampY = new Vector2(0, 0);

    [SerializeField] private GameObject parallax_A;

	//Variables
    private bool centered = false;

    private bool cutscene_bool;
    private Vector3 cutscene_pos;
    private Vector3 position;
    private GameObject follow_obj;
    private Vector2 clamp_pos;
    private float spd;

    private Vector2 parallax_A_pos;

    //Init
    void Awake() {
        spd = 0.02f;

        if (GameObject.FindGameObjectWithTag("Player") != null){
            if (!centered){
                recenterCam();
            }
        }

        if (parallax_A != null){
            parallax_A_pos = new Vector2(parallax_A.transform.position.x, parallax_A.transform.position.y);
        }
    }

    //Update
    void Update () {
        if (GameObject.FindGameObjectWithTag("Choice") != null){
            return;
        }

        Vector2 target = new Vector2(transform.position.x, transform.position.y);
        if (cutscene_bool){
            target = new Vector2(cutscene_pos.x, cutscene_pos.y);
        }
        else{
            if (follow_obj != null){
		        target = new Vector2(follow_obj.transform.position.x, follow_obj.transform.position.y);
            }
        }

        float x_pos = Mathf.Lerp(position.x, target.x, spd);
        float y_pos = Mathf.Lerp(position.y, target.y, spd);
        position = new Vector3(x_pos, y_pos, transform.position.z);
        transform.position = new Vector3(position.x - (position.x % 0.03125f), position.y - (position.y % 0.03125f), transform.position.z);

        ClampX = new Vector2(Mathf.Clamp(ClampX.x, ClampX.x, 0), Mathf.Clamp(ClampX.y, 0, ClampX.y));
        ClampY = new Vector2(Mathf.Clamp(ClampY.x, ClampY.x, 0), Mathf.Clamp(ClampY.y, 0, ClampY.y));
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, clamp_pos.x + Mathf.Min(ClampX.x, ClampX.y), clamp_pos.x + Mathf.Max(ClampX.x, ClampX.y)), Mathf.Clamp(transform.position.y, clamp_pos.y - Mathf.Min(ClampY.x, ClampY.y), clamp_pos.y - Mathf.Max(ClampY.x, ClampY.y)), transform.position.z);

        //Parallax
        if (parallax_A != null){
            parallax_A.transform.position = new Vector3(parallax_A_pos.x + ((transform.position.x - clamp_pos.x) * 0.15f), parallax_A_pos.y, parallax_A.transform.position.z);
        }
    }

    //Public Functions
    public void recenterCam() {
        centered = true;
        clamp_pos = new Vector2(transform.position.x, transform.position.y);
        follow_obj = GameObject.FindGameObjectWithTag("Player");
        transform.position = new Vector3(follow_obj.transform.position.x, follow_obj.transform.position.y , transform.position.z);
        position = transform.position;

        Update();
    }

    public void setFollow(GameObject new_follow){
        follow_obj = new_follow;
    }

    //Getters and Setters
    public bool cutscene {
        get {
            return cutscene_bool;
        }
        set {
            cutscene_bool = value;
        }
    }

    public Vector3 cutscenePosition {
        set {
            cutscene_pos = value;
        }
    }

    //Draw
    void OnDrawGizmosSelected() {
        ClampX = new Vector2(Mathf.Clamp(ClampX.x, ClampX.x, 0), Mathf.Clamp(ClampX.y, 0, ClampX.y));
        ClampY = new Vector2(Mathf.Clamp(ClampY.x, ClampY.x, 0), Mathf.Clamp(ClampY.y, 0, ClampY.y));

        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(transform.position.x - 7.5f + Mathf.Min(ClampX.x, ClampX.y), transform.position.y + 4.25f - Mathf.Min(ClampY.x, ClampY.y), transform.position.z), new Vector3(transform.position.x + 7.5f + Mathf.Max(ClampX.x, ClampX.y), transform.position.y + 4.25f - Mathf.Min(ClampY.x, ClampY.y), transform.position.z));
        Gizmos.DrawLine(new Vector3(transform.position.x - 7.5f + Mathf.Min(ClampX.x, ClampX.y), transform.position.y - 4.25f - Mathf.Max(ClampY.x, ClampY.y), transform.position.z), new Vector3(transform.position.x + 7.5f + Mathf.Max(ClampX.x, ClampX.y), transform.position.y - 4.25f - Mathf.Max(ClampY.x, ClampY.y), transform.position.z));
        Gizmos.DrawLine(new Vector3(transform.position.x - 7.5f + Mathf.Min(ClampX.x, ClampX.y), transform.position.y + 4.25f - Mathf.Min(ClampY.x, ClampY.y), transform.position.z), new Vector3(transform.position.x - 7.5f + Mathf.Min(ClampX.x, ClampX.y), transform.position.y - 4.25f - Mathf.Max(ClampY.x, ClampY.y), transform.position.z));
        Gizmos.DrawLine(new Vector3(transform.position.x + 7.5f + Mathf.Max(ClampX.x, ClampX.y), transform.position.y + 4.25f - Mathf.Min(ClampY.x, ClampY.y), transform.position.z), new Vector3(transform.position.x + 7.5f + Mathf.Max(ClampX.x, ClampX.y), transform.position.y - 4.25f - Mathf.Max(ClampY.x, ClampY.y), transform.position.z));
    }

}
