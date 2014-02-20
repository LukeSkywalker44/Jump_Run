using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using X2DPE;
using X2DPE.Helpers;

namespace Jump_n_Run.classes
{
    /// <summary>
    /// creates Rectangles for the next frame and checks for collsion
    /// </summary>
    static class Collision
    {


        /// <summary>
        /// Checks if the MoveableObject will be in the bound rectangle
        /// in the next frame
        /// </summary>
        /// <param name="bounds">The Rectangle, in which the object should be</param>
        /// <param name="obj">The object, which should be in the rectangle</param>
        /// <returns>true on collision</returns>
        public static bool BoundCollision(Rectangle bounds, MoveableObject obj)
        {

            Rectangle moveable = obj.rectangle;
            int moveSpeed = obj.movementSpeed;

            int objX;
            int objY;

            switch (obj.orientation)
            {
                case Orientation.Up:
                    objY = 0 - moveSpeed;
                    objX = 0;
                    break;

                case Orientation.Down:
                    objY = obj.gravity;
                    objX = 0;
                    break;

                case Orientation.Left:
                    objY = 0;
                    objX = 0 - moveSpeed;
                    break;

                case Orientation.Right:
                    objY = 0;
                    objX = moveSpeed;
                    break;

                default:
                    objX = 0;
                    objY = 0;
                    break;

            }


            Rectangle futureRect = new Rectangle(moveable.X + objX, moveable.Y + objY, moveable.Width, moveable.Height);


            return !bounds.Contains(futureRect);

        }


        /// <summary>
        /// Checks if two Objects would collide in the next frame
        /// </summary>
        /// <param name="Object">the rectangle of Object 1</param>
        /// <param name="obj">the rectangle of Object 2</param>
        /// <returns>true on collision</returns>
        public static bool ObjectCollision(GameObject Object, MoveableObject obj)
        {
            Rectangle moveable = obj.rectangle;
            int moveSpeed = obj.movementSpeed;

            int objX;
            int objY;

            switch (obj.orientation)
            {
                case Orientation.Up:
                    objY = 0 - moveSpeed;
                    objX = 0;
                    break;

                case Orientation.Down:
                    objY = obj.gravity;
                    objX = 0;
                    break;

                case Orientation.Left:
                    objY = 0;
                    objX = 0 - moveSpeed;
                    break;

                case Orientation.Right:
                    objY = 0;
                    objX = moveSpeed;
                    break;

                default:
                    objX = 0;
                    objY = 0;
                    break;

            }

            if ((obj is Enemy) && (Object is Enemy))
            {
                return false;
            }


            Rectangle futureRect = new Rectangle(moveable.X + objX, moveable.Y + objY, moveable.Width, moveable.Height);

            return Object.rectangle.Intersects(futureRect);
        }


        /// <summary>
        /// Checks if the MoveableObject would collide with any of 
        /// the GameObjects in the next frame
        /// </summary>
        /// <param name="Objects">The Objects, the MoveableObject can collide with</param>
        /// <param name="obj">The moving Object</param>
        /// <returns>true on collision</returns>
        public static bool ObjectCollision(IEnumerable<GameObject> Objects, MoveableObject obj)
        {

            bool result = false;

            Rectangle moveable = obj.rectangle;
            int moveSpeed = obj.movementSpeed;

            int objX;
            int objY;

            switch (obj.orientation)
            {
                case Orientation.Up:
                    objY = 0 - moveSpeed;
                    objX = 0;
                    break;

                case Orientation.Down:
                    objY = obj.gravity;
                    objX = 0;
                    break;

                case Orientation.Left:
                    objY = 0;
                    objX = 0 - moveSpeed;
                    break;

                case Orientation.Right:
                    objY = 0;
                    objX = moveSpeed;
                    break;

                default:
                    objX = 0;
                    objY = 0;
                    break;

            }


            Rectangle futureRect = new Rectangle(moveable.X + objX, moveable.Y + objY, moveable.Width, moveable.Height);


            List<GameObject> gobjs = Objects.ToList();


            gobjs.Remove(obj);  // remove the moving object to prevent self-collision




            foreach (GameObject collider in gobjs)
            {

              
                result = collider.rectangle.Intersects(futureRect);

                if (result == true)
                {
                    if ((collider is Items) && (obj is Player))
                    {
                        Items item = (Items)collider;
                        ((Player)obj).ItemPickup(ref item);
                    }
                    if ((obj is Panda) && (collider is Panda))
                    {
                        return false;
                    }
                    if ((obj is Player) && (collider is Panda))
                    {
                        return false;
                    }
                    if ((obj is Panda) && (collider is Player))
                    {
                        return false;
                    }

                   
                    return true;
                }
            }


            return false;
        }

        public static bool ParticleCollision(IEnumerable<GameObject> Objects, Particle particle)
        {
            bool collides = false;
            foreach (GameObject obj in Objects)
            {
                if (obj.GetType().Equals(new GameObject().GetType()) || obj.GetType().Equals(new KeyHole().GetType()) || obj.GetType().Equals(new Particle().GetType()))
                {
                    if (obj.rectangle.Contains(new Point(Convert.ToInt32(particle.Position.X), Convert.ToInt32(particle.Position.Y)))) return true;
                   
                }
            }

           

            return collides;
        }


    }
}
