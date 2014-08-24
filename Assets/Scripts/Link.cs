using UnityEngine;
using System.Collections.Generic;

public class Link : MonoBehaviour {

    public static IEnumerable<Link> GetLinksFor(Node node) {
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Link")) {
            Link link = go.GetComponent<Link>();
            if (link.source == node || link.target == node) {
                yield return link;
            }
        }
    }

    // The magic scale number to stretch a link by.
    public const float MAGIC_SCALE = 3.4f;

    // Note that the order of these is arbitrary.
    public Node source;
    public Node target;
    private Animator animator;

    public enum LinkType {
        White,
        Red,
        Green,
        Blue
    }

    public LinkType linkType;

    // Use this for initialization
    void Start() {
        animator = this.GetComponent<Animator>();

        renderer.material.SetColor("_Color", getColour());
    }
    
    // Update is called once per frame
    void Update() {
        if (source != null && target != null) {
            orient();
        } else {
            Debug.Log("Link requires a source and a target game object.");
        }
    }

    public void MarkAsVisited() {
        animator.Play("visited");
    }

    public void MarkAsIdle() {
        animator.Play("idle");
    }

    // Assumes that this link is attached to the given node.
    public Node GetOtherNode(Node node) {
        if (source == node) {
            return target;
        }

        return source;
    }
    
    public Color getColour() {
        Color colour;

        switch (this.linkType) {
        case LinkType.Red:
            colour = Color.red;
            break;
        case LinkType.Green:
            colour = Color.green;
            break;
        case LinkType.Blue:
            colour = Color.blue;
            break;
        default:
            colour = Color.white;
            break;
        }

        return colour;
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
