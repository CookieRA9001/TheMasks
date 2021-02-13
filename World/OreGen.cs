using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using System.Collections.Generic;
using Terraria.World.Generation;
using Terraria.GameContent.Generation;
using Microsoft.Xna.Framework;

namespace TheMasks.World{
    public class OreGen : ModWorld{
        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight){
            int index = tasks.FindIndex(genpass => genpass.Name.Equals("Shinies"));
            if(index != -1){
                tasks.Insert(index+1,new PassLegacy("Mask Ore Generation", OreGeneration));
            }
        }
        public void OreGeneration(GenerationProgress progress){
            progress.Message = "The Masks power shapes the World";
            int count = 0;
            while(count<1){
                for(var i = 0; i<(int)((double)(Main.maxTilesX*Main.maxTilesY)*0.00002f);i++){
                    int x = WorldGen.genRand.Next(0,Main.maxTilesX);
                    int y = WorldGen.genRand.Next((int)(WorldGen.worldSurfaceHigh*1.5),Main.maxTilesY);

                    WorldGen.TileRunner(x,y,
                        (double)WorldGen.genRand.Next(5,10),
                        WorldGen.genRand.Next(3,8),
                        mod.TileType("RedOre"),
                        false,0f,0f,false,true
                    );
                    count++;
                }
            }
            count = 0;
            while(count<1){
                for(var i = 0; i<(int)((double)(Main.maxTilesX*Main.maxTilesY)*0.00002f);i++){
                    int x = WorldGen.genRand.Next(0,Main.maxTilesX);
                    int y = WorldGen.genRand.Next((int)(WorldGen.worldSurfaceHigh*1.5),Main.maxTilesY);
                    Tile tile = Framing.GetTileSafely(x,y);
                    //for biom exlusiveity
                    //if(tile.active() && (tile.type == TileID.SnowBlock || tile.type == TileID.IceBlock)){}
                    WorldGen.TileRunner(x,y,
                        (double)WorldGen.genRand.Next(5,10),
                        WorldGen.genRand.Next(3,8),
                        mod.TileType("BlueOre")
                    );
                    count++;
                }
            }
        }
    }
}
//https://www.youtube.com/watch?v=mKz_91TlaCo
//https://github.com/tModLoader/tModLoader/blob/master/ExampleMod/ExampleWorld.cs#L83