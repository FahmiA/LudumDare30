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

    // The local animator.
    private Animator animator;

    // The current direction of the resize.
    private bool bigger;

    // The counter to change in size.
    private int resize;

    // The choice of node.
    private int whichNode;

    /* For debugging (aka. cheating), show the enemy position. */
    private bool forceShowEnemy = false;

    public void Start() {
        bigger = Random.Range(0, 10) > 5;
        resize = Random.Range(10, 100);

        whichNode = Random.Range(1, 3);

        animator = GetComponent<Animator>();

        if (IsStart) {
            player = this;
            this.movePlayerOn();
            enemy = this;
        }

        MoveEnemyOff();
    }

    public void Update() {
        resize -= 1;

        float scale = transform.localScale.x;

        if (resize < 0 || scale > 0.6f || scale < 0.4f) {
            bigger = !bigger;
            resize = Random.Range(10, 100);
        }

        float diff = (bigger ? 1 : -1) * 0.001f;

        transform.localScale = new Vector2(transform.localScale.x + diff,
                                           transform.localScale.y + diff);


        showOrHideEnemy();
    }

    public void OnMouseDown() {
        if (!isEnabled) {
            return;
        }

        if (IsLinked(player)) {
            player.movePlayerOff();
            player = this;
            this.movePlayerOn();
        }
    }

    private void showOrHideEnemy() {
        if (this != Enemy) {
            return;
        }

        // Only show the enemy if the player is on a connected node ...
        bool isNextToPlayer = false;
        foreach (Node node in GetConnectedNodes()) {
            if (node == Player) {
                isNextToPlayer = true;
                break;
            }
        }
  
        // ... or when the cheat, ahem "debug", key is pressed.
        if (isNextToPlayer || Input.GetKey(KeyCode.BackQuote)) {
            animator.Play("Enemy" + whichNode);
        } else {
            animator.Play("Idle" + whichNode);
        }
    }

    private void movePlayerOn() {
        animator.Play("Player" + whichNode);
    }

    private void movePlayerOff() {
        animator.Play("Idle" + whichNode);
    }

    public void MoveEnemyOn() {
        enemy.MoveEnemyOff();
        enemy = this;
    }
    
    public void MoveEnemyOff() {
        if (this != player) { // In case we overwrite the player animation
            animator.Play("Idle" + whichNode);
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

    public bool IsLinked(Node node) {
        foreach (Link link in Link.GetLinksFor(this)) {
            if (link.GetOtherNode(this) == node) {
                return true;
            }
        }

        return false;
    }

}
