using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
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
		// ((ModNPC)this).DisplayName.SetDefault("Ignodium");
		Main.npcFrameCount[((ModNPC)this).NPC.type] = 6;
	}

	public override void SetDefaults()
	{
		((ModNPC)this).NPC.scale = 1.4f;
		((ModNPC)this).NPC.width = 128;
		((ModNPC)this).NPC.height = 212;
		((ModNPC)this).NPC.HitSound = ((ModNPC)this).Mod.GetLegacySoundSlot((SoundType)50, "Sounds/GodHit");
		((ModNPC)this).NPC.DeathSound = ((ModNPC)this).Mod.GetLegacySoundSlot((SoundType)50, "Sounds/GodDeath")?.WithVolume(0.7f)?.WithPitchVariance(0.5f);
		((ModNPC)this).NPC.damage = 50;
		((ModNPC)this).NPC.defense = 50;
		((ModNPC)this).NPC.lifeMax = 220000;
		((ModNPC)this).NPC.knockBackResist = 0f;
		((ModNPC)this).NPC.boss = true;
		((ModNPC)this).NPC.noGravity = true;
		((ModNPC)this).NPC.noTileCollide = true;
		((ModNPC)this).NPC.npcSlots = 1f;
		((ModNPC)this).NPC.alpha = 255;
		base.bossBag/* tModPorter Note: Removed. Spawn the treasure bag alongside other loot via npcLoot.Add(ItemDropRule.BossBag(type)) */ = ((ModNPC)this).Mod.Find<ModItem>("IgnodiumBag").Type;
		((ModNPC)this).NPC.aiStyle = -1;
		((ModNPC)this).NPC.value = Item.buyPrice(0, 35);
		players = 1;
		if (!Phase2)
		{
			base.Music = ((ModNPC)this).Mod.GetSoundSlot((SoundType)51, "Sounds/Music/GuardiansPhase1");
		}
		for (int i = 0; i < 206; i++)
		{
			((ModNPC)this).NPC.buffImmune[i] = true;
		}
	}

	public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
	{
		players = numPlayers;
		((ModNPC)this).NPC.lifeMax = 380000 + numPlayers * 38000;
	}

	public override void AI()
	{
		((ModNPC)this).NPC.spriteDirection = ((ModNPC)this).NPC.direction;
		((ModNPC)this).NPC.rotation = ((ModNPC)this).NPC.velocity.X * 0.02f;
		Player player = Main.player[((ModNPC)this).NPC.target];
		_ = Main.expertMode;
		((ModNPC)this).NPC.TargetClosest();
		int num = (Main.expertMode ? 35 : 55);
		if (Main.player[((ModNPC)this).NPC.target].dead || Main.player[((ModNPC)this).NPC.target].dead || !(player.position.Y / 16f > (float)(Main.maxTilesY - 200)))
		{
			((ModNPC)this).NPC.velocity.Y = -45f;
			((ModNPC)this).NPC.ai[3] += 1f;
			if (((ModNPC)this).NPC.ai[3] >= 100f)
			{
				((Entity)((ModNPC)this).NPC).active = false;
			}
		}
		if (((ModNPC)this).NPC.life > ((ModNPC)this).NPC.lifeMax / 2)
		{
			(Main.player[((ModNPC)this).NPC.target].Center - ((ModNPC)this).NPC.Center).Normalize();
			((ModNPC)this).NPC.velocity *= 0.985f;
			Timer++;
			if (Timer < 100)
			{
				((ModNPC)this).NPC.velocity *= 0f;
				((ModNPC)this).NPC.alpha -= 5;
			}
			if (Timer == 100)
			{
				for (int i = 0; i < 60; i++)
				{
					int num2 = Dust.NewDust(((ModNPC)this).NPC.position, ((ModNPC)this).NPC.width, ((ModNPC)this).NPC.height, 6, 0f, -2f, 0, default(Color), 1.5f);
					Main.dust[num2].noGravity = true;
					Main.dust[num2].scale = 2f;
					Main.dust[num2].position.X += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
					Main.dust[num2].position.Y += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
					if (Main.dust[num2].position != ((ModNPC)this).NPC.Center)
					{
						Main.dust[num2].velocity = ((ModNPC)this).NPC.DirectionTo(Main.dust[num2].position) * 10f;
					}
				}
			}
			if (Timer == 150)
			{
				Projectile.NewProjectile(((ModNPC)this).NPC.Center.X, ((ModNPC)this).NPC.Center.Y, 0f, 0f, ((ModNPC)this).Mod.Find<ModProjectile>("ShockWave").Type, 0, 0f, 255, 0f, 0f);
				SoundEngine.PlaySound(((ModNPC)this).Mod.GetLegacySoundSlot((SoundType)50, "Sounds/GuardianGrowl")?.WithVolume(10f), -1, -1);
			}
			if (Timer == 240 || Timer == 300 || Timer == 360 || Timer == 420 || Timer == 480 || Timer == 540)
			{
				Vector2 vector = Main.player[((ModNPC)this).NPC.target].Center - ((ModNPC)this).NPC.Center;
				vector.Normalize();
				vector.X *= 17f;
				vector.Y *= 17f;
				((ModNPC)this).NPC.velocity.X = vector.X;
				((ModNPC)this).NPC.velocity.Y = vector.Y;
				float num3 = 6.5f;
				int num4 = ((ModNPC)this).Mod.Find<ModProjectile>("MoltenGlob").Type;
				SoundEngine.PlaySound(SoundID.Item20, new Vector2(((ModNPC)this).NPC.position.X, ((ModNPC)this).NPC.position.Y));
				float num5 = (float)Math.Atan2(((ModNPC)this).NPC.Center.Y - player.Center.Y, ((ModNPC)this).NPC.Center.X - player.Center.X);
				Projectile.NewProjectile(((ModNPC)this).NPC.Center.X, ((ModNPC)this).NPC.Center.Y, (float)(Math.Cos(num5) * (double)num3 * -1.0), (float)(Math.Sin(num5) * (double)num3 * -1.0), num4, num, 0f, Main.myPlayer, 0f, 0f);
			}
			if (AttackType == 0)
			{
				if (Timer >= 600 && Timer <= 850)
				{
					((ModNPC)this).NPC.velocity.X = 0f;
					((ModNPC)this).NPC.velocity.Y = 0f;
				}
				if (Timer == 660 || Timer == 700 || Timer == 740 || Timer == 780 || Timer == 820)
				{
					SoundEngine.PlaySound(((ModNPC)this).Mod.GetLegacySoundSlot((SoundType)50, "Sounds/GuardianAttack")?.WithVolume(10f), -1, -1);
					float num6 = 6.5f;
					int num7 = ((ModNPC)this).Mod.Find<ModProjectile>("FlameGigaBlast").Type;
					SoundEngine.PlaySound(SoundID.Item20, new Vector2(((ModNPC)this).NPC.position.X, ((ModNPC)this).NPC.position.Y));
					float num8 = (float)Math.Atan2(((ModNPC)this).NPC.Center.Y - player.Center.Y, ((ModNPC)this).NPC.Center.X - player.Center.X);
					Projectile.NewProjectile(((ModNPC)this).NPC.Center.X, ((ModNPC)this).NPC.Center.Y, (float)(Math.Cos(num8) * (double)num6 * -1.0), (float)(Math.Sin(num8) * (double)num6 * -1.0), num7, num, 0f, Main.myPlayer, 0f, 0f);
				}
			}
			if (AttackType == 1)
			{
				if (Timer >= 600 && Timer <= 850)
				{
					float num9 = 0.13f;
					Vector2 vector2 = new Vector2(((ModNPC)this).NPC.position.X + (float)((ModNPC)this).NPC.width * 0.5f, ((ModNPC)this).NPC.position.Y + (float)((ModNPC)this).NPC.height * 0.5f);
					float num10 = Main.player[((ModNPC)this).NPC.target].position.X + (float)(Main.player[((ModNPC)this).NPC.target].width / 2) - vector2.X;
					float num11 = Main.player[((ModNPC)this).NPC.target].position.Y + (float)(Main.player[((ModNPC)this).NPC.target].height / 2) - 120f - vector2.Y;
					float num12 = (float)Math.Sqrt(num10 * num10 + num11 * num11);
					float num13 = 13f / num12;
					num10 *= num13;
					num11 *= num13;
					if (((ModNPC)this).NPC.velocity.X < num10)
					{
						((ModNPC)this).NPC.velocity.X = ((ModNPC)this).NPC.velocity.X + num9;
						if (((ModNPC)this).NPC.velocity.X < 0f && num10 > 0f)
						{
							((ModNPC)this).NPC.velocity.X = ((ModNPC)this).NPC.velocity.X + num9;
						}
					}
					else if (((ModNPC)this).NPC.velocity.X > num10)
					{
						((ModNPC)this).NPC.velocity.X = ((ModNPC)this).NPC.velocity.X - num9;
						if (((ModNPC)this).NPC.velocity.X > 0f && num10 < 0f)
						{
							((ModNPC)this).NPC.velocity.X = ((ModNPC)this).NPC.velocity.X - num9;
						}
					}
					if (((ModNPC)this).NPC.velocity.Y < num11)
					{
						((ModNPC)this).NPC.velocity.Y = ((ModNPC)this).NPC.velocity.Y + num9;
						if (((ModNPC)this).NPC.velocity.Y < 0f && num11 > 0f)
						{
							((ModNPC)this).NPC.velocity.Y = ((ModNPC)this).NPC.velocity.Y + num9;
						}
					}
					else if (((ModNPC)this).NPC.velocity.Y > num11)
					{
						((ModNPC)this).NPC.velocity.Y = ((ModNPC)this).NPC.velocity.Y - num9;
						if (((ModNPC)this).NPC.velocity.Y > 0f && num11 < 0f)
						{
							((ModNPC)this).NPC.velocity.Y = ((ModNPC)this).NPC.velocity.Y - num9;
						}
					}
				}
				if (Timer == 660 || Timer == 720 || Timer == 780)
				{
					SoundEngine.PlaySound(((ModNPC)this).Mod.GetLegacySoundSlot((SoundType)50, "Sounds/GuardianAttack")?.WithVolume(10f), -1, -1);
					for (int j = 0; j < 7; j++)
					{
						Vector2 vector3 = (0.8975979f * (float)j).ToRotationVector2();
						vector3.Normalize();
						vector3 *= 7f;
						Projectile.NewProjectile(((ModNPC)this).NPC.Center.X, ((ModNPC)this).NPC.Center.Y, vector3.X, vector3.Y, ((ModNPC)this).Mod.Find<ModProjectile>("MoltenGlob").Type, num, 1f, Main.myPlayer, 0f, 0f);
					}
				}
			}
			if (AttackType == 2)
			{
				if (Timer >= 660 && Timer <= 720)
				{
					((ModNPC)this).NPC.velocity *= 0f;
					int num14 = 36;
					for (int k = 0; k < num14; k++)
					{
						Vector2 vector4 = (Vector2.One * new Vector2((float)((ModNPC)this).NPC.width / 7f, (float)((ModNPC)this).NPC.height / 7f) * 0.75f * 0.5f).RotatedBy((float)(k - (num14 / 2 - 1)) * ((float)Math.PI * 2f) / (float)num14) + ((ModNPC)this).NPC.Center;
						Vector2 vector5 = vector4 - ((ModNPC)this).NPC.Center;
						Dust obj = Main.dust[Dust.NewDust(vector4 + vector5, 0, 0, 6, vector5.X * 2f, vector5.Y * 2f, 100, default(Color), 1.4f)];
						obj.noGravity = true;
						obj.noLight = false;
						obj.velocity = Vector2.Normalize(vector5) * 3f;
						obj.fadeIn = 1.3f;
					}
				}
				if (Timer == 720 || Timer == 750)
				{
					SoundEngine.PlaySound(((ModNPC)this).Mod.GetLegacySoundSlot((SoundType)50, "Sounds/GuardianGrowl")?.WithVolume(10f), -1, -1);
					Vector2 spinningpoint = new Vector2(6f, 0f).RotatedByRandom(Math.PI * 2.0);
					for (int l = 0; l < 17; l++)
					{
						Vector2 vector6 = spinningpoint.RotatedBy(0.8975979010256552 * ((double)l + Main.rand.NextDouble() - 0.5));
						Projectile.NewProjectile(((ModNPC)this).NPC.Center, vector6, ((ModNPC)this).Mod.Find<ModProjectile>("FlameBolt").Type, num, 0f, Main.myPlayer, 0f, 0f);
					}
				}
			}
			if (AttackType == 3)
			{
				if (Timer > 600)
				{
					((ModNPC)this).NPC.velocity *= 0f;
				}
				if (Timer == 660)
				{
					for (int m = -1; m <= 1; m++)
					{
						Projectile.NewProjectile(((ModNPC)this).NPC.Center, 17f * ((ModNPC)this).NPC.DirectionTo(player.Center).RotatedBy(MathHelper.ToRadians(5f) * (float)m), ((ModNPC)this).Mod.Find<ModProjectile>("FlameBlast").Type, num, 0f, Main.myPlayer, 0f, 0f);
					}
				}
				if (Timer == 720)
				{
					for (int n = -2; n <= 2; n++)
					{
						Projectile.NewProjectile(((ModNPC)this).NPC.Center, 17f * ((ModNPC)this).NPC.DirectionTo(player.Center).RotatedBy(MathHelper.ToRadians(7f) * (float)n), ((ModNPC)this).Mod.Find<ModProjectile>("FlameBlast").Type, num, 0f, Main.myPlayer, 0f, 0f);
					}
				}
				if (Timer == 780)
				{
					for (int num15 = -3; num15 <= 3; num15++)
					{
						Projectile.NewProjectile(((ModNPC)this).NPC.Center, 17f * ((ModNPC)this).NPC.DirectionTo(player.Center).RotatedBy(MathHelper.ToRadians(9f) * (float)num15), ((ModNPC)this).Mod.Find<ModProjectile>("FlameBlast").Type, num, 0f, Main.myPlayer, 0f, 0f);
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
		if (((ModNPC)this).NPC.life < ((ModNPC)this).NPC.lifeMax / 2 && !Phase2)
		{
			Phase2 = true;
			Transition = true;
			base.Music = ((ModNPC)this).Mod.GetSoundSlot((SoundType)51, "Sounds/Music/GuardiansPhase2");
		}
		if (Transition)
		{
			((ModNPC)this).NPC.velocity *= 0f;
			((ModNPC)this).NPC.immortal = true;
			((ModNPC)this).NPC.dontTakeDamage = true;
			TransitionTimer++;
			if (TransitionTimer == 120)
			{
				SoundEngine.PlaySound(((ModNPC)this).Mod.GetLegacySoundSlot((SoundType)50, "Sounds/GuardianUNUN")?.WithVolume(25f), -1, -1);
				for (int num16 = 0; num16 < 60; num16++)
				{
					int num17 = Dust.NewDust(((ModNPC)this).NPC.position, ((ModNPC)this).NPC.width, ((ModNPC)this).NPC.height, 6, 0f, -2f, 0, default(Color), 1.5f);
					Main.dust[num17].noGravity = true;
					Main.dust[num17].scale = 2f;
					Main.dust[num17].position.X += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
					Main.dust[num17].position.Y += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
					if (Main.dust[num17].position != ((ModNPC)this).NPC.Center)
					{
						Main.dust[num17].velocity = ((ModNPC)this).NPC.DirectionTo(Main.dust[num17].position) * 10f;
					}
				}
			}
			if (TransitionTimer == 180)
			{
				Transition = false;
				TransitionTimer = 0;
			}
		}
		if (((ModNPC)this).NPC.life < ((ModNPC)this).NPC.lifeMax / 2 && ((ModNPC)this).NPC.life >= ((ModNPC)this).NPC.lifeMax / 8 && !Transition)
		{
			(Main.player[((ModNPC)this).NPC.target].Center - ((ModNPC)this).NPC.Center).Normalize();
			((ModNPC)this).NPC.velocity *= 0.985f;
			((ModNPC)this).NPC.immortal = false;
			((ModNPC)this).NPC.dontTakeDamage = false;
			Timer2++;
			if (Timer2 >= 0 && Timer2 <= 500)
			{
				float num18 = 0.13f;
				Vector2 vector7 = new Vector2(((ModNPC)this).NPC.position.X + (float)((ModNPC)this).NPC.width * 0.5f, ((ModNPC)this).NPC.position.Y + (float)((ModNPC)this).NPC.height * 0.5f);
				float num19 = Main.player[((ModNPC)this).NPC.target].position.X + (float)(Main.player[((ModNPC)this).NPC.target].width / 2) - vector7.X;
				float num20 = Main.player[((ModNPC)this).NPC.target].position.Y + (float)(Main.player[((ModNPC)this).NPC.target].height / 2) - 120f - vector7.Y;
				float num21 = (float)Math.Sqrt(num19 * num19 + num20 * num20);
				float num22 = 13f / num21;
				num19 *= num22;
				num20 *= num22;
				if (((ModNPC)this).NPC.velocity.X < num19)
				{
					((ModNPC)this).NPC.velocity.X = ((ModNPC)this).NPC.velocity.X + num18;
					if (((ModNPC)this).NPC.velocity.X < 0f && num19 > 0f)
					{
						((ModNPC)this).NPC.velocity.X = ((ModNPC)this).NPC.velocity.X + num18;
					}
				}
				else if (((ModNPC)this).NPC.velocity.X > num19)
				{
					((ModNPC)this).NPC.velocity.X = ((ModNPC)this).NPC.velocity.X - num18;
					if (((ModNPC)this).NPC.velocity.X > 0f && num19 < 0f)
					{
						((ModNPC)this).NPC.velocity.X = ((ModNPC)this).NPC.velocity.X - num18;
					}
				}
				if (((ModNPC)this).NPC.velocity.Y < num20)
				{
					((ModNPC)this).NPC.velocity.Y = ((ModNPC)this).NPC.velocity.Y + num18;
					if (((ModNPC)this).NPC.velocity.Y < 0f && num20 > 0f)
					{
						((ModNPC)this).NPC.velocity.Y = ((ModNPC)this).NPC.velocity.Y + num18;
					}
				}
				else if (((ModNPC)this).NPC.velocity.Y > num20)
				{
					((ModNPC)this).NPC.velocity.Y = ((ModNPC)this).NPC.velocity.Y - num18;
					if (((ModNPC)this).NPC.velocity.Y > 0f && num20 < 0f)
					{
						((ModNPC)this).NPC.velocity.Y = ((ModNPC)this).NPC.velocity.Y - num18;
					}
				}
				if (Timer2 == 60 || Timer2 == 120 || Timer2 == 180 || Timer2 == 240 || Timer2 == 300 || Timer2 == 360 || Timer2 == 420 || Timer2 == 480)
				{
					float num23 = 12f;
					int num24 = ((ModNPC)this).Mod.Find<ModProjectile>("FlameGigaBlast").Type;
					SoundEngine.PlaySound(SoundID.Item20, new Vector2(((ModNPC)this).NPC.position.X, ((ModNPC)this).NPC.position.Y));
					float num25 = (float)Math.Atan2(((ModNPC)this).NPC.Center.Y - player.Center.Y, ((ModNPC)this).NPC.Center.X - player.Center.X);
					Projectile.NewProjectile(((ModNPC)this).NPC.Center.X, ((ModNPC)this).NPC.Center.Y, (float)(Math.Cos(num25) * (double)num23 * -1.0), (float)(Math.Sin(num25) * (double)num23 * -1.0), num24, 30, 0f, Main.myPlayer, 0f, 0f);
				}
			}
			if (Timer2 == 540 || Timer2 == 560 || Timer2 == 580 || Timer2 == 600 || Timer2 == 620 || Timer2 == 640 || Timer2 == 660)
			{
				((ModNPC)this).NPC.velocity *= 0f;
				Projectile.NewProjectile(player.Center.X, player.Center.Y + 550f, 0f, 0f, ((ModNPC)this).Mod.Find<ModProjectile>("EruptionTelegraph").Type, 0, 0f, Main.myPlayer, 0f, 0f);
			}
			if (Timer2 == 720 || Timer2 == 780 || Timer2 == 840 || Timer2 == 900 || Timer2 == 960)
			{
				Vector2 vector8 = Main.player[((ModNPC)this).NPC.target].Center - ((ModNPC)this).NPC.Center;
				vector8.Normalize();
				vector8.X *= 18f;
				vector8.Y *= 18f;
				((ModNPC)this).NPC.velocity.X = vector8.X;
				((ModNPC)this).NPC.velocity.Y = vector8.Y;
				float num26 = 7.5f;
				int num27 = ((ModNPC)this).Mod.Find<ModProjectile>("MoltenGlob").Type;
				SoundEngine.PlaySound(SoundID.Item20, new Vector2(((ModNPC)this).NPC.position.X, ((ModNPC)this).NPC.position.Y));
				float num28 = (float)Math.Atan2(((ModNPC)this).NPC.Center.Y - player.Center.Y, ((ModNPC)this).NPC.Center.X - player.Center.X);
				Projectile.NewProjectile(((ModNPC)this).NPC.Center.X, ((ModNPC)this).NPC.Center.Y, (float)(Math.Cos(num28) * (double)num26 * -1.0), (float)(Math.Sin(num28) * (double)num26 * -1.0), num27, num, 0f, Main.myPlayer, 0f, 0f);
			}
			if (Timer2 > 1020 && Timer2 < 1080)
			{
				((ModNPC)this).NPC.velocity *= 0f;
				Vector2 spinningpoint2 = new Vector2(400f, 0f);
				Spin += 0.23f;
				int num29 = 1;
				for (int num30 = 0; num30 < num29; num30++)
				{
					float num31 = 360 / num29;
					Vector2 vector9 = ((ModNPC)this).NPC.Center + spinningpoint2.RotatedBy((double)Spin + (double)(num31 * (float)num30) * (Math.PI / 4.0));
					float num32 = (float)Math.Atan2(((ModNPC)this).NPC.Center.Y - vector9.Y, ((ModNPC)this).NPC.Center.X - vector9.X);
					Projectile.NewProjectile(((ModNPC)this).NPC.Center.X, ((ModNPC)this).NPC.Center.Y, (float)(Math.Cos(num32) * 10.0 * -1.0), (float)(Math.Sin(num32) * 10.0 * -1.0), ((ModNPC)this).Mod.Find<ModProjectile>("FlameBlast").Type, num, 0f, Main.myPlayer, 0f, (float)num30);
				}
			}
			if (Timer2 >= 1140)
			{
				Timer2 = 0;
			}
		}
		if (((ModNPC)this).NPC.life > ((ModNPC)this).NPC.lifeMax / 8 || Transition)
		{
			return;
		}
		((ModNPC)this).NPC.velocity *= 0f;
		DesperationTimer++;
		if (DesperationTimer < 120)
		{
			((ModNPC)this).NPC.immortal = true;
			((ModNPC)this).NPC.dontTakeDamage = true;
		}
		if (DesperationTimer >= 120)
		{
			((ModNPC)this).NPC.immortal = false;
			((ModNPC)this).NPC.dontTakeDamage = false;
		}
		if (DesperationTimer == 80)
		{
			SoundEngine.PlaySound(((ModNPC)this).Mod.GetLegacySoundSlot((SoundType)50, "Sounds/GuardianUNUN")?.WithVolume(25f), -1, -1);
			for (int num33 = 0; num33 < 60; num33++)
			{
				int num34 = Dust.NewDust(((ModNPC)this).NPC.position, ((ModNPC)this).NPC.width, ((ModNPC)this).NPC.height, 6, 0f, -2f, 0, default(Color), 1.5f);
				Main.dust[num34].noGravity = true;
				Main.dust[num34].scale = 2f;
				Main.dust[num34].position.X += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
				Main.dust[num34].position.Y += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
				if (Main.dust[num34].position != ((ModNPC)this).NPC.Center)
				{
					Main.dust[num34].velocity = ((ModNPC)this).NPC.DirectionTo(Main.dust[num34].position) * 10f;
				}
			}
		}
		if (DesperationTimer == 180 || DesperationTimer == 220 || DesperationTimer == 260)
		{
			SoundEngine.PlaySound(((ModNPC)this).Mod.GetLegacySoundSlot((SoundType)50, "Sounds/GuardianAttack")?.WithVolume(25f), -1, -1);
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
				Main.projectile[Projectile.NewProjectile(vector10.X, vector10.Y, 0f, 0f, ((ModNPC)this).Mod.Find<ModProjectile>("IgnodiumBeamTelegraph").Type, 0, 0f, Main.myPlayer, 0f, 0f)].localAI[1] = 125f;
			}
		}
		if (DesperationTimer == 300 || DesperationTimer == 310 || DesperationTimer == 320)
		{
			float num39 = 1.5f;
			float num40 = (float)Math.Atan2(((ModNPC)this).NPC.Center.Y - player.Center.Y, ((ModNPC)this).NPC.Center.X - player.Center.X);
			Projectile.NewProjectile(((ModNPC)this).NPC.Center.X, ((ModNPC)this).NPC.Center.Y, (float)(Math.Cos(num40) * (double)num39 * -1.0), (float)(Math.Sin(num40) * (double)num39 * -1.0), ((ModNPC)this).Mod.Find<ModProjectile>("IgnodiumBeam").Type, num, 0f, 0, 0f, 0f);
		}
		if (DesperationTimer > 340 && DesperationTimer < 440)
		{
			Vector2 spinningpoint3 = new Vector2(400f, 0f);
			Spin += 0.23f;
			int num41 = 1;
			for (int num42 = 0; num42 < num41; num42++)
			{
				float num43 = 360 / num41;
				Vector2 vector11 = ((ModNPC)this).NPC.Center + spinningpoint3.RotatedBy((double)Spin + (double)(num43 * (float)num42) * (Math.PI / 4.0));
				float num44 = (float)Math.Atan2(((ModNPC)this).NPC.Center.Y - vector11.Y, ((ModNPC)this).NPC.Center.X - vector11.X);
				Projectile.NewProjectile(((ModNPC)this).NPC.Center.X, ((ModNPC)this).NPC.Center.Y, (float)(Math.Cos(num44) * 0.5 * -1.0), (float)(Math.Sin(num44) * 0.5 * -1.0), ((ModNPC)this).Mod.Find<ModProjectile>("IgnodiumBeam").Type, num, 0f, Main.myPlayer, 0f, (float)num42);
			}
		}
		if (DesperationTimer >= 500)
		{
			DesperationTimer = 120;
			for (int num45 = 0; num45 < 50; num45++)
			{
				int num46 = Dust.NewDust(((ModNPC)this).NPC.position, ((ModNPC)this).NPC.width, ((ModNPC)this).NPC.height, 6);
				Main.dust[num46].scale = 1.5f;
			}
			((ModNPC)this).NPC.position.X = player.position.X - 100f;
			((ModNPC)this).NPC.position.Y = player.position.Y - 400f;
			for (int num47 = 0; num47 < 50; num47++)
			{
				int num48 = Dust.NewDust(((ModNPC)this).NPC.position, ((ModNPC)this).NPC.width, ((ModNPC)this).NPC.height, 6);
				Main.dust[num48].scale = 1.5f;
			}
		}
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		if (((ModNPC)this).NPC.life > 0)
		{
			return;
		}
		for (int i = 0; i < 40; i++)
		{
			int num = Dust.NewDust(((ModNPC)this).NPC.position, ((ModNPC)this).NPC.width, ((ModNPC)this).NPC.height, 6, 0f, -2f, 0, default(Color), 1.5f);
			Main.dust[num].noGravity = true;
			Main.dust[num].scale = 2f;
			Main.dust[num].position.X += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
			Main.dust[num].position.Y += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
			if (Main.dust[num].position != ((ModNPC)this).NPC.Center)
			{
				Main.dust[num].velocity = ((ModNPC)this).NPC.DirectionTo(Main.dust[num].position) * 2f;
			}
		}
		((ModNPC)this).NPC.position.X = ((ModNPC)this).NPC.position.X + (float)(((ModNPC)this).NPC.width / 2);
		((ModNPC)this).NPC.position.Y = ((ModNPC)this).NPC.position.Y + (float)(((ModNPC)this).NPC.height / 2);
		((ModNPC)this).NPC.width = 128;
		((ModNPC)this).NPC.height = 212;
		((ModNPC)this).NPC.position.X = ((ModNPC)this).NPC.position.X - (float)(((ModNPC)this).NPC.width / 2);
		((ModNPC)this).NPC.position.Y = ((ModNPC)this).NPC.position.Y - (float)(((ModNPC)this).NPC.height / 2);
		for (int j = 0; j < 20; j++)
		{
			int num2 = Dust.NewDust(new Vector2(((ModNPC)this).NPC.position.X, ((ModNPC)this).NPC.position.Y), ((ModNPC)this).NPC.width, ((ModNPC)this).NPC.height, 6, 0f, 0f, 100, default(Color), 2f);
			Main.dust[num2].velocity *= 3f;
			if (Main.rand.Next(2) == 0)
			{
				Main.dust[num2].scale = 1.5f;
				Main.dust[num2].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
			}
		}
	}

	public override void OnKill()
	{
		if (Main.expertMode)
		{
			((ModNPC)this).NPC.DropBossBags();
		}
		else
		{
			int num = Main.rand.Next(7);
			if (num == 0)
			{
				Item.NewItem((int)((ModNPC)this).NPC.position.X, (int)((ModNPC)this).NPC.position.Y, ((ModNPC)this).NPC.width, ((ModNPC)this).NPC.height, ((ModNPC)this).Mod.Find<ModItem>("HellFlail").Type, 1, false, 0, false, false);
			}
			if (num == 1)
			{
				Item.NewItem((int)((ModNPC)this).NPC.position.X, (int)((ModNPC)this).NPC.position.Y, ((ModNPC)this).NPC.width, ((ModNPC)this).NPC.height, ((ModNPC)this).Mod.Find<ModItem>("HellThrow").Type, 1, false, 0, false, false);
			}
			if (num == 2)
			{
				Item.NewItem((int)((ModNPC)this).NPC.position.X, (int)((ModNPC)this).NPC.position.Y, ((ModNPC)this).NPC.width, ((ModNPC)this).NPC.height, ((ModNPC)this).Mod.Find<ModItem>("HellGun").Type, 1, false, 0, false, false);
			}
			if (num == 3)
			{
				Item.NewItem((int)((ModNPC)this).NPC.position.X, (int)((ModNPC)this).NPC.position.Y, ((ModNPC)this).NPC.width, ((ModNPC)this).NPC.height, ((ModNPC)this).Mod.Find<ModItem>("HellJavelin").Type, 1, false, 0, false, false);
			}
			if (num == 4)
			{
				Item.NewItem((int)((ModNPC)this).NPC.position.X, (int)((ModNPC)this).NPC.position.Y, ((ModNPC)this).NPC.width, ((ModNPC)this).NPC.height, ((ModNPC)this).Mod.Find<ModItem>("HellStaff").Type, 1, false, 0, false, false);
			}
			if (num == 5)
			{
				Item.NewItem((int)((ModNPC)this).NPC.position.X, (int)((ModNPC)this).NPC.position.Y, ((ModNPC)this).NPC.width, ((ModNPC)this).NPC.height, ((ModNPC)this).Mod.Find<ModItem>("HellTome").Type, 1, false, 0, false, false);
			}
			if (num == 6)
			{
				Item.NewItem((int)((ModNPC)this).NPC.position.X, (int)((ModNPC)this).NPC.position.Y, ((ModNPC)this).NPC.width, ((ModNPC)this).NPC.height, ((ModNPC)this).Mod.Find<ModItem>("HellScepter").Type, 1, false, 0, false, false);
			}
			Item.NewItem(((ModNPC)this).NPC.getRect(), ((ModNPC)this).Mod.Find<ModItem>("HellShard").Type, Main.rand.Next(25, 32), false, 0, false, false);
		}
		if (Main.rand.Next(7) == 0)
		{
			Item.NewItem(((ModNPC)this).NPC.getRect(), ((ModNPC)this).Mod.Find<ModItem>("IgnodiumMask").Type, 1, false, 0, false, false);
		}
		if (Main.rand.Next(10) == 0)
		{
			Item.NewItem(((ModNPC)this).NPC.getRect(), ((ModNPC)this).Mod.Find<ModItem>("IgnodiumTrophyItem").Type, 1, false, 0, false, false);
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
		((ModNPC)this).NPC.frameCounter += 1.0;
		if (((ModNPC)this).NPC.frameCounter >= 8.0)
		{
			((ModNPC)this).NPC.frame.Y = (((ModNPC)this).NPC.frame.Y + frameHeight) % (Main.npcFrameCount[((ModNPC)this).NPC.type] * frameHeight);
			((ModNPC)this).NPC.frameCounter = 1.0;
		}
	}
}
