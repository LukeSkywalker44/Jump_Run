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
using System.Diagnostics;
using Jump_n_Run.classes;
using X2DPE;
using X2DPE.Helpers;
using System.Threading.Tasks;

namespace Jump_n_Run.classes
{
    class Enemy : MoveableObject
    {
        Random rand;
        int lastMoveInt = 0;
        int successive = 0;
        int successiveMAX = 60;
        int jumpBase = 800;
        public bool gnd;
        public Emitter emitter;
        public int health = 100;
        public bool dead = false;
        TimeSpan lastMoveKI = new TimeSpan();

        




        public Enemy(int moveSpeed, int gravity, Texture2D texture, Rectangle rect, int jump) : base(moveSpeed, gravity, texture, rect, jump) { rand = new Random(); }


        public void KI_Movement(Rectangle bound, IEnumerable<GameObject> colliders, GameTime gt, List<Particle> particles, ref ParticleComponent pcomponent, Emitter Emitter)
        {

            int CollisionReturn = Collision.ParticleCollision(this, particles);

            if (CollisionReturn > 0)
            {
                pcomponent.particleEmitterList[0].ParticleList.RemoveAt(CollisionReturn);

                this.health -= pcomponent.particleEmitterList.First().Damage;

                if (health <= 0)
                {
                    dead = true;
                }

                if (emitter == null)
                {
                    emitter = Emitter;
                    this.emitter.Active = true;
                    this.emitter.noEmit = false;
                    this.emitter.Position = new Vector2(this.rectangle.Center.X, this.rectangle.Center.Y);
                    pcomponent.particleEmitterList.Add(this.emitter);
                   
                }
                else
                {
                    if(pcomponent.particleEmitterList.Contains(emitter))
                    {
                    pcomponent.particleEmitterList[pcomponent.particleEmitterList.IndexOf(emitter)].noEmit = false;
                    emitter.noEmit = false;
                    pcomponent.particleEmitterList[pcomponent.particleEmitterList.IndexOf(emitter)].Position = new Vector2(this.rectangle.Center.X, this.rectangle.Center.Y);
                    emitter.Position = new Vector2(this.rectangle.Center.X, this.rectangle.Center.Y);
                    }
                }

            }
            else
            {
                if (emitter != null)
                {
                    if (pcomponent.particleEmitterList.Contains(emitter))
                    {

                        pcomponent.particleEmitterList[pcomponent.particleEmitterList.IndexOf(emitter)].noEmit = true;
                        
                    }
                    emitter = null;
                }
            }


            if ((gt.TotalGameTime - lastMoveKI) >= new TimeSpan(0, 0, 0, 0, 17))
            {

                lastMoveKI = gt.TotalGameTime;



                int movement = rand.Next(0, 6);

                //movement = 3;

                this.gnd = this.GroundCollision(bound, colliders);

                if (gnd) jumpBase = this.rectangle.Bottom;
                int height = this.rectangle.Bottom;
                int maxHeight = jumpBase + jumpHeight;

                if (successive < successiveMAX)
                {

                    if (lastMoveInt == 0)
                    {
                        this.MoveIdle(bound, colliders, gt);
                        successiveMAX = 60;
                        lastMoveInt = 0;
                    }
                    else if (lastMoveInt == 1)
                    {
                        this.MoveLeft(bound, colliders, gt);
                        successiveMAX = 60;
                        lastMoveInt = 1;
                    }
                    else if (lastMoveInt == 2)
                    {
                        this.MoveRight(bound, colliders, gt);
                        successiveMAX = 60;
                        lastMoveInt = 2;
                    }
                    else if (lastMoveInt == 3)
                    {
                        this.MoveUp(bound, colliders, gt);
                        successiveMAX = 10;
                        lastMoveInt = 3;
                    }
                    else if (lastMoveInt == 4)
                    {
                        this.MoveUpLeft(bound, colliders, gt);
                        successiveMAX = 10;
                        lastMoveInt = 4;
                    }
                    else if (lastMoveInt == 5)
                    {
                        this.MoveUpRight(bound, colliders, gt);
                        successiveMAX = 10;
                        lastMoveInt = 5;
                    }
                    successive++;
                }
                else
                {
                    successive = 0;
                    if (movement == 0)
                    {
                        this.MoveIdle(bound, colliders, gt);
                        lastMoveInt = 0;
                    }
                    else if (movement == 1)
                    {
                        this.MoveLeft(bound, colliders, gt);
                        lastMoveInt = 1;
                    }
                    else if (movement == 2)
                    {
                        this.MoveRight(bound, colliders, gt);
                        lastMoveInt = 2;
                    }
                    else if (movement == 3)
                    {

                        if (height <= maxHeight)
                        {
                            this.MoveUp(bound, colliders, gt);
                            successiveMAX = 10;
                            lastMoveInt = 3;
                            gnd = false;




                        }

                    }
                    else if (movement == 4)
                    {

                        if (height <= maxHeight)
                        {
                            this.MoveUpLeft(bound, colliders, gt);
                            successiveMAX = 10;
                            lastMoveInt = 4;
                            gnd = false;



                        }

                    }
                    else if (movement == 5)
                    {

                        if (height <= maxHeight)
                        {
                            this.MoveUpRight(bound, colliders, gt);
                            successiveMAX = 10;
                            lastMoveInt = 5;
                            gnd = false;



                        }

                    }


                }
            }
             


        }

