using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace TheMasks.Items.BlueMask{
    public class BlueTome : ModItem{
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Cast Blue Freez Balls");
            DisplayName.SetDefault("Tome of Freezing Blue");
        }
        public override void SetDefaults(){
            //hitbox size
            item.width = 24;
            item.height = 28;
            
            //stack & value
            item.maxStack = 1;
            item.value = Item.sellPrice(gold: 2);
            item.rare = ItemRarityID.LightRed;
            item.material = true;

            // Usage
            item.useTime = 50;
            item.useAnimation = 50;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.noMelee = true;
            item.useTurn = true;
            item.autoReuse = true;

            //stats
            item.damage = 50;
            item.magic = true;
            item.knockBack = 5;
            item.mana = 15;
            item.UseSound = SoundID.DD2_BetsySummon;
            //https://terraria.gamepedia.com/Category:Sound_effects
            
            item.shoot = mod.ProjectileType("BlueFire");
            item.shootSpeed = 6f;
        }

        public override void AddRecipes()
        {
            var rec = new ModRecipe(mod);
            rec.AddIngredient(ItemID.SpellTome, 1);
            rec.AddIngredient(ModContent.ItemType<BlueBar>(), 5);
            rec.AddIngredient(ModContent.ItemType<BlueEssence>(), 10);
            rec.AddTile(TileID.Anvils);
            rec.SetResult(this);
            rec.AddRecipe();
        }
    }
}