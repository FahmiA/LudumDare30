using UnityEngine;
using System.Collections.Generic;

public class AITurn : GameTurn {

    private Node currentEnemy;

    public void Setup() {
        Node.Disable();

        currentEnemy = Node.Enemy;
    }

    public void Update() {
        int distance = 0;
        Node furthest = null;

        foreach (Node node in currentEnemy.GetConnectedNodes()) {
            int fromPlayer = node.DistanceTo(Node.Player);

            if (fromPlayer > distance) {
                distance = fromPlayer;
                furthest = node;
            }
        }

        furthest.MoveEnemyOn();
    }

    public void TearDown() {

    }

    public bool IsComplete() {
        // Enemy node has changed
        return currentEnemy != Node.Enemy;
    }

}

