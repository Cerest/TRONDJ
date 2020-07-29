using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Placeholder for class names before they're created
//When y'all create these classes, delete these lines!
public partial class Player
{
	public int hitcount = 0;
	private float X;
	private float Y;
	public Player(float Xpos, float Ypos)
	{
		X = Xpos;
		Y = Ypos;
	}
	
	public enum Direction {up, down, left, right};
	
	public void ChangeDir(Direction dir)
	{
		if (dir == Direction.up) {
			hitcount++;
		}
	}
	public void Draw()
	{
		GUI.Label(new Rect(X, Y, 100, 100), "Up pressed: " + hitcount.ToString());
	}
}
public partial class Board
{
	public Board(float Xsize, float Ysize)
	{
		playerList = new Player[2];
		playerList[0] = new Player(10, 25);
		playerList[1] = new Player(10, 40);
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
	private GameObject CameraPlayer1;
	private GameObject CameraPlayer2;
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
			returning = true;
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
		count++;
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

