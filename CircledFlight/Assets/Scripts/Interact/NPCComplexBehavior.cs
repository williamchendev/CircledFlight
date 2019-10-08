using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCComplexBehavior : NPCBehavior {

	//NPC
    [Header("Event Settings")]
    [SerializeField] private int questA_item = -1;
    [SerializeField] private string questA_eventname = "";
    [SerializeField] private int questB_item = -1;
    [SerializeField] private string questB_eventname = "";
    [SerializeField] private int questC_item = -1;
    [SerializeField] private string questC_eventname = "";
    [SerializeField] private bool events_cycle = false;
    [SerializeField] private string[] event_cycle_eventnames = new string[]{""};

	//Init
	protected override void init() {
        //Interactable
        base.init();
        
        event_cycle = events_cycle;
        event_names = event_cycle_eventnames;
        questitemA = questA_item;
        questevent_nameA = questA_eventname;
        questitemB = questB_item;
        questevent_nameB = questB_eventname;
        questitemC = questC_item;
        questevent_nameC = questC_eventname;
	}

}
