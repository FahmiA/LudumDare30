using UnityEngine;
using System.Collections.Generic;

using GameController;

public class TurnBasedGameController : MonoBehaviour {

    private GameTurn[] turns;
    private int currentTurnIndex;

    // Use this for initialization
    void Start() {
        turns = new GameTurn[] {
            new PlayerTurn(),
            new AITurn(),
        //new FeedbackTurn()
        };

        currentTurnIndex = -1;
    }
    
    // Update is called once per frame
    void Update() {
        // Everything is given one turn
        ensureCurrentTurn();

        GameTurn currentTurn = turns[currentTurnIndex];

        if (currentTurn.isComplete()) {
            currentTurn.tearDown();

            currentTurnIndex = (currentTurnIndex + 1) % turns.Length;
            currentTurn = turns[currentTurnIndex];
            printDebug();

            currentTurn.setUp();
        } else {
            currentTurn.update();
        }
    }

    private void ensureCurrentTurn() {
        if (currentTurnIndex < 0) {
            currentTurnIndex = 0;
            printDebug();

            turns[currentTurnIndex].setUp();
        }
    }

    private void printDebug() {
        Debug.Log("New Turn: " + turns[currentTurnIndex].ToString() + " (" + currentTurnIndex + ").");
    }
}
