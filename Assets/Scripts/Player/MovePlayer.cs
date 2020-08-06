using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    private Vector3 dublicatePosition = new Vector3();

    void Update() {
        
    }
    void FixedUpdate()
    {
        if (tag.Equals("MainPlayer"))
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) ||
                Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
                transform.Translate(Vector3.forward * GetComponent<Info>().speed  * Input.GetAxis("Vertical"));

            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) ||
                Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
                transform.Translate(Vector3.left * GetComponent<Info>().speed  * -Input.GetAxis("Horizontal"));

            if (Input.GetKey(KeyCode.Space))
                transform.Translate(Vector3.up * Input.GetAxis("Jump"));

            if (Input.GetKeyUp(KeyCode.Escape))
                Application.Quit();
        }

        if (transform.position != dublicatePosition)
        {
            Login.client.Write("POSITION", transform.position);

            dublicatePosition = transform.position;
        }
    }
}
