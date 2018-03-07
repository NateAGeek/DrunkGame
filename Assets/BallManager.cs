using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour {
    public GameObject ballSpawn;
    public GameObject ballSnagger;
    public GameObject groupedBalls;
    public Color ballColor;

    public int NumberToSpawn = 10;

    private BallSpawner ballSpawnerInstance;
    private BallSnagger ballSnaggerInstance;

    private List<GameObject> balls;

    private int timer = 0;

	// Use this for initialization
	void Start () {
        ballSpawnerInstance = ballSpawn.GetComponent<BallSpawner>() as BallSpawner;
        ballSnaggerInstance = ballSnagger.GetComponent<BallSnagger>() as BallSnagger;
        ballSnaggerInstance.setColor(ballColor);
    }
	
	// Update is called once per frame
	void Update () {
        if (timer >= 100) {
            timer = 0;
            GameObject ball = ballSpawnerInstance.spawnBall(ballColor);
            ball.transform.parent = this.groupedBalls.transform;
        } else {
            timer += 1;
        }
	}
}
