using UnityEngine;
using System.Collections.Generic;

public class Node : MonoBehaviour {

    private static Node player = null;
    private static Node enemy = null;

    public static Node Player {
        get {
            return player;
        }
    }

    public static Node Enemy {
        get {
            return enemy;
        }
    }

    private static bool isEnabled = false;

    public static void Enable() {
        isEnabled = true;
    }

    public static void Disable() {
        isEnabled = false;
    }

    // There should only be one Node with this set to true.
    public bool IsStart = false;
    private Animator animator;

    public void Start() {
        animator = GetComponent<Animator>();

        if (IsStart) {
            player = this;
            this.movePlayerOn();
            enemy = this;
        }
    }

    public void OnMouseDown() {
        if (!isEnabled) {
            return;
        }

        if (isLinked(player)) {
            player.movePlayerOff();
            player = this;
            this.movePlayerOn();
        }
    }

    private void movePlayerOn() {
        animator.Play("Occupied");
    }

    private void movePlayerOff() {
        animator.Play("Idle");
    }

    public void MoveEnemyOn() {
        enemy.MoveEnemyOff();
        enemy = this;

        animator.Play("EnemyOccupied");
    }
    
    public void MoveEnemyOff() {
        if (this != player) { // In case we overwrite the player animation
            animator.Play("Idle");
        }
    }

    public int DistanceTo(Node other) {
        // TODO Calculate distance as number of edges that must be travesed, not actual distance.
        return (int) Mathf.Round(Vector2.Distance(this.transform.position,
                                                  other.transform.position));
    }

    public IEnumerable<Node> GetConnectedNodes() {
        foreach (Link link in Link.GetLinksFor(this)) {
            yield return link.GetOtherNode(this);
        }
    }

    private bool isLinked(Node node) {
        foreach (Link link in Link.GetLinksFor(this)) {
            if (link.GetOtherNode(this) == node) {
                return true;
            }
        }

        return false;
    }

}
