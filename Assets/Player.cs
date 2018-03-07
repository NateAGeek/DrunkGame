using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    //Public Prefrancese
    public Vector2 sensitivity = new Vector2(10.0f, 10.0f);
    public float speed = 5.0f;
    public float jumpVelocity = 5.0f;

    //Component Vars
    private Rigidbody rigidbody;
    private Camera camera;

    // Movement Vars
    private Vector2 rotationMin = new Vector2(-360.0f, -60.0f);
    private Vector2 rotationMax = new Vector2(360.0f, 60.0f);
    private Vector3 rotation = new Vector3(0.0f, 0.0f, 0.0f);
    private bool onGround = true;
    private bool hitSomethingInAir = false;
    private bool freeze = false;

    private List<GameObject> Inventory = new List<GameObject>();

    private GameObject InventoryHUD;

    void Start() {
        camera = GetComponentInChildren<Camera>();
        rigidbody = GetComponent<Rigidbody>();
    }

    void Update() {

        //Check if frozen for movment and other abilities to be active
        if (!freeze) {
            //Do the Calculations for rotation
            rotation.x += Input.GetAxis("Mouse X") * sensitivity.x;
            rotation.y += Input.GetAxis("Mouse Y") * sensitivity.y;
            rotation.y = Mathf.Clamp(rotation.y, rotationMin.y, rotationMax.y);

            transform.localEulerAngles = new Vector3(0.0f, rotation.x, 0.0f);
            camera.transform.localEulerAngles = new Vector3(-rotation.y, 0.0f, 0.0f);

            //Movement Controls
            if (!hitSomethingInAir) {
                Vector3 targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
                targetVelocity = transform.TransformDirection(targetVelocity);
                targetVelocity = new Vector3(targetVelocity.x * speed, rigidbody.velocity.y, targetVelocity.z * speed);
                Vector3 velocityChange = targetVelocity - rigidbody.velocity;

                rigidbody.AddForce(velocityChange, ForceMode.VelocityChange);
            }
            if (onGround && Input.GetButtonDown("Jump")) {
                rigidbody.AddForce(transform.up * jumpVelocity, ForceMode.VelocityChange);
            }


            //Item handel this stuff m8
/*            if (Input.GetButtonDown("Interact"))
            {
                RaycastHit hit;
                Ray ray = gameObject.GetComponentInChildren<Camera>().ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform.gameObject.tag == "Interactable")
                    {

                    }
                }
            }
            */
            if (Input.GetButtonDown("Mouse Left Click")) {
                RaycastHit hit;
                //ray through the middle of the screen, i.e. where the player is looking
                Ray ray = gameObject.GetComponentInChildren<Camera>()
                    .ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));

                if (Physics.Raycast(ray, out hit)) {
                    //Our Ray from the center hit something, and put it into hit
                    if (hit.collider.gameObject.tag == "Balls" && hit.distance <= 2.5f) {
                        GameObject ball = hit.collider.gameObject;
                        ball.GetComponent<Rigidbody>().AddForce(5 * ray.direction.normalized, ForceMode.Impulse);

                    }
                }
            }
        }

    }

    void FixedUpdate() {

    }

    void OnCollisionEnter(Collision hit) {

        //If we hit a wall or something while in the air, should begin to ignore velocity vectors
        if (!onGround && hit.collider.tag == "Untagged") {
            hitSomethingInAir = true;
        }
    }

    void OnCollisionStay(Collision hit) {

        //If we are on the ground we have hit something we should acknowledge the velocity vectors
        if (onGround && hit.collider.tag == "Untagged") {
            hitSomethingInAir = false;
        } else {
            //But if not and we are staying on a wall, we should ignore velocity vectors
            hitSomethingInAir = true;
        }
    }

    void OnCollisionExit(Collision hit) {
        //If we exit a collision we should just assume that we have not hit a wall or anything
        if (hit.collider.tag == "Untagged") {
            hitSomethingInAir = false;
        }
    }

    void OnTriggerEnter(Collider hit) {
        //Trigger for feet to see if on floor to entity
        if (hit.tag == "Untagged") {
            onGround = true;
        }
    }

    void OnTriggerExit(Collider hit) {
        //Trigger for feet to see if off floor to entity
        if (hit.tag == "Untagged") {
            onGround = false;
        }
    }
}