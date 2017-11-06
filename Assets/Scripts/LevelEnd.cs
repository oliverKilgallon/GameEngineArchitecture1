using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnd : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
