using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Clients;


public class test : MonoBehaviour
{
	public GameObject camera;
	public Camera goCamera;
    public float speed = .15f;
    private float speedDublicate = 0;
    private Vector3 positionDublicate = new Vector3(0, 0, 0);
    private Vector3 rotation;
    private Vector3 rotationDublicate;
	private float MyAngle = 0F;
	public float sensitivity = 1F;

	public enum ViewObject
	{
		Player,
		Item,
		GUI
	}
	public ViewObject viewObject = ViewObject.Player;
	public string nameObject;
	public GameObject gameObject;

	ClientWrapper client;
    
    void Start()
    {
		client = new ClientWrapper();
		client.init(viewObject.ToString("g"), nameObject);

        positionDublicate = transform.position;
        rotation = new Vector3(camera.transform.rotation.x, camera.transform.rotation.y, camera.transform.rotation.z);
        rotationDublicate = rotation;
    }
    
    void Dublicate()
    {
        if (speedDublicate != speed)
        {
            client.Write("Speed", speed);
            speedDublicate = speed;
        }

        if (positionDublicate != transform.position)
        {
            client.Write("Position", positionDublicate);

            positionDublicate = transform.position;
        }

        if (rotationDublicate != rotation)
        {
            client.Write("Rotation", rotationDublicate);
            rotationDublicate = rotation;
        }
    }

    void Update()
    {
        Dublicate();
        

    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) ||
            Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            transform.Translate(Vector3.forward * speed  * Input.GetAxis("Vertical"));

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) ||
            Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            transform.Translate(Vector3.left * speed  * -Input.GetAxis("Horizontal"));

		if (Input.GetKey(KeyCode.Escape))
			Application.Quit();
    }

    void OnApplicationQuit()
    {
		client.Exit();

        Thread.Sleep(200);
    }
}
