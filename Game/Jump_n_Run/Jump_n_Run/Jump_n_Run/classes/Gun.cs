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
    class Gun : GameObject
    {
        public SpriteEffects se = SpriteEffects.None;
        public float rotation = 0.0f;
        public Gun() : this(null, new Rectangle()) { }
        public Gun(Texture2D texture, Rectangle rect)
            : base(texture, rect) 
        {
            this.Texture = texture;
            this.rectangle = rect;
        }

        public override void Draw(ref SpriteBatch sb)
        {

            sb.Draw(this.Texture, new Vector2(this.rectangle.X, this.rectangle.Y), null, Color.White, rotation, new Vector2(0,0), 0.07f, se, 0);
            
            
        }




    }
}
