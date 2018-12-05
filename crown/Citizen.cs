﻿using System;
using System.Collections.Generic;
using crown.Terrain;
using Microsoft.Xna.Framework;
using TexturePackerLoader;
using static crown.Game1;

namespace crown
{
    public class Citizen
    {
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
                DetermineShortestPath();
            }

            if (!isIdle && path != null && steps == 0) {
                steps = 0;
                if (path.Count > 0)
                    currentDirection = path.Dequeue();
                else
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

        private void DetermineShortestPath() {
            path = new Queue<Direction>();

            // We also need a sorted list that stores the amount of steps one path needs to the destination, so the shortest path can be chosen in the end
            SortedList<int, Queue<Direction>> results = new SortedList<int, Queue<Direction>>();

            // New rectangle to check collision with destination rectangle
            // We will move this during the pathfinding 
            Rectangle pathTangle = new Rectangle(rect.X, rect.Y, rect.Width, rect.Height);

            // First we look which way to go with the start and endpoint
            Road startPoint = null;
            Road endPoint = null;
            DetermineStartAndEndPoint(pathTangle, ref startPoint, ref endPoint);

            if (startPoint != null && endPoint != null) {
                // Possible next roads for the starting point
                List<Road> possibleRoads = getPossibleRoads(startPoint);
                List<List<Road>> roadLists = new List<List<Road>>();

                foreach (Road possibleRoad in possibleRoads) {
                    // We need a list for each possible road added to the list, so we can add new possible
                    // roads to each list after each fork in the road
                    List<Road> roadPath = new List<Road>();
                    roadPath.Add(possibleRoad);
                    roadLists.Add(roadPath);
                }

                foreach (List<Road> list in roadLists) {
                    // Get the possible roads for the last element in each list
                    possibleRoads = getPossibleRoads(list[list.Count - 1]);
                    // Remove last element, because walking back is not allowed
                    possibleRoads.Remove(list[list.Count - 1]);

                    // TODO: add one possible road to the list, 
                    // then for each other possible road, copy the list
                    // the copied list has to be added to the roadLists list for the next iteration
                    Road test = new Road(new Vector2(), new Rectangle()); // TODO: Remove this, this is only for the debug breakpoint
                }

                // Letzte Liste loop path.enqueue(direction.x)
            }
        }

        private List<Road> getPossibleRoads(Road road) {
            List<Road> possible = new List<Road>();
            Road road1 = roads[(int)road.Coords.X / tileSize + 1, (int)road.Coords.Y / tileSize];
            Road road2 = roads[(int)road.Coords.X / tileSize - 1, (int)road.Coords.Y / tileSize];
            Road road3 = roads[(int)road.Coords.X / tileSize, (int)road.Coords.Y / tileSize - 1];
            Road road4 = roads[(int)road.Coords.X / tileSize, (int)road.Coords.Y / tileSize + 1];
            if (road1 != null && road1.Rect.Intersects(new Rectangle(road.Rect.X + tileSize, road.Rect.Y, road.Rect.Width, road.Rect.Height)))
                possible.Add(road1);
            if (road2 != null && road2.Rect.Intersects(new Rectangle(road.Rect.X - tileSize, road.Rect.Y, road.Rect.Width, road.Rect.Height)))
                possible.Add(road2);
            if (road3 != null && road3.Rect.Intersects(new Rectangle(road.Rect.X, road.Rect.Y - tileSize, road.Rect.Width, road.Rect.Height)))
                possible.Add(road3);
            if (road4 != null && road4.Rect.Intersects(new Rectangle(road.Rect.X, road.Rect.Y + tileSize, road.Rect.Width, road.Rect.Height)))
                possible.Add(road4);

            return possible;
        }

        private void DetermineStartAndEndPoint(Rectangle pathTangle, ref Road startPoint, ref Road endPoint) {
            foreach (Road road in roads) {
                if (road != null) {
                    // Determine start point
                    if (road.Rect.Contains(new Vector2(pathTangle.X, pathTangle.Y))) {
                        startPoint = new Road(road.Coords, road.Rect);
                    }
                    // Determine end point, which is a road next to the destination
                    if (road.Rect.Intersects(new Rectangle(destination.X + tileSize, destination.Y, destination.Width, destination.Height))
                        || road.Rect.Intersects(new Rectangle(destination.X - tileSize, destination.Y, destination.Width, destination.Height))
                        || road.Rect.Intersects(new Rectangle(destination.X, destination.Y + tileSize, destination.Width, destination.Height))
                        || road.Rect.Intersects(new Rectangle(destination.X, destination.Y - tileSize, destination.Width, destination.Height))) {
                        endPoint = new Road(road.Coords, road.Rect);
                    }
                }
            }
        }

        internal void SetNewDestination(Rectangle rectangle) {
            // We have to store the destination, so when the citizen hits the destination, he is done
            destination = rectangle;
            // Then turn off idling, so the path will be calculated when the citizen stopped his last 128 pixel step
            isIdle = false;
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
                    break;
                }

            // Now look if the citizen would be on another road in 128 steps
            if (x > 0 && y > 0 && x < roads.GetUpperBound(0) && y < roads.GetUpperBound(1)) {
                CheckForFutureRoadIntersection(possible, x, y, rect);
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

        private void CheckForFutureRoadIntersection(List<Direction> possible, int x, int y, Rectangle collider) {
            if (roads[x + 1, y] != null && roads[x + 1, y].Rect.Intersects(new Rectangle(collider.X + tileSize, collider.Y, collider.Width, collider.Height)))
                possible.Add(Direction.Right);
            if (roads[x - 1, y] != null && roads[x - 1, y].Rect.Intersects(new Rectangle(collider.X - tileSize, collider.Y, collider.Width, collider.Height)))
                possible.Add(Direction.Left);
            if (roads[x, y - 1] != null && roads[x, y - 1].Rect.Intersects(new Rectangle(collider.X, collider.Y - tileSize, collider.Width, collider.Height)))
                possible.Add(Direction.Up);
            if (roads[x, y + 1] != null && roads[x, y + 1].Rect.Intersects(new Rectangle(collider.X, collider.Y + tileSize, collider.Width, collider.Height)))
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

    }
}
