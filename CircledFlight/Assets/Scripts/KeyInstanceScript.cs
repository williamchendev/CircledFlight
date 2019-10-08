using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyInstanceScript : MonoBehaviour {

    //Settings
    [SerializeField] private bool update_check = false;
    [SerializeField] private int on_key = -1;
    [SerializeField] private int off_key = -1;

    void Awake() {
        int[] on_check = new int[]{ on_key };
        int[] off_check = new int[]{ off_key };

        GameObject keycheck = new GameObject("pKeyCheck_" + gameObject.name);
        transform.parent = keycheck.transform;
        keycheck.AddComponent<KeyInstanceBehavior>();
        keycheck.GetComponent<KeyInstanceBehavior>().keycheckinit(update_check, on_check, off_check);
        Destroy(gameObject.GetComponent<KeyInstanceScript>());
    }

}

public class KeyInstanceBehavior : MonoBehaviour {

    //Settings
    private bool update_check;
    private int[] on_key;
    private int[] off_key;
    private GameObject check;

    public void keycheckinit(bool update, int[] on, int[] off) {
        update_check = update;
        on_key = on;
        off_key = off;
        check = transform.GetChild(0).gameObject;

        checkKeys();
    }

    void Update() {
        if (update_check){
            checkKeys();
        }
    }

    private void checkKeys() {
        if (GameManager.instance == null){
            return;
        }

        bool on = false;
        for (int i = 0; i < on_key.Length; i++){
            if (on_key[i] != -1){
                if (GameManager.instance.save.getKey(on_key[i])){
                    on = true;
                    break;
                }
            }
        }

        bool off = false;
        for (int i = 0; i < off_key.Length; i++){
            if (off_key[i] != -1){
                if (GameManager.instance.save.getKey(off_key[i])){
                    off = true;
                    break;
                }
            }
        }

        if (check != null){
            if (on && !off){
                check.SetActive(true);
            }
            else {
                check.SetActive(false);
            }
        }
    }

}
