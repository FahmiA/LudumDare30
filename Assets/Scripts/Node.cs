using UnityEngine;

public class Node : MonoBehaviour {

    public static Node player = null;
    public static Node enemy = null;

    // There should only be one Node with this set to true.
    public bool IsStart = false;

    private bool enabled = false;

    public void Start() {
        if (IsStart) {
            player = this;
            this.moveOn();
            enemy = this;
        }
    }

    public void OnMouseDown() {
        if (!enabled) {
            return;
        }

        if (Link.IsLinked(player, this)) {
            player.moveOff();
            player = this;
            this.moveOn();
        }
    }

    public void enablePlayerInteraction() {
        enabled = true;
    }

    public void disablePlayerInteraction() {
        enabled = false;
    }

    private void moveOn() {
        GetComponent<Animator>().Play("Occupied");
    }

    private void moveOff() {
        GetComponent<Animator>().Play("Idle");
    }
}
