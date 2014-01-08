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
    class Enemy : MoveableObject
    {
        Random rand;
        int lastMove = 0;
        int successive = 0;
        public Enemy(int moveSpeed, int gravity, Texture2D texture, Rectangle rect, int jump) : base(moveSpeed, gravity, texture, rect, jump) { rand = new Random(); }


        public void KI_Movement(Rectangle bound, IEnumerable<GameObject> colliders, GameTime gt)
        {
            int movement = rand.Next(0, 6);


            if (successive < 60)
            {

                    if (lastMove == 0)
                    {
                        this.MoveIdle(bound, colliders, gt);
                        lastMove = 0;
                    }
                    else if (lastMove == 1)
                    {
                        this.MoveLeft(bound, colliders, gt);
                        lastMove = 1;
                    }
                    else if (lastMove == 2)
                    {
                        this.MoveRight(bound, colliders, gt);
                        lastMove = 2;
                    }
                    else if (lastMove == 3)
                    {
                        this.MoveUp(bound, colliders, gt);
                        lastMove = 3;
                    }
                    else if (lastMove == 4)
                    {
                        this.MoveUpLeft(bound, colliders, gt);
                        lastMove = 3;
                    }
                    else if (lastMove == 5)
                    {
                        this.MoveUpRight(bound, colliders, gt);
                        lastMove = 3;
                    }
                    successive++;
            }
            else
            {
                successive = 0;
                if (movement == 0)
                {
                    this.MoveIdle(bound, colliders, gt);
                    lastMove = 0;
                }
                else if (movement == 1)
                {
                    this.MoveLeft(bound, colliders, gt);
                    lastMove = 1;
                }
                else if (movement == 2)
                {
                    this.MoveRight(bound, colliders, gt);
                    lastMove = 2;
                }
                else if (movement == 3)
                {
                    this.MoveUp(bound, colliders, gt);
                    lastMove = 3;
                }
                else if (movement == 4)
                {
                    this.MoveUpLeft(bound, colliders, gt);
                    lastMove = 3;
                }
                else if (movement == 4)
                {
                    this.MoveUpRight(bound, colliders, gt);
                    lastMove = 3;
                }
            }


        }

    }
}
