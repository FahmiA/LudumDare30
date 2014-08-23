using System;

namespace GameController {
    public interface GameTurn {

        void setUp();

        void update();

        void tearDown();

        bool isComplete();
    }
}

