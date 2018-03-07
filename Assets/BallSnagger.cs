using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSnagger : MonoBehaviour {
    public GameObject mesh;

    private Color snaggerColor;

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider collider) {
        if (collider.gameObject.tag == "Balls") {
            GameObject ball = collider.gameObject;
            Renderer ballRen = ball.GetComponent<Renderer>();

            if (this.transform.parent == ball.transform.parent.parent) {
                ballRen.material.SetColor("_Color", Color.green);
            }
        }
    }

    public void setColor(Color color) {
        Renderer meshRen = mesh.GetComponent<Renderer>();
        meshRen.material.SetColor("_Color", color);
    }
}
