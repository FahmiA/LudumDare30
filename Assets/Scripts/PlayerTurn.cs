using UnityEngine;
using System;

public class PlayerTurn : GameTurn {

    public Node currentNode;

    private int turnsLeft;

    public PlayerTurn(Node startNode) {
        currentNode = startNode;
        currentNode.MovePlayerOn();
    }

    public void Setup() {
        Node.Enable();
        turnsLeft = 3;
    }

    public void Update() {

    }

    public void TearDown() {
        Node.Disable();
    }

    public bool IsComplete() {
        return turnsLeft == 0;
    }

    public void MovePlayer(Node to) {
        currentNode.MovePlayerOff();
        currentNode = to;
        currentNode.MovePlayerOn();
        turnsLeft -= 1;
    }

}
