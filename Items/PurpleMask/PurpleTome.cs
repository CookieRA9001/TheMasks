using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace TheMasks.Items.PurpleMask{
    public class PurpleTome : ModItem{
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Cast Purple Fire Balls");
            DisplayName.SetDefault("Tome of Unity Purple");
        }
        public override void SetDefaults(){
            //hitbox size
            item.width = 24;
            item.height = 28;
            
            //stack & value
            item.maxStack = 1;
            item.value = Item.sellPrice(gold: 5);
            item.rare = ItemRarityID.Pink;
            item.material = true;

            // Usage
            item.useTime = 24;
            item.useAnimation = 24;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.noMelee = true;
            item.useTurn = true;
            item.autoReuse = true;

            //stats
            item.damage = 90;
            item.magic = true;
            item.knockBack = 5;
            item.mana = 20;
            item.UseSound = SoundID.DD2_BetsySummon;
            //https://terraria.gamepedia.com/Category:Sound_effects
            
            item.shoot = mod.ProjectileType("PurpleFire");
            item.shootSpeed = 20f;
        }

        public override void AddRecipes()
        {
            var rec = new ModRecipe(mod);
            rec.AddIngredient(ModContent.ItemType<BlueMask.BlueTome>(), 1);
            rec.AddIngredient(ModContent.ItemType<RedMask.RedTome>(), 1);
            rec.AddIngredient(ItemID.HallowedBar, 5);
            rec.AddTile(TileID.Anvils);
            rec.SetResult(this);
            rec.AddRecipe();
        }
    }
}