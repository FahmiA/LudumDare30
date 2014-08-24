using UnityEngine;
using System.Collections.Generic;

public class TurnBasedGameController : MonoBehaviour {

    private GameTurn[] turns;
    private int currentTurnIndex;

    // The number of turns that the enemy gets at the start.
    public int firstTurnHandicap = 2;

    // Use this for initialization
    void Start() {
        turns = new GameTurn[] {
            new AITurn(),
            new PlayerTurn(),
        // new FeedbackTurn()
        };

        currentTurnIndex = -1;
    }
    
    // Update is called once per frame
    void Update() {
        // Everything is given one turn
        ensureCurrentTurn();

        GameTurn currentTurn = turns[currentTurnIndex];

        if (currentTurn.IsComplete()) {
            currentTurn.TearDown();

            currentTurn = pickNextTurn();

            currentTurn.Setup();
        } else {
            currentTurn.Update();
        }
    }

    private GameTurn pickNextTurn() {
        if (firstTurnHandicap > 1) {
            currentTurnIndex = 0;
            firstTurnHandicap -= 1;
        } else {
            currentTurnIndex = (currentTurnIndex + 1) % turns.Length;
        }

        GameTurn currentTurn = turns[currentTurnIndex];

        return currentTurn;
    }

    private void ensureCurrentTurn() {
        if (currentTurnIndex < 0) {
            currentTurnIndex = 0;

            turns[currentTurnIndex].Setup();
        }
    }

}
