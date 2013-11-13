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

        private Rectangle[] playerRects;
        private int playerImgIndex = 0;

        private SpriteEffects playerDirection = SpriteEffects.None;

        private Orientation oldOrientation;

        private int playerTime;
        private const int playerImgChangeTime = 80;

        //ctors
        public Player() : this(null, null, null, null, new Vector2(0, 0), null, 0, new Rectangle(),Keys.Up, Keys.Down, Keys.Left, Keys.Right) { } // static !!!!!!!!!!!!!!!!!!!!!!!!!!
        public Player(Texture2D playerRun, Texture2D playerStand, Texture2D playerActual, Texture2D playerJump, Vector2 position, Rectangle[] rects, int MoveSpeed, Rectangle rect, Keys keyUp, Keys keyDown, Keys keyLeft, Keys keyRight)
            : base(MoveSpeed, playerActual, rect)
        {
            this.playerImgRun = playerRun;
            this.playerImgStand = playerStand;
            this.Texture = playerActual;
            this.playerPosition = position;
            this.playerImgJump = playerJump;
            this.playerRects = rects;
            this.playerTime = 0;
            this.KeyDown = keyDown;
            this.KeyLeft = keyLeft;
            this.KeyRight = keyRight;
            this.KeyUp = keyUp;
        }


        //methods

        public void loadPlayer(GraphicsDeviceManager graphics, ContentManager Content)
        {


            playerImgRun = Content.Load<Texture2D>("Images/gameobjects/Run_new");
            playerImgStand = Content.Load<Texture2D>("Images/gameobjects/Stand_new");
            playerImgJump = Content.Load<Texture2D>("Images/gameobjects/T_Jump");

            int startX = 0;
            int deltaX = 42;
            playerPosition = new Vector2(504, graphics.PreferredBackBufferHeight - playerImgRun.Height - 21);



            playerRects = new Rectangle[playerImgRun.Width / 50];
            for (int i = 0; i < playerRects.Length; i++)
            {
                playerRects[i] = new Rectangle(startX + i * deltaX, 0, 42, 50);
            }

            oldOrientation = Orientation.Idle;

            Texture = playerImgStand;
            this.rectangle = new Rectangle(300, 300, 42, 50);
            this.movementSpeed = 10;

        }
       

        private void CalculatePlayerImgIndex(GameTime gameTime)
        {
            playerTime += gameTime.ElapsedGameTime.Milliseconds;

            if (playerTime >= playerImgChangeTime)
            {
                playerTime = 0;
                playerImgIndex++;
                if (playerImgIndex >= playerRects.Length)
                {
                    playerImgIndex = 0;
                }
            }
        }

        public override void animation(Orientation ori, GameTime gt)
        {
            if (ori == Orientation.Idle)
            {
                // texture auf stand
                oldOrientation = Orientation.Idle;
            }

            if (ori == Orientation.Up)
            {
                if (oldOrientation == Orientation.Up)
                {
                    // animation weiterführen
                }
                else
                {
                    // animation neu beginnen
                }

                oldOrientation = Orientation.Up;
            }

            if (ori == Orientation.Left)
            {
                if (oldOrientation == Orientation.Left)
                {
                    // animation weiterführen
                    playerDirection = SpriteEffects.FlipHorizontally;



                }
                else
                {
                    // animation neu beginnen

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

                    CalculatePlayerImgIndex(gt);

                }
                else
                {
                    // animation neu beginnen

                }

                oldOrientation = Orientation.Right;
            }

            if (ori == Orientation.Down)
            {
                if (oldOrientation == Orientation.Down)
                {
                    // animation weiterführen
                }
                else
                {
                    // animation neu beginnen
                }

                oldOrientation = Orientation.Down;
            }



            //Animation nur jedes 3.mal ausführen (60 mal por sec. wäre zu schnell)
        }
    }

}
