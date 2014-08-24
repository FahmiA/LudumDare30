using UnityEngine;
using System;

public class PlayerTurn : GameTurn {

    private Node[] nodes;
    private Node currentPlayer;

    public PlayerTurn() {
        GameObject[] nodeObjects = GameObject.FindGameObjectsWithTag("Node");
        nodes = new Node[nodeObjects.Length];

        for (int i = 0; i < nodeObjects.Length; i++) {
            nodes[i] = (Node) nodeObjects[i].GetComponent(typeof(Node));
        }
    }

    public void Setup() {
        for (int i = 0; i < nodes.Length; i++) {
            nodes[i].EnablePlayerInteraction();
        }

        currentPlayer = Node.Player;
    }

    public void Update() {

    }

    public void TearDown() {
        for (int i = 0; i < nodes.Length; i++) {
            nodes[i].DisablePlayerInteraction();
        }
    }

    public bool IsComplete() {
        // Player node has changed
        return currentPlayer != Node.Player;
    }

}
