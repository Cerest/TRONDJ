using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board
{
	public Board(float Xsize, float Ysize)
	{
		playerList = new SnakePlayer[2];
		playerList[0] = new SnakePlayer(10, 25, Color.green);
		playerList[1] = new SnakePlayer(10, 40, Color.white);
		powerupList = new List<PowerUp>();
	}
	
	public SnakePlayer[] playerList;
	public List<PowerUp> powerupList;
	
	public void GameEnd()
	{
		playerList = null;
		powerupList = null;
	}
	public void Collide()
	{
		foreach (SnakePlayer player in playerList) {
		    //Players touching powerups
			foreach (PowerUp powerup in powerupList) {
				if (player.CheckCollideHead(powerup.X, powerup.Y)) {
					powerup.Effect(player);
				}
			}
		    //Players touching players
			if (playerList[0].CheckCollide(playerList[1].X, playerList[1].Y)) {
				//Two hit one
				playerList[1].Die();
			}
			if (playerList[1].CheckCollide(playerList[0].X, playerList[0].Y)) {
				//One hit two
				playerList[0].Die();
			}
		}
	}
	public void Update()
	{
		foreach (SnakePlayer player in playerList) {
			player.Update();
			player.Move();
		}
	}
	public void Draw()
	{
		foreach (SnakePlayer player in playerList) {
			player.Draw();
		}
	}
}
