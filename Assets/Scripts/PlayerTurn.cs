using UnityEngine;
using System;

namespace GameController {
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

        public void setUp() {
            for (int i = 0; i < nodes.Length; i++) {
                nodes[i].enablePlayerInteraction();
            }

            currentPlayer = Node.player;
        }
        
        public void update() {

        }

        public void tearDown() {
            for (int i = 0; i < nodes.Length; i++) {
                nodes[i].disablePlayerInteraction();
            }
        }

        public bool isComplete() {
            // Player node has changed
            return currentPlayer != Node.player;
        }
    }
}

