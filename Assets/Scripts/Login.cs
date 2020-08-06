using System;
using UnityEngine;
using Clients;

public class Login : MonoBehaviour
{
    public string username;
    public string password;

    private bool auth = false;

    public GameObject player;

    public static ClientWrapper client;

    void Start()
    {
        client = new ClientWrapper();
        client.init(username, password);

        string auth = client.Read();

        Debug.Log(auth);

        if (auth.Contains("successful"))
        {
            this.auth = true;
            GameObject player = Instantiate(this.player);
            player.name = username;
            player.GetComponent<Info>().username = username;
            player.tag = "MainPlayer";
            float[] position = new float[3];
            float[] rotation = new float[3];

            client.Write("CONNECTION");
            for (int i = 0; i < 2; i++)
            {
                string input = client.Read();
                string transform = client.Read();

                if (input.Equals("POSITION"))
                    for (int sp = 0; sp < 3; sp++) 
                        position[sp] = Convert.ToSingle(transform.Split(';')[sp]);

                if (input.Equals("ROTATION"))
                    for (int sp = 0; sp < 3; sp++) 
                        rotation[sp] = Convert.ToSingle(transform.Split(';')[sp]);
            }

            player.transform.position = new Vector3(position[0], position[1], position[2]);
            player.transform.rotation = Quaternion.Euler(0, rotation[1], 0);
        }


        client.Write("CONNECTED");
    }
}
