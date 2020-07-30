using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class SnakePlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }
    public enum Direction { up, down, left, right };

    private SnakePlayer() { }

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
    public float Speed = 0.1;


    public bool CheckCollide(float X, float Y)
    {
        bool collision;
        if ((this.X - 5 < X && this.X + 5 > X) && (this.Y - 5 < Y && this.Y + 5 > Y))
        {
            collision = true;
        }
        else
        {
            collision = false;
        }
        return collision;
    }

    public bool CheckCollideHead(float X, float Y)
    {
        bool collision;
        if ((this.X - 5 < X && this.X + 5 > X) && (this.Y - 5 < Y && this.Y + 5 > Y))
        {
            collision = true;
        }
        else
        {
            collision = false;
        }
        return collision;
    }

    public Draw()
    {

    }

    public Move()
    {
        switch(currDir)
        {
            case Direction.up:
                Y += Speed;
                break;
            case Direction.down:
                Y -= Speed;
                break;
            case Direction.left:
                X -= Speed;
                break;
            case Direction.right:
                X += Speed;
                break;
        }
    }

    public ChangeDir(Direction dir)
    {

        if (currDir == Direction.down && dir == Direction.up)
        {
            break;
        }
        else if (currDir == Direction.up && dir == Direction.down)
        {
            break;
        }
        else if (currDir == Direction.left && dir == Direction.right)
        {
            break;
        }
        else if (currDir == Direction.right && dir == Direction.left)
        {
            break;
        }
        else if (currDir == dir)
        {
            break;
        }
        else
        {
            currDir = dir;
        }
    }

    public Die()
    {

    }

    // Update is called once per frame
    public void Update()
    {

    }
}
