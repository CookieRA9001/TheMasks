using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace TheMasks.Items.RedMask{
    public class RedTome : ModItem{
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Cast Red Fire Balls");
            DisplayName.SetDefault("Tome of Burning Red");
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
            item.useTime = 40;
            item.useAnimation = 40;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.noMelee = true;
            item.useTurn = true;
            item.autoReuse = true;

            //stats
            item.damage = 30;
            item.magic = true;
            item.knockBack = 3;
            item.mana = 10;
            item.UseSound = SoundID.DD2_BetsySummon;
            //https://terraria.gamepedia.com/Category:Sound_effects
            
            item.shoot = mod.ProjectileType("RedFire");
            item.shootSpeed = 10f;
        }

        public override void AddRecipes()
        {
            var rec = new ModRecipe(mod);
            rec.AddIngredient(ItemID.SpellTome, 1);
            rec.AddIngredient(ModContent.ItemType<RedBar>(), 5);
            rec.AddIngredient(ModContent.ItemType<RedEssence>(), 10);
            rec.AddTile(TileID.Anvils);
            rec.SetResult(this);
            rec.AddRecipe();
        }
    }
}