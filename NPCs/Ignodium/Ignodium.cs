using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.NPCs.Ignodium;

[AutoloadBossHead]
public class Ignodium : ModNPC
{
	private bool Phase2;

	private bool Transition;

	private int TransitionTimer;

	private float Spin;

	public int Timer;

	public int Timer2;

	public int DesperationTimer;

	public int AttackType;

	public int players;

	public override void SetStaticDefaults()
	{
		DisplayName.SetDefault("Ignodium");
		Main.npcFrameCount[NPC.type] = 6;
	}

	public override void SetDefaults()
	{
		NPC.scale = 1.4f;
		NPC.width = 128;
		NPC.height = 212;
		NPC.HitSound = new SoundStyle("Ultranium/Sounds/GodHit");
		NPC.DeathSound = new SoundStyle("Ultranium/Sounds/GodDeath") with { PitchVariance = 0.5f };
		NPC.damage = 50;
		NPC.defense = 50;
		NPC.lifeMax = 220000;
		NPC.knockBackResist = 0f;
		NPC.boss = true;
		NPC.noGravity = true;
		NPC.noTileCollide = true;
		NPC.npcSlots = 1f;
		NPC.alpha = 255;
		NPC.aiStyle = -1;
		NPC.value = Item.buyPrice(0, 35);
		players = 1;
		if (!Phase2)
		{
			base.Music = MusicLoader.GetMusicSlot(Mod, "Sounds/Music/GuardiansPhase1");
		}
		for (int i = 0; i < 206; i++)
		{
			NPC.buffImmune[i] = true;
		}
	}

	public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
	{
		players = numPlayers;
		NPC.lifeMax = 380000 + numPlayers * 38000;
	}

