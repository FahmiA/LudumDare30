using UnityEngine;
using System;

public class PlayerTurn : GameTurn {

    private Node currentPlayer;

    public void Setup() {
        Node.Enable();
        currentPlayer = Node.Player;
    }

    public void Update() {

    }

    public void TearDown() {
        Node.Disable();
    }

    public bool IsComplete() {
        // Player node has changed
        return currentPlayer != Node.Player;
    }

}
