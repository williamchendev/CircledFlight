using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSimpleBehavior : NPCBehavior {

	//NPC
    [SerializeField] private string[] event_cycle_eventnames = new string[]{""};

	//Init
	protected override void init() {
        //Interactable
        base.init();
        
        event_cycle = true;
        event_names = event_cycle_eventnames;
	}

}
