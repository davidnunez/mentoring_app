using UnityEngine;
using System;
using System.Runtime.InteropServices;

namespace FourthSky {
	namespace Android {

		public class ServiceConnection : AndroidWrapper {
			
			// Delegates for service connection
			public delegate void OnConnectedDelegate(IntPtr componentNamePtr, IntPtr binderPtr);
			public delegate void OnDisconnectedDelegate(IntPtr componentNamePtr);
			
			public event OnConnectedDelegate OnServiceConnected;
			public event OnDisconnectedDelegate OnServiceDisconnected;
			
			// Use this for initialization
			public ServiceConnection() : base() {
				
			}
			
			~ServiceConnection() {
				Dispose (false);
			}
			
			/// <summary>
			/// Creates the service connection.
			/// </summary>
			/// <returns>
			/// The service connection.
			/// </returns>
			/// <param name='onConnectedClbk'>
			/// On connected clbk.
			/// </param>
			/// <param name='onDisconnectedClbk'>
			/// On disconnected clbk.
			/// </param>
			/// <exception cref='System.ArgumentNullException'>
			/// Is thrown when the argument null exception.
			/// </exception>
			public bool Bind(AndroidJavaClass serviceClass, int flags = /*BIND_AUTO_CREATED*/1) {
				bool retValue = false;
				
#if UNITY_ANDROID && !UNITY_EDITOR
				// Create java instance of broadcast receiver
				if (mJavaObject == null) {
					IntPtr connPtr = CreateJavaServiceConnection(OnServiceConnected, OnServiceDisconnected);
					if (IntPtr.Zero != connPtr) {
						mJavaObject = AndroidSystem.ConstructJavaObjectFromPtr(connPtr);
					}
				}
				
				// Get application context
				using (AndroidJavaObject context = AndroidSystem.UnityContext) {
					using(AndroidJavaObject intent = new AndroidJavaObject("android.content.Intent", context, serviceClass)) {
					
						// Now, bind to service
						retValue = context.Call<bool>("bindService", intent, mJavaObject, flags);
					}
				}
#endif
			
				return retValue;
			}
			
			/// <summary>
			/// Binds to the Android service.
			/// </summary>
			/// <returns>
			/// The service.
			/// </returns>
			/// <param name='intent'>
			/// If set to <c>true</c> intent.
			/// </param>
			/// <param name='serviceConnection'>
			/// If set to <c>true</c> service connection.
			/// </param>
			/// <param name='flags'>
			/// If set to <c>true</c> flags.
			/// </param>
			/// <exception cref='System.ArgumentNullException'>
			/// Is thrown when the argument null exception.
			/// </exception>
			public bool Bind(string action, int flags = /*Context.BIND_AUTO_CREATE*/1) {
				if (action == null || "" == action) {
					throw new System.ArgumentNullException("Intent action cannot be null");
				}
				
				bool retValue = false;
				
#if UNITY_ANDROID && !UNITY_EDITOR
				// Create java instance of broadcast receiver
				if (mJavaObject == null) {
					IntPtr connPtr = CreateJavaServiceConnection(OnServiceConnected, OnServiceDisconnected);
					if (IntPtr.Zero != connPtr) {
						mJavaObject = AndroidSystem.ConstructJavaObjectFromPtr(connPtr);
					}
				}
				
				// bind to the service
				using (AndroidJavaObject activityInstance = AndroidSystem.UnityActivity) {
					using (AndroidJavaObject intent = new AndroidJavaObject("android.content.Intent", action)) {
						retValue = activityInstance.Call<bool>("bindService", intent, mJavaObject, flags);
					}
				}
#endif
				
				return retValue;
			}
			
			/// <summary>
			/// Unbinds from the Android service.
			/// </summary>
			/// <param name='serviceConnection'>
			/// Service connection.
			/// </param>
			/// <exception cref='System.ArgumentNullException'>
			/// Is thrown when the argument null exception.
			/// </exception>
			public void Unbind() {
#if UNITY_ANDROID && !UNITY_EDITOR
				if (mJavaObject == null &&!Application.isEditor) {
					throw new System.ArgumentNullException("Service connection cannot be null");
				}
				
				using (AndroidJavaObject activityInstance = AndroidSystem.UnityActivity) {
					activityInstance.Call("unbindService", mJavaObject);
				}
#endif
			}
			
			protected override void Dispose(bool disposing) {
				if (!disposed) {
				
					if (disposing) {
						Unbind();
					}
				}
				
				base.Dispose(disposing);
			}
			
#if UNITY_ANDROID && !UNITY_EDITOR
			/// <summary>
			/// Native function used to create Android service connection
			/// </summary>
			/// <returns>
			/// Object pointer to Android service connection.
			/// </returns>
			/// <param name='onConnectedClbk'>
			/// On connected clbk.
			/// </param>
			/// <param name='onDisconnectedClbk'>
			/// On disconnected clbk.
			/// </param>
			[DllImport("unityandroidsystem")]
			private static extern IntPtr CreateJavaServiceConnection(OnConnectedDelegate onConnectedClbk, 
																	 OnDisconnectedDelegate onDisconnectedClbk);
#endif
		}
		
	}
}