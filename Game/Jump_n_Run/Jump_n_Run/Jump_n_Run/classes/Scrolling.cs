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
using Jump_n_Run.classes;

namespace Jump_n_Run.classes
{
    static class Scrolling
    {


        /// <summary>
        /// enables horizontal scrolling relative to the Player object
        /// </summary>
        /// <param name="player">the Player object</param>
        /// <param name="gobjects">the objects which will move</param>
        /// <param name="backgroundRenderFrame">the rectangle used for rendering the background</param>
        static public void Scroll(Player player,   IEnumerable<GameObject> gobjects, ref Rectangle backgroundRenderFrame, Rectangle mainframe)
        {
            if (player.rectangle.X > 700)
            {
                foreach (GameObject obj in gobjects)
                {
                    obj.rectangle.X -= player.movementSpeed;
                }

                backgroundRenderFrame.X -= player.movementSpeed;
            }

            if (player.rectangle.X < 300)
            {
                foreach (GameObject obj in gobjects)
                {
                    obj.rectangle.X += player.movementSpeed;
                }

                backgroundRenderFrame.X += player.movementSpeed;
            }
        }
    }
}
