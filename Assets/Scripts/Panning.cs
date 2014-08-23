using UnityEngine;
using System.Collections;

public class Panning : MonoBehaviour {

    // The distance of the mouse from the screen edge at which the camera should begin panning.
    public int panThreshold = 20;

    // The maximum speed that pan should move at, in hundredths of a unit per frame.
    public int maxPanSpeed = 20;

    void Update() {
        Vector2 mousePosition = Input.mousePosition;

        float xdiff = 0;
        float ydiff = 0;

        if (Input.GetKey(KeyCode.LeftArrow)) {
            xdiff -= panThreshold;
        }

        if (Input.GetKey(KeyCode.RightArrow)) {
            xdiff += panThreshold;
        }

        if (mousePosition.x < panThreshold) {
            xdiff -= Mathf.Min(panThreshold - mousePosition.x, panThreshold);
        } else if (mousePosition.x > Screen.width - panThreshold) {
            xdiff += Mathf.Min(mousePosition.x - Screen.width + panThreshold, panThreshold);
        }

        if (Input.GetKey(KeyCode.DownArrow)) {
            ydiff -= panThreshold;
        }

        if (Input.GetKey(KeyCode.UpArrow)) {
            ydiff += panThreshold;
        }

        if (mousePosition.y < panThreshold) {
            ydiff -= Mathf.Min(panThreshold - mousePosition.y, panThreshold);
        } else if (mousePosition.y > Screen.height - panThreshold) {
            ydiff += Mathf.Min(mousePosition.y - Screen.height + panThreshold, panThreshold);
        }

        if (xdiff != 0 || ydiff != 0) {
            xdiff = Mathf.Clamp(xdiff, -panThreshold, panThreshold);
            ydiff = Mathf.Clamp(ydiff, -panThreshold, panThreshold);

            float mult = ((float) maxPanSpeed / 100) / panThreshold;

            transform.Translate(new Vector2(xdiff * mult, ydiff * mult));
        }
    }
}
