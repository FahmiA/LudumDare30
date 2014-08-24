using UnityEngine;

public class PanTo : MonoBehaviour {

    public delegate void EndPan();
    public delegate void OnPan(EndPan end);

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

                    Vector2 pan = panningFrom;
                    panningFrom = panningTo;
                    panningTo = pan;

                    float size = sizeFrom;
                    sizeFrom = sizeTo;
                    sizeTo = size;

                    isWaiting = true;
                    onPan(delegate() {
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
