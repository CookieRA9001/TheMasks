using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System;

namespace TheMasks.Items.RedMask{
    public class RedSword : ModItem{
        public override void SetDefaults(){
            //hitbox size
            item.width = 110;
            item.height = 124;
            
            //stack & value
            item.maxStack = 1;
            item.value = Item.sellPrice(silver: 50);
            item.rare = ItemRarityID.LightRed;
            item.material = true;

            // Usage
            item.useTime = 40;
            item.useAnimation = 32;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.useTurn = true;
            item.autoReuse = true;

            //stats
            item.damage = 38;
            item.knockBack = 5;
            item.melee = true;
        }
        public override void MeleeEffects(Player player, Rectangle hitbox){
            Vector2 xy;
            xy.X = hitbox.X;
            xy.Y = hitbox.Y;
            Lighting.AddLight(xy, 0.5f, 0.1f, 0.1f);
            Dust.NewDust(xy, hitbox.Width, hitbox.Height, mod.DustType("RedFire"), 1.5f, 1.5f);
        }
        public override void UseStyle(Player player){
            float cosRot = (float)Math.Cos(player.itemRotation);
            float sinRot = (float)Math.Sin(player.itemRotation);
            //Align
            player.itemLocation.X = player.itemLocation.X + (-8 * cosRot * player.direction) + (-15 * sinRot * player.gravDir);
            player.itemLocation.Y = player.itemLocation.Y + (-8 * sinRot * player.direction) - (-15 * cosRot * player.gravDir);
		}
        public override void ModifyHitNPC(Player player, NPC target, ref int damage, ref float knockBack, ref bool crit){
            target.AddBuff(24,200);
        }
        public override void HoldItem(Player player){
            Lighting.AddLight(player.position, 0.4f, 0.1f, 0.1f);
        }
        public override void AddRecipes(){
            var rec = new ModRecipe(mod);
            rec.AddIngredient(ModContent.ItemType<RedBar>(), 10);
            rec.AddIngredient(ModContent.ItemType<RedEssence>(), 5);
            rec.AddTile(TileID.Anvils);
            rec.SetResult(this);
            rec.AddRecipe();
        }
    }
}