using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;

namespace TheMasks.Items.PurpleMask{
    public class PurpleSword : ModItem{
        public override void SetStaticDefaults(){
            DisplayName.SetDefault("Sword of Unity Purple");
            //Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(5, 4));
        }
        public override void SetDefaults(){
            //hitbox size
            item.width = 110;
            item.height = 124;
            
            //stack & value
            item.maxStack = 1;
            item.value = Item.sellPrice(gold: 3);
            item.rare = ItemRarityID.Pink;
            item.material = true;

            // Usage
            item.useTime = 32;
            item.useAnimation = 24;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.useTurn = true;
            item.autoReuse = true;

            //stats
            item.damage = 120;
            item.knockBack = 10;
            item.melee = true;
        }
        public override void MeleeEffects(Player player, Rectangle hitbox){
            Vector2 xy;
            xy.X = hitbox.X;
            xy.Y = hitbox.Y;
            Lighting.AddLight(xy, 0.6f, 0.1f, 0.6f);
            Dust.NewDust(xy, hitbox.Width, hitbox.Height, mod.DustType("PurpleFire"), 1.5f, 1.5f);
        }
        public override void UseStyle(Player player){
            float cosRot = (float)Math.Cos(player.itemRotation);
            float sinRot = (float)Math.Sin(player.itemRotation);
            //Align
            player.itemLocation.X = player.itemLocation.X + (-8 * cosRot * player.direction) + (-15 * sinRot * player.gravDir);
            player.itemLocation.Y = player.itemLocation.Y + (-8 * sinRot * player.direction) - (-15 * cosRot * player.gravDir);
		}
        public override void ModifyHitNPC(Player player, NPC target, ref int damage, ref float knockBack, ref bool crit){
            target.AddBuff(24,400);
            target.AddBuff(44,400);
            target.AddBuff(46,400);
            target.AddBuff(BuffID.Poisoned,200);
        }
        public override void HoldItem(Player player){
            Lighting.AddLight(player.position, 0.4f, 0.1f, 0.4f);
        }
        public override void AddRecipes(){
            var rec = new ModRecipe(mod);
            rec.AddIngredient(ModContent.ItemType<BlueMask.BlueSword>(), 1);
            rec.AddIngredient(ModContent.ItemType<RedMask.RedSword>(), 1);
            rec.AddIngredient(ItemID.HallowedBar, 10);
            rec.AddTile(TileID.Anvils);
            rec.SetResult(this);
            rec.AddRecipe();
        }
        public override bool UseItemFrame(Player player){
            return true;
        }
    }
}