        protected bool GroundCollision(Rectangle bound, IEnumerable<GameObject> colliders)
        {
            Orientation oldori = this.orientation;
            this.orientation = Orientation.Down;
            bool gndCollide = (Collision.ObjectCollision(colliders, this));
            this.orientation = oldori;
            return gndCollide;
        }

        protected override void MoveUp(Rectangle bound, IEnumerable<GameObject> collider, GameTime gt)
        {
            lastMove = new TimeSpan();
            bool moved = false;
                this.orientation = Orientation.Up;
                if ((!Collision.BoundCollision(bound, this)) && (!Collision.ObjectCollision(collider, this)))
                {
                    

                    if ((gt.TotalGameTime - lastMove) >= new TimeSpan(0, 0, 0, 0, 17))
                    {

                        moved = true;
                        this.rectangle.Y -= this.movementSpeed * 2;
                    }
                }
                else
                {
                    int oldMoveSpeed = this.movementSpeed;
                    for (int i = 1; i <= this.movementSpeed; i++)
                    {
                        int newSpeed = this.movementSpeed - i;

                        this.movementSpeed = newSpeed;

                        if ((!Collision.BoundCollision(bound, this)) && (!Collision.ObjectCollision(collider, this)))
                        {
                            

                            if ((gt.TotalGameTime - lastMove) >= new TimeSpan(0, 0, 0, 0, 17))
                            {

                                moved = true;
                                this.rectangle.Y -= newSpeed;
                            }


                            break;
                        }
                        else
                        {
                            animation(Orientation.Idle, gt);
                            jumping = false;
                            break;
                        }

                        this.movementSpeed = oldMoveSpeed;


                    }
                    this.movementSpeed = oldMoveSpeed;
                }


                animation(Orientation.Up, gt);

                if (moved) lastMove = gt.TotalGameTime;
            
               
        }
        protected override void MoveUpLeft(Rectangle bound, IEnumerable<GameObject> collider, GameTime gt)
        {
            lastMove = new TimeSpan();
            bool moved = false;
            this.orientation = Orientation.Up;
            if ((!Collision.BoundCollision(bound, this)) && (!Collision.ObjectCollision(collider, this)))
            {
               

                if ((gt.TotalGameTime - lastMove) >= new TimeSpan(0, 0, 0, 0, 17))
                {

                    moved = true;
                    this.rectangle.Y -= this.movementSpeed;
                }

            }
            else
            {
                int oldMoveSpeed = this.movementSpeed;
                for (int i = 1; i <= this.movementSpeed; i++)
                {
                    int newSpeed = this.movementSpeed - i;

                    this.movementSpeed = newSpeed;

                    if ((!Collision.BoundCollision(bound, this)) && (!Collision.ObjectCollision(collider, this)))
                    {
                        

                        if ((gt.TotalGameTime - lastMove) >= new TimeSpan(0, 0, 0, 0, 17))
                        {

                            moved = true;
                            this.rectangle.Y -= newSpeed;
                        }


                        break;
                    }
                    else
                    {
                        animation(Orientation.Idle, gt);
                        jumping = false;
                        this.MoveDown(bound, collider, gt);
                        break;
                    }
                    this.movementSpeed = oldMoveSpeed;


                }
                this.movementSpeed = oldMoveSpeed;
            }

            this.orientation = Orientation.Left;
            if ((!Collision.BoundCollision(bound, this)) && (!Collision.ObjectCollision(collider, this)))
            {
                

                if ((gt.TotalGameTime - lastMove) >= new TimeSpan(0, 0, 0, 0, 17))
                {

                    moved = true;
                    this.rectangle.X -= this.movementSpeed;
                }
            }
            else
            {
                int oldMoveSpeed = this.movementSpeed;
                for (int i = 1; i <= this.movementSpeed; i++)
                {
                    int newSpeed = this.movementSpeed - i;

                    this.movementSpeed = newSpeed;

                    if ((!Collision.BoundCollision(bound, this)) && (!Collision.ObjectCollision(collider, this)))
                    {
                        


                        if ((gt.TotalGameTime - lastMove) >= new TimeSpan(0, 0, 0, 0, 17))
                        {

                            moved = true;
                            this.rectangle.X -= newSpeed;
                        }


                        break;
                    }
                    this.movementSpeed = oldMoveSpeed;
                }
                this.movementSpeed = oldMoveSpeed;
            }

            if (moved) lastMove = gt.TotalGameTime;
        }
        protected override void MoveUpRight(Rectangle bound, IEnumerable<GameObject> collider, GameTime gt)
        {
            lastMove = new TimeSpan();
            bool moved = false;
            this.orientation = Orientation.Up;
            if ((!Collision.BoundCollision(bound, this)) && (!Collision.ObjectCollision(collider, this)))
            {
                

                if ((gt.TotalGameTime - lastMove) >= new TimeSpan(0, 0, 0, 0, 17))
                {

                    moved = true;
                    this.rectangle.Y -= this.movementSpeed;
                }
            }
            else
            {
                int oldMoveSpeed = this.movementSpeed;
                for (int i = 1; i <= this.movementSpeed; i++)
                {
                    int newSpeed = this.movementSpeed - i;

                    this.movementSpeed = newSpeed;

                    if ((!Collision.BoundCollision(bound, this)) && (!Collision.ObjectCollision(collider, this)))
                    {
                       

                        if ((gt.TotalGameTime - lastMove) >= new TimeSpan(0, 0, 0, 0, 17))
                        {

                            moved = true;
                            this.rectangle.Y -= newSpeed;
                        }


                        break;
                    }
                    else
                    {
                        animation(Orientation.Idle, gt);
                        jumping = false;
                        this.MoveDown(bound, collider, gt);
                        break;
                    }
                    this.movementSpeed = oldMoveSpeed;


                }
                this.movementSpeed = oldMoveSpeed;
            }

            this.orientation = Orientation.Right;
            if ((!Collision.BoundCollision(bound, this)) && (!Collision.ObjectCollision(collider, this)))
            {
                

                if ((gt.TotalGameTime - lastMove) >= new TimeSpan(0, 0, 0, 0, 17))
                {

                    moved = true;
                    this.rectangle.X += this.movementSpeed;
                }

            }
            else
            {
                int oldMoveSpeed = this.movementSpeed;
                for (int i = 1; i <= this.movementSpeed; i++)
                {
                    int newSpeed = this.movementSpeed - i;

                    this.movementSpeed = newSpeed;

                    if ((!Collision.BoundCollision(bound, this)) && (!Collision.ObjectCollision(collider, this)))
                    {
                        


                        if ((gt.TotalGameTime - lastMove) >= new TimeSpan(0, 0, 0, 0, 17))
                        {

                            moved = true;
                            this.rectangle.X += newSpeed;
                        }

                        break;
                    }
                    this.movementSpeed = oldMoveSpeed;
                }
                this.movementSpeed = oldMoveSpeed;
            }

            if (moved) lastMove = gt.TotalGameTime;
        }
        protected override void MoveIdle(Rectangle bound, IEnumerable<GameObject> collider, GameTime gt)
        {
            lastMove = new TimeSpan();
            bool moved = false;
            this.orientation = Orientation.Down;
            if ((!Collision.BoundCollision(bound, this)) && (!Collision.ObjectCollision(collider, this)))
            {
                this.rectangle.Y += this.gravity;

                if ((gt.TotalGameTime - lastMove) >= new TimeSpan(0, 0, 0, 0, 17))
                {

                    moved = true;
                    this.rectangle.Y += this.gravity;
                }
               
            }
            else
            {
                int oldMoveSpeed = this.gravity;
                for (int i = 1; i <= this.gravity; i++)
                {
                    int newSpeed = this.gravity - i;

                    this.gravity = newSpeed;

                    if ((!Collision.BoundCollision(bound, this)) && (!Collision.ObjectCollision(collider, this)))
                    {
                        

                        if ((gt.TotalGameTime - lastMove) >= new TimeSpan(0, 0, 0, 0, 17))
                        {

                            moved = true;
                            this.rectangle.Y += newSpeed;
                        }
                        
                        break;
                    }
                    else
                    {
                        jumpRelative = this.rectangle.Bottom;
                        

                    }
                    this.gravity = oldMoveSpeed;
                }
                
                this.gravity = oldMoveSpeed;
            }


            this.orientation = Orientation.Idle;

            if (moved) lastMove = gt.TotalGameTime;

        }
        protected override void MoveDown(Rectangle bound, IEnumerable<GameObject> collider, GameTime gt)
        {
        }
        protected override void MoveLeft(Rectangle bound, IEnumerable<GameObject> collider, GameTime gt)
        {
            lastMove = new TimeSpan();
            bool moved = false;
            this.orientation = Orientation.Left;
            if ((!Collision.BoundCollision(bound, this)) && (!Collision.ObjectCollision(collider, this)))
            {
                

                if ((gt.TotalGameTime - lastMove) >= new TimeSpan(0, 0, 0, 0, 17))
                {

                    moved = true;
                    this.rectangle.X -= this.movementSpeed;
                }
            }
            else
            {
                int oldMoveSpeed = this.movementSpeed;
                for (int i = 1; i <= this.movementSpeed; i++)
                {
                    int newSpeed = this.movementSpeed - i;

                    this.movementSpeed = newSpeed;

                    if ((!Collision.BoundCollision(bound, this)) && (!Collision.ObjectCollision(collider, this)))
                    {
                        


                        if ((gt.TotalGameTime - lastMove) >= new TimeSpan(0, 0, 0, 0, 17))
                        {

                            moved = true;
                            this.rectangle.X -= newSpeed;
                        }

                        break;
                    }
                    this.movementSpeed = oldMoveSpeed;
                }
                this.movementSpeed = oldMoveSpeed;
            }

            this.orientation = Orientation.Down;
            if ((!Collision.BoundCollision(bound, this)) && (!Collision.ObjectCollision(collider, this)))
            {
               

                if ((gt.TotalGameTime - lastMove) >= new TimeSpan(0, 0, 0, 0, 17))
                {

                    moved = true;
                    this.rectangle.Y += this.gravity;
                }
               
            }
            else
            {
                int oldMoveSpeed = this.gravity;
                for (int i = 1; i <= this.gravity; i++)
                {
                    int newSpeed = this.gravity - i;

                    this.gravity = newSpeed;

                    if ((!Collision.BoundCollision(bound, this)) && (!Collision.ObjectCollision(collider, this)))
                    {
                       

                        if ((gt.TotalGameTime - lastMove) >= new TimeSpan(0, 0, 0, 0, 17))
                        {

                            moved = true;
                            this.rectangle.Y += newSpeed;
                        }


                        break;
                    }
                   
                    this.gravity = oldMoveSpeed;

                }
                this.gravity = oldMoveSpeed;
               
            }

            animation(Orientation.Left, gt);

            if (moved) lastMove = gt.TotalGameTime;

        }
        protected override void MoveRight(Rectangle bound, IEnumerable<GameObject> collider, GameTime gt)
        {
            lastMove = new TimeSpan();
            bool moved = false;
            this.orientation = Orientation.Right;
            if ((!Collision.BoundCollision(bound, this)) && (!Collision.ObjectCollision(collider, this)))
            {
                

                if ((gt.TotalGameTime - lastMove) >= new TimeSpan(0, 0, 0, 0, 17))
                {

                    moved = true;
                    this.rectangle.X += this.movementSpeed;
                }
            }
            else
            {
                int oldMoveSpeed = this.movementSpeed;
                for (int i = 1; i <= this.movementSpeed; i++)
                {
                    int newSpeed = this.movementSpeed - i;

                    this.movementSpeed = newSpeed;

                    if ((!Collision.BoundCollision(bound, this)) && (!Collision.ObjectCollision(collider, this)))
                    {
                       

                        if ((gt.TotalGameTime - lastMove) >= new TimeSpan(0, 0, 0, 0, 17))
                        {

                            moved = true;
                            this.rectangle.X += newSpeed;
                        }

                        break;
                    }
                    this.movementSpeed = oldMoveSpeed;
                }
                this.movementSpeed = oldMoveSpeed;
            }

            this.orientation = Orientation.Down;
            if ((!Collision.BoundCollision(bound, this)) && (!Collision.ObjectCollision(collider, this)))
            {
                

                if ((gt.TotalGameTime - lastMove) >= new TimeSpan(0, 0, 0, 0, 17))
                {

                    moved = true;
                    this.rectangle.Y += this.gravity;
                }

            }
            else
            {
                int oldMoveSpeed = this.gravity;
                for (int i = 1; i <= this.gravity; i++)
                {
                    int newSpeed = this.gravity - i;

                    this.gravity = newSpeed;

                    if ((!Collision.BoundCollision(bound, this)) && (!Collision.ObjectCollision(collider, this)))
                    {
                       

                        if ((gt.TotalGameTime - lastMove) >= new TimeSpan(0, 0, 0, 0, 17))
                        {

                            moved = true;
                            this.rectangle.Y += newSpeed;
                        }

                        break;
                    }

                    this.gravity = oldMoveSpeed;

                }
                this.gravity = oldMoveSpeed;

            }

            animation(Orientation.Right, gt);

            if (moved) lastMove = gt.TotalGameTime;
        }
        public override void animation(Orientation ori, GameTime gt)
        {
           
        }

       
    }
}
