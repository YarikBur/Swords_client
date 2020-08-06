using System;
using UnityEngine;

public class Status : MonoBehaviour
{

	public Material Green;
	public Material Yellow;
	public Material Red;
	public int mode = 0;
	private bool switched = false;

	private float rotateY = 0;
	private Vector3 position;
	private float time = 0;

	void Start()
	{
		position = transform.position;
	}

    void FixedUpdate()
    {
		position = transform.parent.position;

		float sin = Convert.ToSingle(Math.Sin(time));
		time += .05f;

		rotateY += 1;
		if (rotateY >= 360)
			rotateY -= 360;

		transform.rotation = Quaternion.Euler(-90, rotateY, 0);
		transform.position = new Vector3(position.x, (position.y + sin / 5f + 1.5f), position.z);

		switch (mode)
		{
		case (1):
			setYellow();
			break;
		case (2):
			setRed();
			break;
		default:
			setGreen();
			break;
		}
    }

	public void setGreen()
	{
		GetComponent<Renderer>().material = Green;
	}

	public void setYellow()
	{
		GetComponent<Renderer>().material = Yellow;
	}

	public void setRed()
	{
		GetComponent<Renderer>().material = Red;
	}

}
