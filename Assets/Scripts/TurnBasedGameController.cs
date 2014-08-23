using UnityEngine;
using System.Collections.Generic;

using GameController;

public class TurnBasedGameController : MonoBehaviour {

    private GameTurn[] turns;
    private int currentTurnIndex;
    private int firstTurnHandicap = 2;

    // Use this for initialization
    void Start() {
        turns = new GameTurn[] {
            new AITurn(),
            new PlayerTurn(),
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

            currentTurn = pickNextTurn();

            currentTurn.setUp();
        } else {
            currentTurn.update();
        }
    }

    private GameTurn pickNextTurn() {
        if (firstTurnHandicap > 1) {
            currentTurnIndex = 0;
            firstTurnHandicap--;
        } else {
            currentTurnIndex = (currentTurnIndex + 1) % turns.Length;
        }

        GameTurn currentTurn = turns[currentTurnIndex];
        printDebug();

        return currentTurn;
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
