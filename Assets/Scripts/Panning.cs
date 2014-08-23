using UnityEngine;
using System.Collections;

public class Panning : MonoBehaviour {

    public int panThreshold = 20;
    public float panAmount = 0.2f;

    void Update() {
        Vector2 mousePosition = Input.mousePosition;

        if (mousePosition.x < panThreshold) {
            transform.Translate(new Vector2(-panAmount, 0));
        } else if (mousePosition.x > Screen.width - panThreshold) {
            transform.Translate(new Vector2(panAmount, 0));
        }

        if (mousePosition.y < panThreshold) {
            transform.Translate(new Vector2(0, -panAmount));
        } else if (mousePosition.y > Screen.height - panThreshold) {
            transform.Translate(new Vector2(0, panAmount));
        }

        Debug.Log(transform.position);
    }
}
