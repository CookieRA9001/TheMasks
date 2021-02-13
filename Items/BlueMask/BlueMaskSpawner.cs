using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
//using Microsoft.Xna.Framework;

namespace TheMasks.Items.BlueMask{
    public class BlueMaskSpawner : ModItem{
        public override void SetStaticDefaults(){
            DisplayName.SetDefault("Blue Mask Summon");
            Tooltip.SetDefault("Summon The Blue Mask");
        }
        public override void SetDefaults(){
            //hitbox size
            item.width = 16;
            item.height = 16;
            //stack & value
            item.maxStack = 99;
            item.value = Item.sellPrice(silver: 10);
            item.rare = ItemRarityID.Orange;
            item.consumable = true;
            //item.material = true;
            // Usage
            item.useTime = 45;
            item.useAnimation = 45;
            item.useStyle = ItemUseStyleID.HoldingOut;
            //item.useTurn = true;
            //item.autoReuse = true;
            //item.createTile = ModContent.TileType<Tiles.RedMask.RedOre>();
        }
        public override bool CanUseItem(Player player){
            return !NPC.AnyNPCs(mod.NPCType("BlueMaskShell"));
        }
        public override bool UseItem(Player player){
            Main.PlaySound(SoundID.Roar, player.position);
            if(Main.netMode != 1){
                NPC.SpawnOnPlayer(player.whoAmI, mod.NPCType("BlueMaskShell"));
            }
            return true;
        }

        public override void AddRecipes(){
            var rec = new ModRecipe(mod);
            rec.AddIngredient(ModContent.ItemType<BlueBar>(), 5);
            rec.AddTile(TileID.Anvils);
            rec.SetResult(this);
            rec.AddRecipe();
        }
    }
}