using UnityEngine;
using System.Collections;

public class Zooming : MonoBehaviour {

    public int MinZoomDistance = 3;
    public int MaxZoomDistance = 11;

    // Amount to zoom per scroll in hundredths of a size unit.
    public int ZoomSpeed = 100;
    
    void Update() {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        camera.orthographicSize = Mathf.Clamp(camera.orthographicSize - scroll * ZoomSpeed / 100,
                                              MinZoomDistance, MaxZoomDistance);
    }

}
