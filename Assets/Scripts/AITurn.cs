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
        Node far1 = null;
        Node far2 = null;
        Node far3 = null;

        IEnumerable<Node> nodes = currentEnemy.GetConnectedNodes();

        foreach (Node node in nodes) {
            if (node == Node.Player ||
                (currentEnemy != Node.Player && Node.Player.IsLinked(node))) {
                continue;
            }

            int fromPlayer = node.DistanceTo(Node.Player);

            if (fromPlayer > distance) {
                distance = fromPlayer;
                far3 = far2;
                far2 = far1;
                far1 = node;
            }
        }

        if (far1 == null) {
            foreach (Node node in nodes) {
                if (node != Node.Player) {
                    int fromPlayer = node.DistanceTo(Node.Player);

                    if (fromPlayer > distance) {
                        distance = fromPlayer;
                        far3 = far2;
                        far2 = far1;
                        far1 = node;
                    }
                }
            }
        }

        int select = Random.Range(0, 10);

        if (far3 != null && select >= 9) {
            far3.MoveEnemyOn();
        } else if (far2 != null && select >= 6) {
            far2.MoveEnemyOn();
        } else {
            far1.MoveEnemyOn();
        }

    }

    public void TearDown() {

    }

    public bool IsComplete() {
        // Enemy node has changed
        return currentEnemy != Node.Enemy;
    }

}

