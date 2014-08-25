using UnityEngine;
using System.Collections;

public class Zooming : MonoBehaviour {

    public int MinZoomDistance = 3;
    public int MaxZoomDistance = 12;

    // Amount to zoom per scroll in hundredths of a size unit.
    public int ZoomSpeed = 100;

    // Initial camera zoom size
    public int initialSize = 5;

    void Start() {
        // Reset the camera zoom when the scene starts or restarts
        Camera.main.orthographicSize = initialSize;
    }
    
    void Update() {
        if (!PanTo.IsPanning) {
            float scroll = Input.GetAxis("Mouse ScrollWheel");

            camera.orthographicSize =
                Mathf.Clamp(camera.orthographicSize - scroll * ZoomSpeed / 100,
                            MinZoomDistance, MaxZoomDistance);
        }
    }

}
