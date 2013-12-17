using UnityEngine;
using System.IO;
using System.Collections;
using FourthSky.Android;

public class App : MonoBehaviour {
	
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
	
	
	void OnGUI() {
		
#if UNITY_ANDROID

		if (GUI.Button(new Rect(10, 10, 150, 100), "Launch")) {
			AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
			AndroidJavaObject pm = jo.Call<AndroidJavaObject>("getPackageManager");
			AndroidJavaObject intent = pm.Call<AndroidJavaObject>("getLaunchIntentForPackage", "com.android.settings");
			jo.Call("startActivity", intent);	
			
		}
		   
#endif        
		
	}
	
	void OnTap(TapGesture gesture) { 
		Debug.Log ("Trying to launch:" );
		try {
			AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
			AndroidJavaObject pm = jo.Call<AndroidJavaObject>("getPackageManager");
			AndroidJavaObject intent = pm.Call<AndroidJavaObject>("getLaunchIntentForPackage", "edu.mit.media.prg.alligator");
			jo.Call("startActivity", intent);	
		} catch {
			Debug.Log ("BLAH!!!!!");
		}
		
	}
	
	
}
