using UnityEngine;

public class FaceCamera : MonoBehaviour {
    private Camera cam;

    void Start() {
        cam = Camera.main;
    }

    void Update() {
        transform.rotation = Quaternion.LookRotation(transform.position - cam.transform.position);
    }
}