using UnityEngine;
using System.Collections;

public class RestartAfterDelay : MonoBehaviour {

    public float delay;

    private float start;

    // Use this for initialization
    void Start() {
        start = Time.time;
    }
	
    // Update is called once per frame
    void Update() {
        if (Time.time > start + delay) {
            Application.LoadLevel("Scene1");
        }
    }
}
