using UnityEngine;

public class Panning : MonoBehaviour {

    // The distance of the mouse from the screen edge at which the camera should begin panning.
    public int panThreshold = 20;

    // The maximum speed that pan should move at, in thousandths of a unit relative to the
    // orthographic size of the camera per frame.
    public int maxPanSpeed = 50;

    public GameObject background;

    public void Start() {

    }

    public void Update() {
        pann();
        clamp();
    }

    private void pann() {
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
            
            float mult = (camera.orthographicSize * maxPanSpeed / 1000f) / panThreshold;

            transform.Translate(new Vector2(xdiff * mult, ydiff * mult));
        }
    }

    public void clamp() {
        /* Courtesy of:
         * http://answers.unity3d.com/questions/501893/calculating-2d-camera-bounds.html
         */
        if (background != null) {
            // Get the camera extent
            float vertExtent = Camera.main.camera.orthographicSize;
            float horzExtent = vertExtent * Screen.width / Screen.height;
            
            float mapWidth = background.renderer.bounds.size.x;
            float mapHeight = background.renderer.bounds.size.y;

            // Calculations assume map is position at the origin
            float minX = horzExtent - mapWidth / 2.0f;
            float maxX = mapWidth / 2.0f - horzExtent;
            float minY = vertExtent - mapHeight / 2.0f;
            float maxY = mapHeight / 2.0f - vertExtent;

            float padding = mapHeight * 0.05f;
            Vector3 position = transform.position;
            position.x = Mathf.Clamp(position.x, minX + padding, maxX - padding);
            position.y = Mathf.Clamp(position.y, minY + padding, maxY - padding);
            transform.position = position;
        } else {
            Debug.LogWarning("Camera requires a background. Therefore, it cannot clamp to bounds");
        }

    }
}
