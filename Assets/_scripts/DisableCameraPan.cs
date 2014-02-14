using UnityEngine;
using System.Collections;

public class DisableCameraPan : MonoBehaviour {
	
	public TBPan tbPan_component;
	// Use this for initialization
	void Start () {
		tbPan_component = Camera.mainCamera.gameObject.GetComponent<TBPan>();
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnDrag( DragGesture gesture ) {
		Debug.Log(gesture.Phase);
		tbPan_component.enabled = false;
		if (gesture.Phase.Equals(ContinuousGesturePhase.Ended)) {
			tbPan_component.enabled = true;

		}
		
	}
	
	
}
