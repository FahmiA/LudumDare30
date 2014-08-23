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
            List<Link> links = Link.GetLinksFromNode(currentEnemy);
            
            int linkToTake = UnityEngine.Random.Range(0, links.Count - 1);
            Node.enemy = links[linkToTake].target;
        }

        public void tearDown() {
            
        }

        public bool isComplete() {
            // Enemy node has changed
            return currentEnemy != Node.enemy;
        }
    }
}

