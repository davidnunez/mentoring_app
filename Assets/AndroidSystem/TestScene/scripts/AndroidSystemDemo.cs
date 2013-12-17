using UnityEngine;
using System;
using System.IO;
using System.Collections;
using FourthSky.Android;

public class AndroidSystemDemo : MonoBehaviour {

	// Broadcast Receiver constants and variables
	private static readonly string testBroadcastAction = "com.unity.bcast.test";
	private static readonly string ACTION_HEADSET_PLUG = "android.intent.action.HEADSET_PLUG";
	private static readonly string ACTION_BATTERY_CHANGED = "android.intent.action.BATTERY_CHANGED";
	
	private BroadcastReceiver testBroadcastReceiver;
	private static string broadcastMessage = "BroadcastReceiver unregistered";
	
	
	// Service Connection and Binder constants and variables
	private static readonly string billingServiceAction = "com.android.vending.billing.InAppBillingService.BIND";
	/*
	private static readonly string ACTION_NOTIFY = "com.android.vending.billing.IN_APP_NOTIFY";
    private static readonly string ACTION_RESPONSE_CODE = "com.android.vending.billing.RESPONSE_CODE";
    private static readonly string ACTION_PURCHASE_STATE_CHANGED = "com.android.vending.billing.PURCHASE_STATE_CHANGED";
	private static readonly string NOTIFICATION_ID = "notification_id";
    private static readonly string INAPP_SIGNED_DATA = "inapp_signed_data";
    private static readonly string INAPP_SIGNATURE = "inapp_signature";
    private static readonly string INAPP_REQUEST_ID = "request_id";
    private static readonly string INAPP_RESPONSE_CODE = "response_code";
	*/
	
	private ServiceConnection billingServiceConnection;
	private static IInAppBillingService billingBinder;
	private static string packageName;
	private static string billingMessage = "Billing service disabled";
	
	
	// OnActivityResult constants variables
	private static readonly int PICK_IMAGE = 13463;

