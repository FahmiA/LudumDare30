﻿using UnityEngine;

public class Node : MonoBehaviour {

    public static Node player = null;
    public static Node enemy = null;

    // There should only be one Node with this set to true.
    public bool IsStart = false;

    public void Start() {
        if (IsStart) {
            player = this;
            enemy = this;
        }
    }

    public void OnMouseDown() {
        if (Link.IsLinked(player, this)) {
            player = this;
        }
    }

}
