using Microsoft.Xna.Framework.Graphics;
using PlatformerDemo.Interfaces;
using PlatformerDemo.Terrain.Blocks;
using System.Collections.Generic;

namespace PlatformerDemo.Terrain
{
    // Builder Pattern
    // De TerrainBuilder klasse omvat de constructie van een complex terrein, waarbij de representatie en de constructie van elkaar worden gescheiden.
    public class TerrainBuilder
    {
        private IBlueprint blueprint;
        public List<Block> Blocks { get; private set; }

        public TerrainBuilder(IBlueprint blueprint)
        {
            this.blueprint = blueprint;
            Blocks = new List<Block>();
        }

        public void LoadTerrain(Texture2D texture)
        {
            for (int i = 0; i < blueprint.Board.GetLength(0); i++)
            {
                for (int j = 0; j < blueprint.Board.GetLength(1); j++)
                {
                    int tileType = blueprint.Board[i, j];
                    switch (tileType)
                    {
                        case 1:
                            Blocks.Add(new PlatformBlock1(j, i, texture));
                            break;
                        case 2:
                            Blocks.Add(new PlatformBlock2(j, i, texture));
                            break;
                        case 3:
                            Blocks.Add(new PlatformBlock3(j, i, texture));
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        public void DrawTerrain(SpriteBatch spriteBatch)
        {
            foreach (var block in Blocks)
            {
                block.Draw(spriteBatch);
            }
        }
    }
}