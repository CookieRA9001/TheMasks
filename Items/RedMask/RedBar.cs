using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace TheMasks.Items.RedMask{
    public class RedBar : ModItem{
        public override void SetDefaults(){
            //hitbox size
            item.width = 16;
            item.height = 16;
            //stack & value
            item.maxStack = 99;
            item.value = Item.sellPrice(silver: 4);
            item.rare = ItemRarityID.Green;
            item.material = true;

            // Usage
            item.useTime = 12;
            item.useAnimation = 12;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.useTurn = true;
            item.autoReuse = true;
        }

        public override void AddRecipes()
        {
            var rec = new ModRecipe(mod);
            rec.AddIngredient(ModContent.ItemType<RedOre>(), 3);
            rec.AddTile(TileID.Furnaces);
            rec.SetResult(this);
            rec.AddRecipe();
        }
    }
}