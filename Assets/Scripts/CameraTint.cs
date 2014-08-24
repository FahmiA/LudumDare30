using UnityEngine;
using System.Collections;

public class CameraTint : MonoBehaviour {

    public delegate void PostTint();

    private PostTint postTint = null;
    private float tintedAt;

    private Animator animator;

    public void Start() {
        animator = this.GetComponent<Animator>();
    }

    public void Update() {
        if (this.postTint != null && Time.time - tintedAt > 1.3f) {
            PostTint postTint = this.postTint;
            this.postTint = null;
            postTint();
        }
    }

    public void Show(Link link) {
        Color colour = link.getColour();
        renderer.material.SetColor("_Color", colour);
        
        animator.Play("FadeIn");
    }

    public void Show(Link link, PostTint postTint) {
        Show(link);

        this.postTint = postTint;
        tintedAt = Time.time;
    }

}
