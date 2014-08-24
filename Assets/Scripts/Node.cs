using UnityEngine;
using System.Collections.Generic;

public class Node : MonoBehaviour {

    private static HashSet<Node> nodes = new HashSet<Node>();

    public static Node player = null;
    public static Node enemy = null;

    // There should only be one Node with this set to true.
    public bool IsStart = false;

    private bool enabled = false;
    private Animator animator;

    public void Start() {
        nodes.Add(this);
        animator = GetComponent<Animator>();

        if (IsStart) {
            player = this;
            this.movePlayerOn();
            enemy = this;
        }
    }

    public void OnMouseDown() {
        if (!enabled) {
            return;
        }

        if (Link.IsLinked(player, this)) {
            player.movePlayerOff();
            player = this;
            this.movePlayerOn();
        }
    }

    public void enablePlayerInteraction() {
        enabled = true;
    }

    public void disablePlayerInteraction() {
        enabled = false;
    }

    private void movePlayerOn() {
        animator.Play("Occupied");
    }

    private void movePlayerOff() {
        animator.Play("Idle");
    }

    public void moveEnemyOn() {
        enemy.moveEnemyOff();
        enemy = this;

        animator.Play("EnemyOccupied");
    }
    
    public void moveEnemyOff() {
        if (this != player) { // In case we overwrite the player animation
            animator.Play("Idle");
        }
    }

    public int distanceTo(Node other) {
        // TODO: Calculate distance between number of edges that must be travesed, not actual distance
        return (int) Mathf.Round(Vector2.Distance(this.transform.position, other.transform.position));
    }

    #region StaticHelpers
    public static List<Node> GetConnectedNodes(Node node) {
        List<Node> connectedNodes = new List<Node>();
        
        List<Link> links = Link.GetLinksFromNode(node);
        for (int i = 0; i < links.Count; i++) {
            Link link = links[i];
            
            if (link.source == node && link.target != node) {
                connectedNodes.Add(link.target);
            }
            
            if (link.source != node && link.target == node) {
                connectedNodes.Add(link.source);
            }
        }
        
        return connectedNodes;
    }
    #endregion
}
