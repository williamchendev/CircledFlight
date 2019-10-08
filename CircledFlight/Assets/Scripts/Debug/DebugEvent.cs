using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugEvent : MonoBehaviour {

    [SerializeField] private bool save;
    private EventManager em;

	void Awake () {
		em = GetComponent<EventManager>();

        if (save) {
            debug();
            save = false;
        }
	}

    private void debug() {
        EventFile file = new EventFile();
        /*
        file.addText("*whimpers*", Vector2.zero, "will");
        file.addText("I can't believe this is happening", Vector2.zero, "player");
        file.addEnd();
        */

        /*
        file.addSkip(1, 22);
        file.addKey(0);
        file.addText("..auuuuggggggghhh...", Vector2.zero, "will");
        file.addText("I found him like this when I came in", Vector2.zero, "ann");
        file.addText("Are you serious, come on", Vector2.zero, "player");
        file.addText("You barely canvassed", Vector2.zero, "player");
        file.addText("That last house was so mean to me", Vector2.zero, "will");
        file.addChoice("will", "ughhh..", "You only did 5 houses", 1, "You argued for an hour with a 12 year old", 1);
        file.addText("We need you out there knocking doors", Vector2.zero, "ann");
        file.addText("We're down 40 points in the polls", Vector2.zero, "ann");
        file.addChoice("will", "Now's not the time for a break", "Kick him", 1, "Drag him outside", 1, "Verbal Absue", 1);
        file.addText("Hannah...", Vector2.zero, "ann");
        file.addText("What?..", Vector2.zero, "player");
        file.addText("Okay what should we do now?", Vector2.zero, "player");
        file.addText("Well since we're stuck here because of someone...", Vector2.zero, "ann");
        file.addText("*whimpers*", Vector2.zero, "will");
        file.addText("We'll have to find a way out of the house...", Vector2.zero, "ann");
        file.addChoice("will", "*crying*", "ditch will", 1, "throw a brick at the press", 1, "More Verbal Absue", 1);
        file.addText("I know! Grab me a banana", Vector2.zero, "ann");
        file.addText("*inaudiable moans*", Vector2.zero, "will");
        file.addKey(1);
        file.addSkip(0, 4);
        file.addSkip(2, 4);
        file.addText("There should be a banana somewhere around here...", Vector2.zero, "ann");
        file.addText("Please Hannah, you're my only hope <3", Vector2.zero, "ann");
        file.addSkip(0, 11);
        file.addText("We're kinda trapped", Vector2.zero, "ann");
        file.addText("this is a nightmaree...", Vector2.zero, "will");
        file.addText("Alright, time to actually do something", Vector2.zero, "ann");
        file.addChoice("will", "whyyy me...", "what's the plan?", 1, "what's the plan?", 1, "what's the plan?", 1);
        file.addText("Help me toss Will into the van", Vector2.zero, "ann");
        file.addText("You'll get me to canvas over my dead body", Vector2.zero, "will");
        file.addText("good", Vector2.zero, "player");
        file.addText("good", Vector2.zero, "ann");
        file.addText("We have about 3 hours to get 500 signatures", Vector2.zero, "ann");
        file.addText("I think we can do it <3", Vector2.zero, "player");
        file.addEnd();
        */

        /*
        file.addKey(4);
        file.addText("Thanks so much for the Banana Hannah <3", Vector2.zero, "ann");
        file.addText("So how is that going to get us out of here?", Vector2.zero, "player");
        file.addText("Oh it won't, I'm just hungry", Vector2.zero, "ann");
        file.addText("Ann", Vector2.zero, "player");
        file.addText("I can't think of a way out of this if I'm hungry!", Vector2.zero, "ann");
        file.addText("..this is a really good banana...", Vector2.zero, "ann");
        file.addText("Ann!", Vector2.zero, "player");
        file.addText("okay Okay I Got it", Vector2.zero, "ann");
        file.addEnd();
        */

        /*
        file.addSkip(0, 20);
        file.addKey(0);
        file.addText("Omg you weren't kidding", Vector2.zero, "player");
        file.addText("I told you so", Vector2.zero, "Will");
        file.addText("Your favorite diner opened up in Naperville", Vector2.zero, "Will");
        file.addText("I can't beleive it's so empty", Vector2.zero, "player");
        file.addChoice("Will", "What are you going to order?", "Waffle Fries", 1, "Cheesy Waffle Fries", 1);
        file.addText("I thought you said we were getting waffles", Vector2.zero, "Will");
        file.addText("Waffle fries are a type of waffle!", Vector2.zero, "player");
        file.addText("...", Vector2.zero, "Will");
        file.addText("What?", Vector2.zero, "player");
        file.addText("I missed you so much", Vector2.zero, "Will");
        file.addChoice("Will", "What made you come back from LA?", "Waffle Fries", 1, "Cheesy Waffle Fries", 1);
        file.addText("...", Vector2.zero, "Will");
        file.addText("You're such a dumpster fire", Vector2.zero, "Will");
        file.addText("Just grab us some food so we don't starve", Vector2.zero, "Will");
        file.addText("Anything you want in particular?", Vector2.zero, "player");
        file.addText("Some plastic spoons to gouge my eyes out", Vector2.zero, "Will");
        file.addText("Sure thing <3", Vector2.zero, "player");
        file.addSkip(0, 34);

        file.addSkip(1, 4);
        file.addText("I'm starvinggggggggg", Vector2.zero, "Will");
        file.addText("hurry up pleeeeaseeee", Vector2.zero, "Will");
        file.addSkip(0, 30);

        file.addText("So guess what I have for you", Vector2.zero, "Will");
        file.addText("Wait! Last time you gave me a surprise I had a panic attack", Vector2.zero, "player");
        file.addChoice("Will", "Since when did my gift giving hurt anyone?", "Last Febuary", 1, "May two years ago", 1, "The Christmas Apocalypse", 1);
        file.addText("Old people get heart attacks all the time", Vector2.zero, "Will");
        file.addText("He died of shock", Vector2.zero, "player");
        file.addText("WITH A SMILE ON HIS FACE", Vector2.zero, "Will");
        file.addText("You can't argue with results", Vector2.zero, "Will");

        file.addText("But no, this is something entirely different!", Vector2.zero, "Will");
        file.addText("It's a job!", Vector2.zero, "Will");
        file.addText("...", Vector2.zero, "player");
        file.addText("How come you're not that excited?", Vector2.zero, "Will");
        file.addText("In today's economy you just practically won the lottery!", Vector2.zero, "Will");
        file.addText("~yayyy~", Vector2.zero, "player");
        file.addChoice("Will", "You're so lame", "Do I at least get paid?", 1, "What do I have to do?", 1);
        file.addText("Glad you asked!", Vector2.zero, "Will");
        file.addText("We're having an upcoming election for mayor!", Vector2.zero, "Will");
        file.addText("Isn't the mayor alive?", Vector2.zero, "player");
        file.addText("Nope, not since yesterday at least...", Vector2.zero, "Will");
        file.addText("Omg!", Vector2.zero, "player");
        file.addText("He had one of those heart attacks", Vector2.zero, "Will");
        file.addText("really common among old people", Vector2.zero, "Will");
        file.addText("What about his advisors? Or his board?", Vector2.zero, "player");
        file.addText("None of them are running.", Vector2.zero, "Will");
        file.addText("In fact all of them declined to enter this year's campaign", Vector2.zero, "Will");
        file.addText("Which brings us to our main point", Vector2.zero, "Will");
        file.addText("We need a candidate for mayor this year", Vector2.zero, "Will");
        file.addText("You're going to help me badger Daryl Beavers into running for office", Vector2.zero, "Will");
        file.addText("Annnnnnd this is where the demo ends", Vector2.zero, "player");
        file.addText("You did it!", Vector2.zero, "Will");

        file.addEnd();
        */

        /*
        file.addText("Waffles?", Vector2.zero, "Ellie");
        file.addText("Yes", Vector2.zero, "player");
        file.addKey(1);
        file.addEnd();
        */

        /*
        file.addMovement("will", new Vector2(0, 0));
        file.addEnd();
        */

        /*
        file.addText("This is me talking", Vector2.zero, "player");
        file.addText("Hello!!!", Vector2.zero, "player");
        file.addText("I AM STILL TALKING MORE AND MORE", Vector2.zero, "player");
        file.addEnd();
        */

        /*
        file.addText("You ordering?", Vector2.zero, "Ellie");
        file.addText("Heck yes! More waffle fries!", Vector2.zero, "player");
        file.addText("How you're paying?", Vector2.zero, "Ellie");
        file.addText("(Ugghhh I left my card at home)", Vector2.zero, "player");
        file.addText("..put it on my tab?", Vector2.zero, "player");
        file.addText("This ain't a breadline sugar.", Vector2.zero, "Ellie");
        file.addText("(I'll need some money if I come here next time D:)", Vector2.zero, "player"); 
        */

        /*
        file.addMovement("player", new Vector2(-3.16f, -2.44f));
        file.addMovement("Will", new Vector2(-4.26f, -3.18f));
        file.addText("Will! Long time no see!", Vector2.zero, "GovGirl");
        file.addText("Find any candidates to run against us?", Vector2.zero, "GovGirl"); 
        file.addText("How could you back Carver?", Vector2.zero, "Will");
        file.addText("I thought you cared about people!", Vector2.zero, "Will");
        file.addText("Excuse me?", Vector2.zero, "Carver");
        file.addMovement("GovGirl", new Vector2(-3.46f, -3.18f));
        file.addText("Look at me.", Vector2.zero, "GovGirl"); 
        file.addText("I will destroy you.", Vector2.zero, "GovGirl"); 
        file.addText("...", Vector2.zero, "Will");
        file.addMovement("GovGirl", new Vector2(0.65f, -3.05f));
        file.addMovement("GovGirl", new Vector2(0.55f, -3.05f));
        file.addText("...", Vector2.zero, "Carver");
        file.addMusic("GirlthemeSFX", true);
        file.addText("Where are my manners? My name is Isabell, and this is Mayor Anthony Carver.", Vector2.zero, "GovGirl");
        file.addChoice("GovGirl", "I don't think I've met you, who are you again?", "Hannah.", 1, "...", 3);
        file.addText("That's an excellent name!", Vector2.zero, "GovGirl");
        file.addSkip(0, 2);
        file.addText("No matter, I'll find out your name soon enough.", Vector2.zero, "GovGirl");
        file.addText("We're on our way to City Hall.", Vector2.zero, "GovGirl");
        file.addText("Being Mayor is fantastic!", Vector2.zero, "Carver");
        file.addText("Another year to slash our taxes to ribbons!", Vector2.zero, "Carver");
        file.addText("You traitor! I can't beleive you're working with this fascist Milk Boy!", Vector2.zero, "Will");
        file.addText("I hate that name.", Vector2.zero, "Carver");
        file.addText("I solved racism, why can't you be grateful?", Vector2.zero, "Carver");
        file.addText("Listen, Carver offered me something Paesly didn't.", Vector2.zero, "GovGirl");
        file.addText("Paesly was a fat coward that ate himself to an early death.", Vector2.zero, "GovGirl");
        file.addText("At least I'm doing something.", Vector2.zero, "GovGirl");
        file.addText("You're working with the devil!", Vector2.zero, "Will");
        file.addText("You completely quit after you lost!", Vector2.zero, "GovGirl");
        file.addText("Don't blame me for your shortcomings.", Vector2.zero, "GovGirl");
        file.addChoice("GovGirl", "Learn to control your boy.", "He's not my \"boy\".", 1, "Will heel", 1);
        file.addText("Whatever, I don't have time for this.", Vector2.zero, "GovGirl");
        file.addText("The bus is coming, we should go.", Vector2.zero, "Carver");
        file.addMusic("BusSFX", false);
        file.addKey(10);
        file.addMovement("GovGirl", new Vector2(-3.46f, -3.18f));
        file.addMovement("Carver", new Vector2(-0.56f, -3.15f));
        file.addMusic("BusSFX", false);
        file.addKey(11);
        file.addKey(7);
        file.addKey(5);
        file.addText("Ugh what snobs.", Vector2.zero, "Will");
        file.addEnd();
        */

        /*
        file.addMovement("player", new Vector2(4.818685f, -3.407171f));
        file.addMovement("Will", new Vector2(3.52f, -3.82f));
        file.addText("What the hell? Why is this all barricaded?", Vector2.zero, "Will");
        file.addText("Didn't you hear?", Vector2.zero, "GovGirl");
        file.addText("Apparently Russian subversives have been swinging elections all over the country.", Vector2.zero, "GovGirl");
        file.addText("With the war going on, a little security doesn't hurt.", Vector2.zero, "GovGirl");
        file.addText("How are we supposed to get in?", Vector2.zero, "Will");
        file.addText("Well I could let you in..", Vector2.zero, "GovGirl");
        file.addChoice("GovGirl", "..for a price.", "What do you want?", 5, "This is probably illegal.", 1);
        file.addText("Well normally I'd be suspended from barring you entry.", Vector2.zero, "GovGirl");
        file.addText("But we live in strange times.", Vector2.zero, "GovGirl");
        file.addText("Drastic times call fro drastic measures.", Vector2.zero, "GovGirl");
        file.addText("Okay fine, what's your price?", Vector2.zero, "player");
        file.addText("Glad you asked!", Vector2.zero, "GovGirl");
        file.addText("Naperville has the lowest crime rate in all of America.", Vector2.zero, "GovGirl");
        file.addText("Possibly the world.", Vector2.zero, "GovGirl");
        file.addChoice("GovGirl", "Want to guess how I do it?", "Sure", 1, "No.", 3);
        file.addChoice("GovGirl", "...", "Blackmail?", 1, "Intimidation?", 1, "Secret death squads?", 1);
        file.addText("Sort of.", Vector2.zero, "GovGirl");
        file.addText("I've networked the city to know everything about everyone.", Vector2.zero, "GovGirl");
        file.addText("Wait, that's so unethical!", Vector2.zero, "Will");
        file.addText("People are safe, they don't even know they're being watched.", Vector2.zero, "GovGirl");
        file.addText("I mean give me some credit,", Vector2.zero, "GovGirl");
        file.addText("I did all that using the dinosaur computers in the DMV.", Vector2.zero, "GovGirl");
        file.addText("You're a monster.", Vector2.zero, "Will");
        file.addText("I'm not a monster. You asked for this.", Vector2.zero, "GovGirl");
        file.addText("You and the rest of Naperville.", Vector2.zero, "GovGirl");
        file.addText("Property values don't rise by themselves.", Vector2.zero, "GovGirl");
        file.addText("I only did what the free market wanted.", Vector2.zero, "GovGirl");
        file.addChoice("GovGirl", "What have you done to help?", "What do you want from us?", 2, "Are you insane?", 1);
        file.addText("Partially.", Vector2.zero, "GovGirl");
        file.addText("I want you both to bring me the bookstore's computer hard drive.", Vector2.zero, "GovGirl");
        file.addText("Someone's been downloading some \"illegal\" materials.", Vector2.zero, "GovGirl");
        file.addText("We're buring books now?", Vector2.zero, "Will");
        file.addText("Hard drives.", Vector2.zero, "GovGirl");
        file.addText("Books are protected by the first amendment. Data however is not.", Vector2.zero, "GovGirl");
        file.addText("God bless the FCC.", Vector2.zero, "GovGirl");
        file.addText("So you want us to break a hard drive?", Vector2.zero, "player");
        file.addText("No, make sure you bring it back to me.", Vector2.zero, "GovGirl");
        file.addText("All that information is highly valuable. Who knows what could be on it?", Vector2.zero, "GovGirl");
        file.addText("Bring back the hard drive. It's in the book store's computer.", Vector2.zero, "GovGirl");
        file.addText("I hate working for her, but it seems like our only choice.", Vector2.zero, "Will");
        file.addKey(8);
        file.addEnd();
        */

        /*
        file.addText("Hey uhh do you work here?", Vector2.zero, "Parks");
        file.addText("Uhhh, no.", Vector2.zero, "player");
        file.addText("Aw, I need to give the keys ", Vector2.zero, "Parks");
        file.addText("Fine, just pick it up already.", Vector2.zero, "Will");
        file.addKey(14);
        file.addEnd();
        */

        /*
        file.addSkip(20, 15);
        file.addKey(20);
        file.addText("Hey.", Vector2.zero, "Abby");
        file.addText("Hi", Vector2.zero, "player");
        file.addChoice("Abby", "Do you need anything?", "What are you doing out here?", 1, "Aren't you wet?", 1);
        file.addText("I didn't think things through.", Vector2.zero, "Abby");
        file.addText("I borrowed this sweater and thought I'd be dry.", Vector2.zero, "Abby");
        file.addText("Why haven't you gone inside?", Vector2.zero, "player");
        file.addText("I gave up on being dry a long time ago...", Vector2.zero, "Abby");
        file.addText("Is that sweater stolen?", Vector2.zero, "player");
        file.addText("No.", Vector2.zero, "Abby");
        file.addText("...", Vector2.zero, "Abby");
        file.addText("I'll pick locks for you to net snitch on me.", Vector2.zero, "Abby");
        file.addText("Deal.", Vector2.zero, "player");
        file.addSkip(0, 21);
        file.addChoice("Abby", "What do you need?", "Lockpick", 6, "Wishing Well", 1);
        file.addText("Do you have any quarters?", Vector2.zero, "Abby");
        file.addText("Why?", Vector2.zero, "player");
        file.addText("I really want to make a wish.", Vector2.zero, "Abby");
        file.addText("I'll lock pick for a quarter.", Vector2.zero, "Abby");
        file.addSkip(0, -5);
        file.addSkip(17, 2);
        file.addSkip(0, 2);
        file.addSkip(18, 2);
        file.addSkip(0, 2);
        file.addSkip(19, 6);
        file.addText("I want a quarter first.", Vector2.zero, "Abby");
        file.addText("For what?", Vector2.zero, "player");
        file.addText("The wishing well", Vector2.zero, "Abby");
        file.addText("Until I get that quarter, you're not going anywhere.", Vector2.zero, "Abby");
        file.addSkip(0, 5);
        file.addKey(15);
        file.addText("Done.", Vector2.zero, "Abby");
        file.addText("Wait really? That quick?", Vector2.zero, "player");
        file.addText("I mean go check, the lock's probably off now.", Vector2.zero, "Abby");
        file.addEnd();
        em.saveFile("AbbyA", file);
        */

        /*
        file.addText("Wow, you actually managed to grab this!", Vector2.zero, "GovGirl");
        file.addText("So can we come in?", Vector2.zero, "player");
        file.addText("...", Vector2.zero, "GovGirl");
        file.addText("Come on, we did what you asked.", Vector2.zero, "Will");
        file.addText("Fine.", Vector2.zero, "GovGirl");
        file.addText("Just don't break anything expensive.", Vector2.zero, "GovGirl");
        file.addTransition("Ending", 0, "TransitionA", "TransitionB");
        file.addEnd();
        em.saveFile("Ending", file);
        */

        /*
        file.addText("(Inspect Trevor)\n\n(Trevor stands a little bit away from you fiddling with the top of his cane. His eyes cut over to you a few times as you watch him.)", Vector2.zero, "player");
        file.addEnd();
        em.saveFile("InspectTrevor", file);
        */

        /*
        file.addText("(Inspect Barrel)\n\nThe wood is stained with age, and a bright seal is slapped on its side. As you get closer you can see burned into it: 'BEAR'S DEN BOURBON est 1806'.", Vector2.zero, "player");
        file.addText("<b>Havari</b>: Really? On the street?", Vector2.zero, "player");
        file.addEnd();
        em.saveFile("InspectBarrel", file);
        */
        
        /*
        file.addText("(Talk to Trevor)\n\n(Trevor leans back on his cane, looking around at the sleepy road.)", Vector2.zero, "player");
        file.addChoice("trevor", "<b>Trevor</b>: Looks like the locals've all gone to bed already.", "Not a lotta nightlife", 1, "I like the quiet", 4);
        file.addText("<b>Havari</b>: Yeah. Doesn't really look like the kind of place to have a nightlife scene.", Vector2.zero, "player");
        file.addText("<b>Trevor</b>: Yeah, you're ain't gonna find lot down this way.", Vector2.zero, "trevor");
        file.addSkip(0, 3);
        file.addText("<b>Havari</b>: It's nice. Really appreciating the quiet after everything.", Vector2.zero, "player");
        file.addText("<b>Trevor</b>: Yeah, it is.", Vector2.zero, "trevor");
        file.addEnd();
        em.saveFile("TalkToTrevor", file);
        */

        /*
        file.addChoice("npc", "<b>NPC</b>: ...", "Any place to stay?", 1, "Nice night", 3);

        file.addText("<b>Havari</b>: Excuse me. Is there any place to stay around here?", Vector2.zero, "player");
        file.addSkip(0, 4);

        file.addText("<b>Havari</b>: Hey. Nice night? Isn't it?", Vector2.zero, "player");
        file.addText("<b>NPC</b>: Hmph. Suppose so.", Vector2.zero, "npc");
        file.addText("<b>Havari</b>: Yeah y'know, look - is there some place around here to stay for the night?", Vector2.zero, "player");

        file.addChoice("npc", "<b>NPC</b>: Think the ol' motel's still up and runnin', could try your luck down there. Ain't many people looking' to stop and spend a night here, though.", "Why?", 1, "I'll check it out", 3);

        file.addText("<b>Havari</b>: Why's that?", Vector2.zero, "player");
        file.addText("<b>NPC</b>: Oh, we're just not too interestin' I suppose. Ain't got anythin' to bring in tourists and got nothing to keep everyone born here, well, here.", Vector2.zero, "npc");

        file.addText("<b>Havari</b>: Well. Thanks, we'll go check it out.", Vector2.zero, "player");
        file.addText("<b>NPC</b>: Sure. You kids be careful though; motel's out on a back road, and I heard it was callin' for rain.", Vector2.zero, "npc");

        file.addEnd();
        em.saveFile("TalkToNPC", file);
        */
    }

}
