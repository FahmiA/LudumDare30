using UnityEngine;
using System.Collections;

public class Focus : MonoBehaviour {

    void Update() {
        if (!PanTo.IsPanning && Input.GetKey(KeyCode.Space)) {
            TurnBasedGameController director = GameObject.FindGameObjectWithTag("Director")
                .GetComponent<TurnBasedGameController>();
            Node playerNode = director.Player;

            transform.position = new Vector3(playerNode.transform.position.x,
                                             playerNode.transform.position.y,
                                             transform.position.z);
        }
    }

}
