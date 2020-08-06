using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

	public GameObject player;
    public List<GameObject> players;

    

    private void Start() {

    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.N))
		{
            players.Add(Instantiate(player));
            players[players.Count - 1].tag = "Player";
            players[players.Count - 1].name = "TestObject" + players.Count;
            players[players.Count - 1].GetComponentInChildren<Camera>().targetDisplay = 1;
        }
    }
}
