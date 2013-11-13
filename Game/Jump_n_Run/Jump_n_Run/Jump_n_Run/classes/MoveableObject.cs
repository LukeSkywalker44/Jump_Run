using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Jump_n_Run.classes
{

    enum Orientation
    {
        Up, Down, Left, Right, Idle
    }
    class MoveableObject : GameObject
    {
        public int movementSpeed;
        public Orientation orientation; 
        private Keys KeyUp;
        private Keys KeyDown;
        private Keys KeyLeft;
        private Keys KeyRight;


        public MoveableObject(int moveSpeed, Texture2D texture, Rectangle rect, Keys keyUp, Keys keyDown, Keys keyLeft, Keys keyRight)
            
        {
            this.Texture = texture;
            this.rectangle = rect;
            this.movementSpeed = moveSpeed;
            this.orientation = Orientation.Idle;
            this.KeyDown = keyDown;
            this.KeyLeft = keyLeft;
            this.KeyRight = keyRight;
            this.KeyUp = keyUp;
        }


        public MoveableObject(int moveSpeed, Texture2D texture, Rectangle rect)
        {
            this.Texture = texture;
            this.rectangle = rect;
            this.movementSpeed = moveSpeed;
            this.orientation = Orientation.Idle;
            
        }

        // TODO
        // Move-Methods should take an IEnumerable<GameObject> as collider Parameter
        //  ->there will be more than two objects in the game :)

        public void MoveUp(Rectangle bound, IEnumerable<GameObject> collider, GameTime gt)
        {
            this.orientation = Orientation.Up;
            if ((!Collision.BoundCollision(bound, this)) && (!Collision.ObjectCollision(collider, this)))
            {
                this.rectangle.Y -= this.movementSpeed;
            }
            else
            {
                int oldMoveSpeed = this.movementSpeed;
                for (int i = 1; i <= this.movementSpeed; i++)
                {
                    int newSpeed = this.movementSpeed - i;

                    this.movementSpeed = newSpeed;

                    if ((!Collision.BoundCollision(bound, this)) && (!Collision.ObjectCollision(collider, this)))
                    {
                        this.rectangle.Y -= newSpeed;
                        break;
                    }
                    this.movementSpeed = oldMoveSpeed;
                }
                this.movementSpeed = oldMoveSpeed;
            }
            animation(Orientation.Up,gt);
        }

        public void MoveDown(Rectangle bound, IEnumerable<GameObject> collider, GameTime gt)
        {
            this.orientation = Orientation.Down;
            if ((!Collision.BoundCollision(bound, this)) && (!Collision.ObjectCollision(collider, this)))
            {
                this.rectangle.Y += this.movementSpeed;
            }
            else
            {
                int oldMoveSpeed = this.movementSpeed;
                for (int i = 1; i <= this.movementSpeed; i++)
                {
                    int newSpeed = this.movementSpeed - i;

                    this.movementSpeed = newSpeed;

                    if ((!Collision.BoundCollision(bound, this)) && (!Collision.ObjectCollision(collider, this)))
                    {
                        this.rectangle.Y += newSpeed;
                        break;
                    }
                    this.movementSpeed = oldMoveSpeed;
                }
                this.movementSpeed = oldMoveSpeed;
            }
            animation(Orientation.Down, gt);
        }

        public void MoveLeft(Rectangle bound, IEnumerable<GameObject> collider, GameTime gt)
        {
            this.orientation = Orientation.Left;
            if ((!Collision.BoundCollision(bound, this)) && (!Collision.ObjectCollision(collider, this)))
            {
                this.rectangle.X -= this.movementSpeed;
            }
            else
            {
                int oldMoveSpeed = this.movementSpeed;
                for (int i = 1; i <= this.movementSpeed; i++)
                {
                    int newSpeed = this.movementSpeed - i;

                    this.movementSpeed = newSpeed;

                    if ((!Collision.BoundCollision(bound, this)) && (!Collision.ObjectCollision(collider, this)))
                    {
                        this.rectangle.X -= newSpeed;
                        break;
                    }
                    this.movementSpeed = oldMoveSpeed;
                }
                this.movementSpeed = oldMoveSpeed;
            }
            animation(Orientation.Left,gt);
        }

        public void MoveRight(Rectangle bound, IEnumerable<GameObject> collider, GameTime gt)
        {
            this.orientation = Orientation.Right;
            if ((!Collision.BoundCollision(bound, this)) && (!Collision.ObjectCollision(collider, this)))
            {
                this.rectangle.X += this.movementSpeed;
            }
            else
            {
                int oldMoveSpeed = this.movementSpeed;
                for (int i = 0; i <= this.movementSpeed; i++)
                {
                    int newSpeed = this.movementSpeed - i;

                    this.movementSpeed = newSpeed;

                    if ((!Collision.BoundCollision(bound, this)) && (!Collision.ObjectCollision(collider, this)))
                    {
                        this.rectangle.X += newSpeed;
                        break;
                    }
                    this.movementSpeed = oldMoveSpeed;
                 
                }
                this.movementSpeed = oldMoveSpeed;
            }

            animation(Orientation.Right, gt);
        }

        public void MoveIdle(GameTime gt)
        {
            animation(Orientation.Idle, gt);
        }


        public void Move(GameTime gt, KeyboardState kbstate, Rectangle mainFrame, IEnumerable<GameObject> GObjects)
        {
            if (KeyDown != null && KeyLeft != null && KeyRight != null && KeyUp != null)
            {

                if (kbstate.IsKeyDown(KeyUp))
                {
                    this.MoveUp(mainFrame, GObjects,gt);
                }
                else if (kbstate.IsKeyDown(KeyLeft))
                {
                    this.MoveLeft(mainFrame, GObjects,gt);
                }
                else if (kbstate.IsKeyDown(KeyDown))
                {
                    this.MoveDown(mainFrame, GObjects,gt);
                }
                else if (kbstate.IsKeyDown(KeyRight))
                {
                    this.MoveRight(mainFrame, GObjects,gt);
                }
                else
                {
                    this.MoveIdle(gt);
                }
            }

        }

        public virtual void animation(Orientation ori, GameTime gt)
        {
        }
    }
}
