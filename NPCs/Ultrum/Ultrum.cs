using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.NPCs.Ultrum;

[AutoloadBossHead]
public class Ultrum : ModNPC
{
	private bool Phase2;

	private bool Transition;

	private int TransitionTimer;

	public int Timer;

	public int Timer2 = 40;

	public int BulletTimer;

	public int DesperationTimer;

	public int AttackType;

	public int players;

	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Ultrum");
		Main.npcFrameCount[NPC.type] = 6;
		NPCID.Sets.TrailCacheLength[NPC.type] = 10;
		NPCID.Sets.TrailingMode[NPC.type] = 0;
	}

	public override void SetDefaults()
	{
		NPC.scale = 1.4f;
		NPC.width = 128;
		NPC.height = 212;
		NPC.HitSound = new SoundStyle("Ultranium/Sounds/GodHit");
		NPC.DeathSound = new SoundStyle("Ultranium/Sounds/GodDeath") { PitchVariance = 0.5f };
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

	public override void ModifyHitByProjectile(Projectile projectile, ref NPC.HitModifiers modifiers)
	{
		if (projectile.type == ProjectileID.LastPrismLaser)
		{
			modifiers.SourceDamage /= 3;
		}
	}

	public override bool PreAI()
	{
		NPC.spriteDirection = NPC.direction;
		NPC.rotation = NPC.velocity.X * 0.02f;
		Player player = Main.player[NPC.target];
		_ = Main.expertMode;
		NPC.TargetClosest();
		int num = (Main.expertMode ? 35 : 55);
		if (Main.player[NPC.target].dead || Main.player[NPC.target].dead || (double)player.Center.Y > Main.worldSurface * 16.0 || player.ZoneUnderworldHeight)
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
					int num2 = Dust.NewDust(NPC.position, NPC.width, NPC.height, Mod.Find<ModDust>("UltraniumDust").Type, 0f, -2f, 0, default(Color), 1.5f);
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
				Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("ShockWave").Type, 0, 0f, 255, 0f, 0f);
				SoundEngine.PlaySound(new SoundStyle("Ultranium/Sounds/GuardianGrowl"));
			}
			if (Timer <= 800 && Timer > 150)
			{
				if (Timer == 300 || Timer == 400 || Timer == 500 || Timer == 600 || Timer == 700)
				{
					for (int j = -2; j <= 2; j++)
					{
						Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, 17f * NPC.DirectionTo(player.Center).RotatedBy(MathHelper.ToRadians(5f) * (float)j), Mod.Find<ModProjectile>("UltraBeam").Type, num, 0f, Main.myPlayer, 0f, 0f);
					}
				}
				float num3 = 0.13f;
				Vector2 vector = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
				float num4 = Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2) - vector.X;
				float num5 = Main.player[NPC.target].position.Y + (float)(Main.player[NPC.target].height / 2) - 120f - vector.Y;
				float num6 = (float)Math.Sqrt(num4 * num4 + num5 * num5);
				float num7 = 13f / num6;
				num4 *= num7;
				num5 *= num7;
				if (NPC.velocity.X < num4)
				{
					NPC.velocity.X = NPC.velocity.X + num3;
					if (NPC.velocity.X < 0f && num4 > 0f)
					{
						NPC.velocity.X = NPC.velocity.X + num3;
					}
				}
				else if (NPC.velocity.X > num4)
				{
					NPC.velocity.X = NPC.velocity.X - num3;
					if (NPC.velocity.X > 0f && num4 < 0f)
					{
						NPC.velocity.X = NPC.velocity.X - num3;
					}
				}
				if (NPC.velocity.Y < num5)
				{
					NPC.velocity.Y = NPC.velocity.Y + num3;
					if (NPC.velocity.Y < 0f && num5 > 0f)
					{
						NPC.velocity.Y = NPC.velocity.Y + num3;
					}
				}
				else if (NPC.velocity.Y > num5)
				{
					NPC.velocity.Y = NPC.velocity.Y - num3;
					if (NPC.velocity.Y > 0f && num5 < 0f)
					{
						NPC.velocity.Y = NPC.velocity.Y - num3;
					}
				}
			}
			if (AttackType == 0)
			{
				if (Timer >= 800)
				{
					NPC.velocity *= 0f;
				}
				if (Timer == 860)
				{
					SoundEngine.PlaySound(new SoundStyle("Ultranium/Sounds/GuardianAttack"));
					SoundEngine.PlaySound(SoundID.Item20, new Vector2(NPC.position.X, NPC.position.Y));
					float num8 = 10f;
					float num9 = 19f;
					float num10 = MathHelper.ToRadians(360f);
					int num11 = 1;
					for (int k = 0; (float)k < num9; k++)
					{
						Vector2 vector2 = Vector2.One.RotatedBy(MathHelper.Lerp(0f - num10, num10, (float)k / (num9 - 1f))) * num8;
						Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, vector2.X, vector2.Y, Mod.Find<ModProjectile>("UltraniumBlastSpiral").Type, num, 2f, Main.myPlayer, (float)num11, 0f);
					}
				}
				if (Timer == 920)
				{
					SoundEngine.PlaySound(new SoundStyle("Ultranium/Sounds/GuardianAttack"));
					SoundEngine.PlaySound(SoundID.Item20, new Vector2(NPC.position.X, NPC.position.Y));
					float num12 = 10f;
					float num13 = 19f;
					float num14 = MathHelper.ToRadians(360f);
					int num15 = -1;
					for (int l = 0; (float)l < num13; l++)
					{
						Vector2 vector3 = Vector2.One.RotatedBy(MathHelper.Lerp(0f - num14, num14, (float)l / (num13 - 1f))) * num12;
						Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, vector3.X, vector3.Y, Mod.Find<ModProjectile>("UltraniumBlastSpiral").Type, num, 2f, Main.myPlayer, (float)num15, 0f);
					}
				}
			}
			if (AttackType == 1)
			{
				if (Timer >= 800)
				{
					NPC.velocity *= 0f;
				}
				if (Timer == 840 || Timer == 880 || Timer == 920)
				{
					SoundEngine.PlaySound(new SoundStyle("Ultranium/Sounds/GuardianAttack"));
					float num16 = 12.5f;
					int num17 = Mod.Find<ModProjectile>("NatureTornado").Type;
					SoundEngine.PlaySound(SoundID.Item20, new Vector2(NPC.position.X, NPC.position.Y));
					float num18 = (float)Math.Atan2(NPC.Center.Y - player.Center.Y, NPC.Center.X - player.Center.X);
					Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, (float)(Math.Cos(num18) * (double)num16 * -1.0), (float)(Math.Sin(num18) * (double)num16 * -1.0), num17, num, 0f, Main.myPlayer, 0f, 0f);
				}
			}
			if (AttackType == 2)
			{
				if (Timer >= 800 && Timer <= 860)
				{
					NPC.velocity *= 0f;
				}
				if (Timer == 860 || Timer == 905)
				{
					SoundEngine.PlaySound(new SoundStyle("Ultranium/Sounds/GuardianGrowl"));
					Vector2 vector4 = Main.player[NPC.target].Center - NPC.Center;
					vector4.Normalize();
					vector4.X *= 25f;
					vector4.Y *= 25f;
					NPC.velocity.X = vector4.X;
					NPC.velocity.Y = vector4.Y;
					Vector2 vector5 = Main.player[NPC.target].Center - NPC.Center;
					vector5.Normalize();
					vector5.X *= 25f;
					vector5.Y *= 25f;
				}
				if (Timer == 860 || Timer == 870 || Timer == 880 || Timer == 890 || Timer == 900 || Timer == 910 || Timer == 920 || Timer == 930)
				{
					float num19 = 0f;
					int num20 = Mod.Find<ModProjectile>("UltrumEnergyBolt").Type;
					SoundEngine.PlaySound(SoundID.Item20, new Vector2(NPC.position.X, NPC.position.Y));
					float num21 = (float)Math.Atan2(NPC.Center.Y - player.Center.Y, NPC.Center.X - player.Center.X);
					Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, (float)(Math.Cos(num21) * (double)num19 * -1.0), (float)(Math.Sin(num21) * (double)num19 * -1.0), num20, 30, 0f, Main.myPlayer, 0f, 0f);
				}
			}
			if (AttackType == 3)
			{
				if (Timer >= 800)
				{
					NPC.velocity *= 0f;
				}
				if (Timer == 860 || Timer == 890 || Timer == 920 || Timer == 950)
				{
					Vector2 vector6 = Main.player[NPC.target].Center - NPC.Center;
					vector6.Normalize();
					vector6.X *= 8.5f;
					vector6.Y *= 8.5f;
					int num22 = Main.rand.Next(3, 5);
					for (int m = 0; m < num22; m++)
					{
						float num23 = (float)Main.rand.Next(-300, 300) * 0.01f;
						Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, vector6.X + num23, vector6.Y + num23, Mod.Find<ModProjectile>("UltrumEnergyBolt").Type, num, 1f, Main.myPlayer, 0f, 0f);
					}
				}
			}
			if (Timer >= 950)
			{
				Timer = 201;
				if (AttackType < 4)
				{
					AttackType++;
				}
				if (AttackType >= 4)
				{
					AttackType = 0;
				}
			}
		}
		else
		{
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
					for (int n = 0; n < 60; n++)
					{
						int num24 = Dust.NewDust(NPC.position, NPC.width, NPC.height, Mod.Find<ModDust>("UltraniumDust").Type, 0f, -2f, 0, default(Color), 1.5f);
						Main.dust[num24].noGravity = true;
						Main.dust[num24].scale = 2f;
						Main.dust[num24].position.X += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
						Main.dust[num24].position.Y += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
						if (Main.dust[num24].position != NPC.Center)
						{
							Main.dust[num24].velocity = NPC.DirectionTo(Main.dust[num24].position) * 10f;
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
				NPC.immortal = false;
				NPC.dontTakeDamage = false;
				Vector2 vector7 = Main.player[NPC.target].Center - NPC.Center;
				vector7.Normalize();
				NPC.TargetClosest();
				NPC.velocity *= 0.986f;
				Timer2++;
				if (Timer2 == 100 || Timer2 == 160 || Timer2 == 220 || Timer2 == 280 || Timer2 == 340 || Timer2 == 400 || Timer2 == 460 || Timer2 == 520)
				{
					SoundEngine.PlaySound(new SoundStyle("Ultranium/Sounds/GuardianGrowl"));
					int num25 = 36;
					for (int num26 = 0; num26 < num25; num26++)
					{
						Vector2 vector8 = (Vector2.One * new Vector2((float)NPC.width / 7f, (float)NPC.height / 7f) * 0.75f * 0.5f).RotatedBy((float)(num26 - (num25 / 2 - 1)) * ((float)Math.PI * 2f) / (float)num25) + NPC.Center;
						Vector2 vector9 = vector8 - NPC.Center;
						Dust obj = Main.dust[Dust.NewDust(vector8 + vector9, 0, 0, Mod.Find<ModDust>("UltraniumDust").Type, vector9.X * 2f, vector9.Y * 2f, 100, default(Color), 1.4f)];
						obj.noGravity = true;
						obj.noLight = false;
						obj.velocity = Vector2.Normalize(vector9) * 3f;
						obj.fadeIn = 1.3f;
					}
					vector7.X *= 22f;
					vector7.Y *= 22f;
					NPC.velocity.X = vector7.X;
					NPC.velocity.Y = vector7.Y;
					Vector2 vector10 = Main.player[NPC.target].Center - NPC.Center;
					vector10.Normalize();
					vector10.X *= 15f;
					vector10.Y *= 15f;
				}
				if (Timer2 >= 540 && Timer2 <= 700)
				{
					NPC.velocity.X = 0f;
					NPC.velocity.Y = 0f;
				}
				if (Timer2 == 590 || Timer2 == 620 || Timer2 == 650 || Timer2 == 680)
				{
					SoundEngine.PlaySound(new SoundStyle("Ultranium/Sounds/GuardianAttack"));
					Vector2 spinningpoint = new Vector2(6f, 0f).RotatedByRandom(Math.PI * 2.0);
					for (int num27 = 0; num27 < 12; num27++)
					{
						Vector2 vector11 = spinningpoint.RotatedBy(0.8975979010256552 * ((double)num27 + Main.rand.NextDouble() - 0.5));
						Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, vector11, Mod.Find<ModProjectile>("UltrumBolt").Type, num, 0f, Main.myPlayer, 0f, 0f);
					}
				}
				if (Timer2 > 760)
				{
					if (Timer2 == 760 || Timer2 == 820 || Timer2 == 880 || Timer2 == 940 || Timer2 == 1000 || Timer2 == 1060 || Timer2 == 1120 || Timer2 == 1180 || Timer2 == 1240)
					{
						for (int num28 = -2; num28 <= 2; num28++)
						{
							Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, 17f * NPC.DirectionTo(player.Center).RotatedBy(MathHelper.ToRadians(5f) * (float)num28), Mod.Find<ModProjectile>("UltraBeam").Type, num, 0f, Main.myPlayer, 0f, 0f);
						}
					}
					float num29 = 0.15f;
					Vector2 vector12 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
					float num30 = Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2) - vector12.X;
					float num31 = Main.player[NPC.target].position.Y + (float)(Main.player[NPC.target].height / 2) - 120f - vector12.Y;
					float num32 = (float)Math.Sqrt(num30 * num30 + num31 * num31);
					float num33 = 15f / num32;
					num30 *= num33;
					num31 *= num33;
					if (NPC.velocity.X < num30)
					{
						NPC.velocity.X = NPC.velocity.X + num29;
						if (NPC.velocity.X < 0f && num30 > 0f)
						{
							NPC.velocity.X = NPC.velocity.X + num29;
						}
					}
					else if (NPC.velocity.X > num30)
					{
						NPC.velocity.X = NPC.velocity.X - num29;
						if (NPC.velocity.X > 0f && num30 < 0f)
						{
							NPC.velocity.X = NPC.velocity.X - num29;
						}
					}
					if (NPC.velocity.Y < num31)
					{
						NPC.velocity.Y = NPC.velocity.Y + num29;
						if (NPC.velocity.Y < 0f && num31 > 0f)
						{
							NPC.velocity.Y = NPC.velocity.Y + num29;
						}
					}
					else if (NPC.velocity.Y > num31)
					{
						NPC.velocity.Y = NPC.velocity.Y - num29;
						if (NPC.velocity.Y > 0f && num31 < 0f)
						{
							NPC.velocity.Y = NPC.velocity.Y - num29;
						}
					}
				}
				if (Timer2 >= 1300)
				{
					NPC.velocity.X = 0f;
					NPC.velocity.Y = 0f;
				}
				if (Timer2 == 1360 || Timer2 == 1420 || Timer2 == 1480)
				{
					SoundEngine.PlaySound(new SoundStyle("Ultranium/Sounds/GuardianAttack"));
					SoundEngine.PlaySound(SoundID.Item20, new Vector2(NPC.position.X, NPC.position.Y));
					float num34 = 9f;
					float num35 = 21f;
					float num36 = MathHelper.ToRadians(360f);
					int num37 = -1;
					for (int num38 = 0; (float)num38 < num35; num38++)
					{
						Vector2 vector13 = Vector2.One.RotatedBy(MathHelper.Lerp(0f - num36, num36, (float)num38 / (num35 - 1f))) * num34;
						Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, vector13.X, vector13.Y, Mod.Find<ModProjectile>("UltraniumBlastSpiral").Type, num, 2f, Main.myPlayer, (float)num37, 0f);
					}
				}
				if (Timer2 == 1390 || Timer2 == 1450)
				{
					SoundEngine.PlaySound(SoundID.Item20, new Vector2(NPC.position.X, NPC.position.Y));
					float num39 = 9f;
					float num40 = 21f;
					float num41 = MathHelper.ToRadians(360f);
					int num42 = 1;
					for (int num43 = 0; (float)num43 < num40; num43++)
					{
						Vector2 vector14 = Vector2.One.RotatedBy(MathHelper.Lerp(0f - num41, num41, (float)num43 / (num40 - 1f))) * num39;
						Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, vector14.X, vector14.Y, Mod.Find<ModProjectile>("UltraniumBlastSpiral").Type, num, 2f, Main.myPlayer, (float)num42, 0f);
					}
				}
				if (Timer2 == 1600 || Timer2 == 1640)
				{
					float num44 = 2200f;
					float num45 = 10f;
					float num46 = num44 / num45;
					for (int num47 = 0; (float)num47 < num45; num47++)
					{
						Vector2 vector15 = new Vector2(player.Center.X - num44 / 2f + num46 * (float)num47, player.Center.Y + 700f);
						Main.projectile[Projectile.NewProjectile(NPC.GetSource_FromThis(), vector15.X, vector15.Y, 0f, 0f, Mod.Find<ModProjectile>("UltrumTelegraph").Type, 0, 0f, Main.myPlayer, 0f, 0f)].localAI[1] = 125f;
					}
				}
				if (Timer2 > 1700)
				{
					Timer2 = 40;
				}
			}
			if (NPC.life <= NPC.lifeMax / 8 && !Transition)
			{
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
					for (int num48 = 0; num48 < 60; num48++)
					{
						int num49 = Dust.NewDust(NPC.position, NPC.width, NPC.height, Mod.Find<ModDust>("UltraniumDust").Type, 0f, -2f, 0, default(Color), 1.5f);
						Main.dust[num49].noGravity = true;
						Main.dust[num49].scale = 2f;
						Main.dust[num49].position.X += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
						Main.dust[num49].position.Y += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
						if (Main.dust[num49].position != NPC.Center)
						{
							Main.dust[num49].velocity = NPC.DirectionTo(Main.dust[num49].position) * 10f;
						}
					}
				}
				if (DesperationTimer == 180)
				{
					SoundEngine.PlaySound(SoundID.Item20, new Vector2(NPC.position.X, NPC.position.Y));
					for (int num50 = 0; num50 < 12; num50++)
					{
						Vector2 vector16 = ((float)Math.PI / 6f * (float)num50).ToRotationVector2();
						vector16.Normalize();
						vector16 *= 2f;
						Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, vector16.X, vector16.Y, Mod.Find<ModProjectile>("UltrumBeam").Type, num, 1f, Main.myPlayer, 0f, 0f);
					}
				}
				if (DesperationTimer == 200)
				{
					SoundEngine.PlaySound(SoundID.Item20, new Vector2(NPC.position.X, NPC.position.Y));
					for (int num51 = 0; num51 < 18; num51++)
					{
						Vector2 vector17 = ((float)Math.PI / 6f * (float)num51).ToRotationVector2();
						vector17.Normalize();
						vector17 *= 2f;
						Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, vector17.X, vector17.Y, Mod.Find<ModProjectile>("UltrumBeam").Type, num, 1f, Main.myPlayer, 0f, 0f);
					}
				}
				if (DesperationTimer >= 240 && DesperationTimer <= 300)
				{
					float num52 = 3.5f;
					float num53 = (float)Math.Atan2(NPC.Center.Y - player.Center.Y, NPC.Center.X - player.Center.X);
					Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, (float)(Math.Cos(num53) * (double)num52 * -1.0), (float)(Math.Sin(num53) * (double)num52 * -1.0), Mod.Find<ModProjectile>("UltrumBeam").Type, num, 0f, 0, 0f, 0f);
				}
				if (DesperationTimer == 360 || DesperationTimer == 420 || DesperationTimer == 480)
				{
					int num54 = 0;
					if (DesperationTimer == 360)
					{
						num54 = 8;
					}
					if (DesperationTimer == 420)
					{
						num54 = 12;
					}
					if (DesperationTimer == 480)
					{
						num54 = 16;
					}
					int num55 = 650;
					for (float num56 = 0f; num56 < (float)num54; num56 += 1f)
					{
						Vector2 vector18 = player.Center + new Vector2(0f, num55).RotatedBy((double)num56 * (Math.PI * 2.0 / (double)num54));
						Vector2 vector19 = player.Center - vector18;
						vector19.Normalize();
						vector19 *= 1.2f;
						_ = Main.projectile[Projectile.NewProjectile(NPC.GetSource_FromThis(), vector18.X, vector18.Y, vector19.X, vector19.Y, Mod.Find<ModProjectile>("UltrumBeam").Type, num, 6f, 0, 0f, 0f)];
					}
				}
			}
			if (DesperationTimer >= 520)
			{
				DesperationTimer = 120;
				for (int num57 = 0; num57 < 50; num57++)
				{
					int num58 = Dust.NewDust(NPC.position, NPC.width, NPC.height, Mod.Find<ModDust>("UltraniumDust").Type);
					Main.dust[num58].scale = 1.5f;
				}
				NPC.position.X = player.position.X - 100f;
				NPC.position.Y = player.position.Y - 500f;
				for (int num59 = 0; num59 < 50; num59++)
				{
					int num60 = Dust.NewDust(NPC.position, NPC.width, NPC.height, Mod.Find<ModDust>("UltraniumDust").Type);
					Main.dust[num60].scale = 1.5f;
				}
			}
		}
		return false;
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		if (NPC.life > 0)
		{
			return;
		}
		for (int i = 0; i < 40; i++)
		{
			int num = Dust.NewDust(NPC.position, NPC.width, NPC.height, Mod.Find<ModDust>("UltraniumDust").Type, 0f, -2f, 0, default(Color), 1.5f);
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
			int num2 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, Mod.Find<ModDust>("UltraniumDust").Type, 0f, 0f, 100, default(Color), 2f);
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
		npcLoot.Add(ItemDropRule.Common(Mod.Find<ModItem>("UltrumMask").Type, 7));
		npcLoot.Add(ItemDropRule.Common(Mod.Find<ModItem>("UltrumTrophyItem").Type, 10));
        npcLoot.Add(ItemDropRule.BossBag(Mod.Find<ModItem>("UltrumBag").Type));
		LeadingConditionRule notExpert = new LeadingConditionRule(new Conditions.NotExpert());
		notExpert.OnSuccess(ItemDropRule.Common(Mod.Find<ModItem>("UltrumShard").Type, 1, 25, 31));
		notExpert.OnSuccess(ItemDropRule.OneFromOptions(1, Mod.Find<ModItem>("UltraFlail").Type, Mod.Find<ModItem>("UltraniumBow").Type, Mod.Find<ModItem>("UltraniumKunai").Type, Mod.Find<ModItem>("UltraniumStaff").Type, Mod.Find<ModItem>("UltraniumSword").Type, Mod.Find<ModItem>("UltraTome").Type, Mod.Find<ModItem>("UltraniumScepter").Type));
		npcLoot.Add(notExpert);
    }

	public override void OnKill()
	{
		if (!UltraniumWorld.downedUltrum)
		{
			UltraniumWorld.downedUltrum = true;
			if (Main.netMode == NetmodeID.Server)
			{
				NetMessage.SendData(MessageID.WorldData);
			}
		}
	}

	public override void BossLoot(ref int potionType)
	{
		potionType = 3544;
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
