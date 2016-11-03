using UnityEngine;
using System.Collections;

public class TriggerAnimation : MonoBehaviour {
    public string AnimationName;
    public Animator stateMachine;

    private bool created = false;

	// Reference to the Google VR object in the scene
	private GvrViewer viewer;


    void Awake() {
        if (GvrViewer.Instance == null) {
            GvrViewer.Create();
            created = true;
        }
		// Locate the GvrViewer instance
		viewer = (GvrViewer)FindObjectOfType(typeof(GvrViewer));
		if (viewer == null) {
			Debug.LogError("No GvrViewer found. Please drag the GvrViewerMain prefab into the scene.");
			return;
		}

    }

    void Start() {
        if (created) {
            foreach (Camera c in GvrViewer.Instance.GetComponentsInChildren<Camera>()) {
                c.enabled = false; // to use the Gvr SDK without adding cameras we have to disable them
            }
        }

    }

    void Update() {
        GvrViewer.Instance.UpdateState(); //need to update the data here otherwise we dont get mouse clicks; this is because we are automatically creating the GVRSDK (seems like a bug)
		// Don't do anything unless the Google VR object and camera can be located
		viewer = (GvrViewer)FindObjectOfType(typeof(GvrViewer));
		if (viewer == null) {
			return;
		}
		if (viewer.Triggered) {
			stateMachine.SetTrigger (AnimationName);

		}
    }

}