using UnityEngine;
using System.Collections;

public class CameraTint : MonoBehaviour {

    private Animator animator;

    // Use this for initialization
    void Start() {
        animator = this.GetComponent<Animator>();
    }
	
    // Update is called once per frame
    void Update() {

    }

    public void show(Link link) {
        Color colour = link.getColour();
        renderer.material.SetColor("_Color", colour);

        animator.Play("FadeIn");
    }
}
