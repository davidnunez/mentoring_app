using UnityEngine;
using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace FourthSky {
	namespace Android {
		
		/// <summary>
		/// Broadcast receiver.
		/// </summary>
		public class BroadcastReceiver : AndroidWrapper {
		
			/// <summary>
			/// On receive delegate.
			/// </summary>
			public delegate void OnReceiveDelegate(IntPtr contextPtr, IntPtr intentPtr);
			
			
			public event OnReceiveDelegate OnReceive;
			
			public bool mRegistered;
			public bool Registered {
				get {
					return mRegistered;
				}
			}
			
			public BroadcastReceiver() : base() {
				
			}
			
			public void Register(params string[] actions) {
#if UNITY_ANDROID
				// Create java instance of broadcast receiver
				if (mJavaObject == null) {
					IntPtr receiverPtr = CreateJavaBroadcastReceiver(OnReceive);
					if (IntPtr.Zero != receiverPtr) {
						mJavaObject = AndroidSystem.ConstructJavaObjectFromPtr(receiverPtr);
					}
				}
				
				using (AndroidJavaObject activityInstance = AndroidSystem.UnityActivity) {
					using (AndroidJavaObject intentFilter = new AndroidJavaObject(AndroidSystem.INTENT_FILTER)) {
						
						// Configuring actions to receiver
						foreach (string s in actions) {
							intentFilter.Call("addAction", s);
						}
						
						// Register broadcast receiver
						activityInstance.Call<AndroidJavaObject>("registerReceiver", mJavaObject, intentFilter);
					}
				}
				
				mRegistered = true;
#endif
				
			}
			
			public void Unregister() {	
#if UNITY_ANDROID
				if (mRegistered && mJavaObject != null) {
					using (AndroidJavaObject activityInstance = AndroidSystem.UnityActivity) {
							
							// Unregister broadcast receiver
							activityInstance.Call("unregisterReceiver", mJavaObject);
					}
				
					mRegistered = false;
				}
#endif
			}
			
			protected override void Dispose(bool disposing) {
				if (!disposed) {
				
					if (disposing) {
						Unregister();
					}
				}
				
				base.Dispose(disposing);
			}
			
			[DllImport("unityandroidsystem")]
			private static extern IntPtr CreateJavaBroadcastReceiver(OnReceiveDelegate callback);
		}
		
	}
}