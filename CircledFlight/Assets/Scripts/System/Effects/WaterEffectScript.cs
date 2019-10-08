using UnityEngine;

public class WaterEffectScript : MonoBehaviour {

    private float sin_val;
    private Vector3 pos;

    void Start() {
        sin_val = 0;
        pos = transform.position;
    }

    void Update() {
        if (sin_val < 1){
            sin_val += 0.0017f;
            if (sin_val >= 1){
                sin_val = 0;
            }
        }
        float draw_sin = (Mathf.Sin(sin_val * 2 * Mathf.PI) * 2) - 1;

        transform.position = new Vector3((draw_sin * 0.04f) + pos.x, transform.position.y, transform.position.z);
        transform.eulerAngles = new Vector3(transform.localRotation.x, transform.localRotation.y, (draw_sin * 0.08f));
    }

}