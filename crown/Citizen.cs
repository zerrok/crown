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
        int steps;
        int speed;

        public enum Direction { INIT, LEFT, RIGHT, UP, DOWN };

        public Citizen(Vector2 pos, SpriteFrame spriteFrame) {
            this.pos = pos;
            this.spriteFrame = spriteFrame;
            int randomDir = random.Next(1, 5);
            rect = new Rectangle((int)pos.X + 8, (int)pos.Y + 8, 16, 16);

            currentDirection = Direction.INIT;
            steps = 0;
            speed = 1;
            lastDirection = currentDirection;
        }

        public void Movement() {
            List<Direction> possible = new List<Direction>();
            if (lastDirection == Direction.INIT) {
                SetNextDirection(possible);
            }
            // Walk the size of one texture in a direction
            if (steps < tileSize)
                currentDirection = lastDirection;
            else {
                SetNextDirection(possible);
                steps = 0;
            }

            if (currentDirection == Direction.RIGHT) {
                pos.X += speed;
                rect.X += speed;
            }
            if (currentDirection == Direction.LEFT) {
                pos.X -= speed;
                rect.X -= speed;
            }
            if (currentDirection == Direction.UP) {
                pos.Y -= speed;
                rect.Y -= speed;
            }
            if (currentDirection == Direction.DOWN) {
                pos.Y += speed;
                rect.Y += speed;
            }

            steps++;
            lastDirection = currentDirection;
        }

        private void SetNextDirection(List<Direction> possible) {
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
                    // Low chance of changing direction
                    currentDirection = lastDirection;
                } else
                    currentDirection = possible[random.Next(0, possible.Count)];

            }
        }

        private void CheckForFutureIntersection(List<Direction> possible, int x, int y) {
            if (roads[x + 1, y] != null && roads[x + 1, y].Rect.Intersects(new Rectangle(rect.X + tileSize, rect.Y, rect.Width, rect.Height)))
                possible.Add(Direction.RIGHT);
            if (roads[x - 1, y] != null && roads[x - 1, y].Rect.Intersects(new Rectangle(rect.X - tileSize, rect.Y, rect.Width, rect.Height)))
                possible.Add(Direction.LEFT);
            if (roads[x, y - 1] != null && roads[x, y - 1].Rect.Intersects(new Rectangle(rect.X, rect.Y - tileSize, rect.Width, rect.Height)))
                possible.Add(Direction.UP);
            if (roads[x, y + 1] != null && roads[x, y + 1].Rect.Intersects(new Rectangle(rect.X, rect.Y + tileSize, rect.Width, rect.Height)))
                possible.Add(Direction.DOWN);
        }

        private void ReverseDirection() {
            if (lastDirection == Direction.RIGHT)
                currentDirection = Direction.LEFT;
            if (lastDirection == Direction.LEFT)
                currentDirection = Direction.RIGHT;
            if (lastDirection == Direction.UP)
                currentDirection = Direction.DOWN;
            if (lastDirection == Direction.DOWN)
                currentDirection = Direction.UP;
        }

        public SpriteFrame SpriteFrame { get => spriteFrame; set => spriteFrame = value; }
        public Vector2 Pos { get => pos; set => pos = value; }
        public int Steps { get => steps; set => steps = value; }
        public Rectangle Rect { get => rect; set => rect = value; }
    }
}
