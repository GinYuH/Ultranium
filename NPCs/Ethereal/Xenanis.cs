using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Ultranium.Items.Ice;
using Ultranium.Items.Vanity.BossMasks;
using Ultranium.Tiles.Trophy;

namespace Ultranium.NPCs.Ethereal;

[AutoloadBossHead]
public class Xenanis : ModNPC
{
	private int timer;

	private int timer2;

	private int moveSpeed;

	private int moveSpeedY;

	private float HomeY = 100f;

	public bool attacking;

	public static bool Clones;

	public static bool ClonesSpawned;

	public bool Phase2;

	public bool Transition;

	public int players;

	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Xenanis");
		Main.npcFrameCount[NPC.type] = 12;
		NPCID.Sets.TrailCacheLength[NPC.type] = 10;
		NPCID.Sets.TrailingMode[NPC.type] = 0;
	}

	public override void SetDefaults()
	{
		NPC.width = 130;
		NPC.height = 156;
		NPC.damage = 40;
		NPC.lifeMax = 52000;
		NPC.defense = 50;
		NPC.knockBackResist = 0f;
		NPC.boss = true;
		NPC.noGravity = true;
		NPC.noTileCollide = true;
		NPC.HitSound = SoundID.NPCHit7;
		NPC.DeathSound = SoundID.NPCDeath10;
		NPC.value = Item.buyPrice(0, 15);
		NPC.npcSlots = 1f;
		NPC.lavaImmune = true;
		NPC.alpha = 0;
		NPC.buffImmune[24] = true;
		Music = MusicLoader.GetMusicSlot(Mod, "Sounds/Music/Xenanis");
		NPC.netAlways = true;
		NPC.aiStyle = -1;
		players = 1;
	}

	public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
	{
		NPC.lifeMax = (int)(NPC.lifeMax * 0.75f * balance * bossAdjustment);
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		if (NPC.velocity != Vector2.Zero)
		{
			Vector2 vector = new Vector2((float)ModContent.Request<Texture2D>("Ultranium/NPCs/Ethereal/XenanisClone1").Width() * 0.5f, (float)NPC.height * 0.5f);
			for (int i = 0; i < NPC.oldPos.Length; i++)
			{
				SpriteEffects effects = ((NPC.direction != -1) ? SpriteEffects.FlipHorizontally : SpriteEffects.None);
				Vector2 position = NPC.oldPos[i] - Main.screenPosition + vector + new Vector2(0f, NPC.gfxOffY);
				Color color = NPC.GetAlpha(drawColor) * ((float)(NPC.oldPos.Length - i) / (float)NPC.oldPos.Length / 2f);
				spriteBatch.Draw(ModContent.Request<Texture2D>("Ultranium/NPCs/Ethereal/XenanisClone1").Value, position, NPC.frame, color, NPC.rotation, vector, NPC.scale, effects, 0f);
			}
		}
		return true;
	}

	public override void FindFrame(int frameHeight)
	{
		if (!attacking)
		{
			NPC.frameCounter += 1.0;
			if (NPC.frameCounter > 6.0)
			{
				NPC.frame.Y = NPC.frame.Y + frameHeight;
				NPC.frameCounter = 0.0;
			}
			if (NPC.frame.Y >= frameHeight * 6)
			{
				NPC.frame.Y = 0;
			}
		}
		if (attacking)
		{
			NPC.frameCounter += 1.0;
			if (NPC.frameCounter > 6.0)
			{
				NPC.frame.Y = NPC.frame.Y + frameHeight;
				NPC.frameCounter = 0.0;
			}
			if (NPC.frame.Y >= frameHeight * 12)
			{
				NPC.frame.Y = frameHeight * 6;
			}
		}
	}

	public override bool PreAI()
	{
		NPC.spriteDirection = NPC.direction;
		NPC.rotation = NPC.velocity.X * 0.02f;
		Player player = Main.player[NPC.target];
		NPC.netUpdate = true;
		NPC.TargetClosest();
		int num = (Main.expertMode ? 30 : 45);
		if (Main.player[NPC.target].dead || Main.dayTime)
		{
			NPC.ai[0] += 1f;
			NPC.velocity.Y = 40f;
			NPC.ai[3] += 1f;
			if (NPC.ai[3] >= 100f)
			{
				NPC.active = false;
			}
		}
		if (NPC.ai[0] == 0f)
		{
			if (NPC.Center.X >= player.Center.X && moveSpeed >= -53)
			{
				moveSpeed--;
			}
			else if (NPC.Center.X <= player.Center.X && moveSpeed <= 53)
			{
				moveSpeed++;
			}
			NPC.velocity.X = (float)moveSpeed * 0.09f;
			if (NPC.Center.Y >= player.Center.Y - HomeY && moveSpeedY >= -30)
			{
				moveSpeedY--;
				HomeY = 150f;
			}
			else if (NPC.Center.Y <= player.Center.Y - HomeY && moveSpeedY <= 30)
			{
				moveSpeedY++;
			}
			NPC.velocity.Y = (float)moveSpeedY * 0.1f;
		}
		if (NPC.ai[0] == 1f)
		{
			if (NPC.Center.X >= player.Center.X && moveSpeed >= -100)
			{
				moveSpeed--;
			}
			else if (NPC.Center.X <= player.Center.X && moveSpeed <= 100)
			{
				moveSpeed++;
			}
			NPC.velocity.X = (float)moveSpeed * 0.1f;
			if (NPC.Center.Y >= player.Center.Y - HomeY && moveSpeedY >= -30)
			{
				moveSpeedY--;
				HomeY = 150f;
			}
			else if (NPC.Center.Y <= player.Center.Y - HomeY && moveSpeedY <= 30)
			{
				moveSpeedY++;
			}
			NPC.velocity.Y = (float)moveSpeedY * 0.12f;
		}
		if (NPC.ai[0] == 2f)
		{
			NPC.velocity *= 0f;
		}
		if (NPC.life > NPC.lifeMax / 2 && !Transition)
		{
			timer++;
			if (timer == 120 || timer == 150 || timer == 180 || timer == 210 || timer == 240 || timer == 270 || timer == 300)
			{
				NPC.ai[0] = 0f;
				float num2 = 6.5f;
				int num3 = 299;
				SoundEngine.PlaySound(SoundID.Item20, new Vector2(NPC.position.X, NPC.position.Y));
				float num4 = (float)Math.Atan2(NPC.Center.Y - player.Center.Y, NPC.Center.X - player.Center.X);
				Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, (float)(Math.Cos(num4) * (double)num2 * -1.0), (float)(Math.Sin(num4) * (double)num2 * -1.0), num3, num, 0f, Main.myPlayer, 0f, 0f);
			}
			if (timer == 360 || timer == 450 || timer == 540 || timer == 630 || timer == 720)
			{
				NPC.ai[0] = 2f;
				for (int i = 0; i < 50; i++)
				{
					int num5 = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.PurpleTorch);
					Main.dust[num5].scale = 1.5f;
				}
				int num6 = Main.rand.Next(4);
				if (num6 == 0)
				{
					NPC.position.X = player.position.X + 500f;
					NPC.position.Y = player.position.Y + 300f;
				}
				if (num6 == 1)
				{
					NPC.position.X = player.position.X + 500f;
					NPC.position.Y = player.position.Y - 400f;
				}
				if (num6 == 2)
				{
					NPC.position.X = player.position.X - 600f;
					NPC.position.Y = player.position.Y - 400f;
				}
				if (num6 == 3)
				{
					NPC.position.X = player.position.X - 600f;
					NPC.position.Y = player.position.Y + 300f;
				}
				for (int j = 0; j < 50; j++)
				{
					int num7 = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.PurpleTorch);
					Main.dust[num7].scale = 1.5f;
				}
			}
			if (timer == 380 || timer == 470 || timer == 560 || timer == 650 || timer == 740)
			{
				for (int k = -2; k <= 2; k++)
				{
					Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, 7.5f * NPC.DirectionTo(player.Center).RotatedBy(MathHelper.ToRadians(6f) * (float)k), Mod.Find<ModProjectile>("EtherealBlast").Type, num, 0f, Main.myPlayer, 0f, 0f);
				}
			}
			if (timer == 800)
			{
				for (int l = 0; l < 50; l++)
				{
					int num8 = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.PurpleTorch);
					Main.dust[num8].scale = 1.5f;
				}
				NPC.position.X = player.position.X - 50f;
				NPC.position.Y = player.position.Y - 400f;
				for (int m = 0; m < 50; m++)
				{
					int num9 = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.PurpleTorch);
					Main.dust[num9].scale = 1.5f;
				}
			}
			if (timer == 860)
			{
				Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X + 100f, NPC.Center.Y + 100f, 0f, 0f, Mod.Find<ModProjectile>("EtherealPortalSmall").Type, num, 1f, Main.myPlayer, 0f, 0f);
				Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X + 100f, NPC.Center.Y - 100f, 0f, 0f, Mod.Find<ModProjectile>("EtherealPortalSmall").Type, num, 1f, Main.myPlayer, 0f, 0f);
				Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X - 100f, NPC.Center.Y + 100f, 0f, 0f, Mod.Find<ModProjectile>("EtherealPortalSmall").Type, num, 1f, Main.myPlayer, 0f, 0f);
				Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X - 100f, NPC.Center.Y - 100f, 0f, 0f, Mod.Find<ModProjectile>("EtherealPortalSmall").Type, num, 1f, Main.myPlayer, 0f, 0f);
			}
			if (timer == 950)
			{
				NPC.ai[0] = 1f;
			}
			if (timer == 1010 || timer == 1070 || timer == 1130 || timer == 1190 || timer == 1250)
			{
				float num10 = 10f;
				int num11 = Mod.Find<ModProjectile>("EtherealFireBall").Type;
				SoundEngine.PlaySound(SoundID.Item20, new Vector2(NPC.position.X, NPC.position.Y));
				float num12 = (float)Math.Atan2(NPC.Center.Y - player.Center.Y, NPC.Center.X - player.Center.X);
				Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, (float)(Math.Cos(num12) * (double)num10 * -1.0), (float)(Math.Sin(num12) * (double)num10 * -1.0), num11, num, 0f, Main.myPlayer, 0f, 0f);
			}
			if (timer == 1340)
			{
				NPC.ai[0] = 2f;
				for (int n = 0; n < 50; n++)
				{
					int num13 = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.PurpleTorch);
					Main.dust[num13].scale = 1.5f;
				}
				NPC.position.X = player.position.X - 100f;
				NPC.position.Y = player.position.Y - 500f;
				for (int num14 = 0; num14 < 50; num14++)
				{
					int num15 = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.PurpleTorch);
					Main.dust[num15].scale = 1.5f;
				}
			}
			if (timer == 1370)
			{
				Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("EtherealLaserRift").Type, num, 1f, Main.myPlayer, 0f, 0f);
			}
			if (timer >= 1900)
			{
				timer = 0;
				NPC.ai[0] = 0f;
			}
		}
		if (NPC.localAI[1] == 0f && NPC.life <= NPC.lifeMax / 2)
		{
			Projectile.NewProjectile(NPC.GetSource_FromThis(), player.Center.X + 500f, player.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("PurpleCloneSpawner").Type, 0, 1f, Main.myPlayer, 0f, 0f);
			Projectile.NewProjectile(NPC.GetSource_FromThis(), player.Center.X - 500f, player.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("BlackCloneSpawner").Type, 0, 1f, Main.myPlayer, 0f, 0f);
			NPC.localAI[1] += 1f;
		}
		if (NPC.AnyNPCs(Mod.Find<ModNPC>("XenanisClone1").Type) || NPC.AnyNPCs(Mod.Find<ModNPC>("XenanisClone2").Type))
		{
			Transition = true;
			NPC.immortal = true;
			NPC.dontTakeDamage = true;
			NPC.position.X = player.position.X - 100f;
			NPC.position.Y = player.position.Y - 500f;
			timer = 0;
			timer2 = 0;
		}
		else
		{
			Transition = false;
			NPC.immortal = false;
			NPC.dontTakeDamage = false;
		}
		if (NPC.life <= NPC.lifeMax / 2 && !Transition)
		{
			timer = 0;
			timer2++;
			if (timer2 < 200 && timer2 > 60)
			{
				NPC.ai[0] = 1f;
			}
			if (timer2 == 80 || timer2 == 100 || timer2 == 120 || timer2 == 140 || timer2 == 160 || timer2 == 180)
			{
				float num16 = 12f;
				int num17 = Mod.Find<ModProjectile>("EtherealFireBall").Type;
				SoundEngine.PlaySound(SoundID.Item20, new Vector2(NPC.position.X, NPC.position.Y));
				float num18 = (float)Math.Atan2(NPC.Center.Y - player.Center.Y, NPC.Center.X - player.Center.X);
				Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, (float)(Math.Cos(num18) * (double)num16 * -1.0), (float)(Math.Sin(num18) * (double)num16 * -1.0), num17, num, 0f, Main.myPlayer, 0f, 0f);
			}
			if (timer2 == 240 || timer2 == 330 || timer2 == 420 || timer2 == 510)
			{
				NPC.ai[0] = 2f;
				for (int num19 = 0; num19 < 50; num19++)
				{
					int num20 = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.PurpleTorch);
					Main.dust[num20].scale = 1.5f;
				}
				NPC.position.X = player.position.X - 50f;
				NPC.position.Y = player.position.Y - 400f;
				for (int num21 = 0; num21 < 50; num21++)
				{
					int num22 = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.PurpleTorch);
					Main.dust[num22].scale = 1.5f;
				}
			}
			if (timer2 == 260 || timer2 == 350 || timer2 == 450)
			{
				SoundEngine.PlaySound(SoundID.Item103, NPC.position);
				Vector2 spinningpoint = new Vector2(5f, 0f).RotatedByRandom(Math.PI * 2.0);
				for (int num23 = 0; num23 < 12; num23++)
				{
					Vector2 vector = spinningpoint.RotatedBy(Math.PI / 3.0 * ((double)num23 + Main.rand.NextDouble() - 0.5));
					Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, vector, Mod.Find<ModProjectile>("EtherealBlast").Type, num, 0f, Main.myPlayer, 0f, 0f);
				}
			}
			if (timer2 == 540)
			{
				Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("EtherealPortalBig").Type, num + 30, 1f, Main.myPlayer, 0f, 0f);
			}
			if (timer2 == 720)
			{
				NPC.ai[0] = 1f;
			}
			if (timer2 == 740 || timer2 == 760 || timer2 == 780 || timer2 == 800 || timer2 == 820)
			{
				float num24 = 10f;
				int num25 = Mod.Find<ModProjectile>("EtherealPortalSmall").Type;
				SoundEngine.PlaySound(SoundID.Item20, new Vector2(NPC.position.X, NPC.position.Y));
				float num26 = (float)Math.Atan2(NPC.Center.Y - player.Center.Y, NPC.Center.X - player.Center.X);
				Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, (float)(Math.Cos(num26) * (double)num24 * -1.0), (float)(Math.Sin(num26) * (double)num24 * -1.0), num25, num, 0f, Main.myPlayer, 0f, 0f);
			}
			if (timer2 == 940)
			{
				NPC.ai[0] = 2f;
				for (int num27 = 0; num27 < 50; num27++)
				{
					int num28 = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.PurpleTorch);
					Main.dust[num28].scale = 1.5f;
				}
				NPC.position.X = player.position.X - 100f;
				NPC.position.Y = player.position.Y - 500f;
				for (int num29 = 0; num29 < 50; num29++)
				{
					int num30 = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.PurpleTorch);
					Main.dust[num30].scale = 1.5f;
				}
			}
			if (timer2 == 980)
			{
				Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("EtherealLaserRift2").Type, num + 10, 1f, Main.myPlayer, 0f, 0f);
			}
			if (timer2 == 1480)
			{
				timer2 = 0;
				NPC.ai[0] = 0f;
			}
		}
		if (timer < 800 || (timer > 950 && timer < 1340))
		{
			attacking = false;
		}
		if ((timer > 800 && timer < 950) || timer > 1340 || (timer2 >= 520 && timer2 < 720) || timer2 >= 940)
		{
			attacking = true;
		}
		return true;
	}

	public override bool CheckDead()
	{
		Gore.NewGore(null, NPC.position, NPC.velocity, Mod.Find<ModGore>("XenanisGore1").Type);
		Gore.NewGore(null, NPC.position, NPC.velocity, Mod.Find<ModGore>("XenanisGore2").Type);
		Gore.NewGore(null, NPC.position, NPC.velocity, Mod.Find<ModGore>("XenanisGore3").Type);
		Gore.NewGore(null, NPC.position, NPC.velocity, Mod.Find<ModGore>("XenanisGore4").Type);
		return true;
	}

    public override void ModifyNPCLoot(NPCLoot npcLoot)
    {
        npcLoot.Add(ItemDropRule.BossBag(Mod.Find<ModItem>("EtherealBag").Type));
		npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), Mod.Find<ModItem>("XenanisFlesh").Type, 1, 10, 17));
		npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), Mod.Find<ModItem>("XenanisWings").Type, 20));
		npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), Mod.Find<ModItem>("EtherealDidgeridoo").Type, 10));
        LeadingConditionRule notExpert = new LeadingConditionRule(new Conditions.NotExpert());
        notExpert.OnSuccess(ItemDropRule.OneFromOptions(1, Mod.Find<ModItem>("EtherealSword").Type, Mod.Find<ModItem>("EtherealBow").Type, Mod.Find<ModItem>("EtherealTome").Type, Mod.Find<ModItem>("EtherealSummon").Type));
        npcLoot.Add(notExpert);
		npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<XenanisMask>(), 7));
		npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<XenanisTrophyItem>(), 10));
	}

	public override void OnKill()
	{
		if (!UltraniumWorld.downedXenanis)
		{
			UltraniumWorld.downedXenanis = true;
			if (Main.netMode == NetmodeID.Server)
			{
				NetMessage.SendData(MessageID.WorldData);
			}
		}
	}

	public override void BossLoot(ref int potionType)
	{
		potionType = 499;
	}
}
