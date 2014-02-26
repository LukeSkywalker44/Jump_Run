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
    class Items : GameObject
    {
        // für Schlüssel, Blöcke, usw.

    
        
        // ctors
        public Items() : this(null, new Rectangle()) { }
        public Items(Texture2D texture, Rectangle rect)
            :base(texture, rect)
        {
            this.Texture = texture;
            this.rectangle = rect;
        }

    }

    class Key : Items
    {
        public Key() : this(null, new Rectangle()) { }
        public Key(Texture2D texture, Rectangle rect)
            :base(texture, rect)
        {
            this.Texture = texture;
            this.rectangle = rect;
        }
    }

    class KeyHole : Items
    {
        public float transperency = 1.0f;
        public KeyHole() : this(null, new Rectangle()) { }
        public KeyHole(Texture2D texture, Rectangle rect)
            :base(texture, rect)
        {
            this.Texture = texture;
            this.rectangle = rect;
        }

        public override void Draw(ref SpriteBatch sb)
        {
            sb.Draw(this.Texture, this.rectangle, Color.White * transperency);
        }
    }

    class GunItem : Items 
    {
        public SpriteEffects se = SpriteEffects.None;
        public float rotation = 0.0f;
        public GunItem() : this(null, new Rectangle()) { }
        public GunItem(Texture2D texture, Rectangle rect)
            : base(texture, rect) 
        {
            this.Texture = texture;
            this.rectangle = rect;
        }

        public override void Draw(ref SpriteBatch sb)
        {

            sb.Draw(this.Texture, new Vector2(this.rectangle.X,this.rectangle.Y), null, Color.White, rotation, new Vector2(0,0), 0.07f, se,0);
            
            
        }
    }

}
