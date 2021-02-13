using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Threading.Tasks;

namespace TheMasks.NPCs.Bosses.RedMask{ 
    public class BadRedFire: ModProjectile{
        float power_scale = 1f;
        public override void SetDefaults(){
            projectile.Name = "bad red fire";
            projectile.width = 32;
            projectile.height = 34;
            if(Main.hardMode) power_scale *= 1.2f;
            if(Main.expertMode) power_scale *= 1.5f;
            
            projectile.magic = true;
            projectile.hostile = true;
            projectile.friendly = false;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
            Task.Delay(new TimeSpan(0, 0, 0, 0, 100)).ContinueWith(o => { projectile.tileCollide = true;});
            Main.projFrames[projectile.type] = 4;
            
            projectile.timeLeft = (int)(100*power_scale);
            projectile.penetrate = 1;
        }
        public override void OnHitPlayer(Player target, int damage, bool crit){
            target.AddBuff(24,(int)(300*power_scale));
            projectile.timeLeft = 1;
        }
        public override void AI() {
            Lighting.AddLight(projectile.position, 0.6f, 0.1f, 0.1f);
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.5f;
            if (Main.rand.NextBool(4)) {
                Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, mod.DustType("RedFire"), projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f);
            }
        }
        public override void Kill(int timeLeft){
            Collision.HitTiles(projectile.position, projectile.velocity, projectile.width/4, projectile.height/4); 
            for(int i=0;i<6;i++) Dust.NewDust(projectile.position, projectile.width, projectile.height, mod.DustType("RedFire"),  Main.rand.NextFloat(-8,8), Main.rand.NextFloat(-8,8));
            Main.PlaySound(SoundID.DD2_LightningBugZap, projectile.position); 
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor){
            projectile.frameCounter++;
            if(projectile.frameCounter>=8){
                projectile.frame++;
                projectile.frameCounter = 0;
                if(projectile.frame>Main.projFrames[projectile.type]-1){
                    projectile.frame = 0;
                }
            }
            return true;
        }
    }
}
//https://terraria.gamepedia.com/Debuffs