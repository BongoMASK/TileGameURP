using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] float zoomAmount = 0.5f;

    [SerializeField] Transform boardParent;

    private void Awake() {
        SetPosition();
    }

    private void Update() {
        DragMove();
        ScrollToZoom();
    }

    void DragMove() {

    }

    void ScrollToZoom() {
        float camsize = cam.orthographicSize - zoomAmount * Input.mouseScrollDelta.y;

        cam.orthographicSize = Mathf.Clamp(camsize, 1, 5);
    }

    void SetPosition() {
        Vector3 centroid = Vector3.zero;

        for (int i = 0; i < boardParent.childCount; i++) {
            centroid += boardParent.GetChild(i).position;
        }

        centroid /= boardParent.childCount;
        transform.position = centroid;
    }

}