	public override void AI()
	{
		NPC.spriteDirection = NPC.direction;
		NPC.rotation = NPC.velocity.X * 0.02f;
		Player player = Main.player[NPC.target];
		_ = Main.expertMode;
		NPC.TargetClosest();
		int num = (Main.expertMode ? 35 : 55);
		if (Main.player[NPC.target].dead || Main.player[NPC.target].dead || !(player.position.Y / 16f > (float)(Main.maxTilesY - 200)))
		{
			NPC.velocity.Y = -45f;
			NPC.ai[3] += 1f;
			if (NPC.ai[3] >= 100f)
			{
				((Entity)NPC).active = false;
			}
		}
		if (NPC.life > NPC.lifeMax / 2)
		{
			(Main.player[NPC.target].Center - NPC.Center).Normalize();
			NPC.velocity *= 0.985f;
			Timer++;
			if (Timer < 100)
			{
				NPC.velocity *= 0f;
				NPC.alpha -= 5;
			}
			if (Timer == 100)
			{
				for (int i = 0; i < 60; i++)
				{
					int num2 = Dust.NewDust(NPC.position, NPC.width, NPC.height, 6, 0f, -2f, 0, default(Color), 1.5f);
					Main.dust[num2].noGravity = true;
					Main.dust[num2].scale = 2f;
					Main.dust[num2].position.X += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
					Main.dust[num2].position.Y += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
					if (Main.dust[num2].position != NPC.Center)
					{
						Main.dust[num2].velocity = NPC.DirectionTo(Main.dust[num2].position) * 10f;
					}
				}
			}
			if (Timer == 150)
			{
				Projectile.NewProjectile(null, NPC.Center.X, NPC.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("ShockWave").Type, 0, 0f, 255, 0f, 0f);
				SoundEngine.PlaySound(new SoundStyle("Ultranium/Sounds/GuardianGrowl"));
			}
			if (Timer == 240 || Timer == 300 || Timer == 360 || Timer == 420 || Timer == 480 || Timer == 540)
			{
				Vector2 vector = Main.player[NPC.target].Center - NPC.Center;
				vector.Normalize();
				vector.X *= 17f;
				vector.Y *= 17f;
				NPC.velocity.X = vector.X;
				NPC.velocity.Y = vector.Y;
				float num3 = 6.5f;
				int num4 = Mod.Find<ModProjectile>("MoltenGlob").Type;
				SoundEngine.PlaySound(SoundID.Item20, new Vector2(NPC.position.X, NPC.position.Y));
				float num5 = (float)Math.Atan2(NPC.Center.Y - player.Center.Y, NPC.Center.X - player.Center.X);
				Projectile.NewProjectile(null, NPC.Center.X, NPC.Center.Y, (float)(Math.Cos(num5) * (double)num3 * -1.0), (float)(Math.Sin(num5) * (double)num3 * -1.0), num4, num, 0f, Main.myPlayer, 0f, 0f);
			}
			if (AttackType == 0)
			{
				if (Timer >= 600 && Timer <= 850)
				{
					NPC.velocity.X = 0f;
					NPC.velocity.Y = 0f;
				}
				if (Timer == 660 || Timer == 700 || Timer == 740 || Timer == 780 || Timer == 820)
				{
					SoundEngine.PlaySound(new SoundStyle("Ultranium/Sounds/GuardianAttack"));
					float num6 = 6.5f;
					int num7 = Mod.Find<ModProjectile>("FlameGigaBlast").Type;
					SoundEngine.PlaySound(SoundID.Item20, new Vector2(NPC.position.X, NPC.position.Y));
					float num8 = (float)Math.Atan2(NPC.Center.Y - player.Center.Y, NPC.Center.X - player.Center.X);
					Projectile.NewProjectile(null, NPC.Center.X, NPC.Center.Y, (float)(Math.Cos(num8) * (double)num6 * -1.0), (float)(Math.Sin(num8) * (double)num6 * -1.0), num7, num, 0f, Main.myPlayer, 0f, 0f);
				}
			}
			if (AttackType == 1)
			{
				if (Timer >= 600 && Timer <= 850)
				{
					float num9 = 0.13f;
					Vector2 vector2 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
					float num10 = Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2) - vector2.X;
					float num11 = Main.player[NPC.target].position.Y + (float)(Main.player[NPC.target].height / 2) - 120f - vector2.Y;
					float num12 = (float)Math.Sqrt(num10 * num10 + num11 * num11);
					float num13 = 13f / num12;
					num10 *= num13;
					num11 *= num13;
					if (NPC.velocity.X < num10)
					{
						NPC.velocity.X = NPC.velocity.X + num9;
						if (NPC.velocity.X < 0f && num10 > 0f)
						{
							NPC.velocity.X = NPC.velocity.X + num9;
						}
					}
					else if (NPC.velocity.X > num10)
					{
						NPC.velocity.X = NPC.velocity.X - num9;
						if (NPC.velocity.X > 0f && num10 < 0f)
						{
							NPC.velocity.X = NPC.velocity.X - num9;
						}
					}
					if (NPC.velocity.Y < num11)
					{
						NPC.velocity.Y = NPC.velocity.Y + num9;
						if (NPC.velocity.Y < 0f && num11 > 0f)
						{
							NPC.velocity.Y = NPC.velocity.Y + num9;
						}
					}
					else if (NPC.velocity.Y > num11)
					{
						NPC.velocity.Y = NPC.velocity.Y - num9;
						if (NPC.velocity.Y > 0f && num11 < 0f)
						{
							NPC.velocity.Y = NPC.velocity.Y - num9;
						}
					}
				}
				if (Timer == 660 || Timer == 720 || Timer == 780)
				{
					SoundEngine.PlaySound(new SoundStyle("Ultranium/Sounds/GuardianAttack"));
					for (int j = 0; j < 7; j++)
					{
						Vector2 vector3 = (0.8975979f * (float)j).ToRotationVector2();
						vector3.Normalize();
						vector3 *= 7f;
						Projectile.NewProjectile(null, NPC.Center.X, NPC.Center.Y, vector3.X, vector3.Y, Mod.Find<ModProjectile>("MoltenGlob").Type, num, 1f, Main.myPlayer, 0f, 0f);
					}
				}
			}
			if (AttackType == 2)
			{
				if (Timer >= 660 && Timer <= 720)
				{
					NPC.velocity *= 0f;
					int num14 = 36;
					for (int k = 0; k < num14; k++)
					{
						Vector2 vector4 = (Vector2.One * new Vector2((float)NPC.width / 7f, (float)NPC.height / 7f) * 0.75f * 0.5f).RotatedBy((float)(k - (num14 / 2 - 1)) * ((float)Math.PI * 2f) / (float)num14) + NPC.Center;
						Vector2 vector5 = vector4 - NPC.Center;
						Dust obj = Main.dust[Dust.NewDust(vector4 + vector5, 0, 0, 6, vector5.X * 2f, vector5.Y * 2f, 100, default(Color), 1.4f)];
						obj.noGravity = true;
						obj.noLight = false;
						obj.velocity = Vector2.Normalize(vector5) * 3f;
						obj.fadeIn = 1.3f;
					}
				}
				if (Timer == 720 || Timer == 750)
				{
					SoundEngine.PlaySound(new SoundStyle("Ultranium/Sounds/GuardianGrowl"));
					Vector2 spinningpoint = new Vector2(6f, 0f).RotatedByRandom(Math.PI * 2.0);
					for (int l = 0; l < 17; l++)
					{
						Vector2 vector6 = spinningpoint.RotatedBy(0.8975979010256552 * ((double)l + Main.rand.NextDouble() - 0.5));
						Projectile.NewProjectile(null, NPC.Center, vector6, Mod.Find<ModProjectile>("FlameBolt").Type, num, 0f, Main.myPlayer, 0f, 0f);
					}
				}
			}
			if (AttackType == 3)
			{
				if (Timer > 600)
				{
					NPC.velocity *= 0f;
				}
				if (Timer == 660)
				{
					for (int m = -1; m <= 1; m++)
					{
						Projectile.NewProjectile(null, NPC.Center, 17f * NPC.DirectionTo(player.Center).RotatedBy(MathHelper.ToRadians(5f) * (float)m), Mod.Find<ModProjectile>("FlameBlast").Type, num, 0f, Main.myPlayer, 0f, 0f);
					}
				}
				if (Timer == 720)
				{
					for (int n = -2; n <= 2; n++)
					{
						Projectile.NewProjectile(null, NPC.Center, 17f * NPC.DirectionTo(player.Center).RotatedBy(MathHelper.ToRadians(7f) * (float)n), Mod.Find<ModProjectile>("FlameBlast").Type, num, 0f, Main.myPlayer, 0f, 0f);
					}
				}
				if (Timer == 780)
				{
					for (int num15 = -3; num15 <= 3; num15++)
					{
						Projectile.NewProjectile(null, NPC.Center, 17f * NPC.DirectionTo(player.Center).RotatedBy(MathHelper.ToRadians(9f) * (float)num15), Mod.Find<ModProjectile>("FlameBlast").Type, num, 0f, Main.myPlayer, 0f, 0f);
					}
				}
			}
			if (Timer >= 850)
			{
				Timer = 300;
				if (AttackType < 4)
				{
					AttackType++;
				}
				if (AttackType >= 4)
				{
					AttackType = 0;
				}
			}
			return;
		}
		if (NPC.life < NPC.lifeMax / 2 && !Phase2)
		{
			Phase2 = true;
			Transition = true;
			base.Music = MusicLoader.GetMusicSlot(Mod, "Sounds/Music/GuardiansPhase2");
		}
		if (Transition)
		{
			NPC.velocity *= 0f;
			NPC.immortal = true;
			NPC.dontTakeDamage = true;
			TransitionTimer++;
			if (TransitionTimer == 120)
			{
				SoundEngine.PlaySound(new SoundStyle("Ultranium/Sounds/GuardianUNUN"));
				for (int num16 = 0; num16 < 60; num16++)
				{
					int num17 = Dust.NewDust(NPC.position, NPC.width, NPC.height, 6, 0f, -2f, 0, default(Color), 1.5f);
					Main.dust[num17].noGravity = true;
					Main.dust[num17].scale = 2f;
					Main.dust[num17].position.X += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
					Main.dust[num17].position.Y += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
					if (Main.dust[num17].position != NPC.Center)
					{
						Main.dust[num17].velocity = NPC.DirectionTo(Main.dust[num17].position) * 10f;
					}
				}
			}
			if (TransitionTimer == 180)
			{
				Transition = false;
				TransitionTimer = 0;
			}
		}
		if (NPC.life < NPC.lifeMax / 2 && NPC.life >= NPC.lifeMax / 8 && !Transition)
		{
			(Main.player[NPC.target].Center - NPC.Center).Normalize();
			NPC.velocity *= 0.985f;
			NPC.immortal = false;
			NPC.dontTakeDamage = false;
			Timer2++;
			if (Timer2 >= 0 && Timer2 <= 500)
			{
				float num18 = 0.13f;
				Vector2 vector7 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
				float num19 = Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2) - vector7.X;
				float num20 = Main.player[NPC.target].position.Y + (float)(Main.player[NPC.target].height / 2) - 120f - vector7.Y;
				float num21 = (float)Math.Sqrt(num19 * num19 + num20 * num20);
				float num22 = 13f / num21;
				num19 *= num22;
				num20 *= num22;
				if (NPC.velocity.X < num19)
				{
					NPC.velocity.X = NPC.velocity.X + num18;
					if (NPC.velocity.X < 0f && num19 > 0f)
					{
						NPC.velocity.X = NPC.velocity.X + num18;
					}
				}
				else if (NPC.velocity.X > num19)
				{
					NPC.velocity.X = NPC.velocity.X - num18;
					if (NPC.velocity.X > 0f && num19 < 0f)
					{
						NPC.velocity.X = NPC.velocity.X - num18;
					}
				}
				if (NPC.velocity.Y < num20)
				{
					NPC.velocity.Y = NPC.velocity.Y + num18;
					if (NPC.velocity.Y < 0f && num20 > 0f)
					{
						NPC.velocity.Y = NPC.velocity.Y + num18;
					}
				}
				else if (NPC.velocity.Y > num20)
				{
					NPC.velocity.Y = NPC.velocity.Y - num18;
					if (NPC.velocity.Y > 0f && num20 < 0f)
					{
						NPC.velocity.Y = NPC.velocity.Y - num18;
					}
				}
				if (Timer2 == 60 || Timer2 == 120 || Timer2 == 180 || Timer2 == 240 || Timer2 == 300 || Timer2 == 360 || Timer2 == 420 || Timer2 == 480)
				{
					float num23 = 12f;
					int num24 = Mod.Find<ModProjectile>("FlameGigaBlast").Type;
					SoundEngine.PlaySound(SoundID.Item20, new Vector2(NPC.position.X, NPC.position.Y));
					float num25 = (float)Math.Atan2(NPC.Center.Y - player.Center.Y, NPC.Center.X - player.Center.X);
					Projectile.NewProjectile(null, NPC.Center.X, NPC.Center.Y, (float)(Math.Cos(num25) * (double)num23 * -1.0), (float)(Math.Sin(num25) * (double)num23 * -1.0), num24, 30, 0f, Main.myPlayer, 0f, 0f);
				}
			}
			if (Timer2 == 540 || Timer2 == 560 || Timer2 == 580 || Timer2 == 600 || Timer2 == 620 || Timer2 == 640 || Timer2 == 660)
			{
				NPC.velocity *= 0f;
				Projectile.NewProjectile(null, player.Center.X, player.Center.Y + 550f, 0f, 0f, Mod.Find<ModProjectile>("EruptionTelegraph").Type, 0, 0f, Main.myPlayer, 0f, 0f);
			}
			if (Timer2 == 720 || Timer2 == 780 || Timer2 == 840 || Timer2 == 900 || Timer2 == 960)
			{
				Vector2 vector8 = Main.player[NPC.target].Center - NPC.Center;
				vector8.Normalize();
				vector8.X *= 18f;
				vector8.Y *= 18f;
				NPC.velocity.X = vector8.X;
				NPC.velocity.Y = vector8.Y;
				float num26 = 7.5f;
				int num27 = Mod.Find<ModProjectile>("MoltenGlob").Type;
				SoundEngine.PlaySound(SoundID.Item20, new Vector2(NPC.position.X, NPC.position.Y));
				float num28 = (float)Math.Atan2(NPC.Center.Y - player.Center.Y, NPC.Center.X - player.Center.X);
				Projectile.NewProjectile(null, NPC.Center.X, NPC.Center.Y, (float)(Math.Cos(num28) * (double)num26 * -1.0), (float)(Math.Sin(num28) * (double)num26 * -1.0), num27, num, 0f, Main.myPlayer, 0f, 0f);
			}
			if (Timer2 > 1020 && Timer2 < 1080)
			{
				NPC.velocity *= 0f;
				Vector2 spinningpoint2 = new Vector2(400f, 0f);
				Spin += 0.23f;
				int num29 = 1;
				for (int num30 = 0; num30 < num29; num30++)
				{
					float num31 = 360 / num29;
					Vector2 vector9 = NPC.Center + spinningpoint2.RotatedBy((double)Spin + (double)(num31 * (float)num30) * (Math.PI / 4.0));
					float num32 = (float)Math.Atan2(NPC.Center.Y - vector9.Y, NPC.Center.X - vector9.X);
					Projectile.NewProjectile(null, NPC.Center.X, NPC.Center.Y, (float)(Math.Cos(num32) * 10.0 * -1.0), (float)(Math.Sin(num32) * 10.0 * -1.0), Mod.Find<ModProjectile>("FlameBlast").Type, num, 0f, Main.myPlayer, 0f, (float)num30);
				}
			}
			if (Timer2 >= 1140)
			{
				Timer2 = 0;
			}
		}
		if (NPC.life > NPC.lifeMax / 8 || Transition)
		{
			return;
		}
		NPC.velocity *= 0f;
		DesperationTimer++;
		if (DesperationTimer < 120)
		{
			NPC.immortal = true;
			NPC.dontTakeDamage = true;
		}
		if (DesperationTimer >= 120)
		{
			NPC.immortal = false;
			NPC.dontTakeDamage = false;
		}
		if (DesperationTimer == 80)
		{
			SoundEngine.PlaySound(new SoundStyle("Ultranium/Sounds/GuardianUNUN"));
			for (int num33 = 0; num33 < 60; num33++)
			{
				int num34 = Dust.NewDust(NPC.position, NPC.width, NPC.height, 6, 0f, -2f, 0, default(Color), 1.5f);
				Main.dust[num34].noGravity = true;
				Main.dust[num34].scale = 2f;
				Main.dust[num34].position.X += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
				Main.dust[num34].position.Y += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
				if (Main.dust[num34].position != NPC.Center)
				{
					Main.dust[num34].velocity = NPC.DirectionTo(Main.dust[num34].position) * 10f;
				}
			}
		}
		if (DesperationTimer == 180 || DesperationTimer == 220 || DesperationTimer == 260)
		{
			SoundEngine.PlaySound(new SoundStyle("Ultranium/Sounds/GuardianAttack"));
			float num35 = 2200f;
			float num36 = 0f;
			if (DesperationTimer == 180)
			{
				num36 = 10f;
			}
			if (DesperationTimer == 220)
			{
				num36 = 13f;
			}
			if (DesperationTimer == 260)
			{
				num36 = 16f;
			}
			float num37 = num35 / num36;
			for (int num38 = 0; (float)num38 < num36; num38++)
			{
				Vector2 vector10 = new Vector2(player.Center.X - num35 / 2f + num37 * (float)num38, player.Center.Y + 700f);
				Main.projectile[Projectile.NewProjectile(null, vector10.X, vector10.Y, 0f, 0f, Mod.Find<ModProjectile>("IgnodiumBeamTelegraph").Type, 0, 0f, Main.myPlayer, 0f, 0f)].localAI[1] = 125f;
			}
		}
		if (DesperationTimer == 300 || DesperationTimer == 310 || DesperationTimer == 320)
		{
			float num39 = 1.5f;
			float num40 = (float)Math.Atan2(NPC.Center.Y - player.Center.Y, NPC.Center.X - player.Center.X);
			Projectile.NewProjectile(null, NPC.Center.X, NPC.Center.Y, (float)(Math.Cos(num40) * (double)num39 * -1.0), (float)(Math.Sin(num40) * (double)num39 * -1.0), Mod.Find<ModProjectile>("IgnodiumBeam").Type, num, 0f, 0, 0f, 0f);
		}
		if (DesperationTimer > 340 && DesperationTimer < 440)
		{
			Vector2 spinningpoint3 = new Vector2(400f, 0f);
			Spin += 0.23f;
			int num41 = 1;
			for (int num42 = 0; num42 < num41; num42++)
			{
				float num43 = 360 / num41;
				Vector2 vector11 = NPC.Center + spinningpoint3.RotatedBy((double)Spin + (double)(num43 * (float)num42) * (Math.PI / 4.0));
				float num44 = (float)Math.Atan2(NPC.Center.Y - vector11.Y, NPC.Center.X - vector11.X);
				Projectile.NewProjectile(null, NPC.Center.X, NPC.Center.Y, (float)(Math.Cos(num44) * 0.5 * -1.0), (float)(Math.Sin(num44) * 0.5 * -1.0), Mod.Find<ModProjectile>("IgnodiumBeam").Type, num, 0f, Main.myPlayer, 0f, (float)num42);
			}
		}
		if (DesperationTimer >= 500)
		{
			DesperationTimer = 120;
			for (int num45 = 0; num45 < 50; num45++)
			{
				int num46 = Dust.NewDust(NPC.position, NPC.width, NPC.height, 6);
				Main.dust[num46].scale = 1.5f;
			}
			NPC.position.X = player.position.X - 100f;
			NPC.position.Y = player.position.Y - 400f;
			for (int num47 = 0; num47 < 50; num47++)
			{
				int num48 = Dust.NewDust(NPC.position, NPC.width, NPC.height, 6);
				Main.dust[num48].scale = 1.5f;
			}
		}
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		if (NPC.life > 0)
		{
			return;
		}
		for (int i = 0; i < 40; i++)
		{
			int num = Dust.NewDust(NPC.position, NPC.width, NPC.height, 6, 0f, -2f, 0, default(Color), 1.5f);
			Main.dust[num].noGravity = true;
			Main.dust[num].scale = 2f;
			Main.dust[num].position.X += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
			Main.dust[num].position.Y += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
			if (Main.dust[num].position != NPC.Center)
			{
				Main.dust[num].velocity = NPC.DirectionTo(Main.dust[num].position) * 2f;
			}
		}
		NPC.position.X = NPC.position.X + (float)(NPC.width / 2);
		NPC.position.Y = NPC.position.Y + (float)(NPC.height / 2);
		NPC.width = 128;
		NPC.height = 212;
		NPC.position.X = NPC.position.X - (float)(NPC.width / 2);
		NPC.position.Y = NPC.position.Y - (float)(NPC.height / 2);
		for (int j = 0; j < 20; j++)
		{
			int num2 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 6, 0f, 0f, 100, default(Color), 2f);
			Main.dust[num2].velocity *= 3f;
			if (Main.rand.Next(2) == 0)
			{
				Main.dust[num2].scale = 1.5f;
				Main.dust[num2].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
			}
		}
	}

    public override void ModifyNPCLoot(NPCLoot npcLoot)
    {
        npcLoot.Add(ItemDropRule.BossBag(Mod.Find<ModItem>("IgnodiumBossBag").Type));
		npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), Mod.Find<ModItem>("HellShard").Type, 1, 25, 31));
		npcLoot.Add(ItemDropRule.Common(Mod.Find<ModItem>("IgnodiumMask").Type, 7));
		npcLoot.Add(ItemDropRule.Common(Mod.Find<ModItem>("IgnodiumTrophyItem").Type, 10));
		npcLoot.Add(new LeadingConditionRule(new Conditions.NotExpert()).OnSuccess(ItemDropRule.OneFromOptions(1, Mod.Find<ModItem>("HellFlail").Type, Mod.Find<ModItem>("HellThrow").Type, Mod.Find<ModItem>("HellGun").Type, Mod.Find<ModItem>("HellJavelin").Type, Mod.Find<ModItem>("HellStaff").Type, Mod.Find<ModItem>("HellTome").Type, Mod.Find<ModItem>("HellScepter").Type)));
    }

	public override void OnKill()
	{
		if (!UltraniumWorld.downedIgnodium)
		{
			UltraniumWorld.downedIgnodium = true;
			if (Main.netMode == 2)
			{
				NetMessage.SendData(7);
			}
		}
	}

	public override void BossLoot(ref int potionType)
	{
		potionType = 3544;
	}

	public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
	{
		scale = 1.5f;
		return null;
	}

	public override void FindFrame(int frameHeight)
	{
		NPC.frameCounter += 1.0;
		if (NPC.frameCounter >= 8.0)
		{
			NPC.frame.Y = (NPC.frame.Y + frameHeight) % (Main.npcFrameCount[NPC.type] * frameHeight);
			NPC.frameCounter = 1.0;
		}
	}
}
