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
    class Panda : Enemy
    {
        // fields
        private Texture2D pandaImgRun, pandaImgJump;
       // private Vector2 pandaPosition;
        private Rectangle[] pandaRectsRun;
        private Rectangle[] pandaRectsJump;


        // ctors
        public Panda(int moveSpeed, int gravity, Texture2D texture, Rectangle rect, int jump, Texture2D pandaImgRun, Texture2D pandaImgJump)
            : base(moveSpeed, gravity, texture, rect, jump) 
        {
            this.pandaImgRun = pandaImgRun;
            this.pandaImgJump = pandaImgJump;

            Texture = this.pandaImgRun;
        }
        public void loadPanda(GraphicsDeviceManager graphics, ContentManager Content)
        {
            pandaImgRun = Content.Load<Texture2D>("Images/gameobjects/PandaRunv1");      // ändern/weck
            pandaImgJump = Content.Load<Texture2D>("Images/gameobjects/PandaJumpv1");     // ändern/weck

            int startX = 0;
            int deltaX = 31;

            this.gravity = 10;

            pandaRectsRun = new Rectangle[8];
            for (int i = 0; i < pandaRectsRun.Length; i++)
            {
                pandaRectsRun[i] = new Rectangle(startX + i * deltaX, 0, 54, 66);
            }

            pandaRectsJump = new Rectangle[4];
            for (int i = 0; i < pandaRectsJump.Length; i++)
            {
                pandaRectsJump[i] = new Rectangle(startX + i * deltaX, 0, 58, 60);
            }
        
        }
        


   



    }
}
