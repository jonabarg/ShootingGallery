using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMaker : MonoBehaviour {
    [SerializeField]
    protected Transform prefab;
    protected List<Vector3> positions = new List<Vector3>();
    protected Vector3 position;
    protected Transform target;
    protected List<Transform> targets = new List<Transform>();
    protected List<int> targetsHit = new List<int>();

    protected void makeCube(int index) {
        position = new Vector3(Random.Range(-10, 10), Random.Range(-3, 5), Random.Range(-2, 2));
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
        position = new Vector3(Random.Range(-10, 10), Random.Range(-3, 5), Random.Range(-2, 2));
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

    protected void keepScore(int index) {
        if (!targetsHit.Contains(index)) {
            targetsHit.Add(index);
        }
        bool hitAll = true;
        for (int i = 0; i < 10; i++) {
            if (!targetsHit.Contains(i)) {
                hitAll = false;
            }
        }
        if (hitAll) {
            Debug.Log("Player has hit all the targets at least once!");
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
                Debug.Log("Player clicked on Target '" + rch.transform.name + "'");
                moveCube(int.Parse(rch.transform.name));
                keepScore(int.Parse(rch.transform.name));
            }
            else {
                Debug.Log("Player missed");
            }
        }
    }
}
