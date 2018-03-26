using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TexturePackerLoader;
using static crown.Game1;

namespace crown {
    class Drawing {
        public static void drawTerrain(SpriteRender spriteRender, Vector2[,] map) {

            foreach (Vector2 coord in map) { // TODO: Zeichnet natürlich alles, muss noch nur das auf dem Bildschirm tatsächlich rendern
                if ((coord.X / 16) % 2 == 1) {
                    spriteRender.Draw(tileAtlas.Sprite(TexturePackerMonoGameDefinitions.texturePackerSpriteAtlas.Grass1), coord);
                } else {
                    spriteRender.Draw(tileAtlas.Sprite(TexturePackerMonoGameDefinitions.texturePackerSpriteAtlas.Stone1), coord);
                }
            }
        }
    }
}
