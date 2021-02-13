using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System;
using System.Threading.Tasks;

namespace TheMasks.NPCs.Bosses.BlueMask{
    public class BlueMaskEyes : ModNPC {
        int frame = 0;
        int counter = 0;

        //stats
        float power_scale = 1f;
        float speedMax = 0;
        float acc = 0;
        float speed = 0;
        //targeting
        float pX = 0;
        float pY = 0;
        float d = 0;
        float targX = 0;
        float targY = 0;
        int mode = 0;
        int nextmode = 0;
        int att_count = 0;

        public override void SetStaticDefaults(){
            DisplayName.SetDefault("Blue Eye");
            Main.npcFrameCount[npc.type] = 2;
        }
        public override void SetDefaults(){
            npc.width = 16;
            npc.height = 16;
            npc.damage = 25;
            npc.lifeMax = 500;
            npc.defense = 6;
            npc.noGravity = true;

            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;

            npc.lavaImmune = true;
            npc.noTileCollide = true;

            frame = 0;
            counter = 0;

            targX = 0;
            targY = 0;
            mode = 0;
            nextmode = 0;
            speedMax = 10;
            acc = 1;
            speed = 0;
            att_count = 0;
            power_scale = 1f;
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale){
            npc.lifeMax = 600 + numPlayers*150;
            power_scale = 1.5f;
            npc.defense = (int)(npc.defense*power_scale);
            speedMax = (int)(speedMax*power_scale);
        }

        public override void NPCLoot(){
            NPC boss = Main.npc[(int)npc.ai[0]];
            if(boss.type == mod.NPCType("BlueMaskShell")){
                boss.ai[0] -= 1;
            }
        }
        public override void AI(){
            //looks
            Lighting.AddLight(npc.position, 0.1f, 0.1f, 1f);
            //buff resistence
            if(npc.HasBuff(46)) npc.DelBuff(npc.FindBuffIndex(46));
            if(npc.HasBuff(44)) npc.DelBuff(npc.FindBuffIndex(44));
            //player targeting
            Player player = Main.player[npc.target];
            pX = player.Center.X;
            pY = player.Center.Y;
            float X = npc.Center.X;
            float Y = npc.Center.Y;
            d = (float)(Math.Sqrt((pX-X)*(pX-X) + (pY-Y)*(pY-Y)));
            //speed control
           
            if(speedMax > speed){
                speed = Math.Min(speedMax,speed+acc);
            }
            else if(speedMax<speed){
                speed = Math.Max(speedMax,speed-acc);
            }
            //mode logic
            switch(mode){
                case 0:{ // curcling
                    if(npc.life > npc.lifeMax) npc.life = npc.lifeMax; // no life overflow
                    targX = (float)((X-pX)*Math.Cos(0.4f) - (Y-pY)*Math.Sin(0.4f) + pX);
                    targY = (float)((X-pX)*Math.Sin(0.4f) + (Y-pY)*Math.Cos(0.4f) + pY);
                    float PtoT = (float)(Math.Sqrt((pX-targX)*(pX-targX) + (pY-targY)*(pY-targY)));
                    if(PtoT!=200){
                        targX = pX + (targX-pX)*(200/PtoT);
                        targY = pY + (targY-pY)*(200/PtoT);
                    }
                    float td = (float)(Math.Sqrt((targX-X)*(targX-X) + (targY-Y)*(targY-Y)));
                    if(td != 0){
                        Vector2 targPos;
                        targPos.X = targX;
                        targPos.Y = targY;
                        npc.velocity = npc.DirectionTo(targPos)*speed;
                    }
                    // next attack
                    if(nextmode==0){
                        nextmode = (int)Math.Ceiling(Main.rand.NextFloat(0,2));
                        switch(nextmode){
                            case 1:{
                                Task.Delay(new TimeSpan(0, 0, 6)).ContinueWith(o => { 
                                    mode = 1;
                                    npc.velocity = new Vector2 (0,0);
                                    Task.Delay(new TimeSpan(0, 0, 1)).ContinueWith(o2 => { 
                                        X = npc.Center.X;
                                        Y = npc.Center.Y;
                                        pX = player.Center.X;
                                        pY = player.Center.Y;
                                        targX = (float)(pX-(X-pX)*1.1);
                                        targY = (float)(pY-(Y-pY)*1.1);
                                        nextmode=0;
                                    });
                                });
                            }break;
                            case 2:{
                                Task.Delay(new TimeSpan(0, 0, 6)).ContinueWith(o => { 
                                    mode = 2;
                                    npc.velocity = new Vector2 (0,0);
                                    Task.Delay(new TimeSpan(0, 0, 1)).ContinueWith(o2 => { 
                                        X = npc.Center.X;
                                        Y = npc.Center.Y;
                                        pX = player.Center.X;
                                        pY = player.Center.Y;
                                        att_count = 0;
                                        nextmode=0;
                                    });
                                });
                            }break;
                        }
                    }
                }break;
                case 1:{ // dash
                    if(nextmode==0){
                        float td = (float)(Math.Sqrt((targX-X)*(targX-X) + (targY-Y)*(targY-Y)));
                        speed = (int)(20*power_scale);
                        if(td != 0){
                            Vector2 targPos;
                            targPos.X = targX;
                            targPos.Y = targY;
                            npc.velocity = npc.DirectionTo(targPos)*speed;
                        }
                        if(td<=20 || d>=500){
                            mode = 0;
                        }  
                    }
                }break;
                case 2:{ // fire
                    if(nextmode==0){
                        att_count+=1;
                        if((att_count%20)==0){
                            Vector2 shootPos = npc.Center;
                            float ame = 15f / power_scale;
                            Vector2 shootVel = player.Center - shootPos + new Vector2(Main.rand.NextFloat(-ame,ame),Main.rand.NextFloat(-ame,ame));
                            shootVel.Normalize();
                            shootVel *= 9f;
                            Projectile.NewProjectile(shootPos,shootVel,mod.ProjectileType("BadBlueFire"),(int)(15*power_scale),1f);
                        }
                        if((att_count%60)==0){
                            Vector2 shootPos = npc.Center;
                            for(int i = 1; i<3; i++){
                                float ame = (30f*i) / power_scale;
                                Vector2 shootVel = player.Center - shootPos + new Vector2(Main.rand.NextFloat(-ame,ame),Main.rand.NextFloat(-ame,ame));
                                shootVel.Normalize();
                                shootVel *= 9f;
                                Projectile.NewProjectile(shootPos,shootVel,mod.ProjectileType("BadBlueFire"),(int)(15*power_scale),1f);
                            }
                        }
                        if(att_count>=70){
                            att_count = 0;
                            mode = 0; 
                        }
                    }
                }break;
            }
        }
        
        public override void FindFrame(int frameHeight){
            if(frame == 0){
                counter+=1;
                if(counter >= 15) {
                    frame = 1;
                    counter = 0;
                }
            }
            else{
                counter+=1;
                if(counter >= 15) {
                    frame = 0;
                    counter = 0;
                    NPC boss = Main.npc[(int)npc.ai[0]];
                    if(boss.type == mod.NPCType("BlueMaskShell")){
                        if(npc.life>2) npc.life -= 2;
                    }
                    else{
                        npc.life -= 10;
                    }
                    
                }
            }
            npc.frame.Y = frameHeight * frame;
            
        }
    }
}