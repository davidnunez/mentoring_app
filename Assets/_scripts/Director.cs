using UnityEngine;
using System.Collections;

public class Director : MonoBehaviour {
	
	string[] appIds = new string[] {
"BMA_CO.Phonics_Lv1_Unit10",
"BMA_CO.Phonics_Lv1_Unit2",
"BMA_CO.Phonics_Lv1_Unit3",
"BMA_CO.Phonics_Lv1_Unit4",
"BMA_CO.Phonics_Lv1_Unit5",
"BMA_CO.Phonics_Lv1_Unit6",
"BMA_CO.Phonics_Lv1_Unit7",
"BMA_CO.Phonics_Lv1_Unit8",
"BMA_CO.Phonics_Lv1_Unit9",
"BMA_CO.Phonics_Lv2_Unit10",
"BMA_CO.Phonics_Lv2_Unit2",
"BMA_CO.Phonics_Lv2_Unit3",
"BMA_CO.Phonics_Lv2_Unit4",
"BMA_CO.Phonics_Lv2_Unit5",
"BMA_CO.Phonics_Lv2_Unit7",
"BMA_CO.Phonics_Lv2_Unit8",
"BMA_CO.Phonics_Lv2_Unit9",
"BMA_CO.Phonics_Lv3_Unit10",
"BMA_CO.Phonics_Lv3_Unit4",
"BMA_CO.Phonics_Lv3_Unit5",
"BMA_CO.Phonics_Lv3_Unit7",
"air.Tinkrbook",
"air.bftv.larryABCs",

"com.arent.myfirstpuzzles",
"com.arent.myfirsttangrams",
"com.aruhat.mobileapps.animalabc",
"com.carrotapp.protectpronew",
"com.codingcaveman.Solo",
"com.dolphin.browser",
"com.dornbachs.zebra",
"com.example.helloworld",

"zok.android.shapes",
		
		"com.vanluyen.KidColoringNoAds",
"com.zoodles.book.jackandthebeanstalk",
"com.zoodles.book.littleredridinghood",
"com.zoodles.book.thecountrymouseandthecitymouse",
"com.zoodles.book.theelvesandtheshoemaker",
"com.zoodles.book.theemperorsnewclothes",
"com.zoodles.book.thethreelittlepigs",
"com.zoodles.book.thetortoiseandthehare",
"com.zoodles.book.thevelveteenrabbit",
		
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
		int j = 0;
		foreach (string appId in appIds) {
			GameObject instance = (GameObject)Instantiate(Resources.Load("prefabs/app"));
			App app = instance.GetComponent<App>();
			
			app.package_name = appId;
			app.application_icon = "apps/icons/" + appId;
			Texture2D texture = Resources.Load(app.application_icon) as Texture2D;
			app.renderer.material.mainTexture = texture;
			//app.transform.localPosition = new Vector3(Random.Range(-1900,1900), Random.Range (-400,400), 0);
			
			app.transform.localPosition = new Vector3(-1000 + i*200, -400 + j * 200, 0);
			
			
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
			if (i > 8) {
				j = j+1;
				i = 0;
			}
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

	
}
