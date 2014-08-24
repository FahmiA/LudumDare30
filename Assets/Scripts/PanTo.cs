using UnityEngine;

public class PanTo : MonoBehaviour {

    public delegate void PanEnd();
    public delegate void OnPan(PanEnd end);

    public static bool IsPanning = false;

    private Vector2 panningFrom;
    private Vector2 panningTo;
    private float sizeFrom;
    private float sizeTo;
    private float lerp = 0;
    private bool isPanningBack = false;
    private OnPan onPan;
    private bool isWaiting = false;

    public void Update() {
        if (IsPanning && !isWaiting) {
            if (lerp > 1) {
                lerp = 0;

                if (!isPanningBack) {
                    isPanningBack = true;
                    
                    panningTo = panningFrom;
                    panningFrom = transform.position;

                    sizeTo = sizeFrom;
                    sizeFrom = camera.orthographicSize;

                    isWaiting = true;

                    onPan(delegate () {
                        isWaiting = false;
                    });
                } else {
                    IsPanning = false;
                }
            } else {
                Vector2 diff = Vector2.Lerp(panningFrom, panningTo, lerp);
                transform.position = new Vector3(diff.x, diff.y, transform.position.z);
                camera.orthographicSize = Mathf.Lerp(sizeFrom, sizeTo, lerp);
                lerp += 0.05f;
            }
        }
    }

    public void MoveTo(GameObject landscape, OnPan onPan) {
        IsPanning = true;
        isPanningBack = false;
        panningFrom = transform.position;
        panningTo = landscape.transform.position;
        sizeFrom = camera.orthographicSize;
        sizeTo = 5;
        this.onPan = onPan;
    }

}
