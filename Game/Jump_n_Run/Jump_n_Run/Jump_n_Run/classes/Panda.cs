﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace Jump_n_Run.classes
{
    class Panda : Enemy
    {
        // fields
        private Texture2D pandaImgRun, pandaImgJump, pandaImgStand, pandaImgDead;
       // private Vector2 pandaPosition;
        private Rectangle pandaRectIdle;
        private Rectangle[] pandaRectsRun;
        private Rectangle[] pandaRectsJump;
        private Rectangle[] pandaRectsDead;
        private Rectangle renderRect;
        private SpriteEffects pandaDirection = SpriteEffects.None;
        private int pandaTime;
        private const int pandaImgChangeTimeRun = 80;
        private const int pandaImgChangeTimeJump = 80;
        private const int pandaImgChangeTimeDead = 80;
        private int pandaImgIndexRun = 0;
        private int pandaImgIndexJump = 0;
        private int pandaImgIndexDead = 0;
        private Orientation oldOrientation;

        Color drawColor = Color.White;

        // ctors
        public Panda() : this(0,0,null,new Rectangle(0,0,0,0),0,null, null) { }
        public Panda(int moveSpeed, int gravity, Texture2D texture, Rectangle rect, int jump, Texture2D pandaImgRun, Texture2D pandaImgJump)
            : base(moveSpeed, gravity, texture, rect, jump) 
        {
            this.pandaImgRun = pandaImgRun;
            this.pandaImgJump = pandaImgJump;
        }


        public void loadPanda(GraphicsDeviceManager graphics, ContentManager Content)
        {
            pandaImgStand = Content.Load<Texture2D>("Images/gameobjects/PandaStand");
            pandaImgRun = Content.Load<Texture2D>("Images/gameobjects/PandaRunv1");      // ändern/weck
            pandaImgJump = Content.Load<Texture2D>("Images/gameobjects/PandaJumpv1");     // ändern/weck
            pandaImgDead = Content.Load<Texture2D>("Images/gameobjects/Panda_Dead_T");

            int startX = 0;
            int deltaX = 54;
            int deltaY = 58;
            int deltaD = 69;

            this.gravity = 10;

            pandaRectIdle = new Rectangle(0, 0, 63, 62);

            pandaRectsRun = new Rectangle[8];
            for (int i = 0; i < pandaRectsRun.Length; i++)
            {
                pandaRectsRun[i] = new Rectangle(startX + i * deltaX, 0, 54, 66);
            }

            pandaRectsJump = new Rectangle[4];
            for (int i = 0; i < pandaRectsJump.Length; i++)
            {
                pandaRectsJump[i] = new Rectangle(startX + i * deltaY, 0, 58, 60);
            }

            pandaRectsDead = new Rectangle[4];
            for (int i = 0; i < pandaRectsDead.Length; i++)
            {
                pandaRectsDead[i] = new Rectangle(startX + i * deltaD, 0, 69, 59);
            }

            oldOrientation = Orientation.Idle;

            Texture = pandaImgStand;

            this.movementSpeed = 7;
            this.jumpHeight = 30;
            this.rectangle = new Rectangle(200, 200, 60, 60);
            renderRect = pandaRectIdle;
        }

        public void CalculatePandaImgIndex(GameTime gametime, Orientation ori) 
        {
            pandaTime += gametime.ElapsedGameTime.Milliseconds;

            if (ori == Orientation.Right || ori == Orientation.Left) 
            {
                if (pandaTime >= pandaImgChangeTimeRun)
                {
                    pandaTime = 0;
                    pandaImgIndexRun++;
                    if (pandaImgIndexRun >= pandaRectsRun.Length)
                    {
                        pandaImgIndexRun = 0;
                    }

                    renderRect = pandaRectsRun[pandaImgIndexRun];
                }
            }

            if (ori == Orientation.Up || ori == Orientation.Down)
            {
                if (pandaTime >= pandaImgChangeTimeJump)
                {
                    pandaTime = 0;
                    pandaImgIndexJump++;
                    if (pandaImgIndexJump >= pandaRectsJump.Length)
                    {
                        pandaImgIndexJump = 0;
                    }

                    renderRect = pandaRectsJump[pandaImgIndexJump];
                }
            }

            if (health <= 0)
            {
                if (pandaTime >= pandaImgChangeTimeDead)
                {
                    pandaTime = 0;
                    pandaImgIndexDead++;
                    if (pandaImgIndexDead >= pandaRectsDead.Length)
                    {
                        pandaImgIndexDead = 0;
                    }

                    renderRect = pandaRectsDead[pandaImgIndexDead];
                }
            }
           
        }


        public  override void animation(Orientation ori, GameTime gt) 
        {
            if (ori == Orientation.Idle)
            {
                this.Texture = pandaImgStand;
                renderRect = pandaRectIdle;
                oldOrientation = Orientation.Idle;
            }

            if (ori == Orientation.Up)
            {
                if (oldOrientation == Orientation.Up)
                {
                    pandaDirection = SpriteEffects.None;
                    Texture = pandaImgJump;
                    CalculatePandaImgIndex(gt, Orientation.Up);
                }
                else if (oldOrientation == Orientation.Left)
                {
                    pandaDirection = SpriteEffects.FlipHorizontally;
                    Texture = pandaImgRun;
                    pandaImgIndexRun = 0;
                    CalculatePandaImgIndex(gt, Orientation.Up);
                }

                else
                {
                    pandaDirection = SpriteEffects.None;
                    Texture = pandaImgJump;
                    pandaImgIndexRun = 0;
                    CalculatePandaImgIndex(gt, Orientation.Up);
                }
                oldOrientation = Orientation.Up;
            }

            if (ori == Orientation.UpLeft)
            {
                if (oldOrientation == Orientation.UpLeft)
                {
                    pandaDirection = SpriteEffects.FlipHorizontally;
                    Texture = pandaImgJump;
                    CalculatePandaImgIndex(gt, Orientation.Up);
                }
                else
                {
                    pandaDirection = SpriteEffects.FlipHorizontally;
                    Texture = pandaImgJump;
                    pandaImgIndexRun = 0;
                    CalculatePandaImgIndex(gt, Orientation.Up);
                }
                oldOrientation = Orientation.UpLeft;
            }

            if (ori == Orientation.UpRight)
            {
                if (oldOrientation == Orientation.UpRight)
                {
                    pandaDirection = SpriteEffects.None;
                    Texture = pandaImgJump;
                    CalculatePandaImgIndex(gt, Orientation.Up);
                }
                else
                {
                    pandaDirection = SpriteEffects.None;
                    Texture = pandaImgJump;
                    pandaImgIndexRun = 0;
                    CalculatePandaImgIndex(gt, Orientation.Up);
                }
                oldOrientation = Orientation.UpRight;
            }

            if (ori == Orientation.Left)
            {
                if (oldOrientation == Orientation.Left)
                {
                    pandaDirection = SpriteEffects.FlipHorizontally;
                    Texture = pandaImgRun;
                    CalculatePandaImgIndex(gt, Orientation.Left);
                }
                else
                {
                    pandaDirection = SpriteEffects.FlipHorizontally;
                    Texture = pandaImgRun;
                    pandaImgIndexRun = 0;
                    CalculatePandaImgIndex(gt, Orientation.Left);
                }
                oldOrientation = Orientation.Left;
            }

            if (ori == Orientation.Right)
            {
                if (oldOrientation == Orientation.Right)
                {
                    pandaDirection = SpriteEffects.None;
                    Texture = pandaImgRun;
                    CalculatePandaImgIndex(gt, Orientation.Right);
                }
                else
                {
                    pandaDirection = SpriteEffects.None;
                    Texture = pandaImgRun;
                    pandaImgIndexRun = 0;
                    CalculatePandaImgIndex(gt, Orientation.Right);
                }
                oldOrientation = Orientation.Right;
            }

            if (ori == Orientation.Down)
            {
                this.Texture = pandaImgStand;
                renderRect = pandaRectIdle;

                oldOrientation = Orientation.Down;
            }

           
        }

        public override void Draw(ref SpriteBatch sb)
        {
            
            sb.Draw(Texture, rectangle, renderRect, drawColor, 0.0f, new Vector2(0.0f, 0.0f), pandaDirection, 0.0f);
        }


    }
}
