using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

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
                    objY = moveSpeed;
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
                    objY = moveSpeed;
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




            return Object.rectangle.Intersects(futureRect);
        }
    }
}
