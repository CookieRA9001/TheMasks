using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Threading.Tasks;

namespace TheMasks.Items.Projectiles{ 
    public class BlueFire: ModProjectile{
        public int RandomNumber(int min, int max){  
            Random _random = new Random();   
            return _random.Next(min, max);  
        } 
        public override void SetDefaults(){
            projectile.Name = "blue fire";
            projectile.width = 32;
            projectile.height = 34;

            projectile.friendly = true;
            projectile.penetrate = 1;
            Main.projFrames[projectile.type] = 4;
            projectile.magic = true;
            projectile.hostile = false;
            projectile.tileCollide = false;
            Task.Delay(new TimeSpan(0, 0, 0, 0, 80)).ContinueWith(o => { projectile.tileCollide = true;});
            projectile.ignoreWater = true;

            projectile.timeLeft = 100;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit){
            target.AddBuff(44,300);
            target.AddBuff(46,300);
            Player p = Main.player[projectile.owner];
            int manaBack = damage/10;             
            p.statMana +=manaBack;
            p.ManaEffect(manaBack);
        }
        public override void AI() {
            Lighting.AddLight(projectile.position, 0.2f, 0.2f, 0.8f);
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.5f;
            if (Main.rand.NextBool(3)) {
                Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, mod.DustType("BlueFire"), projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f);
            }
        }
        public override void Kill(int timeLeft){
            Collision.HitTiles(projectile.position, projectile.velocity, projectile.width/2, projectile.height/2); 
            for(int i=0;i<10;i++) Dust.NewDust(projectile.position, projectile.width, projectile.height, mod.DustType("BlueFire"),  (float)RandomNumber(-10,10), (float)RandomNumber(-10,10));
            Main.PlaySound(SoundID.DD2_LightningBugZap, projectile.position); 
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor){
            projectile.frameCounter++;
            if(projectile.frameCounter>=10){
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