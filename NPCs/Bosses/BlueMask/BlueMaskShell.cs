using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace TheMasks.NPCs.Bosses.BlueMask{
    [AutoloadBossHead]
    public class BlueMaskShell : ModNPC {
        //animation
        public int animation;
        int fram;
        int counter;
        int eyeFram=0, eyeFramTime=0;
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
        float X = 0;
        float Y = 0;
        int mode = 0;
        int nextmode = 0;
        int att_count = 0;
        ModNPC eyeType = ModContent.GetModNPC(ModContent.NPCType<NPCs.Bosses.BlueMask.BlueMaskEyes>());
        
        //other entities
        //ModNPC EyeType;
        //NPC EyeR, EyeL;

        public override void SetStaticDefaults(){
            DisplayName.SetDefault("The Blue Mask");
            Main.npcFrameCount[npc.type] = 4;
        }
        public override void SetDefaults(){
            npc.width = 64;
            npc.height = 62;
            npc.damage = 45;
            npc.lifeMax = 6500;
            npc.defense = 24;
            npc.noGravity = true;
            npc.boss = true;

            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            music = MusicID.Plantera;

            npc.lavaImmune = true;
            npc.noTileCollide = true;
            npc.netAlways = true;

            fram = 0;
            animation = 0;
            counter = 0;
            eyeFram = 0;
            eyeFramTime =0;

            targX = 0;
            targY = 0;
            mode = 0;
            nextmode = 0;
            speedMax = 8;
            acc = 2;
            speed = 0;
            att_count = 0;
            power_scale = 1f;
            npc.ai[0] = 0;

            //bossBag = mod.ItemType("RedMaskBag");
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale){
            npc.lifeMax = (int)(npc.lifeMax*power_scale) + numPlayers*2200;
            power_scale = 1.5f;
            npc.defense = (int)(npc.defense*power_scale);
            npc.damage = (int)(npc.damage*power_scale);
            speedMax = (int)(speedMax*power_scale);
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor){
            //draw back
            Texture2D MaskTexture = mod.GetTexture("NPCs/Bosses/BlueMask/BlueMask");
            Vector2 MaskPos = npc.Center - Main.screenPosition;
            MaskPos.Y -= 26;
            spriteBatch.Draw(MaskTexture, MaskPos,
                MaskTexture.Frame(), drawColor, npc.rotation,
                MaskTexture.Size() /2, npc.scale, SpriteEffects.None, 1f);
            //eyes looking
            Vector2 n = new Vector2(npc.position.X+1,npc.position.Y);
            int Yofset=0, Xofset=0;
            if(pX > n.X+(50*(d/100))){Xofset = 2;}
            else if(pX < n.X-(50*(d/100))){Xofset = -2;}
            else{Xofset = 0;}
            if(pY > n.Y+(50*(d/100))){Yofset = 2;}
            else if(pY < n.Y-(50*(d/100))){Yofset = -2;}
            else{Yofset = 0;}
            //draw eyes
            Texture2D EyeTexture = mod.GetTexture("NPCs/Bosses/BlueMask/BlueMaskEyes");
            Vector2 EyePos = npc.Center - Main.screenPosition;
            EyePos.Y -= 36 - Yofset;
            EyePos.X -= 26 - Xofset;
            spriteBatch.Draw(EyeTexture, EyePos,
                new Rectangle(0,32*eyeFram,32,32), drawColor, npc.rotation,
                new Vector2(16,16), npc.scale, SpriteEffects.None, 1f);
            EyePos = npc.Center - Main.screenPosition;
            EyePos.Y -= 36 - Yofset;
            EyePos.X += 26 + Xofset;
            spriteBatch.Draw(EyeTexture, EyePos,
                new Rectangle(0,32*eyeFram,32,32), drawColor, npc.rotation,
                new Vector2(16,16), npc.scale, SpriteEffects.None, 1f);
            return true;
        }
        
        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor){
            //draw gem
            Texture2D MaskTexture = mod.GetTexture("NPCs/Bosses/BlueMask/BlueGem");
            Vector2 GemPos = npc.Center - Main.screenPosition;
            GemPos.Y -= 56;
            spriteBatch.Draw(MaskTexture, GemPos,
                MaskTexture.Frame(), drawColor, npc.rotation,
                MaskTexture.Size() /2, npc.scale, SpriteEffects.None, 1f);
        }
        
        public override void AI(){
            //looks
            Lighting.AddLight(npc.position, 0.4f, 0.4f, 2f);
            //buff resistence
            if(npc.HasBuff(44)) {npc.DelBuff(npc.FindBuffIndex(44)); npc.life+=(int)(10*power_scale);}
            if(npc.HasBuff(46)) {npc.DelBuff(npc.FindBuffIndex(46)); npc.life+=(int)(10*power_scale);}
            //player targeting
            Player player = Main.player[npc.target];
            pX = player.Center.X;
            pY = player.Center.Y;
            d = (float)(Math.Sqrt((pX-X)*(pX-X) + (pY-Y)*(pY-Y)));
            X = npc.Center.X;
            Y = npc.Center.Y;
            //speed control
            if(d>=1500){
                speed = Math.Min(90,speed+10);
            }
            else if(speed >= 40) {
                speed = Math.Max(speedMax,speed-20);
            }
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
                    targX = (float)((X-pX)*Math.Cos(0.5f) - (Y-pY)*Math.Sin(0.5f) + pX);
                    targY = (float)((X-pX)*Math.Sin(0.5f) + (Y-pY)*Math.Cos(0.5f) + pY);
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
                        nextmode = (int)Math.Ceiling(Main.rand.NextFloat(0,3));;
                        switch(nextmode){
                            case 1:{
                                Task.Delay(new TimeSpan(0, 0, 6)).ContinueWith(o => { 
                                    mode = 1;
                                    npc.velocity = new Vector2 (0,0);
                                    animation = 1;
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
                                    animation = 1;
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
                            case 3:{
                                if(npc.ai[0]>=(5*power_scale)) {nextmode =0; mode = 0;}
                                else{
                                    Task.Delay(new TimeSpan(0, 0, 8)).ContinueWith(o => { 
                                        mode = 3;
                                        npc.velocity = new Vector2 (0,0);
                                        animation = 1;
                                        Task.Delay(new TimeSpan(0, 0, 1)).ContinueWith(o2 => { 
                                            nextmode=0;
                                        });
                                    });
                                }
                            }break;
                        }
                    }
                break;}
                case 1:{ // dash
                    if(nextmode==0){
                        float td = (float)(Math.Sqrt((targX-X)*(targX-X) + (targY-Y)*(targY-Y)));
                        speed = (int)(16*power_scale);
                        if(td != 0){
                            Vector2 targPos;
                            targPos.X = targX;
                            targPos.Y = targY;
                            npc.velocity = npc.DirectionTo(targPos)*speed;
                        }
                        if(td<=20 || d>=800){
                            animation = 3;
                            mode = 0;
                        }  
                    }
                }break;
                case 2:{ // fire
                    if(nextmode==0){
                        att_count+=1;
                        if((att_count%15)==0){
                            Vector2 shootPos = npc.Center;
                            shootPos.Y -= 24;
                            float ame = (10f-(npc.ai[0]*0.5f)) / power_scale;
                            Vector2 shootVel = player.Center - shootPos + new Vector2(Main.rand.NextFloat(-ame,ame),Main.rand.NextFloat(-ame,ame));
                            shootVel.Normalize();
                            shootVel *= 10f;
                            Projectile.NewProjectile(shootPos,shootVel,mod.ProjectileType("BadBlueFire"),(int)(35*power_scale),1f);
                        }
                        if((att_count%60)==0){
                            Vector2 shootPos = npc.Center;
                            shootPos.Y -= 24;
                            for(int i = 1; i<5; i++){
                                float ame = (20f-(npc.ai[0]*0.5f))*i / power_scale;
                                Vector2 shootVel = player.Center - shootPos + new Vector2(Main.rand.NextFloat(-ame,ame),Main.rand.NextFloat(-ame,ame));
                                shootVel.Normalize();
                                shootVel *= 10f;
                                Projectile.NewProjectile(shootPos,shootVel,mod.ProjectileType("BadBlueFire"),(int)(35*power_scale),1f);
                            }
                        }
                        if(att_count>=70){
                            att_count = 0;
                            animation = 3;
                            mode = 0; 
                        }
                    }
                }break;
                case 3:{ // spawn eye
                    if(nextmode==0){
                        int eye = eyeType.SpawnNPC((int)(npc.Center.X/16), (int)(npc.Center.Y/16));
                        Main.npc[eye].ai[0] = npc.whoAmI;
                        npc.ai[0] += 1;
                        animation = 3;
                        mode = 0;
                        //Main.NewText(((int)npc.ai[0]).ToString(), 150, 250, 150);
                    }
                }break;
            }
            //animation logic
            switch(animation){
                case 0:{
                    npc.defense = (int)(24*power_scale);
                    npc.damage = (int)(45*power_scale);
                break;}
                case 1: case 3:{
                    npc.defense = (int)(20*power_scale);
                    npc.damage = (int)(50*power_scale);
                break;}
                case 2:{
                    npc.defense = (int)(10*power_scale);
                    npc.damage = (int)(55*power_scale);
                break;}
            }
            if(player.dead){
                npc.active = false;
                npc.velocity = new Vector2(0,-18);
            }
        }

        public override void BossLoot(ref string name, ref int potionType){
            //MyBG.life = 0;
            if(!Main.expertMode){
                Item.NewItem((int)npc.position.X,(int)npc.position.Y,npc.width,npc.height,mod.ItemType("BlueEssence"),Main.rand.Next(10,15));
                Item.NewItem((int)npc.position.X,(int)npc.position.Y,npc.width,npc.height,ItemID.CopperCoin,Main.rand.Next(50,90));
                Item.NewItem((int)npc.position.X,(int)npc.position.Y,npc.width,npc.height,ItemID.SilverCoin,Main.rand.Next(30,60));
                Item.NewItem((int)npc.position.X,(int)npc.position.Y,npc.width,npc.height,ItemID.GoldCoin,1);
            }
            else{
                Item.NewItem((int)npc.position.X,(int)npc.position.Y,npc.width,npc.height,mod.ItemType("BlueEssence"),Main.rand.Next(12,16));
                Item.NewItem((int)npc.position.X,(int)npc.position.Y,npc.width,npc.height,ItemID.CopperCoin,Main.rand.Next(60,95));
                Item.NewItem((int)npc.position.X,(int)npc.position.Y,npc.width,npc.height,ItemID.SilverCoin,Main.rand.Next(50,80));
                Item.NewItem((int)npc.position.X,(int)npc.position.Y,npc.width,npc.height,ItemID.GoldCoin,1);
            }
            potionType = 499;
        }
        public override void FindFrame(int frameHeight){
            //anim. 0 = closed
            //anim. 1 = opening
            //anim. 2 = opened
            //anim. 3 = closing
            switch(animation){
                case 0:{
                    fram = 0;
                }break;
                case 1:{
                    counter++;
                    if(counter>=12){
                        fram++;
                        counter = 0;
                        if(fram>=3){
                            counter = 0;
                            animation = 2;
                        }
                    }
                }break;
                case 2:{
                    fram = 3;
                }break;
                case 3:{
                    counter++;
                    if(counter>=12){
                        fram--;
                        counter = 0;
                        if(fram<=0){
                            counter = 0;
                            animation = 0;
                        }
                    }
                }break;
            }
            npc.frame.Y = frameHeight * fram;
            //Eyes
            eyeFramTime++;
            if(eyeFramTime>=15){
                eyeFram = (eyeFram==0? 1 : 0);
                eyeFramTime = 0;
            }
        } 
    }
}