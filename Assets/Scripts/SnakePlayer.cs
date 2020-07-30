using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakePlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }
	public enum Direction {up,down,left,right};
	private SnakePlayer(){}
    public SnakePlayer(float X, float Y, Color shade)
    {

    }
	
	public float X;
	public float Y;
	public int lives;

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

    }

    public void ChangeDir(Direction dir)
    {

    }

    public void Die()
    {

    }

    // Update is called once per frame
    public void Update()
    {

    }
}
