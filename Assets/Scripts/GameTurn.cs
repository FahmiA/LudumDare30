using System;

public interface GameTurn {

    void Setup();

    void Update();

    void TearDown();

    bool IsComplete();

}
