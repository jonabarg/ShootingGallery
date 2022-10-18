using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;
using TMPro;

public class CubeMaker : MonoBehaviour {
    [SerializeField]
    protected Transform prefab;
    protected List<Vector3> positions = new List<Vector3>();
    protected Vector3 position;
    protected Transform target;
    protected List<Transform> targets = new List<Transform>();
    protected List<int> targetsHit = new List<int>();

    [SerializeField]
    protected TextMeshProUGUI textBox;
    public int _score = 0;
    public int missed = 0;

    protected void makeCube(int index) {
        position = new Vector3(Random.Range(-10, 10), Random.Range(8, 12), Random.Range(-2, 2));
        if (positions.Contains(position)) {
            makeCube(index);
        }
        else {
            positions.Add(position);
            target = Instantiate(prefab);
            target.position = positions[index];
            target.name = "" + index;
            targets.Add(target);
        }
    }

    protected void moveCube(int index) {
        position = new Vector3(Random.Range(-10, 10), Random.Range(8, 12), Random.Range(-2, 2));
        if (positions.Contains(position)) {
            moveCube(index);
        }
        else {
            positions[index] = position;
            target = targets[index];
            target.position = positions[index];
            targets[index] = target;
        }
    }

    protected void Start() {
        for (int i = 0; i < 10; i++) {
            makeCube(i);
        }
    }

    [SerializeField]
    protected Camera cam;

    protected void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Vector2 mousePos = Input.mousePosition;
            Ray ray = cam.ScreenPointToRay(mousePos);

            RaycastHit rch;

            if (Physics.Raycast(ray, out rch)) {
                moveCube(int.Parse(rch.transform.name));
                _score++;
            }
        }
        textBox.text = "Score: " + _score + "\nMissed: " + missed;
    }

    private void OnTriggerEnter(Collider collision) {
        moveCube(int.Parse(collision.transform.name));
        missed++;
    }

    public void buttonClick() {
        missed = 0;
        _score = 0;
    }
}
