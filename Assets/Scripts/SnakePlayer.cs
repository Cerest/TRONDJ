using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakePlayer
{
    // Start is called before the first frame update
    void Start()
    {

    }
	public enum Direction {up,down,left,right};
	private SnakePlayer(){}
    public SnakePlayer(float X, float Y, Color shade)
    {
		this.X = X;
		this.Y = Y;
		lives = 5;
		currDir = Direction.up;
    }
	
	public float X;
	public float Y;
	public int lives;
	public Direction currDir;

    public bool CheckCollide(float X, float Y)
    {
		return false;
    }

    public bool CheckCollideHead(float X, float Y)
    {
		return false;
    }

    public void Draw()
    {

    }

    public void Move()
    {
		switch (currDir) {
			case Direction.up :
				Y++;
				break;
			case Direction.down :
				Y--;
				break;
			case Direction.left :
				X--;
				break;
			case Direction.right :
				X++;
				break;
		}
    }

    public void ChangeDir(Direction dir)
    {
		currDir = dir;
    }

    public void Die()
    {

    }

    // Update is called once per frame
    public void Update()
    {

    }
}
