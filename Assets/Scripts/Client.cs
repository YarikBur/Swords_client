using System;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

namespace Clients
{



	public class ClientWrapper
	{
		private static string HOST = "127.0.0.1";
		private static int PORT = 5586;
		private Client client;

		public void init(string username, string password)
		{
			client = new Client();
			client.Connect(HOST, PORT);
			client.Write(username);
			client.Write(password);
		}

		public void Write(string name, float f)
		{
			client.Write(name + ": " + f);
		}

		public void Write(string command)
		{
			client.Write(command);
		}

		public void Write(string name, int i)
		{
			client.Write(name + ": " + i);
		}

		public void Write(string name, Vector3 vec)
		{
			client.Write(name);
			client.Write(vec.x + ";" + vec.y + ";" + vec.z);
		}

		public void Exit()
		{
			client.Exit();
		}

		public string Read() {
			return client.Read();
		}
	}

    public class Client
    {
        private IPEndPoint addres;
        private Socket client;

        public void Connect(string host, int port)
        {
            addres = new IPEndPoint(IPAddress.Parse(host), port);

            client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            client.Connect(addres);
        }

        public void Write(string textToSend)
        {
            int toSendLen = System.Text.Encoding.ASCII.GetByteCount(textToSend);
            byte[] toSendBytes = System.Text.Encoding.ASCII.GetBytes(textToSend);
            byte[] toSendLenBytes = System.BitConverter.GetBytes(toSendLen);
            client.Send(toSendLenBytes);
            client.Send(toSendBytes);
        }

        public string Read()
        {
			byte[] rcvLenBytes = new byte[4];
			client.Receive(rcvLenBytes);
			int rcvLen = System.BitConverter.ToInt32(rcvLenBytes, 0);
			byte[] rcvBytes = new byte[rcvLen];
			client.Receive(rcvBytes);
			String rcv = System.Text.Encoding.ASCII.GetString(rcvBytes);

            return rcv;
        }

		public void Exit()
		{
			Write("EXIT");
		}
    }
}
