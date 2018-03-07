using UnityEngine;

public class BallSpawner : MonoBehaviour {
    public GameObject SpawnableBall;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public GameObject spawnBall() {
        GameObject ball      = Instantiate(SpawnableBall, this.transform);
        Rigidbody  ballRigi  = ball.GetComponent<Rigidbody>();
        Vector3 direction = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));

        ballRigi.AddForce(500 * direction);

        return ball;
    }

    public GameObject spawnBall(Color color)
    {
        GameObject ball = Instantiate(SpawnableBall, this.transform);
        Rigidbody ballRigi = ball.GetComponent<Rigidbody>();
        Vector3 direction = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
        Renderer ren = ball.GetComponent<Renderer>();
        ren.material.SetColor("_Color", color);


        ballRigi.AddForce(500 * direction);

        return ball;
    }
}
