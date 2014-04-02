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

    enum Orientation
    {
        Up, Down, Left, Right, Idle, UpLeft, UpRight
    }
    class MoveableObject : GameObject
    {
        public int movementSpeed;
        public Orientation orientation;
        protected Keys KeyUp = Keys.None;
        protected Keys KeyDown = Keys.None;
        protected Keys KeyLeft = Keys.None;
        protected Keys KeyRight = Keys.None;
        public int gravity;
        public bool jumping = false;
        public int jumpHeight;
        public int jumpRelative;
        protected int actualjumpHeight = 0;
        bool canJump;
        protected TimeSpan lastMove = new TimeSpan();
        protected Rectangle renderRect;

        


        public MoveableObject(int moveSpeed, int gravity, Texture2D texture, Rectangle rect, Keys keyUp, Keys keyDown, Keys keyLeft, Keys keyRight, int jump)
        {
            this.Texture = texture;
            this.rectangle = rect;
            this.movementSpeed = moveSpeed;
            this.orientation = Orientation.Idle;
            this.KeyDown = keyDown;
            this.KeyLeft = keyLeft;
            this.KeyRight = keyRight;
            this.KeyUp = keyUp;
            this.gravity = gravity;
            this.jumpHeight = jump;
        }


        public MoveableObject(int moveSpeed, int gravity, Texture2D texture, Rectangle rect, int jump)
        {
            this.Texture = texture;
            this.rectangle = rect;
            this.movementSpeed = moveSpeed;
            this.orientation = Orientation.Idle;
            this.gravity = gravity;
            this.jumpHeight = jump;

        }


        protected virtual void MoveUp(Rectangle bound, IEnumerable<GameObject> collider, GameTime gt)
        {
            bool moved = false;
            if (!jumping)
            {
                this.jumpRelative = this.rectangle.Bottom;
            }
          
            if (canJump)
            {
               
                // new Jump sequence
                if (!jumping)
                {
                    this.jumpRelative = this.rectangle.Bottom;
                }

                if (jumpRelative - jumpHeight < rectangle.Bottom)
                {


                    jumping = true;

                    this.orientation = Orientation.Up;
                    if ((!Collision.BoundCollision(bound, this)) && (!Collision.ObjectCollision(collider, this)))
                    {
                       
                        if ((gt.TotalGameTime - lastMove) >= new TimeSpan(0, 0, 0, 0, 17))
                        {
                            this.rectangle.Y -= this.movementSpeed;
                            moved = true;
                           
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
                                    this.rectangle.Y -= newSpeed;

                                    moved = true;
                                    
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


                    animation(Orientation.Up, gt);
                }
                else
                {
                    animation(Orientation.Idle, gt);
                    jumping = false;
                    this.MoveDown(bound, collider, gt);
                }

            }
            else
            {
                // gravity

                // Enemys brauchen keine Gravity, da die Korrektheit ihrer Bewegungungen
                // in der Enemy Klasse gesteuert wird

                if(!(this is Enemy))this.MoveDown(bound, collider, gt);
            }

            if (moved) lastMove = gt.TotalGameTime;
        }

        protected virtual void MoveUpLeft(Rectangle bound, IEnumerable<GameObject> collider, GameTime gt)
        {
            bool moved = false;
            if (!jumping)
            {
                this.jumpRelative = this.rectangle.Bottom;
            }
          
            if (canJump)
            {
               
                // new Jump sequence
                if (!jumping)
                {
                    this.jumpRelative = this.rectangle.Bottom;
                }

                if (jumpRelative - jumpHeight < rectangle.Bottom)
                {


                    jumping = true;

                    this.orientation = Orientation.Up;
                    if ((!Collision.BoundCollision(bound, this)) && (!Collision.ObjectCollision(collider, this)))
                    {
                        

                        if ((gt.TotalGameTime - lastMove) >= new TimeSpan(0, 0, 0, 0, 17))
                        {
                            this.rectangle.Y -= this.movementSpeed;
                            moved = true;
                           
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
                                    break;
                                }
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
                    this.orientation = Orientation.Up;

                    animation(Orientation.UpLeft, gt);
                }
                else
                {
                    jumping = false;
                    this.MoveDown(bound, collider, gt);
                }

            }
            else
            {
                // gravity
                this.orientation = Orientation.Down;
                if ((!Collision.BoundCollision(bound, this)) && (!Collision.ObjectCollision(collider, this)))
                {
                    
                    if (!(this is Enemy)) canJump = false;

                    if ((gt.TotalGameTime - lastMove) >= new TimeSpan(0, 0, 0, 0, 17))
                    {

                        moved = true;
                        this.rectangle.Y += this.gravity;
                        if (!(this is Enemy)) canJump = false;
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
                         
                            if (!(this is Enemy)) canJump = false;
                            

                            if ((gt.TotalGameTime - lastMove) >= new TimeSpan(0, 0, 0, 0, 17))
                            {

                                moved = true;
                                this.rectangle.Y += newSpeed;
                                if (!(this is Enemy)) canJump = false;
                                
                            }
                            break;

                        }
                        else
                        {
                            jumpRelative = this.rectangle.Bottom;
                            canJump = true;

                        }
                        this.gravity = oldMoveSpeed;
                    }
                    this.gravity = oldMoveSpeed;
                }
                animation(Orientation.Left, gt);

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
                                break;
                            }
                            break;
                        }
                        this.movementSpeed = oldMoveSpeed;
                    }
                    this.movementSpeed = oldMoveSpeed;
                }
            }
            if (moved) lastMove = gt.TotalGameTime;
        }

        protected virtual void MoveUpRight(Rectangle bound, IEnumerable<GameObject> collider, GameTime gt)
        {
            bool moved = false;
            if (!jumping)
            {
                this.jumpRelative = this.rectangle.Bottom;
            }
          
            if (canJump)
            {
               
                // new Jump sequence
                if (!jumping)
                {
                    this.jumpRelative = this.rectangle.Bottom;
                }

                if (jumpRelative - jumpHeight < rectangle.Bottom)
                {


                    jumping = true;

                    this.orientation = Orientation.Up;
                    if ((!Collision.BoundCollision(bound, this)) && (!Collision.ObjectCollision(collider, this)))
                    {
                        if((gt.TotalGameTime-lastMove) >= new TimeSpan(0,0,0,0,17))
                        {
                          this.rectangle.Y -= this.movementSpeed;
                          moved = true;
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
                                if((gt.TotalGameTime-lastMove) >= new TimeSpan(0,0,0,0,17))
                        {
                                this.rectangle.Y -= newSpeed;

                                moved = true;
                                break;
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
                        if((gt.TotalGameTime-lastMove) >= new TimeSpan(0,0,0,0,17))
                        {
                            this.rectangle.X += this.movementSpeed;
                            moved = true;
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
                                    break;
                                }
                                break;
                            }
                            this.movementSpeed = oldMoveSpeed;
                        }
                        this.movementSpeed = oldMoveSpeed;
                    }
                    this.orientation = Orientation.Up;

                    animation(Orientation.UpRight, gt);
                }
                else
                {
                    jumping = false;
                    this.MoveDown(bound, collider, gt);
                }

            }
            else
            {
                // gravity
                this.orientation = Orientation.Down;
                if ((!Collision.BoundCollision(bound, this)) && (!Collision.ObjectCollision(collider, this)))
                {


                    if (!(this is Enemy)) canJump = false;
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


                            if (!(this is Enemy)) canJump = false;
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
                            canJump = true;

                        }
                        this.gravity = oldMoveSpeed;
                    }
                    this.gravity = oldMoveSpeed;
                }
                animation(Orientation.Right, gt);

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
                                break;

                            }
                            break;
                           
                        }
                        this.movementSpeed = oldMoveSpeed;
                    }
                    this.movementSpeed = oldMoveSpeed;
                }
            }
            if (moved) lastMove = gt.TotalGameTime;
        }

        protected virtual void MoveDown(Rectangle bound, IEnumerable<GameObject> collider, GameTime gt)
        {
            bool moved = false;
            if (!jumping)
            {
                this.jumpRelative = this.rectangle.Bottom;
            }
            this.orientation = Orientation.Down;
            if ((!Collision.BoundCollision(bound, this)) && (!Collision.ObjectCollision(collider, this)))
            {

                if (!(this is Enemy)) canJump = false;
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


                        if (!(this is Enemy)) canJump = false;
                        if ((gt.TotalGameTime - lastMove) >= new TimeSpan(0, 0, 0, 0, 17))
                        {

                            moved = true;

                            this.rectangle.Y += newSpeed;
                            

                        }


                        break;
                    }
                    else
                    {
                        canJump = true;
                        jumpRelative = this.rectangle.Bottom;

                    }
                    this.gravity = oldMoveSpeed;
                }
                this.gravity = oldMoveSpeed;
            }
            animation(Orientation.Down, gt);

            if (moved) lastMove = gt.TotalGameTime;
        }

        protected virtual void MoveLeft(Rectangle bound, IEnumerable<GameObject> collider, GameTime gt)
        {
            bool moved = false;
            if (!jumping)
            {
                this.jumpRelative = this.rectangle.Bottom;
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

            this.orientation = Orientation.Down;
            if ((!Collision.BoundCollision(bound, this)) && (!Collision.ObjectCollision(collider, this)))
            {


                if (!(this is Enemy)) canJump = false;
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




                        if (!(this is Enemy)) canJump = false;
                        if ((gt.TotalGameTime - lastMove) >= new TimeSpan(0, 0, 0, 0, 17))
                        {

                            moved = true;

                            this.rectangle.Y += newSpeed;
                           


                        }


                        break;
                    }
                    else
                    {
                        canJump = true;

                    }
                    this.gravity = oldMoveSpeed;
                    
                }
                this.gravity = oldMoveSpeed;
                canJump = true;
            }


            this.orientation = Orientation.Left;

            animation(Orientation.Left, gt);

            if (moved) lastMove = gt.TotalGameTime;
        }

        protected virtual void MoveRight(Rectangle bound, IEnumerable<GameObject> collider, GameTime gt)
        {
            bool moved = false;
            if (!jumping)
            {
                this.jumpRelative = this.rectangle.Bottom;
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
                for (int i = 0; i <= this.movementSpeed; i++)
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

                if (!(this is Enemy)) canJump = false;
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


                        if (!(this is Enemy)) canJump = false;

                        if ((gt.TotalGameTime - lastMove) >= new TimeSpan(0, 0, 0, 0, 17))
                        {

                            moved = true;

                            this.rectangle.Y += newSpeed;
                            


                        }

                        break;
                    }
                    else
                    {
                        canJump = true;

                    }
                    this.gravity = oldMoveSpeed;
                }
                this.gravity = oldMoveSpeed;
                canJump = true;
            }

            this.orientation = Orientation.Right;

            animation(Orientation.Right, gt);
            if (moved) lastMove = gt.TotalGameTime;
        }

        protected virtual void MoveIdle(Rectangle bound, IEnumerable<GameObject> collider, GameTime gt)
        {
            bool moved = false;
            if (!jumping)
            {
                this.jumpRelative = this.rectangle.Bottom;
            }
            this.orientation = Orientation.Down;
            if ((!Collision.BoundCollision(bound, this)) && (!Collision.ObjectCollision(collider, this)))
            {
                if (!(this is Enemy)) canJump = false;

                if ((gt.TotalGameTime - lastMove) >= new TimeSpan(0, 0, 0, 0, 17))
                {

                    moved = true;

                    this.rectangle.Y += this.gravity;
                   


                }


            }
            else
            {
                Collision.ObjCollision = true;
                int oldMoveSpeed = this.gravity;
                for (int i = 1; i <= this.gravity; i++)
                {
                    int newSpeed = this.gravity - i;

                    this.gravity = newSpeed;

                    if ((!Collision.BoundCollision(bound, this)) && (!Collision.ObjectCollision(collider, this)))
                    {

                        if (!(this is Enemy)) canJump = false;
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
                        canJump = true;

                    }
                    this.gravity = oldMoveSpeed;
                }
                canJump = true;
                this.gravity = oldMoveSpeed;
            }

            animation(Orientation.Idle, gt);

            if (moved) lastMove = gt.TotalGameTime;

        }

        public override void Move(GameTime gt, KeyboardState kbstate, Rectangle mainFrame, IEnumerable<GameObject> GObjects)
        {



            

            bool KeyUpPress = false;
            bool KeyDownPress = false;
            bool KeyLeftPress = false;
            bool KeyRightPress = false;

            Keys[] pressedKeys = kbstate.GetPressedKeys();


            if (pressedKeys.Contains(this.KeyUp))
            {
                KeyUpPress = true;
            }
            if (pressedKeys.Contains(KeyDown))
            {
                KeyDownPress = true;
            }
            if (pressedKeys.Contains(KeyLeft))
            {
                KeyLeftPress = true;
            }
            if (pressedKeys.Contains(KeyRight))
            {
                KeyRightPress = true;
            }








            if (KeyUpPress && KeyLeftPress)
            {
                this.MoveUpLeft(mainFrame, GObjects, gt);
            }

            else if (KeyUpPress && KeyRightPress)
            {
                this.MoveUpRight(mainFrame, GObjects, gt);
            }


            else if (KeyUpPress)
            {
                this.MoveUp(mainFrame, GObjects, gt);
            }
            else if (KeyLeftPress)
            {
                this.MoveLeft(mainFrame, GObjects, gt);
            }
            else if (KeyDownPress)
            {
                this.MoveDown(mainFrame, GObjects, gt);
            }
            else if (KeyRightPress)
            {
                this.MoveRight(mainFrame, GObjects, gt);
            }
            else
            {
                this.MoveIdle(mainFrame, GObjects, gt);
                
            }


        }

        public virtual void animation(Orientation ori, GameTime gt)
        {
        }

        public override void Draw(ref SpriteBatch sb)
        {
            sb.Draw(this.Texture, this.rectangle, Color.White);

           
        }

        public virtual void ItemPickup(ref Items item)
        {

        }
    }
}
