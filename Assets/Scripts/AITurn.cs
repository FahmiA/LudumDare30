using UnityEngine;
using System;

namespace GameController {
    public class AITurn : GameTurn {
        private Node[] nodes;

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
        }
        
        public void update() {

        }

        public void tearDown() {
            
        }

        public bool isComplete() {
            return true;
        }
    }
}

