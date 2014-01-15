using System;
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
    class Player : MoveableObject
    {
        //fields

        private Texture2D playerImgRun, playerImgStand, playerImgJump;
        private Vector2 playerPosition;
        private Rectangle[] playerRectsRun;
        private Rectangle[] playerRectsJump;
        private Rectangle playerRectIdle;
        private int playerImgIndexRun = 0;
        private int playerImgIndexJump = 0;
        private SpriteEffects playerDirection = SpriteEffects.None;
        private Orientation oldOrientation;
        private int playerTime;
        private const int playerImgChangeTimeRun = 80;
        private const int playerImgChangeTimeJump = 40;
        private Rectangle renderRect;
        private bool hasKey = false;
        private int keyCounter = 0;

        //ctors
        /*STATIC !!!!!!!!!*/public Player() : this(null, null, null, null, new Vector2(0, 0), null, 0, new Rectangle(),Keys.Up, Keys.Down, Keys.Left, Keys.Right,0,0) { } // static !!!!!!!!!!!!!!!!!!!!!!!!!!
        public Player(Texture2D playerRun, Texture2D playerStand, Texture2D playerActual, Texture2D playerJump, Vector2 position, Rectangle[] rects, int MoveSpeed, Rectangle rect, Keys keyUp, Keys keyDown, Keys keyLeft, Keys keyRight, int gravity, int jump)
            : base(MoveSpeed,gravity, playerActual, rect,jump)
        {
            this.playerImgRun = playerRun;
            this.playerImgStand = playerStand;
            this.Texture = playerActual;
            this.playerPosition = position;
            this.playerImgJump = playerJump;
            this.playerRectsRun = rects;
            this.playerTime = 0;
            this.KeyDown = keyDown;
            this.KeyLeft = keyLeft;
            this.KeyRight = keyRight;
            this.KeyUp = keyUp;
        }


        //methods

        public void loadPlayer(GraphicsDeviceManager graphics, ContentManager Content)
        {


            playerImgRun = Content.Load<Texture2D>("Images/gameobjects/Runv2");
            playerImgStand = Content.Load<Texture2D>("Images/gameobjects/Stand");
            playerImgJump = Content.Load<Texture2D>("Images/gameobjects/Jumpv3");

            int startX = 0;
            int deltaX = 31;
            playerPosition = new Vector2(504, graphics.PreferredBackBufferHeight - playerImgRun.Height - 15);

            this.gravity = 10;

            playerRectIdle = new Rectangle(0, 0, 31, 40);

            playerRectsRun = new Rectangle[12];
            for (int i = 0; i < playerRectsRun.Length; i++)
            {
                playerRectsRun[i] = new Rectangle(startX + i * deltaX, 0, 31, 40);

                
            }

            playerRectsJump = new Rectangle[11];
            for (int i = 0; i < playerRectsJump.Length; i++)
            {
                playerRectsJump[i] = new Rectangle(startX + i * deltaX, 0, 31, 45); // !!!!!!!!!!!!!!!!!!!!!!!!!!!


            }

            oldOrientation = Orientation.Idle;

            Texture = playerImgStand;
            this.rectangle = new Rectangle(300, 300,42, 50); //(300, 300, 42, 50);
            this.movementSpeed = 5;
            this.jumpHeight = 80;

        }
       

        private void CalculatePlayerImgIndex(GameTime gameTime,Orientation ori)
        {

            playerTime += gameTime.ElapsedGameTime.Milliseconds;

            if (ori == Orientation.Right || ori == Orientation.Left)
            {
                if (playerTime >= playerImgChangeTimeRun)
                {
                    playerTime = 0;
                    playerImgIndexRun++;
                    if (playerImgIndexRun >= playerRectsRun.Length)
                    {
                        playerImgIndexRun = 0;
                    }

                    renderRect = playerRectsRun[playerImgIndexRun];
                }

            }

            if (ori == Orientation.Up)
            {

                if (playerTime >= playerImgChangeTimeJump)
                {
                    playerTime = 0;
                    playerImgIndexJump++;
                    if (playerImgIndexJump >= playerRectsJump.Length)
                    {
                        playerImgIndexJump = 0;
                    }

                    renderRect = playerRectsJump[playerImgIndexJump];
                }

            }


           

         
        }

        public override void animation(Orientation ori, GameTime gt)
        {
            if (ori == Orientation.Idle)
            {
                // texture auf stand
                this.Texture = playerImgStand;
                renderRect = playerRectIdle;
                oldOrientation = Orientation.Idle;
            }

            if (ori == Orientation.Up)
            {
                if (oldOrientation == Orientation.Up)
                {
                    // animation weiterführen
                    playerDirection = SpriteEffects.None;

                    Texture = playerImgJump;

                    CalculatePlayerImgIndex(gt, Orientation.Up);

                }
                else if (oldOrientation == Orientation.Left)
                {
                    // animation neu beginnen
                    playerDirection = SpriteEffects.FlipHorizontally;

                    Texture = playerImgJump;
                    playerImgIndexRun = 0;

                    CalculatePlayerImgIndex(gt, Orientation.Up);

                }

                else
                {
                    // animation neu beginnen
                    playerDirection = SpriteEffects.None;

                    Texture = playerImgJump;
                    playerImgIndexRun = 0;

                    CalculatePlayerImgIndex(gt, Orientation.Up);

                }


                oldOrientation = Orientation.Up;
            }

            if (ori == Orientation.UpLeft)
            {
                if (oldOrientation == Orientation.UpLeft)
                {
                    // animation weiterführen
                    playerDirection = SpriteEffects.FlipHorizontally;

                    Texture = playerImgJump;

                    CalculatePlayerImgIndex(gt, Orientation.Up);

                }
                else
                {
                    // animation neu beginnen
                    playerDirection = SpriteEffects.FlipHorizontally;

                    Texture = playerImgJump;
                    playerImgIndexRun = 0;

                    CalculatePlayerImgIndex(gt, Orientation.Up);

                }


                oldOrientation = Orientation.UpLeft;
            }

            if (ori == Orientation.UpRight)
            {
                if (oldOrientation == Orientation.UpRight)
                {
                    // animation weiterführen
                    playerDirection = SpriteEffects.None;

                    Texture = playerImgJump;

                    CalculatePlayerImgIndex(gt, Orientation.Up);

                }
                else
                {
                    // animation neu beginnen
                    playerDirection = SpriteEffects.None;

                    Texture = playerImgJump;
                    playerImgIndexRun = 0;

                    CalculatePlayerImgIndex(gt, Orientation.Up);

                }


                oldOrientation = Orientation.UpRight;
            }

            if (ori == Orientation.Left)
            {
                if (oldOrientation == Orientation.Left)
                {
                    // animation weiterführen
                    playerDirection = SpriteEffects.FlipHorizontally;

                    Texture = playerImgRun;

                    CalculatePlayerImgIndex(gt, Orientation.Left);

                }
                else
                {
                    // animation neu beginnen
                    playerDirection = SpriteEffects.FlipHorizontally;

                    Texture = playerImgRun;
                    playerImgIndexRun = 0;

                    CalculatePlayerImgIndex(gt, Orientation.Left);

                }

                oldOrientation = Orientation.Left;
            }

            if (ori == Orientation.Right)
            {
                if (oldOrientation == Orientation.Right)
                {
                    // animation weiterführen
                    playerDirection = SpriteEffects.None;

                    Texture = playerImgRun;

                    CalculatePlayerImgIndex(gt,Orientation.Right);

                }
                else
                {
                    // animation neu beginnen
                    playerDirection = SpriteEffects.None;
                    Texture = playerImgRun;
                    playerImgIndexRun = 0;

                    CalculatePlayerImgIndex(gt, Orientation.Left);

                }

                oldOrientation = Orientation.Right;
            }

            if (ori == Orientation.Down)
            {
                this.Texture = playerImgStand;
                renderRect = playerRectIdle;

                oldOrientation = Orientation.Down;
            }



            //Animation nur jedes 3.mal ausführen (60 mal por sec. wäre zu schnell)
        }


        public override void Draw(ref SpriteBatch sb)
        {
            sb.Draw(Texture, rectangle, renderRect, Color.White,0.0f,new Vector2(0.0f,0.0f),playerDirection,0.0f);
        }

        public override  void ItemPickup( ref Items item)
        {
            if (item is Key)
            {
                this.hasKey = true;
                item.rectangle = new Rectangle(0, 0, 0, 0);
            }
            else if (item is KeyHole)
            {
                if (hasKey)
                {
                    float trans =  (300 - keyCounter) / 300.0f;

                    ((KeyHole)item).transperency = trans;

                    keyCounter++;

                    if (keyCounter == 300)
                    {
                        item.rectangle = new Rectangle(0, 0, 0, 0);
                        keyCounter = 0;
                    }

                
                }
                else
                {
                }
            }
        }
    }

}
