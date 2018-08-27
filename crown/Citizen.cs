using System;
using System.Collections.Generic;
using crown.Terrain;
using Microsoft.Xna.Framework;
using TexturePackerLoader;
using static crown.Game1;

namespace crown {
    public class Citizen {
        Vector2 pos;
        Rectangle rect;
        SpriteFrame spriteFrame;
        Direction lastDirection;
        Direction currentDirection;
        Queue<Direction> path;
        Rectangle destination;
        bool isIdle;

        int steps;
        int speed;

        public enum Direction { Init, Left, Right, Up, Down };

        public SpriteFrame SpriteFrame { get => spriteFrame; set => spriteFrame = value; }
        public Vector2 Pos { get => pos; set => pos = value; }
        public int Steps { get => steps; set => steps = value; }
        public Rectangle Rect { get => rect; set => rect = value; }
        public Queue<Direction> Paths { get => path; set => path = value; }
        public bool IsIdle { get => isIdle; set => isIdle = value; }
        public Rectangle Destination { get => destination; set => destination = value; }

        public Citizen(Vector2 pos, SpriteFrame spriteFrame) {
            this.pos = pos;
            this.spriteFrame = spriteFrame;
            int randomDir = random.Next(1, 5);
            rect = new Rectangle((int)pos.X + 8, (int)pos.Y + 8, 16, 16);

            currentDirection = Direction.Init;
            steps = 0;
            speed = 1;
            lastDirection = currentDirection;
            isIdle = true;
        }

        public void Movement() {
            // We can only calulate the paths if step count is 0, so idle movement is still enabled until all the steps are done! 
            if (isIdle || steps != 0)
                IdleMovement();


            if (!isIdle && path == null && steps == 0) {
                // Determine new path
                path = new Queue<Direction>();
            }

            if (!isIdle && path != null) {
                // Set the next direction
                speed = 0;
            }

            if (currentDirection == Direction.Right) {
                pos.X += speed;
                rect.X += speed;
            }
            if (currentDirection == Direction.Left) {
                pos.X -= speed;
                rect.X -= speed;
            }
            if (currentDirection == Direction.Up) {
                pos.Y -= speed;
                rect.Y -= speed;
            }
            if (currentDirection == Direction.Down) {
                pos.Y += speed;
                rect.Y += speed;
            }

            steps++;
            lastDirection = currentDirection;
        }

        private void IdleMovement() {
            List<Direction> possible = new List<Direction>();
            if (lastDirection == Direction.Init) {
                SetNextIdleDirection(possible);
            }
            // Walk the size of one texture in a direction
            if (steps < tileSize)
                currentDirection = lastDirection;
            else {
                SetNextIdleDirection(possible);
                steps = 0;
            }
        }

        private void SetNextIdleDirection(List<Direction> possible) {
            int x = 0;
            int y = 0;
            // Get the road which the citizen is walking on
            foreach (Road road in roads)
                if (road != null && road.Rect.Contains(pos)) {
                    x = (int)road.Coords.X / tileSize;
                    y = (int)road.Coords.Y / tileSize;
                }

            // Now look if the citizen would be on another road in 128 steps
            if (x > 0 && y > 0 && x < roads.GetUpperBound(0) && y < roads.GetUpperBound(1)) {
                CheckForFutureIntersection(possible, x, y);
            } else {
                ReverseDirection();
            }

            if (possible.Count > 0) {
                if (possible.Contains(lastDirection) && possible.Count > 2 && random.Next(0, 20) > 5) {
                    // Chance to change direction at an intersection
                    Direction dirToRemove = possible.Find(direction => direction.Equals(lastDirection));
                    possible.Remove(dirToRemove);
                    currentDirection = possible[random.Next(0, possible.Count)];
                } else if (possible.Contains(lastDirection) && random.Next(0, 25000) > 5) {
                    // Keep current direction
                    currentDirection = lastDirection;
                } else
                    currentDirection = possible[random.Next(0, possible.Count)];

            }
        }

        private void CheckForFutureIntersection(List<Direction> possible, int x, int y) {
            if (roads[x + 1, y] != null && roads[x + 1, y].Rect.Intersects(new Rectangle(rect.X + tileSize, rect.Y, rect.Width, rect.Height)))
                possible.Add(Direction.Right);
            if (roads[x - 1, y] != null && roads[x - 1, y].Rect.Intersects(new Rectangle(rect.X - tileSize, rect.Y, rect.Width, rect.Height)))
                possible.Add(Direction.Left);
            if (roads[x, y - 1] != null && roads[x, y - 1].Rect.Intersects(new Rectangle(rect.X, rect.Y - tileSize, rect.Width, rect.Height)))
                possible.Add(Direction.Up);
            if (roads[x, y + 1] != null && roads[x, y + 1].Rect.Intersects(new Rectangle(rect.X, rect.Y + tileSize, rect.Width, rect.Height)))
                possible.Add(Direction.Down);
        }

        private void ReverseDirection() {
            if (lastDirection == Direction.Right)
                currentDirection = Direction.Left;
            if (lastDirection == Direction.Left)
                currentDirection = Direction.Right;
            if (lastDirection == Direction.Up)
                currentDirection = Direction.Down;
            if (lastDirection == Direction.Down)
                currentDirection = Direction.Up;
        }

        internal void SetNewDestination(Rectangle rectangle) {
            // We have to store the destination 
            destination = rectangle;
            // Then turn off idling, so the path will be calculated when the citizen stopped his last 128 pixel step
            isIdle = false;
        }

    }
}
