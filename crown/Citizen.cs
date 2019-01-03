using System;
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
            // First we look which way to go with the start and endpoint
            Road startPoint = null;
            Road endPoint = null;
            DetermineStartAndEndPoint(new Rectangle(rect.X, rect.Y, rect.Width, rect.Height), ref startPoint, ref endPoint);

            if (startPoint != null && endPoint != null) {
                // If the start point and end point are the same we can just stop here
                if (startPoint == endPoint)
                    return;

                // Possible next roads for the starting point
                List<List<Road>> roadLists = new List<List<Road>>();

                // Add starting road tile to the list first
                roadLists.Add(new List<Road>() { startPoint });

                // Now we try to go through every possible direction that was found until the endpoint is reached or no more possible roads are detected
                // in that case we just throw the stupid thing away I guess
                for (int i = 0; i < roadLists.Count; i++) {
                    Boolean gotToEndpoint = false;

                    Console.WriteLine("List No. " + (i + 1));

                    int roadDebugCount = 1;
                    while (!gotToEndpoint) {
                        if (DoesListContainRoadCoords(roadLists[i], endPoint)) {
                            Console.WriteLine("REACHED END!");
                            gotToEndpoint = true;
                            break;
                        }
                        if (roadDebugCount == 1) {
                            Console.WriteLine("Startpoint (X/Y): " + startPoint.Coords.X / tileSize + "/" + startPoint.Coords.Y / tileSize + ".");
                            Console.WriteLine("Endpoint (X/Y): " + endPoint.Coords.X / tileSize + "/" + endPoint.Coords.Y / tileSize + ".");
                        }
                        Console.WriteLine("Road No. " + roadDebugCount++);
                        // This is the road list we handle this runthrough
                        List<Road> currRoadList = roadLists[i];
                        // We have to look at the possible roads for the last added item to the list
                        List<Road> posRoads = getPossibleRoads(currRoadList[currRoadList.Count - 1]);



                        // Remove duplicates so the search does not go backwards
                        foreach (Road road in currRoadList) {
                            if (posRoads.Contains(road)) {
                                posRoads.Remove(road);
                                Console.WriteLine("-->REMOVED Road (X/Y): " + road.Coords.X / tileSize + "/" + road.Coords.Y / tileSize + ".");
                            }
                        }
                        // We have to put one of the possible roads into the roadLists list, and we need possible new lists for more possible roads
                        if (posRoads.Count < 1) {
                            gotToEndpoint = true;
                            // Remove lists which have no endpoint, then continue with next iteration
                            roadLists.RemoveAt(i);
                            Console.WriteLine("Deleted this list.");
                            i--;
                            continue;
                        } else {
                            roadLists[i].Add(posRoads[0]);
                            Console.WriteLine("---->Added Road (X/Y): " + posRoads[0].Coords.X / tileSize + "/" + posRoads[0].Coords.Y / tileSize + ".");
                            if (posRoads.Count > 1) {
                                CreateRoadListCopies(roadLists, currRoadList, posRoads);
                            }
                        }
                    }
                }

                int debug = 0;
                path = new Queue<Direction>();
                // Letzte Liste loop path.enqueue(direction.x)
            }
        }

        private static void CreateRoadListCopies(List<List<Road>> roadLists, List<Road> rL, List<Road> possiRoads) {
            for (int x = 1; x < possiRoads.Count; x++) {
                // Add remaining possible roads to a list which is copied from the current road list
                List<Road> copy = new List<Road>();
                foreach (Road roadCopy in rL) {
                    copy.Add(roadCopy);
                }
                copy.Add(possiRoads[x]);
                roadLists.Add(copy);
            }
        }

        private bool DoesListContainRoadCoords(List<Road> rL, Road road) {
            bool contains = false;
            foreach (Road r in rL) {
                if (r.Coords.X == road.Coords.X && r.Coords.Y == road.Coords.Y) {
                    contains = true;
                    break;
                } else
                    contains = false;
            }
            return contains;
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
                        startPoint = road;
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
