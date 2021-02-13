using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace TheMasks.Tiles.RedMask{
    
    public class RedOre : ModTile {
        public override void SetDefaults(){
            Main.tileSolid[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileMergeDirt[Type] = true;
            minPick = 50;

            drop = ModContent.ItemType<Items.RedMask.RedOre>();
            
            AddMapEntry(Color.DarkBlue);
        }
    }
}