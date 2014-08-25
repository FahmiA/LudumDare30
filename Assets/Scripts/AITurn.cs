using UnityEngine;
using System.Collections.Generic;

public class AITurn : GameTurn {

    public Node currentNode;
    private bool panTurn = false;

    private int turnsLeft;
    private bool isWaiting = false;
    private bool handicap = true;

    private float wait;

    private TurnBasedGameController director;

    public AITurn(TurnBasedGameController director, Node startNode) {
        this.director = director;
        currentNode = startNode;
    }

    public void Setup() {
        Node.Disable();
        turnsLeft = handicap ? 4 : 2;
        wait = Time.time;
    }

    public void Update() {
        if (isWaiting || Time.time < wait + 1) {
            return;
        }

        int distance = 0;
        Link far1 = null;
        Link far2 = null;
        Link far3 = null;

        IEnumerable<Link> links = Link.GetLinksFor(currentNode);

        Node playerNode = director.Player;

        foreach (Link link in links) {
            Node node = link.GetOtherNode(currentNode);

            if (node == playerNode ||
                (currentNode != playerNode && playerNode.IsLinked(node))) {
                continue;
            }

            int fromPlayer = node.DistanceTo(playerNode);

            if (fromPlayer > distance) {
                distance = fromPlayer;
                far3 = far2;
                far2 = far1;
                far1 = link;
            }
        }

        if (far1 == null) {
            foreach (Link link in links) {
                Node node = link.GetOtherNode(currentNode);

                if (node != playerNode) {
                    int fromPlayer = node.DistanceTo(playerNode);

                    if (fromPlayer > distance) {
                        distance = fromPlayer;
                        far3 = far2;
                        far2 = far1;
                        far1 = link;
                    }
                }
            }
        }

        int select = handicap ? 0 : Random.Range(0, 10);

        Link far = far3 != null && select >= 9 ? far3 : far2 != null && select >= 6 ? far2 : far1;
        Node move = far.GetOtherNode(currentNode);

        currentNode.MoveEnemyOff();
        currentNode = move;

        CameraTint tint = GameObject.FindGameObjectWithTag("CameraTint").GetComponent<CameraTint>();
        bool tinted = false;

        if (!handicap && turnsLeft == 2) {
            if (playerNode.transform.parent != move.transform.parent) {
                if (panTurn) {
                    GameObject landmark =
                        move.transform.parent.gameObject.GetComponent<Landmark>().asset;
                    GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PanTo>()
                        .MoveTo(landmark, delegate(PanTo.EndPan end) {
                        tint.Show(far, delegate () {
                            end(delegate () {
                                turnsLeft -= 1;
                                isWaiting = false;
                            });
                        });
                    });
                    tinted = true;
                }

                panTurn = !panTurn;
            } else {
                panTurn = false;
            }
        }

        if (!tinted) {
            tint.Show(far, delegate() {
                turnsLeft -= 1;
                isWaiting = false;
            });
        }

        isWaiting = true;
    }

    public void TearDown() {
        handicap = false;
    }

    public bool IsComplete() {
        return turnsLeft == 0;
    }

}
