using UnityEngine;
using System.Collections.Generic;

public class TurnBasedGameController : MonoBehaviour {

    private PlayerTurn playerTurn;
    private AITurn aiTurn;

    private GameTurn currentTurn;

    public float startDelay;
    private float start;
    private bool started = false;

    public Node Player {
        get {
            if (!started) {
                return playerStartNode;
            }

            return playerTurn.currentNode;
        }
    }

    public Node Enemy {
        get {
            if (!started) {
                return enemyStartNode;
            }

            return aiTurn.currentNode;
        }
    }

    public Node playerStartNode;
    public Node enemyStartNode;

    private bool first = true;

    public void Start() {
        start = Time.time;
    }

    public void LateUpdate() {
        if (first) {
            playerTurn = new PlayerTurn(playerStartNode);
            aiTurn = new AITurn(this, enemyStartNode);
            currentTurn = aiTurn;
            currentTurn.Setup();
            first = false;
        }

        if (!started && Time.time < start + startDelay) {
            return;
        }

        started = true;

        if (currentTurn.IsComplete()) {
            currentTurn.TearDown();

            pickNextTurn();

            currentTurn.Setup();
        } else {
            currentTurn.Update();
        }
    }

    public void MovePlayer(Node to) {
        if (to == Enemy) {
            Application.LoadLevel("SceneEnd");
        } else {
            playerTurn.MovePlayer(to);
        }
    }

    private void pickNextTurn() {
        currentTurn = currentTurn == playerTurn ? (GameTurn) aiTurn : playerTurn;
    }

}
