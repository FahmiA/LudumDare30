using UnityEngine;
using System;
using System.Collections.Generic;

namespace GameController {
    public class AITurn : GameTurn {
        private Node[] nodes;
        private Node currentEnemy;

        public AITurn() {
            GameObject[] nodeObjects = GameObject.FindGameObjectsWithTag("Node");
            nodes = new Node[nodeObjects.Length];
            
            for (int i = 0; i < nodeObjects.Length; i++) {
                nodes[i] = (Node) nodeObjects[i].GetComponent(typeof(Node));
            }
        }

        public void setUp() {
            for (int i = 0; i < nodes.Length; i++) {
                nodes[i].disablePlayerInteraction();
            }

            currentEnemy = Node.enemy;
        }
        
        public void update() {
            /* Pick a node away from the player. */

            // Get all nodes
            List<Node> nodes = Node.GetConnectedNodes(currentEnemy);

            // Sort by distance from player
            nodes.Sort(delegate(Node a, Node b) {
                return a.distanceTo(b);
            });

            // Select furthest nodes
            Node furthestNode = nodes[nodes.Count - 1];

            // Go to the furthest node
            furthestNode.moveEnemyOn();

            Debug.Log("AI is now at node: " + furthestNode.name);
        }

        public void tearDown() {
            
        }

        public bool isComplete() {
            // Enemy node has changed
            return currentEnemy != Node.enemy;
        }
    }
}

