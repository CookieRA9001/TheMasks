using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace TheMasks.Items.RedMask{
    public class RedOre : ModItem{
        public override void SetDefaults(){
            //hitbox size
            item.width = 12;
            item.height = 12;
            //stack & value
            item.maxStack = 999;
            item.value = Item.sellPrice(silver: 1);
            item.rare = ItemRarityID.Blue;
            item.consumable = true;
            item.material = true;
            // Usage
            item.useTime = 12;
            item.useAnimation = 12;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.useTurn = true;
            item.autoReuse = true;
            item.createTile = ModContent.TileType<Tiles.RedMask.RedOre>();
        }
    }
}