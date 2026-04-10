using System;
using Microsoft.Xna.Framework;
using Terraria;
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
		((ModNPC)this).DisplayName.SetDefault("Ignodium");
		Main.npcFrameCount[((ModNPC)this).npc.type] = 6;
	}

	public override void SetDefaults()
	{
		((ModNPC)this).npc.scale = 1.4f;
		((ModNPC)this).npc.width = 128;
		((ModNPC)this).npc.height = 212;
		((ModNPC)this).npc.HitSound = ((ModNPC)this).mod.GetLegacySoundSlot((SoundType)50, "Sounds/GodHit");
		((ModNPC)this).npc.DeathSound = ((ModNPC)this).mod.GetLegacySoundSlot((SoundType)50, "Sounds/GodDeath")?.WithVolume(0.7f)?.WithPitchVariance(0.5f);
		((ModNPC)this).npc.damage = 50;
		((ModNPC)this).npc.defense = 50;
		((ModNPC)this).npc.lifeMax = 220000;
		((ModNPC)this).npc.knockBackResist = 0f;
		((ModNPC)this).npc.boss = true;
		((ModNPC)this).npc.noGravity = true;
		((ModNPC)this).npc.noTileCollide = true;
		((ModNPC)this).npc.npcSlots = 1f;
		((ModNPC)this).npc.alpha = 255;
		base.bossBag = ((ModNPC)this).mod.ItemType("IgnodiumBag");
		((ModNPC)this).npc.aiStyle = -1;
		((ModNPC)this).npc.value = Item.buyPrice(0, 35);
		players = 1;
		if (!Phase2)
		{
			base.music = ((ModNPC)this).mod.GetSoundSlot((SoundType)51, "Sounds/Music/GuardiansPhase1");
		}
		for (int i = 0; i < 206; i++)
		{
			((ModNPC)this).npc.buffImmune[i] = true;
		}
	}

	public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
	{
		players = numPlayers;
		((ModNPC)this).npc.lifeMax = 380000 + numPlayers * 38000;
	}

	public override void AI()
	{
		((ModNPC)this).npc.spriteDirection = ((ModNPC)this).npc.direction;
		((ModNPC)this).npc.rotation = ((ModNPC)this).npc.velocity.X * 0.02f;
		Player player = Main.player[((ModNPC)this).npc.target];
		_ = Main.expertMode;
		((ModNPC)this).npc.TargetClosest();
		int num = (Main.expertMode ? 35 : 55);
		if (Main.player[((ModNPC)this).npc.target].dead || Main.player[((ModNPC)this).npc.target].dead || !(player.position.Y / 16f > (float)(Main.maxTilesY - 200)))
		{
			((ModNPC)this).npc.velocity.Y = -45f;
			((ModNPC)this).npc.ai[3] += 1f;
			if (((ModNPC)this).npc.ai[3] >= 100f)
			{
				((Entity)((ModNPC)this).npc).active = false;
			}
		}
		if (((ModNPC)this).npc.life > ((ModNPC)this).npc.lifeMax / 2)
		{
			(Main.player[((ModNPC)this).npc.target].Center - ((ModNPC)this).npc.Center).Normalize();
			((ModNPC)this).npc.velocity *= 0.985f;
			Timer++;
			if (Timer < 100)
			{
				((ModNPC)this).npc.velocity *= 0f;
				((ModNPC)this).npc.alpha -= 5;
			}
			if (Timer == 100)
			{
				for (int i = 0; i < 60; i++)
				{
					int num2 = Dust.NewDust(((ModNPC)this).npc.position, ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, 6, 0f, -2f, 0, default(Color), 1.5f);
					Main.dust[num2].noGravity = true;
					Main.dust[num2].scale = 2f;
					Main.dust[num2].position.X += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
					Main.dust[num2].position.Y += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
					if (Main.dust[num2].position != ((ModNPC)this).npc.Center)
					{
						Main.dust[num2].velocity = ((ModNPC)this).npc.DirectionTo(Main.dust[num2].position) * 10f;
					}
				}
			}
			if (Timer == 150)
			{
				Projectile.NewProjectile(((ModNPC)this).npc.Center.X, ((ModNPC)this).npc.Center.Y, 0f, 0f, ((ModNPC)this).mod.ProjectileType("ShockWave"), 0, 0f, 255, 0f, 0f);
				Main.PlaySound(((ModNPC)this).mod.GetLegacySoundSlot((SoundType)50, "Sounds/GuardianGrowl")?.WithVolume(10f), -1, -1);
			}
			if (Timer == 240 || Timer == 300 || Timer == 360 || Timer == 420 || Timer == 480 || Timer == 540)
			{
				Vector2 vector = Main.player[((ModNPC)this).npc.target].Center - ((ModNPC)this).npc.Center;
				vector.Normalize();
				vector.X *= 17f;
				vector.Y *= 17f;
				((ModNPC)this).npc.velocity.X = vector.X;
				((ModNPC)this).npc.velocity.Y = vector.Y;
				float num3 = 6.5f;
				int num4 = ((ModNPC)this).mod.ProjectileType("MoltenGlob");
				Main.PlaySound(2, (int)((ModNPC)this).npc.position.X, (int)((ModNPC)this).npc.position.Y, 20, 1f, 0f);
				float num5 = (float)Math.Atan2(((ModNPC)this).npc.Center.Y - player.Center.Y, ((ModNPC)this).npc.Center.X - player.Center.X);
				Projectile.NewProjectile(((ModNPC)this).npc.Center.X, ((ModNPC)this).npc.Center.Y, (float)(Math.Cos(num5) * (double)num3 * -1.0), (float)(Math.Sin(num5) * (double)num3 * -1.0), num4, num, 0f, Main.myPlayer, 0f, 0f);
			}
			if (AttackType == 0)
			{
				if (Timer >= 600 && Timer <= 850)
				{
					((ModNPC)this).npc.velocity.X = 0f;
					((ModNPC)this).npc.velocity.Y = 0f;
				}
				if (Timer == 660 || Timer == 700 || Timer == 740 || Timer == 780 || Timer == 820)
				{
					Main.PlaySound(((ModNPC)this).mod.GetLegacySoundSlot((SoundType)50, "Sounds/GuardianAttack")?.WithVolume(10f), -1, -1);
					float num6 = 6.5f;
					int num7 = ((ModNPC)this).mod.ProjectileType("FlameGigaBlast");
					Main.PlaySound(2, (int)((ModNPC)this).npc.position.X, (int)((ModNPC)this).npc.position.Y, 20, 1f, 0f);
					float num8 = (float)Math.Atan2(((ModNPC)this).npc.Center.Y - player.Center.Y, ((ModNPC)this).npc.Center.X - player.Center.X);
					Projectile.NewProjectile(((ModNPC)this).npc.Center.X, ((ModNPC)this).npc.Center.Y, (float)(Math.Cos(num8) * (double)num6 * -1.0), (float)(Math.Sin(num8) * (double)num6 * -1.0), num7, num, 0f, Main.myPlayer, 0f, 0f);
				}
			}
			if (AttackType == 1)
			{
				if (Timer >= 600 && Timer <= 850)
				{
					float num9 = 0.13f;
					Vector2 vector2 = new Vector2(((ModNPC)this).npc.position.X + (float)((ModNPC)this).npc.width * 0.5f, ((ModNPC)this).npc.position.Y + (float)((ModNPC)this).npc.height * 0.5f);
					float num10 = Main.player[((ModNPC)this).npc.target].position.X + (float)(Main.player[((ModNPC)this).npc.target].width / 2) - vector2.X;
					float num11 = Main.player[((ModNPC)this).npc.target].position.Y + (float)(Main.player[((ModNPC)this).npc.target].height / 2) - 120f - vector2.Y;
					float num12 = (float)Math.Sqrt(num10 * num10 + num11 * num11);
					float num13 = 13f / num12;
					num10 *= num13;
					num11 *= num13;
					if (((ModNPC)this).npc.velocity.X < num10)
					{
						((ModNPC)this).npc.velocity.X = ((ModNPC)this).npc.velocity.X + num9;
						if (((ModNPC)this).npc.velocity.X < 0f && num10 > 0f)
						{
							((ModNPC)this).npc.velocity.X = ((ModNPC)this).npc.velocity.X + num9;
						}
					}
					else if (((ModNPC)this).npc.velocity.X > num10)
					{
						((ModNPC)this).npc.velocity.X = ((ModNPC)this).npc.velocity.X - num9;
						if (((ModNPC)this).npc.velocity.X > 0f && num10 < 0f)
						{
							((ModNPC)this).npc.velocity.X = ((ModNPC)this).npc.velocity.X - num9;
						}
					}
					if (((ModNPC)this).npc.velocity.Y < num11)
					{
						((ModNPC)this).npc.velocity.Y = ((ModNPC)this).npc.velocity.Y + num9;
						if (((ModNPC)this).npc.velocity.Y < 0f && num11 > 0f)
						{
							((ModNPC)this).npc.velocity.Y = ((ModNPC)this).npc.velocity.Y + num9;
						}
					}
					else if (((ModNPC)this).npc.velocity.Y > num11)
					{
						((ModNPC)this).npc.velocity.Y = ((ModNPC)this).npc.velocity.Y - num9;
						if (((ModNPC)this).npc.velocity.Y > 0f && num11 < 0f)
						{
							((ModNPC)this).npc.velocity.Y = ((ModNPC)this).npc.velocity.Y - num9;
						}
					}
				}
				if (Timer == 660 || Timer == 720 || Timer == 780)
				{
					Main.PlaySound(((ModNPC)this).mod.GetLegacySoundSlot((SoundType)50, "Sounds/GuardianAttack")?.WithVolume(10f), -1, -1);
					for (int j = 0; j < 7; j++)
					{
						Vector2 vector3 = (0.8975979f * (float)j).ToRotationVector2();
						vector3.Normalize();
						vector3 *= 7f;
						Projectile.NewProjectile(((ModNPC)this).npc.Center.X, ((ModNPC)this).npc.Center.Y, vector3.X, vector3.Y, ((ModNPC)this).mod.ProjectileType("MoltenGlob"), num, 1f, Main.myPlayer, 0f, 0f);
					}
				}
			}
			if (AttackType == 2)
			{
				if (Timer >= 660 && Timer <= 720)
				{
					((ModNPC)this).npc.velocity *= 0f;
					int num14 = 36;
					for (int k = 0; k < num14; k++)
					{
						Vector2 vector4 = (Vector2.One * new Vector2((float)((ModNPC)this).npc.width / 7f, (float)((ModNPC)this).npc.height / 7f) * 0.75f * 0.5f).RotatedBy((float)(k - (num14 / 2 - 1)) * ((float)Math.PI * 2f) / (float)num14) + ((ModNPC)this).npc.Center;
						Vector2 vector5 = vector4 - ((ModNPC)this).npc.Center;
						Dust obj = Main.dust[Dust.NewDust(vector4 + vector5, 0, 0, 6, vector5.X * 2f, vector5.Y * 2f, 100, default(Color), 1.4f)];
						obj.noGravity = true;
						obj.noLight = false;
						obj.velocity = Vector2.Normalize(vector5) * 3f;
						obj.fadeIn = 1.3f;
					}
				}
				if (Timer == 720 || Timer == 750)
				{
					Main.PlaySound(((ModNPC)this).mod.GetLegacySoundSlot((SoundType)50, "Sounds/GuardianGrowl")?.WithVolume(10f), -1, -1);
					Vector2 spinningpoint = new Vector2(6f, 0f).RotatedByRandom(Math.PI * 2.0);
					for (int l = 0; l < 17; l++)
					{
						Vector2 vector6 = spinningpoint.RotatedBy(0.8975979010256552 * ((double)l + Main.rand.NextDouble() - 0.5));
						Projectile.NewProjectile(((ModNPC)this).npc.Center, vector6, ((ModNPC)this).mod.ProjectileType("FlameBolt"), num, 0f, Main.myPlayer, 0f, 0f);
					}
				}
			}
			if (AttackType == 3)
			{
				if (Timer > 600)
				{
					((ModNPC)this).npc.velocity *= 0f;
				}
				if (Timer == 660)
				{
					for (int m = -1; m <= 1; m++)
					{
						Projectile.NewProjectile(((ModNPC)this).npc.Center, 17f * ((ModNPC)this).npc.DirectionTo(player.Center).RotatedBy(MathHelper.ToRadians(5f) * (float)m), ((ModNPC)this).mod.ProjectileType("FlameBlast"), num, 0f, Main.myPlayer, 0f, 0f);
					}
				}
				if (Timer == 720)
				{
					for (int n = -2; n <= 2; n++)
					{
						Projectile.NewProjectile(((ModNPC)this).npc.Center, 17f * ((ModNPC)this).npc.DirectionTo(player.Center).RotatedBy(MathHelper.ToRadians(7f) * (float)n), ((ModNPC)this).mod.ProjectileType("FlameBlast"), num, 0f, Main.myPlayer, 0f, 0f);
					}
				}
				if (Timer == 780)
				{
					for (int num15 = -3; num15 <= 3; num15++)
					{
						Projectile.NewProjectile(((ModNPC)this).npc.Center, 17f * ((ModNPC)this).npc.DirectionTo(player.Center).RotatedBy(MathHelper.ToRadians(9f) * (float)num15), ((ModNPC)this).mod.ProjectileType("FlameBlast"), num, 0f, Main.myPlayer, 0f, 0f);
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
		if (((ModNPC)this).npc.life < ((ModNPC)this).npc.lifeMax / 2 && !Phase2)
		{
			Phase2 = true;
			Transition = true;
			base.music = ((ModNPC)this).mod.GetSoundSlot((SoundType)51, "Sounds/Music/GuardiansPhase2");
		}
		if (Transition)
		{
			((ModNPC)this).npc.velocity *= 0f;
			((ModNPC)this).npc.immortal = true;
			((ModNPC)this).npc.dontTakeDamage = true;
			TransitionTimer++;
			if (TransitionTimer == 120)
			{
				Main.PlaySound(((ModNPC)this).mod.GetLegacySoundSlot((SoundType)50, "Sounds/GuardianUNUN")?.WithVolume(25f), -1, -1);
				for (int num16 = 0; num16 < 60; num16++)
				{
					int num17 = Dust.NewDust(((ModNPC)this).npc.position, ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, 6, 0f, -2f, 0, default(Color), 1.5f);
					Main.dust[num17].noGravity = true;
					Main.dust[num17].scale = 2f;
					Main.dust[num17].position.X += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
					Main.dust[num17].position.Y += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
					if (Main.dust[num17].position != ((ModNPC)this).npc.Center)
					{
						Main.dust[num17].velocity = ((ModNPC)this).npc.DirectionTo(Main.dust[num17].position) * 10f;
					}
				}
			}
			if (TransitionTimer == 180)
			{
				Transition = false;
				TransitionTimer = 0;
			}
		}
		if (((ModNPC)this).npc.life < ((ModNPC)this).npc.lifeMax / 2 && ((ModNPC)this).npc.life >= ((ModNPC)this).npc.lifeMax / 8 && !Transition)
		{
			(Main.player[((ModNPC)this).npc.target].Center - ((ModNPC)this).npc.Center).Normalize();
			((ModNPC)this).npc.velocity *= 0.985f;
			((ModNPC)this).npc.immortal = false;
			((ModNPC)this).npc.dontTakeDamage = false;
			Timer2++;
			if (Timer2 >= 0 && Timer2 <= 500)
			{
				float num18 = 0.13f;
				Vector2 vector7 = new Vector2(((ModNPC)this).npc.position.X + (float)((ModNPC)this).npc.width * 0.5f, ((ModNPC)this).npc.position.Y + (float)((ModNPC)this).npc.height * 0.5f);
				float num19 = Main.player[((ModNPC)this).npc.target].position.X + (float)(Main.player[((ModNPC)this).npc.target].width / 2) - vector7.X;
				float num20 = Main.player[((ModNPC)this).npc.target].position.Y + (float)(Main.player[((ModNPC)this).npc.target].height / 2) - 120f - vector7.Y;
				float num21 = (float)Math.Sqrt(num19 * num19 + num20 * num20);
				float num22 = 13f / num21;
				num19 *= num22;
				num20 *= num22;
				if (((ModNPC)this).npc.velocity.X < num19)
				{
					((ModNPC)this).npc.velocity.X = ((ModNPC)this).npc.velocity.X + num18;
					if (((ModNPC)this).npc.velocity.X < 0f && num19 > 0f)
					{
						((ModNPC)this).npc.velocity.X = ((ModNPC)this).npc.velocity.X + num18;
					}
				}
				else if (((ModNPC)this).npc.velocity.X > num19)
				{
					((ModNPC)this).npc.velocity.X = ((ModNPC)this).npc.velocity.X - num18;
					if (((ModNPC)this).npc.velocity.X > 0f && num19 < 0f)
					{
						((ModNPC)this).npc.velocity.X = ((ModNPC)this).npc.velocity.X - num18;
					}
				}
				if (((ModNPC)this).npc.velocity.Y < num20)
				{
					((ModNPC)this).npc.velocity.Y = ((ModNPC)this).npc.velocity.Y + num18;
					if (((ModNPC)this).npc.velocity.Y < 0f && num20 > 0f)
					{
						((ModNPC)this).npc.velocity.Y = ((ModNPC)this).npc.velocity.Y + num18;
					}
				}
				else if (((ModNPC)this).npc.velocity.Y > num20)
				{
					((ModNPC)this).npc.velocity.Y = ((ModNPC)this).npc.velocity.Y - num18;
					if (((ModNPC)this).npc.velocity.Y > 0f && num20 < 0f)
					{
						((ModNPC)this).npc.velocity.Y = ((ModNPC)this).npc.velocity.Y - num18;
					}
				}
				if (Timer2 == 60 || Timer2 == 120 || Timer2 == 180 || Timer2 == 240 || Timer2 == 300 || Timer2 == 360 || Timer2 == 420 || Timer2 == 480)
				{
					float num23 = 12f;
					int num24 = ((ModNPC)this).mod.ProjectileType("FlameGigaBlast");
					Main.PlaySound(2, (int)((ModNPC)this).npc.position.X, (int)((ModNPC)this).npc.position.Y, 20, 1f, 0f);
					float num25 = (float)Math.Atan2(((ModNPC)this).npc.Center.Y - player.Center.Y, ((ModNPC)this).npc.Center.X - player.Center.X);
					Projectile.NewProjectile(((ModNPC)this).npc.Center.X, ((ModNPC)this).npc.Center.Y, (float)(Math.Cos(num25) * (double)num23 * -1.0), (float)(Math.Sin(num25) * (double)num23 * -1.0), num24, 30, 0f, Main.myPlayer, 0f, 0f);
				}
			}
			if (Timer2 == 540 || Timer2 == 560 || Timer2 == 580 || Timer2 == 600 || Timer2 == 620 || Timer2 == 640 || Timer2 == 660)
			{
				((ModNPC)this).npc.velocity *= 0f;
				Projectile.NewProjectile(player.Center.X, player.Center.Y + 550f, 0f, 0f, ((ModNPC)this).mod.ProjectileType("EruptionTelegraph"), 0, 0f, Main.myPlayer, 0f, 0f);
			}
			if (Timer2 == 720 || Timer2 == 780 || Timer2 == 840 || Timer2 == 900 || Timer2 == 960)
			{
				Vector2 vector8 = Main.player[((ModNPC)this).npc.target].Center - ((ModNPC)this).npc.Center;
				vector8.Normalize();
				vector8.X *= 18f;
				vector8.Y *= 18f;
				((ModNPC)this).npc.velocity.X = vector8.X;
				((ModNPC)this).npc.velocity.Y = vector8.Y;
				float num26 = 7.5f;
				int num27 = ((ModNPC)this).mod.ProjectileType("MoltenGlob");
				Main.PlaySound(2, (int)((ModNPC)this).npc.position.X, (int)((ModNPC)this).npc.position.Y, 20, 1f, 0f);
				float num28 = (float)Math.Atan2(((ModNPC)this).npc.Center.Y - player.Center.Y, ((ModNPC)this).npc.Center.X - player.Center.X);
				Projectile.NewProjectile(((ModNPC)this).npc.Center.X, ((ModNPC)this).npc.Center.Y, (float)(Math.Cos(num28) * (double)num26 * -1.0), (float)(Math.Sin(num28) * (double)num26 * -1.0), num27, num, 0f, Main.myPlayer, 0f, 0f);
			}
			if (Timer2 > 1020 && Timer2 < 1080)
			{
				((ModNPC)this).npc.velocity *= 0f;
				Vector2 spinningpoint2 = new Vector2(400f, 0f);
				Spin += 0.23f;
				int num29 = 1;
				for (int num30 = 0; num30 < num29; num30++)
				{
					float num31 = 360 / num29;
					Vector2 vector9 = ((ModNPC)this).npc.Center + spinningpoint2.RotatedBy((double)Spin + (double)(num31 * (float)num30) * (Math.PI / 4.0));
					float num32 = (float)Math.Atan2(((ModNPC)this).npc.Center.Y - vector9.Y, ((ModNPC)this).npc.Center.X - vector9.X);
					Projectile.NewProjectile(((ModNPC)this).npc.Center.X, ((ModNPC)this).npc.Center.Y, (float)(Math.Cos(num32) * 10.0 * -1.0), (float)(Math.Sin(num32) * 10.0 * -1.0), ((ModNPC)this).mod.ProjectileType("FlameBlast"), num, 0f, Main.myPlayer, 0f, (float)num30);
				}
			}
			if (Timer2 >= 1140)
			{
				Timer2 = 0;
			}
		}
		if (((ModNPC)this).npc.life > ((ModNPC)this).npc.lifeMax / 8 || Transition)
		{
			return;
		}
		((ModNPC)this).npc.velocity *= 0f;
		DesperationTimer++;
		if (DesperationTimer < 120)
		{
			((ModNPC)this).npc.immortal = true;
			((ModNPC)this).npc.dontTakeDamage = true;
		}
		if (DesperationTimer >= 120)
		{
			((ModNPC)this).npc.immortal = false;
			((ModNPC)this).npc.dontTakeDamage = false;
		}
		if (DesperationTimer == 80)
		{
			Main.PlaySound(((ModNPC)this).mod.GetLegacySoundSlot((SoundType)50, "Sounds/GuardianUNUN")?.WithVolume(25f), -1, -1);
			for (int num33 = 0; num33 < 60; num33++)
			{
				int num34 = Dust.NewDust(((ModNPC)this).npc.position, ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, 6, 0f, -2f, 0, default(Color), 1.5f);
				Main.dust[num34].noGravity = true;
				Main.dust[num34].scale = 2f;
				Main.dust[num34].position.X += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
				Main.dust[num34].position.Y += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
				if (Main.dust[num34].position != ((ModNPC)this).npc.Center)
				{
					Main.dust[num34].velocity = ((ModNPC)this).npc.DirectionTo(Main.dust[num34].position) * 10f;
				}
			}
		}
		if (DesperationTimer == 180 || DesperationTimer == 220 || DesperationTimer == 260)
		{
			Main.PlaySound(((ModNPC)this).mod.GetLegacySoundSlot((SoundType)50, "Sounds/GuardianAttack")?.WithVolume(25f), -1, -1);
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
				Main.projectile[Projectile.NewProjectile(vector10.X, vector10.Y, 0f, 0f, ((ModNPC)this).mod.ProjectileType("IgnodiumBeamTelegraph"), 0, 0f, Main.myPlayer, 0f, 0f)].localAI[1] = 125f;
			}
		}
		if (DesperationTimer == 300 || DesperationTimer == 310 || DesperationTimer == 320)
		{
			float num39 = 1.5f;
			float num40 = (float)Math.Atan2(((ModNPC)this).npc.Center.Y - player.Center.Y, ((ModNPC)this).npc.Center.X - player.Center.X);
			Projectile.NewProjectile(((ModNPC)this).npc.Center.X, ((ModNPC)this).npc.Center.Y, (float)(Math.Cos(num40) * (double)num39 * -1.0), (float)(Math.Sin(num40) * (double)num39 * -1.0), ((ModNPC)this).mod.ProjectileType("IgnodiumBeam"), num, 0f, 0, 0f, 0f);
		}
		if (DesperationTimer > 340 && DesperationTimer < 440)
		{
			Vector2 spinningpoint3 = new Vector2(400f, 0f);
			Spin += 0.23f;
			int num41 = 1;
			for (int num42 = 0; num42 < num41; num42++)
			{
				float num43 = 360 / num41;
				Vector2 vector11 = ((ModNPC)this).npc.Center + spinningpoint3.RotatedBy((double)Spin + (double)(num43 * (float)num42) * (Math.PI / 4.0));
				float num44 = (float)Math.Atan2(((ModNPC)this).npc.Center.Y - vector11.Y, ((ModNPC)this).npc.Center.X - vector11.X);
				Projectile.NewProjectile(((ModNPC)this).npc.Center.X, ((ModNPC)this).npc.Center.Y, (float)(Math.Cos(num44) * 0.5 * -1.0), (float)(Math.Sin(num44) * 0.5 * -1.0), ((ModNPC)this).mod.ProjectileType("IgnodiumBeam"), num, 0f, Main.myPlayer, 0f, (float)num42);
			}
		}
		if (DesperationTimer >= 500)
		{
			DesperationTimer = 120;
			for (int num45 = 0; num45 < 50; num45++)
			{
				int num46 = Dust.NewDust(((ModNPC)this).npc.position, ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, 6);
				Main.dust[num46].scale = 1.5f;
			}
			((ModNPC)this).npc.position.X = player.position.X - 100f;
			((ModNPC)this).npc.position.Y = player.position.Y - 400f;
			for (int num47 = 0; num47 < 50; num47++)
			{
				int num48 = Dust.NewDust(((ModNPC)this).npc.position, ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, 6);
				Main.dust[num48].scale = 1.5f;
			}
		}
	}

	public override void HitEffect(int hitDirection, double damage)
	{
		if (((ModNPC)this).npc.life > 0)
		{
			return;
		}
		for (int i = 0; i < 40; i++)
		{
			int num = Dust.NewDust(((ModNPC)this).npc.position, ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, 6, 0f, -2f, 0, default(Color), 1.5f);
			Main.dust[num].noGravity = true;
			Main.dust[num].scale = 2f;
			Main.dust[num].position.X += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
			Main.dust[num].position.Y += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
			if (Main.dust[num].position != ((ModNPC)this).npc.Center)
			{
				Main.dust[num].velocity = ((ModNPC)this).npc.DirectionTo(Main.dust[num].position) * 2f;
			}
		}
		((ModNPC)this).npc.position.X = ((ModNPC)this).npc.position.X + (float)(((ModNPC)this).npc.width / 2);
		((ModNPC)this).npc.position.Y = ((ModNPC)this).npc.position.Y + (float)(((ModNPC)this).npc.height / 2);
		((ModNPC)this).npc.width = 128;
		((ModNPC)this).npc.height = 212;
		((ModNPC)this).npc.position.X = ((ModNPC)this).npc.position.X - (float)(((ModNPC)this).npc.width / 2);
		((ModNPC)this).npc.position.Y = ((ModNPC)this).npc.position.Y - (float)(((ModNPC)this).npc.height / 2);
		for (int j = 0; j < 20; j++)
		{
			int num2 = Dust.NewDust(new Vector2(((ModNPC)this).npc.position.X, ((ModNPC)this).npc.position.Y), ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, 6, 0f, 0f, 100, default(Color), 2f);
			Main.dust[num2].velocity *= 3f;
			if (Main.rand.Next(2) == 0)
			{
				Main.dust[num2].scale = 1.5f;
				Main.dust[num2].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
			}
		}
	}

	public override void NPCLoot()
	{
		if (Main.expertMode)
		{
			((ModNPC)this).npc.DropBossBags();
		}
		else
		{
			int num = Main.rand.Next(7);
			if (num == 0)
			{
				Item.NewItem((int)((ModNPC)this).npc.position.X, (int)((ModNPC)this).npc.position.Y, ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, ((ModNPC)this).mod.ItemType("HellFlail"), 1, false, 0, false, false);
			}
			if (num == 1)
			{
				Item.NewItem((int)((ModNPC)this).npc.position.X, (int)((ModNPC)this).npc.position.Y, ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, ((ModNPC)this).mod.ItemType("HellThrow"), 1, false, 0, false, false);
			}
			if (num == 2)
			{
				Item.NewItem((int)((ModNPC)this).npc.position.X, (int)((ModNPC)this).npc.position.Y, ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, ((ModNPC)this).mod.ItemType("HellGun"), 1, false, 0, false, false);
			}
			if (num == 3)
			{
				Item.NewItem((int)((ModNPC)this).npc.position.X, (int)((ModNPC)this).npc.position.Y, ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, ((ModNPC)this).mod.ItemType("HellJavelin"), 1, false, 0, false, false);
			}
			if (num == 4)
			{
				Item.NewItem((int)((ModNPC)this).npc.position.X, (int)((ModNPC)this).npc.position.Y, ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, ((ModNPC)this).mod.ItemType("HellStaff"), 1, false, 0, false, false);
			}
			if (num == 5)
			{
				Item.NewItem((int)((ModNPC)this).npc.position.X, (int)((ModNPC)this).npc.position.Y, ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, ((ModNPC)this).mod.ItemType("HellTome"), 1, false, 0, false, false);
			}
			if (num == 6)
			{
				Item.NewItem((int)((ModNPC)this).npc.position.X, (int)((ModNPC)this).npc.position.Y, ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, ((ModNPC)this).mod.ItemType("HellScepter"), 1, false, 0, false, false);
			}
			Item.NewItem(((ModNPC)this).npc.getRect(), ((ModNPC)this).mod.ItemType("HellShard"), Main.rand.Next(25, 32), false, 0, false, false);
		}
		if (Main.rand.Next(7) == 0)
		{
			Item.NewItem(((ModNPC)this).npc.getRect(), ((ModNPC)this).mod.ItemType("IgnodiumMask"), 1, false, 0, false, false);
		}
		if (Main.rand.Next(10) == 0)
		{
			Item.NewItem(((ModNPC)this).npc.getRect(), ((ModNPC)this).mod.ItemType("IgnodiumTrophyItem"), 1, false, 0, false, false);
		}
		if (!UltraniumWorld.downedIgnodium)
		{
			UltraniumWorld.downedIgnodium = true;
			if (Main.netMode == 2)
			{
				NetMessage.SendData(7);
			}
		}
	}

	public override void BossLoot(ref string name, ref int potionType)
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
		((ModNPC)this).npc.frameCounter += 1.0;
		if (((ModNPC)this).npc.frameCounter >= 8.0)
		{
			((ModNPC)this).npc.frame.Y = (((ModNPC)this).npc.frame.Y + frameHeight) % (Main.npcFrameCount[((ModNPC)this).npc.type] * frameHeight);
			((ModNPC)this).npc.frameCounter = 1.0;
		}
	}
}
