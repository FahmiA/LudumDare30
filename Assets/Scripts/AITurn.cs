using UnityEngine;
using System.Collections.Generic;

public class AITurn : GameTurn {

    private Node currentEnemy;
    private bool panTurn = false;

    public void Setup() {
        Node.Disable();

        currentEnemy = Node.Enemy;
    }

    public void Update() {
        int distance = 0;
        Link far1 = null;
        Link far2 = null;
        Link far3 = null;

        IEnumerable<Link> links = Link.GetLinksFor(currentEnemy);

        foreach (Link link in links) {
            Node node = link.GetOtherNode(currentEnemy);

            if (node == Node.Player ||
                (currentEnemy != Node.Player && Node.Player.IsLinked(node))) {
                continue;
            }

            int fromPlayer = node.DistanceTo(Node.Player);

            if (fromPlayer > distance) {
                distance = fromPlayer;
                far3 = far2;
                far2 = far1;
                far1 = link;
            }
        }

        if (far1 == null) {
            foreach (Link link in links) {
                Node node = link.GetOtherNode(currentEnemy);

                if (node != Node.Player) {
                    int fromPlayer = node.DistanceTo(Node.Player);

                    if (fromPlayer > distance) {
                        distance = fromPlayer;
                        far3 = far2;
                        far2 = far1;
                        far1 = link;
                    }
                }
            }
        }

        int select = Random.Range(0, 10);

        Link far = far3 != null && select >= 9 ? far3 : far2 != null && select >= 6 ? far2 : far1;
        Node move = far.GetOtherNode(currentEnemy);

        move.MoveEnemyOn();

        CameraTint tint = GameObject.FindGameObjectWithTag("CameraTint").GetComponent<CameraTint>();
        bool tinted = false;

        if (Node.Player.transform.parent != move.transform.parent) {
            if (panTurn) {
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PanTo>()
                    .MoveTo(move.transform.parent.gameObject, delegate(PanTo.PanEnd end) {
                    tint.Show(far, delegate () {
                        end();
                    });
                });
                tinted = true;
            }

            panTurn = !panTurn;
        } else {
            panTurn = false;
        }

        if (!tinted) {
            tint.Show(far);
        }
    }

    public void TearDown() {

    }

    public bool IsComplete() {
        // Enemy node has changed
        return currentEnemy != Node.Enemy;
    }

}
