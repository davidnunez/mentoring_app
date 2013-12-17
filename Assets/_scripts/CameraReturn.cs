using UnityEngine;
using System.Collections;

public class CameraReturn : MonoBehaviour {
	
	public bool freeToReturn = true;
	
	void LateUpdate() {
		//iTween.Destroy();
			
	}
	void OnFingerDown(FingerDownEvent e) { 
	
		iTween.Stop(gameObject);
	
	}
	void OnFingerUp(FingerUpEvent e) { 
		iTween.MoveTo(gameObject, iTween.Hash("x", 0, "y", 0, "easetype", "easeInOutSine", "time", 10.0f, "islocal", true));
	
	}
	
}
