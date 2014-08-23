﻿using UnityEngine;
using System.Collections;

public class Link : MonoBehaviour {

    public GameObject source;
    public GameObject target;
    public float MAGIC_SCALE = 8.348744f;

    // Use this for initialization
    void Start() {
	
    }
	
    // Update is called once per frame
    void Update() {
        if (source != null && target != null) {
            orient();
        } else {
            Debug.Log("Link requires a source and a target game object.");
        }
    }

    private void orient() {
        // Set position to source
        // (assumes sprite pivot has been set to bottom)
        transform.position = source.transform.position;

        // Point to target
        Vector3 angleVect = target.transform.position -
            source.transform.position;
        float angle = Mathf.Atan2(angleVect.y, angleVect.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

        // Update length to reach 
        float distance = Vector2.Distance(source.transform.position, target.transform.position);
        transform.localScale = new Vector2(transform.localScale.x, distance * MAGIC_SCALE);
    }
}
