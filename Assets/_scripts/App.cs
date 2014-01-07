using UnityEngine;
using System.IO;
using System.Collections;
using FourthSky.Android;

public class App : MonoBehaviour {
	public enum ApplicationType {app, video};
	public ApplicationType application_type;
	public string application_label = "";
	public string package_name = "";
	public string package_versionCode = "";
	public string package_versionName = "";
	public string application_icon  = "";
	public string launchable_activity_name = "";
	
	public string[] tagNames;
	public float[] tagWeights;
	
	public bool installed;
	public float relevance;
	
	public float relevance_influence_on_size;
	public float relevance_influence_on_distance_from_center;
	public float relevance_influence_on_color;
	public float relevance_influence_on_bouncing;
	public float relevance_incluence_on_gravity;
	
	bool t1;
	bool t2;
	bool t3;
	bool t0;

	Mentor mentor;
	// Use this for initialization
	void Awake() {

		t0 = (Random.value >= 0.5);
		t1 = (Random.value >= 0.5);
		t2 = (Random.value >= 0.5);
		t3 = (Random.value >= 0.5);
	
	
		int numberOfTrues = 0;
		if (t0) {
			numberOfTrues++;
		}
		if (t1) {
			numberOfTrues++;
		}		
		if (t2) {
			numberOfTrues++;
		}
		if (t3) {
			numberOfTrues++;
		}
		
		
		tagNames = new string[numberOfTrues];
		
		int i = 0;
		
		if (t0) {
			tagNames[i] = "t0";
			i++;
		}
		if (t1) {
			tagNames[i] = "t1";
			i++;
		}		
		if (t2) {
			tagNames[i] = "t2";	
			i++;
		}
		if (t3) {
			tagNames[i] = "t3";
			i++;
		}
		
		
	}

		
		
		
	
		
	
	
	void Start () {
		GameObject go = GameObject.Find ("Mentor");
		mentor = go.GetComponent<Mentor>();		
		//transform.localPosition = new Vector3(transform.localPosition.x, Random.Range(-1000 , 1000) ,transform.localPosition.z); 
	
	}
	
	
	
	
	
	// Update is called once per frame
	void Update () {
	
	}
	
	
	
	void LateUpdate() {
		//relevance = mentor.GetAverageTagRelevance(tagNames);
//		transform.localScale	
		//float localScaleTransform = 2 * relevance; // * relevance_influence_on_size;
		//transform.localScale = new Vector3 (localScaleTransform, localScaleTransform, 1);
		//transform.localPosition = new Vector3(1900*relevance, transform.localPosition.y ,0); 
	
	}
	
	
	
	void LaunchApp() {
		Debug.Log("Launching App");	
	}	
	
	

	
	void OnTap(TapGesture gesture) { 
		Debug.Log ("Trying to launch:" );
		try {
			if (application_type == ApplicationType.app) {
				AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
				AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
				AndroidJavaObject pm = jo.Call<AndroidJavaObject>("getPackageManager");
				AndroidJavaObject intent = pm.Call<AndroidJavaObject>("getLaunchIntentForPackage", package_name);
				jo.Call("startActivity", intent);	
			}
			if (application_type == ApplicationType.video) {
				
				Handheld.PlayFullScreenMovie ("file:///sdcard/Movies/" + package_name, Color.black, FullScreenMovieControlMode.Full);	
			}
			
			sendLog ("LauncherApp", package_name);
		} catch {
			Debug.Log ("Had Trouble Starting: " + package_name);
		}
		
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
