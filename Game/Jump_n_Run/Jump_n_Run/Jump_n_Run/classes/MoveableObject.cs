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
    class MoveableObject
    {
        public int movementSpeed;
        public Texture2D Texture;
        public Rectangle rectangle;
       

        public MoveableObject(int moveSpeed, Texture2D texture, Rectangle rect)
        {
            this.movementSpeed = moveSpeed;
            this.Texture = texture;
            this.rectangle = rect;
           
        }
    }
}
