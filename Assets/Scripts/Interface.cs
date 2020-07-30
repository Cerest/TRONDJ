using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Placeholder for class names before they're created
//When y'all create these classes, delete these lines!
public partial class Player
{
	public int hitcount = 0;
	public float X;
	public float Y;
	public Direction face;
	public GameObject cube;
	public Player(float Xpos, float Ypos, GameObject sprite)
	{
		X = Xpos;
		Y = Ypos;
		cube = sprite;
	}
	
	public enum Direction {up, down, left, right};
	
	public void ChangeDir(Direction dir)
	{
		face = dir;
	}
	
	public void Update()
	{
		switch (face) {
			case Direction.up :
				Y+=1;
				break;
			case Direction.down :
				Y-=1;
				break;
			case Direction.left :
				X-=1;
				break;
			case Direction.right :
				X+=1;
				break;
		}
	}
	
	public void Draw()
	{
		cube.GetComponent<Transform>().position = new Vector3(X, Y, -100);
	}
}
public partial class Board
{
	public Board(float Xsize, float Ysize)
	{
		playerList = new Player[2];
		playerList[0] = new Player(10, 25, GameObject.Find("Player1"));
		playerList[1] = new Player(10, 40, GameObject.Find("Player2"));
	}
	
	public Player[] playerList;
	public void GameEnd()
	{
		;
	}
	public void Collide()
	{
		;
	}
	public void Update()
	{
		foreach (Player player in playerList) {
			player.Update();
		}
	}
	public void Draw()
	{
		foreach (Player player in playerList) {
			player.Draw();
		}
	}
}

//END PLACEHOLDER

//The Interface class itself

public class Interface : MonoBehaviour
{
	
	private Camera MainCamera;
	public GameObject CameraPlayer1;
	public GameObject CameraPlayer2;
	private screenMode mode;
	public int winner;
	
	public enum Screen { None, Title, Play, Over };
	
    // Start is called before the first frame update
    void Start()
    {
		MainCamera = Camera.main;
		CameraPlayer1 = GameObject.Find("CameraPlayer1");
		CameraPlayer2 = GameObject.Find("CameraPlayer2");
		State(Screen.Title);
    }

    // Update is called once per frame
	void Update()
	{
		mode.Update(this);
	}
	
	void OnGUI()
	{
		mode.OnGUI(this);
	}
	
	public void Hurt(Player n)
	{
		mode.Hurt(this, n);
	}
	
	public void State(Screen n)
	{
		switch (n) {
			case Screen.Title :
			    MainCamera.enabled = true;
				CameraPlayer1.GetComponent<Camera>().enabled = false;
				CameraPlayer2.GetComponent<Camera>().enabled = false;
				MainCamera.transform.position = new Vector3(-100, -124, -100);
				mode = new Title();
				break;
			case Screen.Play :
				MainCamera.enabled = false;
				CameraPlayer1.GetComponent<Camera>().enabled = true;
				CameraPlayer2.GetComponent<Camera>().enabled = true;
				mode = new Play(2, 100, 100);
				break;
			case Screen.Over :
			    MainCamera.enabled = true;
				CameraPlayer1.GetComponent<Camera>().enabled = false;
				CameraPlayer2.GetComponent<Camera>().enabled = false;
				MainCamera.transform.position = new Vector3(-120, -124, -100);
				GameObject[] winmsgs = GameObject.FindGameObjectsWithTag("WinMsgs");
				foreach (GameObject msg in winmsgs) {
					msg.GetComponent<Transform>().position = new Vector3(-120, -1240, -100);
				}
				winmsgs[winner].GetComponent<Transform>().position = new Vector3(-120, -124, -100);
				mode = new Over(winner);
				break;
			default :
				break;
		}
	}
}

interface screenMode
{
	void Update(Interface parent);
	void OnGUI(Interface parent);
	void Hurt(Interface parent, Player n);
}

