using UnityEngine;
using System.Collections;
using System.IO;
using FourthSky.Android;

public class Director : MonoBehaviour {

	string[,] appIds = new string[,] {
{"WHERE_THE_WILD_THINGS_ARE6.mp4","Alligators all around","video"},
{"MAKE_WAY_FOR_DUCKLINGS5.mp4","Cat & Canary","video"},
{"WHOLE_WORLD3.mp4","Come on Rain","video"},
{"WHOLE_WORLD2.mp4","He's got the whole world in his hands (Song)","video"},
{"Meet_the_Letters.mp4","Meet the Letters","video"},
{"LADY_WHO_SWALLOWED_A_FLY4.mp4","Over in the Meadow","video"},
{"LADY_WHO_SWALLOWED_A_FLY4.mp4","Over in the Meadow","video"},
{"IS_YOUR_MAMA_A_LLAMA5.mp4","Reading to Your Bunny","video"},
{"JAMES_MARSHALL_COLLECTION6.mp4","Space Case","video"},
{"STREGA_NONA4.mp4","The Amazing Bone","video"},
{"IS_YOUR_MAMA_A_LLAMA4.mp4","The Little Red Hen","video"},
{"BLUEGOSCHOOL.mp4","Blue Goes to School 1","video"},
{"CLICK_CLACK_MOO5.mp4","Hot Hippo","video"},
{"CHICKA_CHICKA_BOOM_BOOM7.mp4","Millions of Cats","video"},
{"EZRA_JACK_KEATS5.m4v.mp4","A Letter to Amy","video"},
{"PEEP_eng_2.mp4","A Peep of a Different Color","video"},
{"CHICKA_CHICKA_BOOM_BOOM6.mp4","A Story A Story","video"},
{"WHERE_THE_WILD_THINGS_ARE6.mp4","Alligators all around","video"},
{"MAKE_WAY_FOR_DUCKLINGS4.mp4","Angus and the Ducks","video"},
{"HARRY_THE_DIRTY_DOG6.mp4","Angus Lost","video"},
{"I_STINK6.mp4","Ban the Doughnut","video"},
{"CHICKA_CHICKA_BOOM_BOOM5.mp4","Blueberries for Sal","video"},
{"WHEELS_ON_THE_BUS4.mp4","Bugs! Bugs! Bugs!","video"},
{"ANTARCTIC_ANTICS4.mp4","Caps for Sale","video"},
{"MAKE_WAY_FOR_DUCKLINGS5.mp4","Cat & Canary","video"},
{"LADY_WHO_SWALLOWED_A_FLY8.mp4","Changes, Changes","video"},
{"LADY_WHO_SWALLOWED_A_FLY8.mp4","Changes, Changes","video"},
{"WHERE_THE_WILD_THINGS_ARE4.mp4","Chicken soup with rice","video"},
{"WHOLE_WORLD3.mp4","Come on Rain","video"},
{"CORDUROY2.mp4","Corduroy","video"},
{"HOW_DO_DINOSAURS_SAY_GOODNIGHT4.mp4","Danny and the Dinosaur","video"},
{"HOW_DO_DINOSAURS_SAY_GOODNIGHT5.mp4","Dinosaur Bones","video"},
{"LE_QUACK3.m4v.mp4","Dooby Dooby Moo","video"},
{"HARRY_THE_DIRTY_DOG4.mp4","Dot the Fire Dog","video"},
{"GOOD_NIGHT_GORILLA6.m4v.mp4","Elizabeti's Doll","video"},
{"CURIOUS_GEORGE_BIKE12.mp4","Frog Goes to Dinner","video"},
{"LADY_WHO_SWALLOWED_A_FLY5.mp4","Giraffes Can't Dance","video"},
{"CORDUROY7.mp4","Goose","video"},
{"CURIOUS_GEORGE_BIKE11.mp4","Here Comes the Cat","video"},
{"CHRYSANTHEMUM5.mp4","Hondo & Fabian","video"},
{"CLICK_CLACK_MOO5.mp4","Hot Hippo","video"},
{"CHICKA_CHICKA_123.mp4","How Much is a Million","video"},
{"I_LOVE_MY_ABCS_ANIMALS.mp4","I Love My ABCs - Animals","video"},
{"HARRY_THE_DIRTY_DOG5.mp4","I Want a Dog","video"},
{"WHEELS_ON_THE_BUS8.mp4","Keeping House","video"},
{"WHEELS_ON_THE_BUS8.mp4","Keeping House","video"},
{"LEAP_FROG_LETTER_FACTORY.mp4","Leap Frog Letter Factory","video"},
{"LEAPFROG_PHONIC_FARM.mp4","LeapFrog Phonic Farm","video"},
{"Swimmy8.mp4","Let's Give Kitty a Bath","video"},
{"littleeinsteins1.mp4","Little Einsteins","video"},
{"A_VERY_BRAVE_WITCH13.mp4","Little Tim and the Brave Sea Captain","video"},
{"RAPUNZEL5.mp4","Lon Po PO ( A Red-Riding Hood Story from China)","video"},
{"Meet_the_numbers.mp4","Meet the Numbers","video"},
{"MEET_THE_SHAPES_081910.mp4","Meet the Shapes","video"},
{"CHICKA_CHICKA_BOOM_BOOM7.mp4","Millions of Cats","video"},
{"CHRYSANTHEMUM6.mp4","Mouse Around","video"},
{"PEEP_eng_1.mp4","Night Light","video"},
{"IS_YOUR_MAMA_A_LLAMA3.mp4","Noisy Nora","video"},
{"WHERE_THE_WILD_THINGS_ARE5.mp4","One was Johnny","video"},
{"LADY_WHO_SWALLOWED_A_FLY4.mp4","Over in the Meadow","video"},
{"WHOLE_WORLD4.mp4","Owl Moon","video"},
{"WHOLE_WORLD5.mp4","Patrick","video"},
{"PEEP_eng_5.mp4","Peep Plants a Seed","video"},
{"EZRA_JACK_KEATS4.m4v.mp4","Pet Show!","video"},
{"RAPUNZEL2.mp4","Rapunzel","video"},
{"IS_YOUR_MAMA_A_LLAMA5.mp4","Reading to Your Bunny","video"},
{"TEACHER_FROM_THE_BLACK_LAGOON9.mp4","Shrinking Violet","video"},
{"TEACHER_FROM_THE_BLACK_LAGOON9.mp4","Shrinking Violet","video"},
{"CORDUROY5.mp4","Smile for Auntie","video"},
{"PEEP_eng_4.mp4","Sounds Like..","video"},
{"JAMES_MARSHALL_COLLECTION6.mp4","Space Case","video"},
{"HOW_DO_DINOSAURS_SAY_GOODNIGHT9.mp4","T is For Terrible","video"},
{"STREGA_NONA4.mp4","The Amazing Bone","video"},
{"STREGA_NONA4.mp4","The Amazing Bone","video"},
{"LADY_WHO_SWALLOWED_A_FLY7.mp4","The Chinese Violin","video"},
{"PEEP_eng_3.mp4","The Fish Museum","video"},
{"HAROLD_AND_THE_PURPLE_CRAYON6.mp4","The Foolish Frog","video"},
{"PETES_A_PIZZA4.mp4","The Great White Man-Eating Shark","video"},
{"CORDUROY6.mp4","The Happy Owls","video"},
{"IS_YOUR_MAMA_A_LLAMA4.mp4","The Little Red Hen","video"},
{"ANTARCTIC_ANTICS6.mp4","The Little Red Lighthouse and the Great Grey Bridge","video"},
{"RAPUNZEL4.mp4","The Three Billy Goats Gruff","video"},
{"A_VERY_BRAVE_WITCH11.mp4","The Three-Legged Cat","video"},
{"EZRA_JACK_KEATS6.m4v.mp4","The Trip","video"},
{"PEEP_eng_6.mp4","The Whatchamacallit","video"},
{"HAROLD_AND_THE_PURPLE_CRAYON5.mp4","Time of Wonder","video"},
{"Tinga_Tinga_whyhenpecksground.mp4","Tinga Tinga - Why Hen Pecks the Ground","video"},
{"Tinga_Tinga_whysnakehasnolegs.mp4","Tinga Tinga - Why Snake has no Legs","video"},
{"TINGA_TINGA 2-5.mp4","Tinga Tinga 2-5","video"},
{"TINGA_TINGA 2-6.mp4","Tinga Tinga 2-6","video"},
{"TINGA_TINGA 2-7.mp4","Tinga Tinga 2-7","video"},
{"TINGA_TINGA 3-1.mp4","Tinga Tinga 3-1","video"},
{"TINGA_TINGA 3-2.mp4","Tinga Tinga 3-2","video"},
{"TINGA_TINGA 3-3.mp4","Tinga Tinga 3-3","video"},
{"TINGA_TINGA 3-4.mp4","Tinga Tinga 3-4","video"},
{"TINGA_TINGA 3-5.mp4","Tinga Tinga 3-5","video"},
{"TINGA_TINGA 3-6.mp4","Tinga Tinga 3-6","video"},
{"TINGA_TINGA 3-7.mp4","Tinga Tinga 3-7","video"},
{"CLICK_CLACK_MOO6.mp4","Waiting for Wings","video"},
{"PETES_A_PIZZA7.mp4","What's Under My Bed?","video"},
{"MAKE_WAY_FOR_DUCKLINGS6.mp4","Wings: A Tale of Two Chickens","video"},
{"GOOD_NIGHT_GORILLA5.m4v.mp4","Zin! Zin! Zin! A Violin!","video"},
{"BMA_CO.Phonics_Lv1_Unit10","","app"},
{"BMA_CO.Phonics_Lv1_Unit2","","app"},
{"BMA_CO.Phonics_Lv1_Unit3","","app"},
{"BMA_CO.Phonics_Lv1_Unit4","","app"},
{"BMA_CO.Phonics_Lv1_Unit5","","app"},
{"BMA_CO.Phonics_Lv1_Unit6","","app"},
{"BMA_CO.Phonics_Lv1_Unit7","","app"},
{"BMA_CO.Phonics_Lv1_Unit8","","app"},
{"BMA_CO.Phonics_Lv1_Unit9","","app"},
{"BMA_CO.Phonics_Lv2_Unit10","","app"},
{"BMA_CO.Phonics_Lv2_Unit2","","app"},
{"BMA_CO.Phonics_Lv2_Unit3","","app"},
{"BMA_CO.Phonics_Lv2_Unit4","","app"},
{"BMA_CO.Phonics_Lv2_Unit5","","app"},
{"BMA_CO.Phonics_Lv2_Unit7","","app"},
{"BMA_CO.Phonics_Lv2_Unit8","","app"},
{"BMA_CO.Phonics_Lv2_Unit9","","app"},
{"BMA_CO.Phonics_Lv3_Unit10","","app"},
{"BMA_CO.Phonics_Lv3_Unit4","","app"},
{"BMA_CO.Phonics_Lv3_Unit5","","app"},
{"BMA_CO.Phonics_Lv3_Unit7","","app"},
{"air.bftv.larryABCs","","app"},
{"com.arent.myfirstpuzzles","","app"},
{"com.arent.myfirsttangrams","","app"},
{"com.aruhat.mobileapps.animalabc","","app"},
{"com.codingcaveman.Solo","","app"},
{"com.dornbachs.zebra","","app"},
{"edu.mit.media.prg.tinkerbook_unity","","app"},
{"zok.android.shapes","","app"},
{"com.vanluyen.KidColoringNoAds","","app"},
{"com.zoodles.book.jackandthebeanstalk","","app"},
{"com.zoodles.book.littleredridinghood","","app"},
{"com.zoodles.book.thecountrymouseandthecitymouse","","app"},
{"com.zoodles.book.theelvesandtheshoemaker","","app"},
{"com.zoodles.book.theemperorsnewclothes","","app"},
{"com.zoodles.book.thethreelittlepigs","","app"},
{"com.zoodles.book.thetortoiseandthehare","","app"},
{"com.zoodles.book.thevelveteenrabbit","","app"},
{"com.intellijoy.sightwords","","app"},
{"com.goatella.beginningblends","","app"},	
{"com.vanluyen.KidColoringNoAds", "", "app"},
{"russh.toddler.game", "", "app"},
{"com.tomatointeractive.gpp", "", "app"},
{"com.oncilla.LetterTracing", "", "app"},
{"com.dornbachs.zebra", "", "app"},
{"org.pogi.DrawingPad", "", "app"},
{"com.tomatointeractive.gmz", "", "app"},
{"pl.ayground.littlepiano", "", "app"},
{"com.thup.lunchbox", "", "app"},
{"com.arent.myfirstpuzzles", "", "app"},
{"com.babyfirsttv.larrythingsthatgo", "", "app"},
{"com.toddlerjukebox", "", "app"},		

	};
	
	
	
