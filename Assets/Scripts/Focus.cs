using UnityEngine;
using System.Collections;

public class Focus : MonoBehaviour {

    void Update() {
        if (!PanTo.IsPanning && Input.GetKey(KeyCode.Space)) {
            transform.position = new Vector3(Node.Player.transform.position.x,
                                             Node.Player.transform.position.y,
                                             transform.position.z);
        }
    }

}
