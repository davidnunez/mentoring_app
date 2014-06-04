using UnityEngine;
using System.Collections;
using System.IO;
using FourthSky.Android;
using SimpleJSON;
public class Director : MonoBehaviour {

	
	
	public GameObject cameraDolly;
	
	
	void Awake() {
		Screen.orientation = ScreenOrientation.LandscapeLeft;

		string fileName = Application.persistentDataPath + "/" + "apps.json";
		string appsJSON = "";
		try {
			appsJSON = File.ReadAllText(fileName);
		} catch (System.Exception e) {
			appsJSON = @"{
	""apps"": [
		{""file"":""edu.mit.media.prg.tinkerbook_unity"",""title"":"""",""type"":""app""},	
	]
}";
		} 
			
			
			
		var N = JSON.Parse(appsJSON);
		JSONArray appArray = N["apps"].AsArray;
		
		Debug.Log("Count: " + appArray.Count);
		foreach (JSONNode node in appArray) {
			Debug.Log("title:" + node["title"] + ", file:" + node["file"] + ", type:" + node["type"]);
		}
		
		int numApps = appArray.Count;
		float numCols = 15.0f;
		float numRows = Mathf.Ceil(numApps/numCols);

		GameObject apps = GameObject.Find ("Apps");
		int i = 0;
		int j = 0;
		foreach (JSONNode node in appArray) {
			// Load app Instance
			GameObject instance = (GameObject)Instantiate(Resources.Load("prefabs/app"));
			App app = instance.GetComponent<App>();
			instance.transform.parent = apps.transform;

			
			app.package_name = node["file"];	

			if (node["type"].Value == "app") {
				app.application_type = App.ApplicationType.app;
				app.application_icon = "apps/icons/" + node["file"];
				Texture2D texture = Resources.Load(app.application_icon) as Texture2D;
				app.renderer.material.mainTexture = texture;

			} 
			if (node["type"].Value == "video") {
				app.application_type = App.ApplicationType.video;
				app.application_icon = "apps/icons/video_icon";
				Texture2D texture = Resources.Load(app.application_icon) as Texture2D;
				app.renderer.material.mainTexture = texture;

			}
			app.transform.localPosition = new Vector3(0 + i*200, 0 - j * 200, 0);
			
			float relevanceFactor = -100.0f;
			app.transform.localScale = new Vector3(relevanceFactor,-relevanceFactor,relevanceFactor);
			app.rigidbody.isKinematic = true;
						
			i = i+1;
			if (i >= numCols) {
				j = j+1;
				i = 0;
			}
		}
		
		// Position & Size Camera Dolly
		cameraDolly.transform.position = new Vector3((numCols-1.0f)*200.0f/2.0f, -(numRows-1.0f)*200.0f/2.0f, -700.0f);
		cameraDolly.transform.localScale = new Vector3((numCols-1-4)*200, (numRows-1-2)*200, 1);	
		

		 
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