	/*
	 * 
	 * "com.geesun.android.loveabc",
"com.gi.disney.winnie_the_pooh_puzzle_book.main",
"com.google.android.gms",
"com.intellijoy.abc.trains.lite",
"com.kiddoware.letters",
"com.mds.apps.abcsarefun",
"com.mds.kidsapps.alphakids",
"com.metago.astro",
"com.noshufou.android.su",
"com.oncilla.LetterTracing",
"com.ramkystech.android.alphabets.lite",
"com.rechild.advancedtaskkillerpro",
"com.remind4u2.sounds.of.letters.alphabet.kids",
"com.shinycube.android.fun4kids.preschoolmemorygamep",
"com.sight.words.rock.learn",
"com.sight.words.two.rock.learn",
"com.storychimes.theredshoes",
"com.storychimes.theredshoesnoad",
"com.storychimes.uglyducklingnoad",
"com.tb.hd.en.gato_con_botas.main",
"com.tb.hd.en.granja.main",
"com.tb.hd.en.luna.main",
"com.tb.sd.en.playa.main",
"com.theappmechanics.ngalphabetanimals",
"com.thup.lunchbox",
"com.toddlerjukebox",
"com.tomatointeractive.gmz",
"com.tomatointeractive.gpp",
"com.unity3d.JavaPlugin",
"com.unity3d.player",

"edu.mit.media.funf.bgcollector",
"edu.mit.media.prg.alligator",
"edu.mit.media.prg.androidsystem",
"edu.mit.media.prg.fixitapp",
"edu.mit.media.prg.funftoggle",
"edu.mit.media.prg",
"edu.mit.media.prg.launcher",
"edu.mit.media.prg.tinkrbook.bath",
"jackpal.androidterm",
"lt.andro.broadcastlogger",
"net.cwfk.ig88.intents",
"org.adwfreak.launcher",
"org.droidgames.abcs",
"org.pogi.DrawingPad",
"pl.ayground.littlepiano",
"russh.toddler.colors",
"russh.toddler.game",
"sunny_day.kids_piano",
"tipitap.coloring",
"tk.pankratz.learningletters",
"zok.android.letters",
	 * 
	 * 
	 * 
	 */
	
	
	
	
	
	
	void Awake() {
		GameObject apps = GameObject.Find ("Apps");
		int i = 0;
		int j = 1;
		for (int k=0; k < appIds.GetLength(0); k++) {
			
			GameObject instance = (GameObject)Instantiate(Resources.Load("prefabs/app"));
			App app = instance.GetComponent<App>();
			
			app.package_name = appIds[k, 0];	
			
/*			app.application_icon = "apps/icons/" + appIds[k, 0];
			Texture2D texture = Resources.Load(app.application_icon) as Texture2D;
			app.renderer.material.mainTexture = texture;
					 */
			if (appIds[k, 2] == "app") {
				app.application_type = App.ApplicationType.app;
				app.application_icon = "apps/icons/" + appIds[k, 0];
				Texture2D texture = Resources.Load(app.application_icon) as Texture2D;
				app.renderer.material.mainTexture = texture;

			} 
			if (appIds[k, 2] == "video") {
				app.application_type = App.ApplicationType.video;
				app.application_icon = "apps/icons/video_icon";
				Texture2D texture = Resources.Load(app.application_icon) as Texture2D;
				app.renderer.material.mainTexture = texture;

			}
			//app.transform.localPosition = new Vector3(Random.Range(-1900,1900), Random.Range (-400,400), 0);
			
			
			//app.transform.localPosition = new Vector3(-1500 + i*200, 400 - j * 200, 0);
			app.transform.localPosition = new Vector3(0 + i*200, 0 - j * 200, 0);
			
			//float relevanceFactor = Remap(Mathf.Abs(instance.transform.position.x), 1900, 0, 24, 128);
			float relevanceFactor = -100.0f;
			app.transform.localScale = new Vector3(relevanceFactor,-relevanceFactor,relevanceFactor);
			
			
			app.rigidbody.isKinematic = true;
			
			//instance.AddComponent<SpringJoint>();
			//SpringJoint springJoint = instance.GetComponent<SpringJoint>();
			//springJoint.spring = 1000;
			//springJoint.damper = 100;
			//springJoint.maxDistance = 0;
			
			instance.transform.parent = apps.transform;
			i = i+1;
			if (i >= 15) {
				j = j+1;
				i = 0;
			}
		}
		string fileName = Application.persistentDataPath + "/" + "test.txt";
		StreamWriter fileWriter = File.CreateText(fileName);
		fileWriter.WriteLine("Hello world");
		fileWriter.Close();
		
	}
	
	
	void OnApplicationPause (bool pause) {
   		if(pause) {
			sendLog ("LauncherAppPaused", "{\"paused\": true}");
			Debug.Log ("launcherapp paused");
		} else {
			sendLog ("LauncherAppPaused", "{\"paused\": false}");
			Debug.Log ("launcherapp unpaused");
   		}
	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	
	public float Remap (this float value, float from1, float to1, float from2, float to2) {

    	return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
	}
	
	void sendLog(string _name, string _value) {
		var unixTime = System.DateTime.Now.ToUniversalTime() - new System.DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
		
		Hashtable extras = new Hashtable();
		extras.Add("DATABASE_NAME", "mainPipeline");
		extras.Add("TIMESTAMP", (long)unixTime.TotalMilliseconds);
		extras.Add("NAME", _name);
		extras.Add("VALUE", _value);
			
		AndroidSystem.SendBroadcast("edu.mit.media.funf.RECORD", extras);
	}
	
}
