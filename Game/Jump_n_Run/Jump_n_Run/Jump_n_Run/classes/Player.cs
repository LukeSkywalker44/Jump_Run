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
    class Player
    {
        //fields
        private Texture2D playerImgRun, playerImgStand, playerImgActual, playerImgJump;
        private Vector2 playerPosition;

        private Rectangle[] playerRects;
        private int playerImgIndex = 0;

        private SpriteEffects playerDirection = SpriteEffects.None;
       
        //ctors
        public Player() : this(null, null, null,null, new Vector2(0,0),null) { }
        public Player(Texture2D playerRun, Texture2D playerStand, Texture2D playerActual,Texture2D playerJump, Vector2 position, Rectangle[] rects) {
            this.playerImgRun = playerRun;
            this.playerImgStand = playerStand;
            this.playerImgActual = playerActual;
            this.playerPosition = position;
            this.playerImgJump = playerJump;
            this.playerRects = rects;
        }


        //methods

        public void loadPlayer(GraphicsDeviceManager graphics, ContentManager Content) {
            
            
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
            
        }
        public void DrawPlayer(SpriteBatch spriteBatch) {
            if (playerImgActual == playerImgStand)
            {
                spriteBatch.Draw(playerImgActual, playerPosition, Color.White);
            }
            else
            {
                //spriteBatch.Draw(playerImgActual, playerPosition, playerRects[playerImgIndex], Color.White, 0, Vector2.Zero, 1, playerDirection, 0);
            }
        
        }



    }
}
