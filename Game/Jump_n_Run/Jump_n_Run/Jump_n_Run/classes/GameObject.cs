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
    class GameObject
    {
        public Texture2D Texture;
        public Rectangle rectangle;

        public GameObject(Texture2D tex, Rectangle rect)
        {
            this.Texture = tex;
            this.rectangle = rect;
        }

        public GameObject()
        {
        }

        public virtual void  Draw(ref SpriteBatch sb)
        {
            sb.Draw(this.Texture, this.rectangle, Color.White);
        }


    }
}
