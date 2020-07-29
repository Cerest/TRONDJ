using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Placeholder for class names before they're created
//When y'all create these classes, delete these lines!
public partial class Player
{
	public Player(float Xpos, float Ypos)
	{
		;
	}
}
public partial class Board
{
	public Board(float Xsize, float Ysize)
	{
		;
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
		;
	}
}

//The Interface class itself

public class Interface : MonoBehaviour
{
	
	private Board playboard;
	private int[] lifecount;
	
	public enum Screen { None, Title, Play, Over };
	
    // Start is called before the first frame update
    void Start()
    {
        playboard = new Board(500, 500);
		lifecount = new int[2];
		lifecount[0] = 5;
		lifecount[1] = 5;
    }

    // Update is called once per frame
    void Update()
    {
        playboard.Collide();
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
		//?
	}
}
