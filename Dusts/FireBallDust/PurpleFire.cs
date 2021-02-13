using Microsoft.Xna.Framework;
using Terraria;
//using Terraria.ID;
using Terraria.ModLoader;
//using Microsoft.Xna.Framework.Graphics;

namespace TheMasks.Dusts.FireBallDust{
    class PurpleFire : ModDust{
        public override void OnSpawn(Dust dust){
            dust.noGravity = false;
            dust.noLight = false;
        }
        public override bool Update(Dust dust){
            Lighting.AddLight(dust.position, 0.8f, 0.2f, 0.8f);
            dust.scale -= 0.01f;
            if (dust.scale < 0.75f){
                dust.active = false;
            }
            return false;
		}
        public override Color? GetAlpha(Dust dust, Color lightColor){
            return new Color(lightColor.R, lightColor.G, lightColor.B, 25);
        }
    }
}