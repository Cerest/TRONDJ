using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Placeholder for class names before they're created
//When y'all create these classes, delete these lines!
public partial class Player
{
	private int hitcount = 0;
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


//The Interface class itself

public class Interface : MonoBehaviour
{
	
	private Camera MainCamera;
	private GameObject CameraPlayer1;
	private GameObject CameraPlayer2;
	
	private Board playboard;
	private int[] lifecount;
	private int count = 0;
	public enum Screen { None, Title, Play, Over };
	delegate void Event();
	
    // Start is called before the first frame update
    void Start()
    {
		MainCamera = Camera.main;
		CameraPlayer1 = GameObject.Find("CameraPlayer1");
		CameraPlayer2 = GameObject.Find("CameraPlayer2");
		State(Screen.Title);
		lifecount = new int[2];
		lifecount[0] = 5;
		lifecount[1] = 5;
    }

    // Update is called once per frame
	Event Update;
	void UpdateEmpty()
	{
		;
	}
    void UpdatePlay()
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
        playboard.Collide();
		count++;
    }
	
	void OnGUI()
	{
		GUI.Label(new Rect(10, 10, 1000, 1000), count.ToString());
		playboard.Draw();
	}
	
	public void Hurt(Player n)
	{
		lifecount[Array.IndexOf(playboard.playerList, n)]--;
		if (lifecount[0] == 0 || lifecount[1] == 0)
		{
			playboard.GameEnd();
		}
	}
	
	public void State(Screen n)
	{
		switch (n) {
			case Screen.Title :
			    MainCamera.enabled = false;
				Update = UpdateEmpty;
				break;
			case Screen.Play :
				playboard = new Board(0, 0);
				Update = UpdatePlay;
				break;
			case Screen.Over :
				break;
			default :
				break;
		}
	}
}


