using UnityEngine;

public class Node : MonoBehaviour {

    public static Node selected = null;

    public Owner owner;
	
    public void OnMouseDown() {
        if (owner == Owner.Player) {
            selected = this;
        } else if (selected != null) {
            TryMoveTo();
        }
    }

    private bool TryMoveTo() {
        if (Link.IsLinked(this, selected)) {
            this.owner = Owner.Player;
            return true;
        }

        return false;
    }

}
