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
        updateTintSize();

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

    private void updateTintSize() {
        // Get the camera extent
        float vertExtent = Camera.main.orthographicSize;
        float horzExtent = vertExtent * Screen.width / Screen.height;
        float tintSpriteSize = 10.0f;

        // Update the tint's scale to fit the camera (and no bigger)
        transform.localScale = new Vector3((horzExtent * 1000.0f) / tintSpriteSize,
                                           (vertExtent * 1000.0f) / tintSpriteSize,
                                           transform.localScale.z);
    }

}
