using UnityEngine;
using System.Collections.Generic;

public class Link : MonoBehaviour {

    private static HashSet<Link> links = new HashSet<Link>();

    public static bool IsLinked(Node a, Node b) {
        foreach (Link link in links) {
            if ((link.source == a && link.target == b) || (link.source == b && link.target == a)) {
                return true;
            }
        }

        return false;
    }

    // The magic scale number to stretch a link by.
    public const float MAGIC_SCALE = 1.9f;

    // Note that the order of these is arbitrary.
    public Node source;
    public Node target;

    // Use this for initialization
    void Start() {
        links.Add(this);
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

        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        // Update length to reach 
        float distance = Vector2.Distance(source.transform.position, target.transform.position);
        transform.localScale = new Vector2(distance * MAGIC_SCALE, transform.localScale.y);
    }
}
