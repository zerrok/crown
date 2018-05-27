using crown.Terrain;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;
using static crown.Game1;

namespace crown {
    public class Controls {

        public static MouseAction BuildStuff(MouseAction mouseAction, Vector2 mousePositionInWorld) {
            foreach (Tile tile in tileMap)
                if (tile != null && tile.Rect.Contains(mousePositionInWorld)) {
                    if (mouseAction == MouseAction.Townhall) {
                        // Only allowed to build it once
                        foreach (Building building in mechanics.Buildings)
                            if (building.Type == Building.BuildingTypes.Townhall) {
                                mouseAction = MouseAction.Nothing;
                                return mouseAction;
                            }
                        BuildingOperations.BuildTownHall(tile, new Costs(0, 0, 0, 0, 0, 0, 0, 0, 0, 0));
                        mouseAction = MouseAction.Nothing;
                    }
                    if (mouseAction == MouseAction.House) {
                        BuildingOperations.BuildSmallBuilding(tile, Building.BuildingTypes.House, Costs.HouseCosts());
                    }
                    if (mouseAction == MouseAction.Farm) {
                        BuildingOperations.BuildLargeBuilding(tile, Building.BuildingTypes.Farm, Costs.FarmCosts());
                    }
                    if (mouseAction == MouseAction.Woodcutter) {
                        BuildingOperations.BuildSmallBuilding(tile, Building.BuildingTypes.Woodcutter, Costs.WoodcutterCosts());
                    }
                    if (mouseAction == MouseAction.Quarry) {
                        BuildingOperations.BuildQuarry(tile, Building.BuildingTypes.Quarry, Costs.WoodcutterCosts());
                    }
                    if (mouseAction == MouseAction.Scientist) {
                        BuildingOperations.BuildLargeBuilding(tile, Building.BuildingTypes.Scientist, Costs.ScientistCosts());
                    }
                    if (mouseAction == MouseAction.Road) {
                        BuildingOperations.BuildRoad(tile, false);
                    }

                }

            // Sort buildings for rendering later
            IOrderedEnumerable<Building> sortedBuildings = mechanics.Buildings.OrderBy(building => building.Position.Y);
            mechanics.Buildings = new List<Building>();
            foreach (Building building in sortedBuildings) {
                mechanics.Buildings.Add(building);
            }

            mechanics.MaxPop = mechanics.GetMaxPop();
            return mouseAction;
        }

        public static void InteractWithStuff(Vector2 mousePositionInWorld) {
            foreach (Interactive inter in interactives) {
                if (inter.Type == Interactive.IntType.TREE) {
                    if (inter.Rect.Contains(mousePositionInWorld)) {
                        inter.Health--;
                        // TODO: Select interactive and show info
                        break;
                    }
                }
            }
            foreach (Building building in mechanics.Buildings) {
                if (building.Rect.Contains(mousePositionInWorld)) {
                    selectedBuilding = building;
                    break;
                }
            }
        }

        public static void CameraControls(Camera2d cam, float camSpeed) {
            // Camera Movement
            if (Keyboard.GetState().IsKeyDown(Keys.S)) {
                cam.Move(new Vector2(0, camSpeed));
            }
            if (Keyboard.GetState().IsKeyDown(Keys.W)) {
                cam.Move(new Vector2(0, -camSpeed));
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A)) {
                cam.Move(new Vector2(-camSpeed, 0));
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D)) {
                cam.Move(new Vector2(camSpeed, 0));
            }
            if (Keyboard.GetState().IsKeyDown(Keys.N)) {
                if (cam.Zoom >= 0.2f)
                    cam.Zoom -= 0.01f;
                if (cam.Zoom < 0.2f)
                    cam.Zoom = 0.2f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.M)) {
                if (cam.Zoom <= 2f)
                    cam.Zoom += 0.01f;
            }
        }

    }
}
