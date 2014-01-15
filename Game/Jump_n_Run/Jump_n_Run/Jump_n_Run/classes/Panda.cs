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

        public void PandaRun() 
        {
            // pandaImgRun = content.Load<Texture2D>("Images/gameobjects/Runv2");      // ändern/weck
            // pandaImgJump = content.Load<Texture2D>("Images/gameobjects/Runv2");     // ändern/weck


            pandaRectsRun = new Rectangle[8];
            for (int i = 0; i < pandaRectsRun.Length; i++)
            {
                pandaRectsRun[i] = new Rectangle(100, 100, 54, 66);
            }
        }

        public void PandaJump() 
        {
            pandaRectsJump = new Rectangle[4];
            for (int i = 0; i < pandaRectsJump.Length; i++)
            {
                pandaRectsJump[i] = new Rectangle(100, 100, 58, 60);
            }

        }



    }
}
