using UnityEngine;
using System.Collections.Generic;

public class Node : MonoBehaviour {

    private static bool isEnabled = false;

    public static void Enable() {
        isEnabled = true;
    }

    public static void Disable() {
        isEnabled = false;
    }

    public AudioClip[] sounds;

    // The local animator.
    private Animator animator;

    // The current direction of the resize.
    private bool bigger;

    // The counter to change in size.
    private int resize;

    // The choice of node.
    private int whichNode;

    public void Start() {
        bigger = Random.Range(0, 10) > 5;
        resize = Random.Range(10, 100);

        whichNode = Random.Range(1, 3);

        animator = GetComponent<Animator>();
        animator.Play("Idle" + whichNode);
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

        TurnBasedGameController director =
            GameObject.FindGameObjectWithTag("Director").GetComponent<TurnBasedGameController>();
        Node player = director.Player;

        if (IsLinked(player)) {
            director.MovePlayer(this);
        }
    }

    private void showOrHideEnemy() {
        TurnBasedGameController director =
            GameObject.FindGameObjectWithTag("Director").GetComponent<TurnBasedGameController>();

        if (this != director.Enemy) {
            return;
        }

        // Only show the enemy if the player is on a connected node ...
        bool isNextToPlayer = false;
        foreach (Node node in GetConnectedNodes()) {
            if (node == director.Player) {
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

    private void playSound() {
        AudioClip sound = sounds[Random.Range(0, sounds.Length)];
        audio.PlayOneShot(sound);
    }

    public void MovePlayerOn() {
        animator.Play("Player" + whichNode);
        playSound();
    }

    public void MovePlayerOff() {
        animator.Play("Idle" + whichNode);
    }
    
    public void MoveEnemyOff() {
        animator.Play("Idle" + whichNode);
    }

    public int DistanceTo(Node other) {
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