//Handles Title Screen control
public class Title : screenMode
{
	public void Update(Interface parent)
	{
		if (Input.GetButton("Player1Fire") && Input.GetButton("Player2Fire"))
		{
			parent.State(Interface.Screen.Play);
		}
	}
	public void OnGUI(Interface parent)
	{
		GUI.Label(new Rect(10, 10, 1000, 100), "Snake");
		GUI.Label(new Rect(10, 25, 1000, 100), "Press space + period to begin");
	}
	public void Hurt(Interface parent, Player n)
	{
		;
	}
}

//Handles Game Over Screen control
public class Over : screenMode
{
	private Over(){}
	public Over(int winner)
	{
		this.winner = winner;
	}
	
	private int winner;
	private bool returning = false;
	public void Update(Interface parent)
	{
		if (Input.GetButton("Player1Fire") && Input.GetButton("Player2Fire"))
		{
			//returning = true;
		}
		if (returning && !Input.GetButton("Player1Fire") && !Input.GetButton("Player2Fire"))
		{
			parent.State(Interface.Screen.Title);
		}
	}
	public void OnGUI(Interface parent)
	{
		GUI.Label(new Rect(10, 10, 1000, 100), "GAME OVER");
		GUI.Label(new Rect(10, 25, 1000, 100), "Player " + winner.ToString() + " wins!");
		GUI.Label(new Rect(10, 40, 1000, 100), "Press space + period to return to title screen.");
	}
	public void Hurt(Interface parent, Player n)
	{
		;
	}
}

//Handles Gameplay Screen control
public class Play : screenMode
{
	private int[] lifecount;
	private Board playboard;
	private int count = 0;
	
	private Play(){}	//No parameterless constructor
	public Play(int playerCount, float boardSizeX, float boardSizeY)
	{
		lifecount = new int[playerCount];
		playboard = new Board(boardSizeX, boardSizeY);
	}
	
    public void Update(Interface parent)
    {
		if (Input.GetButtonDown("Player1Up")) {
			playboard.playerList[0].ChangeDir(Player.Direction.up);
		}
		if (Input.GetButtonDown("Player1Down")) {
			playboard.playerList[0].ChangeDir(Player.Direction.down);
		}
		if (Input.GetButtonDown("Player1Left")) {
			playboard.playerList[0].ChangeDir(Player.Direction.left);
		}
		if (Input.GetButtonDown("Player1Right")) {
			playboard.playerList[0].ChangeDir(Player.Direction.left);
		}
		if (Input.GetButtonDown("Player2Up")) {
			playboard.playerList[1].ChangeDir(Player.Direction.up);
		}
		if (Input.GetButtonDown("Player2Down")) {
			playboard.playerList[1].ChangeDir(Player.Direction.down);
		}
		if (Input.GetButtonDown("Player2Left")) {
			playboard.playerList[1].ChangeDir(Player.Direction.left);
		}
		if (Input.GetButtonDown("Player2Right")) {
			playboard.playerList[1].ChangeDir(Player.Direction.right);
		}
		if (Input.GetKey("tab")) {
			parent.winner = playboard.playerList[0].hitcount > playboard.playerList[1].hitcount ? 1 : 2;
			parent.State(Interface.Screen.Over);
		}
        playboard.Collide();
		playboard.Update();
		count++;
		var cam1trans = parent.CameraPlayer1.GetComponent<Transform>();
		var cam2trans = parent.CameraPlayer2.GetComponent<Transform>();
		cam1trans.position = new Vector3(playboard.playerList[0].X, playboard.playerList[0].Y, cam1trans.position.z);
		cam2trans.position = new Vector3(playboard.playerList[1].X, playboard.playerList[1].Y, cam2trans.position.z);
    }
	
	public void OnGUI(Interface parent)
	{
		GUI.Label(new Rect(10, 10, 1000, 1000), count.ToString());
		playboard.Draw();
	}
	
	public void Hurt(Interface parent, Player n)
	{
		lifecount[Array.IndexOf(playboard.playerList, n)]--;
		if (lifecount[0] == 0 && lifecount[1] == 0)
		{
			parent.winner = 0;
			playboard.GameEnd();
			parent.State(Interface.Screen.Over);
		}
	}
}

