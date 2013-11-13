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

        public void MoveUp(Rectangle bound, GameObject collider)
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
            animation(Orientation.Up);
        }

        public void MoveDown(Rectangle bound, GameObject collider)
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
            animation(Orientation.Down);
        }

        public void MoveLeft(Rectangle bound, GameObject collider)
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
            animation(Orientation.Left);
        }

        public void MoveRight(Rectangle bound, GameObject collider)
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

            animation(Orientation.Right);
        }

        public void MoveIdle()
        {
        }

        private virtual void animation(Orientation ori)
        {
        }
    }
}
