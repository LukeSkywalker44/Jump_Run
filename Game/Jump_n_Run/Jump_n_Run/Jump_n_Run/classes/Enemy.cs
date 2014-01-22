﻿using System;
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
using System.Diagnostics;

namespace Jump_n_Run.classes
{
    class Enemy : MoveableObject
    {
        Random rand;
        int lastMove = 0;
        int successive = 0;
        int successiveMAX = 60;
        int jumpBase = 800;
        public bool gnd;
        public Enemy(int moveSpeed, int gravity, Texture2D texture, Rectangle rect, int jump) : base(moveSpeed, gravity, texture, rect, jump) { rand = new Random(); }


        public void KI_Movement(Rectangle bound, IEnumerable<GameObject> colliders, GameTime gt)
        {



            
            int movement = rand.Next(0, 6);

           

            

       

             this.gnd = this.GroundCollision(bound, colliders);

            if (gnd) jumpBase = this.rectangle.Bottom;
            int height = this.rectangle.Bottom;
            int maxHeight = jumpBase + jumpHeight;

            if (successive < successiveMAX)
            {

                    if (lastMove == 0)
                    {
                        this.MoveIdle(bound, colliders, gt);
                        successiveMAX = 60;
                        lastMove = 0;
                    }
                    else if (lastMove == 1)
                    {
                        this.MoveLeft(bound, colliders, gt);
                        successiveMAX = 60;
                        lastMove = 1;
                    }
                    else if (lastMove == 2)
                    {
                        this.MoveRight(bound, colliders, gt);
                        successiveMAX = 60;
                        lastMove = 2;
                    }
                    else if (lastMove == 3)
                    {
                        this.MoveUp(bound, colliders, gt);
                        successiveMAX = 10;
                        lastMove = 3;
                    }
                    else if (lastMove == 4)
                    {
                        this.MoveUpLeft(bound, colliders, gt);
                        successiveMAX = 10;
                        lastMove = 4;
                    }
                    else if (lastMove == 5)
                    {
                        this.MoveUpRight(bound, colliders, gt);
                        successiveMAX = 10;
                        lastMove = 5;
                    }
                    successive++;
            }
            else
            {
                successive = 0;
                if (movement == 0)
                {
                    this.MoveIdle(bound, colliders, gt);
                    lastMove = 0;
                }
                else if (movement == 1)
                {
                    this.MoveLeft(bound, colliders, gt);
                    lastMove = 1;
                }
                else if (movement == 2)
                {
                    this.MoveRight(bound, colliders, gt);
                    lastMove = 2;
                }
                else if (movement == 3)
                {
                    
                    if (height <= maxHeight)
                    {
                        this.MoveUp(bound, colliders, gt);
                        successiveMAX = 10;
                        lastMove = 3;
                        gnd = false;
                       
                       
                        
                        
                    }
                   
                }
                else if (movement == 4)
                {
                    
                    if (height <= maxHeight)
                    {
                        this.MoveUpLeft(bound, colliders, gt);
                        successiveMAX = 10;
                        lastMove = 4;
                        gnd = false;
                        
                      
                        
                    }
                    
                }
                else if (movement == 5)
                {
                    
                    if (height <= maxHeight)
                    {
                        this.MoveUpRight(bound, colliders, gt);
                        successiveMAX = 10;
                        lastMove = 5;
                        gnd = false;
                       
                      
                       
                    }
                   
                }
               
              
            }


        }

        protected bool GroundCollision(Rectangle bound, IEnumerable<GameObject> colliders)
        {
            Orientation oldori = this.orientation;
            this.orientation = Orientation.Down;
            bool gndCollide = (Collision.ObjectCollision(colliders, this));
            this.orientation = oldori;
            return gndCollide;
        }

        protected override void MoveUp(Rectangle bound, IEnumerable<GameObject> collider, GameTime gt)
        {

                this.orientation = Orientation.Up;
                if ((!Collision.BoundCollision(bound, this)) && (!Collision.ObjectCollision(collider, this)))
                {
                    this.rectangle.Y -= this.movementSpeed * 2;
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
                        else
                        {
                            animation(Orientation.Idle, gt);
                            jumping = false;
                            break;
                        }

                        this.movementSpeed = oldMoveSpeed;


                    }
                    this.movementSpeed = oldMoveSpeed;
                }


