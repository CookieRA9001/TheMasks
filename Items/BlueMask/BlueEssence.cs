using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace TheMasks.Items.BlueMask{
    public class BlueEssence : ModItem{
        public override void SetDefaults(){
            //hitbox size
            item.width = 16;
            item.height = 16;
            //stack & value
            item.maxStack = 99;
            item.value = Item.sellPrice(silver: 5);
            item.rare = ItemRarityID.Orange;
            item.material = true;
            // Usage
            item.useTime = 12;
            item.useAnimation = 12;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.useTurn = true;
            item.autoReuse = true;
        }
    }
}