	private static string imagePickPath = "No image choosen";
	private static bool foundTexture = false;
	public GameObject photoPlane;
	
	
	// GUI measures
	private int GUI_MARGIN = 30;
	private int GUI_BUTTON_WIDTH = Screen.width - 60;
	private int GUI_BUTTON_HEIGHT = 50;	
	
	
	// Use this for initialization
	void Awake () {
		
#if UNITY_ANDROID
		if (!Application.isEditor) {
			packageName = AndroidSystem.UnityActivity.Call<string>("getPackageName");
		}
#endif
		
		// Create broadcast receiver
		testBroadcastReceiver = new BroadcastReceiver();
		testBroadcastReceiver.OnReceive += OnReceive;
		
		// Create service connection 
		billingServiceConnection = new ServiceConnection();
		billingServiceConnection.OnServiceConnected += OnServiceConnected;
		billingServiceConnection.OnServiceDisconnected += OnServiceDisconnected;
	}
	
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetKey(KeyCode.Escape)) {
			Application.Quit();
		}
		
		// Load choosen texture from gallery
		// Has to be done here, because Unity objects can only be created on the main thread
		LoadTextureFromBytes();
	}
	
	void OnApplicationQuit() {
		// Dispose objects
		if (testBroadcastReceiver != null) 
			testBroadcastReceiver.Dispose();
		
		if (billingServiceConnection != null) 
			billingServiceConnection.Dispose();
	}
	
	void OnGUI() {
		GUI.Label(new Rect(GUI_MARGIN, 15, 650, 25), "Android System Plugin Demo from Fourth Sky Interactive");
		
		// Register broadcast receiver
		if (GUI.Button(new Rect(GUI_MARGIN, 40, GUI_BUTTON_WIDTH, GUI_BUTTON_HEIGHT), "Register Broadcast Receiver")) {
			testBroadcastReceiver.Register(testBroadcastAction, ACTION_HEADSET_PLUG, ACTION_BATTERY_CHANGED);
			
			broadcastMessage = "BroadcastReceiver registered";
		}

		// Send a custom broadcast
		if (GUI.Button(new Rect(GUI_MARGIN, 100, GUI_BUTTON_WIDTH, GUI_BUTTON_HEIGHT), "Send Broadcast")) {
			Hashtable extras = new Hashtable();
			extras.Add("message", "Hello broadcast!!!");
			
			AndroidSystem.SendBroadcast(testBroadcastAction, extras);
		}
		
		// Unregister broadcast receiver
		if (GUI.Button(new Rect(GUI_MARGIN, 160, GUI_BUTTON_WIDTH, GUI_BUTTON_HEIGHT), "Unregister Broadcast Receiver")) {
			testBroadcastReceiver.Unregister();
			
			broadcastMessage = "BroadcastReceiver unregistered";
		}
		
		// Printing broadcast messages
		GUI.Label(new Rect(GUI_MARGIN, 220, 500, 30), broadcastMessage);		
		
		
		// Bind service connection to In-App Billing v3
		if (GUI.Button(new Rect(GUI_MARGIN, 250, GUI_BUTTON_WIDTH, GUI_BUTTON_HEIGHT), "Initiate Billing Service")) {
			billingServiceConnection.Bind(billingServiceAction);
		}
		
		// Check if billing is supported on this device
		// This can be affected if device has network connection
		if (GUI.Button(new Rect(GUI_MARGIN, 310, GUI_BUTTON_WIDTH, GUI_BUTTON_HEIGHT), "Check Billing Supported")) {
			int responseCode = billingBinder.IsBillingSupported(3, packageName, "inapp");
			if (responseCode == 0) {
				billingMessage = "Billing supported";
			} else {
				billingMessage = "Billing unsupported";
			}
		}
		
		// Unbind In-App Billing
		if (GUI.Button(new Rect(GUI_MARGIN, 370, GUI_BUTTON_WIDTH, GUI_BUTTON_HEIGHT), "Shutdown Billing Service")) {
			billingServiceConnection.Unbind();
			billingMessage = "Billing service disabled";
		}
		
		// Printing billing messages
		GUI.Label(new Rect(GUI_MARGIN, 430, 200, 30), billingMessage);
		
		
		// Pick an image from gallery
		if (GUI.Button(new Rect(GUI_MARGIN, 460, GUI_BUTTON_WIDTH, GUI_BUTTON_HEIGHT), "Pick an image from gallery")) {
			PickImageFromGallery(true);			
		}
		
		// Printing image url
		GUI.Label(new Rect(GUI_MARGIN, 520, 700, 30), imagePickPath);
	}
	
	// Broadcast receiver callback
	private static void OnReceive(IntPtr contextPtr, IntPtr intentPtr) {
		AndroidJavaObject intent = AndroidSystem.ConstructJavaObjectFromPtr(intentPtr);
		string action = intent.Call<string>("getAction");
		
		if (action == testBroadcastAction) {
			string message = intent.Call<string>("getStringExtra", "message");
		
			broadcastMessage = "Broadcast message received: " + message;
			
		} else if (action == ACTION_HEADSET_PLUG) {
			int headsetState = intent.Call<int>("getIntExtra", "state", 0);
			
			if (headsetState == 1) {
				string headsetName = intent.Call<string>("getStringExtra", "name");
				int hasMicrophone = intent.Call<int>("getIntExtra", "microphone", 0);
				broadcastMessage = "Headset plugged (" + headsetName + " - " + (hasMicrophone == 1 ? " with mic" : "no mic") + ")";
				
				Debug.Log("Headset plugged (" + headsetName + " - " + (hasMicrophone == 1 ? " with mic" : "no mic") + ")");
			} else {
				broadcastMessage = "Headset unplugged";
				
				Debug.Log("Headset unplugged");
			}
			
		} else if (action == ACTION_BATTERY_CHANGED) {
			int batteryLevel = intent.Call<int>("getIntExtra", "level", 0);
			int batteryMaxLevel = intent.Call<int>("getIntExtra", "scale", 0);
			int isPlugged = intent.Call<int>("getIntExtra", "plugged", 0);
			
			if (isPlugged == 1) {
				broadcastMessage = "Power plugged (battery level " + batteryLevel + "/" + batteryMaxLevel + ")";				
			} else {
				broadcastMessage = "Power unplugged (battery level " + batteryLevel + "/" + batteryMaxLevel + ")";
			}
			
		}
	}
	
	// Service connected callback
	private static void OnServiceConnected(IntPtr namePtr, IntPtr binderPtr) {
		if (binderPtr == IntPtr.Zero) {
			Debug.Log("Something's wrong");	
		}
		
		billingBinder = IInAppBillingService.Wrap(binderPtr);
		billingMessage = "Billing service enabled";
	}
	
	// Service disconnected callback
	private static void OnServiceDisconnected(IntPtr name) {
		billingBinder.Dispose();
		billingBinder = null;
		billingMessage = "Billing service disabled";
	}
	
	// OnActivityResult callback
	private static void OnActivityResult(int requestCode, int resultCode, IntPtr intentPtr) {
		try {
			if (requestCode == PICK_IMAGE) {
				switch(resultCode) {				
					case AndroidSystem.RESULT_OK:
						AndroidJavaObject intent = AndroidSystem.ConstructJavaObjectFromPtr(intentPtr);
					
						// Get URI path (for example: content://media/external/images/media/712)
						string contentUri = intent.Call<AndroidJavaObject>("getData").Call<string>("toString");
						Debug.Log("URI from picked image: " + contentUri);
						
						// Get real path of file (for example: mnt/images/IMG357.jpg )
						imagePickPath = GetFilePathFromUri(contentUri);
						Debug.Log("File path of picked image: " + imagePickPath);
						
						// Load bytes and signal main thread create texture
						foundTexture = true;
						
						break;
					
					case AndroidSystem.RESULT_CANCELED:
						Debug.Log("Pick image operation cancelled");
						break;
					
					default:
						Debug.LogError("Error occurred picking image from gallery");
						break;
						
				}
			}
			
		} catch (Exception ex) {
			Debug.LogException(ex);
		}
	}
	
	private void PickImageFromGallery(bool externalStorage) {
		// Get ACTION_PICK constant from Intent class
		string pickImageAction = new AndroidJavaClass(AndroidSystem.INTENT).GetStatic<string>("ACTION_PICK");
		
		// Alert: "$" character denotes inner classes
		// From MediaStore.Images class (there are also MediaStore.Video, MediaStore.Audio and MediaStore.Files) ...
		using (AndroidJavaClass mediaClass = new AndroidJavaClass("android.provider.MediaStore$Images$Media")) {
			
			AndroidJavaObject pickImageURI = null;
			if (externalStorage) {
				// ... pick URI for external content (SD card) ...
				pickImageURI = mediaClass.GetStatic<AndroidJavaObject>("EXTERNAL_CONTENT_URI");
			
			} else {
				// ... or internal content (internal memory) ...
				pickImageURI = mediaClass.GetStatic<AndroidJavaObject>("INTERNAL_CONTENT_URI");
				
			}
			
			// ... and start gallery app to pick an image.
			AndroidSystem.StartActivityForResult(pickImageAction, pickImageURI, PICK_IMAGE, OnActivityResult);
		}
	}
	
	// Load image from 
	private void LoadTextureFromBytes() {
		
		if (foundTexture) {
			foundTexture = false;
			
			// Read all bytes from image file
			float start = Time.realtimeSinceStartup;
			byte[] imgBytes = File.ReadAllBytes(imagePickPath);
			Debug.Log("Image bytes read from file in " + (Time.realtimeSinceStartup - start) + " seconds");
			
			// Create a new texture and load file content to it
			Texture2D tex = new Texture2D(4, 4);
			tex.wrapMode = TextureWrapMode.Clamp;
			
			start = Time.realtimeSinceStartup;
			bool loaded = tex.LoadImage(imgBytes);
			Debug.Log("Image bytes loaded in " + (Time.realtimeSinceStartup - start) + " seconds");
			
			if (loaded) {
				Debug.Log("Texture format: " + tex.format + "\n" +
						  "Texture dimensions" + tex.width + "x" + tex.height);
				
				// Assign texture to plane object
				photoPlane.renderer.material.mainTexture = tex;
				
			} else {
				Debug.LogError("Error loading image from " + imagePickPath);
				
			}
			
		}
	}
	
	// Get real path of file (for example: /mnt/sdcard/DCIM/Camera/IMG010.jpg)
	// Code example from http://stackoverflow.com/questions/3401579/get-filename-and-path-from-uri-from-mediastore
	private static string GetFilePathFromUri(string contentUri) {
		// Uri of the file for the gallery 
		AndroidJavaObject uri = new AndroidJavaClass("android.net.Uri").CallStatic<AndroidJavaObject>("parse", contentUri);
		
		// Array of column names to get from database
		AndroidJavaObject proj = AndroidSystem.ConstructJavaObjectFromPtr(AndroidJNI.NewObjectArray(1, 
																	AndroidJNI.FindClass("java/lang/String"), 
																	AndroidJNI.NewStringUTF("_data")));		
		
		// This code is for Android 3.0 (SDK 11) and up
		/*
		AndroidJavaObject loader = new AndroidJavaObject("android.content.CursorLoader", 
														 AndroidSystem.UnityContext,
														 contentUri,
														 proj,
														 null,
														 null,
														 null);
		AndroidJavaObject cursor = loader.Call<AndroidJavaObject>("loadInBackground");
		*/
		
		// These two lines is for any Android version, but is deprecated for Android 3.0 and up
		// Need to get method through AndroidJNI
		IntPtr managedQueryMethod = AndroidJNIHelper.GetMethodID(AndroidSystem.UnityActivity.GetRawClass(), "managedQuery");
		AndroidJavaObject cursor = AndroidSystem.ConstructJavaObjectFromPtr(AndroidJNI.CallObjectMethod(AndroidSystem.UnityActivity.GetRawObject(), 
																					 					managedQueryMethod,
																					 					AndroidJNIHelper.CreateJNIArgArray(new object[] {uri, 
																																	proj, 
																																	null, 
																																	null, 
																																	null})));
		
		// This way is easier, but don't works, maybe due to return type not clearly specified
		// AndroidJavaObject cursor = AndroidSystem.UnityActivity.Call<AndroidJavaObject>("managedQuery", uri, proj, "", null, "");
																																	
		// Get column index
		int columnIndex = cursor.Call<int>("getColumnIndexOrThrow", "_data");
		cursor.Call<bool>("moveToFirst");
		
		// Finally, get image file path
		string contentPath = cursor.Call<string>("getString", columnIndex);
		
		cursor.Dispose();
		proj.Dispose();
		
		return contentPath;
	}
}

