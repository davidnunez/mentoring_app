using UnityEngine;
using System;
using System.Collections;
using System.Reflection;
using System.Runtime.InteropServices;

namespace FourthSky {
	namespace Android {

		public static class AndroidSystem {
		
			// Delegate for activity result
			public delegate void OnActivityResult(int requestCode, int resultCode, IntPtr intentPtr);

			// Android class names
			internal static readonly string UNITY_PLAYER = "com.unity3d.player.UnityPlayer";
			internal static readonly string INTENT = "android.content.Intent";
			internal static readonly string INTENT_FILTER = "android.content.IntentFilter";
			internal static readonly string PARCELABLE = "android.os.Parcelable";
			
			// Constants for return of StartActivityForResult
			public const int RESULT_OK = -1;
			public const int RESULT_CANCELED = 0x0;
			public const int RESULT_FIRST_USER = 0x1;

#if UNITY_ANDROID && !UNITY_EDITOR			
			[DllImport("unityandroidsystem")]
			private static extern bool RegisterOnActivityResultCallback(IntPtr activityPtr, OnActivityResult callback);			
#endif
			
			public static AndroidJavaObject UnityActivity {
				get {
#if UNITY_ANDROID && !UNITY_EDITOR
					using (AndroidJavaClass unityPlayerClass = new AndroidJavaClass(UNITY_PLAYER)) {
						return unityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity");
					}
#else
					return null;				
#endif
				}
			}
			
			public static AndroidJavaObject UnityContext {
				get {
#if UNITY_ANDROID && !UNITY_EDITOR
					using (AndroidJavaClass unityPlayerClass = new AndroidJavaClass(UNITY_PLAYER)) {
		            	using (AndroidJavaObject activityInstance = unityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity")) {
							return activityInstance.Call<AndroidJavaObject>("getApplicationContext");
						}
					}
#else
					return null;				
#endif
				}
			}
			
			// TODO implement
			public static Hashtable ParseBundle(AndroidJavaObject bundleObject) {
			
				return null;
			}
			
			public static AndroidJavaObject ConstructJavaObjectFromPtr(IntPtr javaPtr) {
#if UNITY_3_3 || UNITY_3_4 || UNITY_3_5 || UNITY_4_0 || UNITY_4_0_1 || UNITY_4_1
				return new AndroidJavaObject(javaPtr);			
#else
				// Starting with Unity 4.2, AndroidJavaObject constructor with IntPtr arg is private,
				// so, construct using brute force :)
				Type t = typeof(AndroidJavaObject);
				Type[] types = new Type[1];
				types[0] = typeof(IntPtr);
				ConstructorInfo javaObjConstructor = t.GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic, null, types, null);
				
				return javaObjConstructor.Invoke(new object[] { javaPtr }) as AndroidJavaObject;
#endif
			}
			
			public static void SendBroadcast(string action, Hashtable extras = null) {
#if UNITY_ANDROID
				if (!Application.isEditor) {
					using (AndroidJavaClass unityPlayerClass = new AndroidJavaClass(AndroidSystem.UNITY_PLAYER)) {
						using (AndroidJavaObject activityInstance = unityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity")) {
							AndroidJavaObject intent = new AndroidJavaObject(AndroidSystem.INTENT, action);
							
							// Add args to intent
							if (extras != null) {
								foreach (DictionaryEntry entry in extras) {
									if (entry.Value is short) {
										intent.Call<AndroidJavaObject>("putExtra", (string)entry.Key, (short)entry.Value);
									} else if (entry.Value is int) {
										intent.Call<AndroidJavaObject>("putExtra", (string)entry.Key, (int)entry.Value);
									} else if (entry.Value is long) {
										intent.Call<AndroidJavaObject>("putExtra", (string)entry.Key, (long)entry.Value);
									} else if (entry.Value is float) {
										intent.Call<AndroidJavaObject>("putExtra", (string)entry.Key, (float)entry.Value);
									} else if (entry.Value is double) {
										intent.Call<AndroidJavaObject>("putExtra", (string)entry.Key, (double)entry.Value);
									} else if (entry.Value is string) {
										intent.Call<AndroidJavaObject>("putExtra", (string)entry.Key, (string)entry.Value);
									} else if (entry.Value is AndroidJavaObject) {
										AndroidJavaObject javaObj = entry.Value as AndroidJavaObject;
										
										using (AndroidJavaClass _Parcelable = new AndroidJavaClass(AndroidSystem.PARCELABLE)) {
											if (AndroidJNI.IsInstanceOf(javaObj.GetRawObject(), _Parcelable.GetRawClass())) {
												intent.Call<AndroidJavaObject>("putExtra", (string)entry.Key, javaObj);
											} else {
												throw new ArgumentException("Argument is not a Android Parcelable", "extra." + entry.Key);
											}
										}
									}
								}
							}
							
							activityInstance.Call("sendBroadcast", intent);
						}
					}
				}
#endif
			}
			
			public static bool StartActivityForResult(string action, int requestCode, OnActivityResult callback) {
				return StartActivityForResult(action, null, requestCode, callback);
			}
			
			public static bool StartActivityForResult(string action, AndroidJavaObject uriData, int requestCode, OnActivityResult callback) {
				if (callback == null) {
					throw new System.ArgumentNullException("OnActivityResult callback cannot be null");
				}
				
				if (string.IsNullOrEmpty(action)) {
					throw new System.ArgumentNullException("");
				}
				
				bool ret = false;
#if UNITY_ANDROID && !UNITY_EDITOR
				using (AndroidJavaObject activityInstance = AndroidSystem.UnityActivity) {
					// Unregister previous callback
					activityInstance.Set<int>("mActivityResultCallbackPtr", 0);
					
					// Register given callback
					ret = RegisterOnActivityResultCallback(activityInstance.GetRawObject(), callback);
					
					// Start given action
					AndroidJavaObject intent = uriData != null ? new AndroidJavaObject(AndroidSystem.INTENT, action, uriData)
															   : new AndroidJavaObject(AndroidSystem.INTENT, action);
					activityInstance.Call("startActivityForResult", intent, requestCode);
				}
#endif
				
				return ret;
			}
		
		}
	}
}
