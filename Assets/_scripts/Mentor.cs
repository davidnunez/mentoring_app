using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Mentor : MonoBehaviour {
	
	public Dictionary<string, float> tags = new Dictionary<string, float>() {};
	public string[] tagNames;
	public float[] tagRelevances;	
	
	void Awake() {
				int i = 0;

		foreach (string tagName in tagNames) {
			tags[tagName] = tagRelevances[i];
			i++;
		}
	}
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	void LateUpdate() {
		int i = 0;
		foreach (string tagName in tagNames) {
			tags[tagName] = tagRelevances[i];
			i++;
		}
	}
	
	
	public float GetAverageTagRelevance(string[] tagNames) {
		float total = 0;
		foreach (string tagName in tagNames) {
			total = total + tags[tagName];
			
		}
		if (tagNames.Length != 0) {
		
			return total / tagNames.Length;	
		} else {
			
			return 0;
		}
	}
	
	
	
	
}
