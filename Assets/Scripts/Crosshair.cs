using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour {
    [SerializeField]
    protected RectTransform crosshair;


    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    protected void Update() {
        Vector2 mousePos = Input.mousePosition;
        crosshair.position = mousePos;

        if (mousePos.x > 0 && mousePos.y > 0 && mousePos.x < Screen.width && mousePos.y < Screen.height) {
            Cursor.visible = false;
            crosshair.gameObject.SetActive(true);
        }
        else {
            Cursor.visible = true;
            crosshair.gameObject.SetActive(false);
        }
    }
}
