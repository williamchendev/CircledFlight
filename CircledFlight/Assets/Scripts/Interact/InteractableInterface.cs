using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableInterface : MonoBehaviour {

    [SerializeField] protected Vector2 position;
    public abstract Vector2 getPosition();
    protected abstract void init();
    protected abstract void step();

}