                animation(Orientation.Up, gt);
            
               
        }
        protected override void MoveUpLeft(Rectangle bound, IEnumerable<GameObject> collider, GameTime gt)
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
                    else
                    {
                        animation(Orientation.Idle, gt);
                        jumping = false;
                        this.MoveDown(bound, collider, gt);
                        break;
                    }
                    this.movementSpeed = oldMoveSpeed;


                }
                this.movementSpeed = oldMoveSpeed;
            }

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
        }
        protected override void MoveUpRight(Rectangle bound, IEnumerable<GameObject> collider, GameTime gt)
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
                    else
                    {
                        animation(Orientation.Idle, gt);
                        jumping = false;
                        this.MoveDown(bound, collider, gt);
                        break;
                    }
                    this.movementSpeed = oldMoveSpeed;


                }
                this.movementSpeed = oldMoveSpeed;
            }

            this.orientation = Orientation.Right;
            if ((!Collision.BoundCollision(bound, this)) && (!Collision.ObjectCollision(collider, this)))
            {
                this.rectangle.X += this.movementSpeed;
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
                        this.rectangle.X += newSpeed;
                        break;
                    }
                    this.movementSpeed = oldMoveSpeed;
                }
                this.movementSpeed = oldMoveSpeed;
            }
        }
        protected override void MoveIdle(Rectangle bound, IEnumerable<GameObject> collider, GameTime gt)
        {
            this.orientation = Orientation.Down;
            if ((!Collision.BoundCollision(bound, this)) && (!Collision.ObjectCollision(collider, this)))
            {
                this.rectangle.Y += this.gravity;
               
            }
            else
            {
                int oldMoveSpeed = this.gravity;
                for (int i = 1; i <= this.gravity; i++)
                {
                    int newSpeed = this.gravity - i;

                    this.gravity = newSpeed;

                    if ((!Collision.BoundCollision(bound, this)) && (!Collision.ObjectCollision(collider, this)))
                    {
                        this.rectangle.Y += newSpeed;
                        
                        break;
                    }
                    else
                    {
                        jumpRelative = this.rectangle.Bottom;
                        

                    }
                    this.gravity = oldMoveSpeed;
                }
                
                this.gravity = oldMoveSpeed;
            }

        }
        protected override void MoveDown(Rectangle bound, IEnumerable<GameObject> collider, GameTime gt)
        {
        }
        protected override void MoveLeft(Rectangle bound, IEnumerable<GameObject> collider, GameTime gt)
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

            this.orientation = Orientation.Down;
            if ((!Collision.BoundCollision(bound, this)) && (!Collision.ObjectCollision(collider, this)))
            {
                this.rectangle.Y += this.gravity;
               
            }
            else
            {
                int oldMoveSpeed = this.gravity;
                for (int i = 1; i <= this.gravity; i++)
                {
                    int newSpeed = this.gravity - i;

                    this.gravity = newSpeed;

                    if ((!Collision.BoundCollision(bound, this)) && (!Collision.ObjectCollision(collider, this)))
                    {
                        this.rectangle.Y += newSpeed;
                        break;
                    }
                   
                    this.gravity = oldMoveSpeed;

                }
                this.gravity = oldMoveSpeed;
               
            }

            animation(Orientation.Left, gt);

        }
        protected override void MoveRight(Rectangle bound, IEnumerable<GameObject> collider, GameTime gt)
        {
            this.orientation = Orientation.Right;
            if ((!Collision.BoundCollision(bound, this)) && (!Collision.ObjectCollision(collider, this)))
            {
                this.rectangle.X += this.movementSpeed;
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
                        this.rectangle.X += newSpeed;
                        break;
                    }
                    this.movementSpeed = oldMoveSpeed;
                }
                this.movementSpeed = oldMoveSpeed;
            }

            this.orientation = Orientation.Down;
            if ((!Collision.BoundCollision(bound, this)) && (!Collision.ObjectCollision(collider, this)))
            {
                this.rectangle.Y += this.gravity;

            }
            else
            {
                int oldMoveSpeed = this.gravity;
                for (int i = 1; i <= this.gravity; i++)
                {
                    int newSpeed = this.gravity - i;

                    this.gravity = newSpeed;

                    if ((!Collision.BoundCollision(bound, this)) && (!Collision.ObjectCollision(collider, this)))
                    {
                        this.rectangle.Y += newSpeed;
                        break;
                    }

                    this.gravity = oldMoveSpeed;

                }
                this.gravity = oldMoveSpeed;

            }

            animation(Orientation.Right, gt);
        }
    }
}
