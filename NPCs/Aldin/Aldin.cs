using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Ultranium.NPCs.Town;

namespace Ultranium.NPCs.Aldin;

public class Aldin : ModNPC
{
	private Player player;

	private float speed;

	public static int timer;

	public bool Transition;

	public bool Phase2;

	public bool Phase3;

	public int TransitionTimer;

	public int TransitionTimer2;

	public int Phase;

	public int AttackType;

	private float Spin;

	public int players;

	public override void SetStaticDefaults()
	{
		// ((ModNPC)this).DisplayName.SetDefault("???");
		Main.npcFrameCount[((ModNPC)this).NPC.type] = 7;
		NPCID.Sets.TrailCacheLength[((ModNPC)this).NPC.type] = 10;
		NPCID.Sets.TrailingMode[((ModNPC)this).NPC.type] = 0;
	}

	public override void SetDefaults()
	{
		((ModNPC)this).NPC.width = 354;
		((ModNPC)this).NPC.height = 320;
		((ModNPC)this).NPC.lifeMax = 520000;
		((ModNPC)this).NPC.damage = 0;
		((ModNPC)this).NPC.defense = 300;
		((ModNPC)this).NPC.knockBackResist = 0f;
		((ModNPC)this).NPC.boss = true;
		((ModNPC)this).NPC.lavaImmune = true;
		((ModNPC)this).NPC.noGravity = true;
		((ModNPC)this).NPC.noTileCollide = true;
		((ModNPC)this).NPC.netAlways = true;
		((ModNPC)this).NPC.HitSound = ((ModNPC)this).Mod.GetLegacySoundSlot((SoundType)50, "Sounds/GodHit");
		((ModNPC)this).NPC.DeathSound = ((ModNPC)this).Mod.GetLegacySoundSlot((SoundType)50, "Sounds/GodDeath")?.WithVolume(1f)?.WithPitchVariance(0.5f);
		((ModNPC)this).NPC.value = Item.buyPrice(0, 50);
		((ModNPC)this).NPC.npcSlots = 1000f;
		base.Music = ((ModNPC)this).Mod.GetSoundSlot((SoundType)51, "Sounds/Music/Aldin");
		base.bossBag/* tModPorter Note: Removed. Spawn the treasure bag alongside other loot via npcLoot.Add(ItemDropRule.BossBag(type)) */ = ((ModNPC)this).Mod.Find<ModItem>("AldinBag").Type;
		((ModNPC)this).NPC.aiStyle = -1;
		players = 1;
		for (int i = 0; i < 206; i++)
		{
			((ModNPC)this).NPC.buffImmune[i] = true;
		}
	}

	public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
	{
		players = numPlayers;
		((ModNPC)this).NPC.lifeMax = 600000 + numPlayers * 60000;
		((ModNPC)this).NPC.damage = 0;
	}

	public override void FindFrame(int frameHeight)
	{
		((ModNPC)this).NPC.frameCounter += 1.0;
		if (((ModNPC)this).NPC.frameCounter > 6.0)
		{
			((ModNPC)this).NPC.frame.Y = ((ModNPC)this).NPC.frame.Y + frameHeight;
			((ModNPC)this).NPC.frameCounter = 0.0;
		}
		if (((ModNPC)this).NPC.frame.Y >= frameHeight * 7)
		{
			((ModNPC)this).NPC.frame.Y = frameHeight;
		}
	}

	public override void ModifyHitByProjectile(Projectile projectile, ref NPC.HitModifiers modifiers)
	{
		if (projectile.type == 634 || projectile.type == 617 || projectile.type == 620 || projectile.type == 632 || projectile.type == 631 || projectile.type == 639 || projectile.type == 616 || projectile.type == 502 || projectile.type == 503 || projectile.type == 636)
		{
			damage /= 10;
		}
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		if (((ModNPC)this).NPC.velocity != Vector2.Zero)
		{
			Vector2 vector = new Vector2((float)ModContent.GetTexture("Ultranium/NPCs/Aldin/AldinTrail").Width * 0.5f, (float)((ModNPC)this).NPC.height * 0.5f);
			for (int i = 0; i < ((ModNPC)this).NPC.oldPos.Length; i++)
			{
				Vector2 position = ((ModNPC)this).NPC.oldPos[i] - Main.screenPosition + vector + new Vector2(0f, ((ModNPC)this).NPC.gfxOffY);
				Color color = ((ModNPC)this).NPC.GetAlpha(drawColor) * ((float)(((ModNPC)this).NPC.oldPos.Length - i) / (float)((ModNPC)this).NPC.oldPos.Length / 2f);
				spriteBatch.Draw(ModContent.GetTexture("Ultranium/NPCs/Aldin/AldinTrail"), position, ((ModNPC)this).NPC.frame, color, ((ModNPC)this).NPC.rotation, vector, ((ModNPC)this).NPC.scale, SpriteEffects.None, 0f);
			}
		}
		return true;
	}

	public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		Main.spriteBatch.Draw(((ModNPC)this).Mod.GetTexture("NPCs/Aldin/AldinGlow"), ((ModNPC)this).NPC.Center - Main.screenPosition + new Vector2(0f, ((ModNPC)this).NPC.gfxOffY), ((ModNPC)this).NPC.frame, Color.White, ((ModNPC)this).NPC.rotation, ((ModNPC)this).NPC.frame.Size() / 2f, ((ModNPC)this).NPC.scale, SpriteEffects.None, 0f);
		Color[] array = new Color[5]
		{
			new Color(153, 255, 178),
			new Color(117, 235, 215),
			new Color(83, 168, 222),
			new Color(65, 102, 207),
			new Color(72, 37, 169)
		};
		float amount = (float)(Main.GameUpdateCount % 60) / 60f;
		int num = (int)(Main.GameUpdateCount / 60 % 5);
		Main.spriteBatch.Draw(((ModNPC)this).Mod.GetTexture("NPCs/Aldin/AldinWingsGlow"), ((ModNPC)this).NPC.Center - Main.screenPosition + new Vector2(0f, ((ModNPC)this).NPC.gfxOffY), ((ModNPC)this).NPC.frame, Color.Lerp(array[num], array[(num + 1) % 5], amount), ((ModNPC)this).NPC.rotation, ((ModNPC)this).NPC.frame.Size() / 2f, ((ModNPC)this).NPC.scale, SpriteEffects.None, 0f);
	}

	public override void AI()
	{
		this.player = Main.player[((ModNPC)this).NPC.target];
		int num = (Main.expertMode ? 75 : 90);
		if (!((Entity)this.player).active || this.player.dead || Main.dayTime)
		{
			((ModNPC)this).NPC.TargetClosest(faceTarget: false);
			this.player = Main.player[((ModNPC)this).NPC.target];
			if (!((Entity)this.player).active || this.player.dead)
			{
				((Entity)((ModNPC)this).NPC).active = false;
				Keeper.CanSpawnAldin = false;
				NPC.NewNPC((int)((ModNPC)this).NPC.Center.X, (int)((ModNPC)this).NPC.Center.Y, ((ModNPC)this).Mod.Find<ModNPC>("Keeper").Type, 0, 0f, 0f, 0f, 0f, 255);
				return;
			}
		}
		timer++;
		if (timer > 175)
		{
			((ModNPC)this).NPC.velocity *= 0f;
		}
		else if (timer < 175 && timer > 75)
		{
			Move(new Vector2(0f, -300f));
		}
		if (Vector2.Distance(((ModNPC)this).NPC.Center, this.player.Center) > 2000f)
		{
			((ModNPC)this).NPC.position.X = this.player.position.X - 170f;
			((ModNPC)this).NPC.position.Y = this.player.position.Y - 400f;
		}
		if (((ModNPC)this).NPC.life > ((ModNPC)this).NPC.lifeMax / 2 && !Transition)
		{
			if (AttackType == 0)
			{
				if (timer == 180 || timer == 220)
				{
					int num2 = 0;
					if (timer == 180)
					{
						num2 = 5;
					}
					if (timer == 220)
					{
						num2 = 6;
					}
					float num3 = 2200f;
					float num4 = 2.5f;
					float num5 = num3 / (float)num2;
					for (int i = 0; i < num2; i++)
					{
						Vector2 vector = new Vector2(((ModNPC)this).NPC.Center.X - num3 / num4 + num5 * (float)i, ((ModNPC)this).NPC.Center.Y - 950f);
						_ = Main.projectile[Projectile.NewProjectile(vector.X, vector.Y, 0f, 14f, ((ModNPC)this).Mod.Find<ModProjectile>("CosmicStar").Type, num, 0f, Main.myPlayer, 0f, 0f)];
					}
				}
				if (timer >= 400)
				{
					timer = 120;
					AttackType++;
				}
			}
			if (AttackType == 1)
			{
				if (timer == 180)
				{
					for (int j = 0; j < 16; j++)
					{
						Vector2 vector2 = ((float)Math.PI / 8f * (float)j).ToRotationVector2();
						vector2.Normalize();
						vector2 *= 2f;
						Projectile.NewProjectile(((ModNPC)this).NPC.Center.X, ((ModNPC)this).NPC.Center.Y, vector2.X, vector2.Y, ((ModNPC)this).Mod.Find<ModProjectile>("CosmosBoltHome").Type, num, 1f, Main.myPlayer, 0f, 0f);
					}
				}
				if (timer >= 250)
				{
					timer = 120;
					AttackType++;
				}
			}
			if (AttackType == 2)
			{
				if (timer == 210 || timer == 240 || timer == 270 || timer == 300 || timer == 330 || timer == 360 || timer == 390)
				{
					SoundEngine.PlaySound(((ModNPC)this).Mod.GetLegacySoundSlot((SoundType)50, "Sounds/CosmicRay")?.WithVolume(1f), -1, -1);
					float num6 = 10f;
					float num7 = (float)Math.Atan2(((ModNPC)this).NPC.Center.Y - this.player.Center.Y, ((ModNPC)this).NPC.Center.X - this.player.Center.X);
					_ = Main.projectile[Projectile.NewProjectile(((ModNPC)this).NPC.Center.X, ((ModNPC)this).NPC.Center.Y, (float)(Math.Cos(num7) * (double)num6 * -1.0), (float)(Math.Sin(num7) * (double)num6 * -1.0), ((ModNPC)this).Mod.Find<ModProjectile>("CosmicRay").Type, num + 10, 0f, 0, 0f, 0f)];
				}
				if (timer >= 410)
				{
					timer = 120;
					AttackType++;
				}
			}
			if (AttackType == 3)
			{
				if (timer > 180 && timer < 360 && Main.rand.Next(20) == 0)
				{
					Projectile.NewProjectile(this.player.Center.X + 500f, this.player.Center.Y, 0f, 0f, ((ModNPC)this).Mod.Find<ModProjectile>("CosmicFireball").Type, 0, 1f, Main.myPlayer, 0f, 0f);
					Projectile.NewProjectile(this.player.Center.X - 500f, this.player.Center.Y, 0f, 0f, ((ModNPC)this).Mod.Find<ModProjectile>("CosmicFireball").Type, 0, 1f, Main.myPlayer, 0f, 0f);
				}
				if (timer >= 360)
				{
					timer = 120;
					AttackType++;
				}
			}
			if (AttackType == 4)
			{
				if (timer == 180)
				{
					Vector2 spinningpoint = new Vector2(3f, 0f).RotatedByRandom(Math.PI * 5.0);
					for (int k = 0; k < 3; k++)
					{
						Vector2 vector3 = spinningpoint.RotatedBy(Math.PI * 2.0 / 3.0 * ((double)k + Main.rand.NextDouble() - 0.5));
						Projectile.NewProjectile(((ModNPC)this).NPC.Center, vector3, 465, num, 0f, Main.myPlayer, 0f, 0f);
					}
				}
				if (timer >= 360)
				{
					timer = 120;
					if ((double)((ModNPC)this).NPC.life > (double)((ModNPC)this).NPC.lifeMax / 1.4)
					{
						AttackType = 0;
					}
					else
					{
						AttackType++;
					}
				}
			}
			if ((double)((ModNPC)this).NPC.life <= (double)((ModNPC)this).NPC.lifeMax / 1.4)
			{
				if (AttackType == 5)
				{
					float num8 = 2f;
					float num9 = 0f;
					if (timer == 240 || timer == 280 || timer == 320)
					{
						SoundEngine.PlaySound(((ModNPC)this).Mod.GetLegacySoundSlot((SoundType)50, "Sounds/CosmicRay")?.WithVolume(1f), -1, -1);
					}
					if (timer == 240)
					{
						num9 = 12f;
					}
					if (timer == 280)
					{
						num9 = 16f;
					}
					if (timer == 320)
					{
						num9 = 20f;
					}
					float num10 = MathHelper.ToRadians(360f);
					int num11 = 1;
					for (int l = 0; (float)l < num9; l++)
					{
						Vector2 vector4 = Vector2.One.RotatedBy(MathHelper.Lerp(0f - num10, num10, (float)l / num9)) * num8;
						Projectile.NewProjectile(((ModNPC)this).NPC.Center.X, ((ModNPC)this).NPC.Center.Y, vector4.X, vector4.Y, ((ModNPC)this).Mod.Find<ModProjectile>("CosmicRay").Type, num, 2f, Main.myPlayer, (float)num11, 0f);
					}
					if (timer >= 400)
					{
						timer = 120;
						AttackType++;
					}
				}
				if (AttackType == 6)
				{
					if (timer == 220)
					{
						SoundEngine.PlaySound(((ModNPC)this).Mod.GetLegacySoundSlot((SoundType)50, "Sounds/CosmicRay")?.WithVolume(1f), -1, -1);
						for (int m = 0; m < 10; m++)
						{
							Vector2 vector5 = ((float)Math.PI / 5f * (float)m).ToRotationVector2();
							vector5.Normalize();
							vector5 *= 14f;
							Projectile.NewProjectile(((ModNPC)this).NPC.Center.X, ((ModNPC)this).NPC.Center.Y, vector5.X, vector5.Y, ((ModNPC)this).Mod.Find<ModProjectile>("CosmicStar").Type, num, 1f, Main.myPlayer, 0f, 0f);
						}
					}
					if (timer >= 320)
					{
						timer = 120;
						AttackType++;
					}
				}
				if (AttackType == 7)
				{
					if (timer == 180 || timer == 240 || timer == 300 || timer == 360)
					{
						int num12 = 0;
						if (timer == 180)
						{
							num12 = 8;
						}
						if (timer == 240)
						{
							num12 = 11;
						}
						if (timer == 300)
						{
							num12 = 14;
						}
						if (timer == 360)
						{
							num12 = 17;
						}
						int num13 = 650;
						for (float num14 = 0f; num14 < (float)num12; num14 += 1f)
						{
							Vector2 vector6 = this.player.Center + new Vector2(0f, num13).RotatedBy((double)num14 * (Math.PI * 2.0 / (double)num12));
							Vector2 vector7 = this.player.Center - vector6;
							vector7.Normalize();
							vector7 *= 6f;
							_ = Main.projectile[Projectile.NewProjectile(vector6.X, vector6.Y, vector7.X, vector7.Y, ((ModNPC)this).Mod.Find<ModProjectile>("CosmicBlast").Type, num, 6f, 0, 0f, 0f)];
						}
					}
					if (timer >= 460)
					{
						timer = 120;
						AttackType = 0;
					}
				}
			}
		}
		if (((ModNPC)this).NPC.life <= ((ModNPC)this).NPC.lifeMax / 2 && !Phase2)
		{
			Transition = true;
			Phase2 = true;
		}
		if (Transition && ((ModNPC)this).NPC.life <= ((ModNPC)this).NPC.lifeMax / 2 && !Phase3)
		{
			TransitionTimer++;
			((ModNPC)this).NPC.position.X = this.player.position.X - 170f;
			((ModNPC)this).NPC.position.Y = this.player.position.Y - 400f;
			((ModNPC)this).NPC.immortal = true;
			((ModNPC)this).NPC.dontTakeDamage = true;
			if (TransitionTimer == 120)
			{
				Ultranium.seizureAmount = 20f;
			}
			if (TransitionTimer == 240)
			{
				TransitionTimer = 0;
				timer = 120;
				AttackType = 0;
				Transition = false;
			}
		}
		if (((ModNPC)this).NPC.life <= ((ModNPC)this).NPC.lifeMax / 2 && Phase2 && !Phase3 && !Transition)
		{
			((ModNPC)this).NPC.immortal = false;
			((ModNPC)this).NPC.dontTakeDamage = false;
			if (AttackType == 0)
			{
				if (timer == 180 || timer == 240 || timer == 300 || timer == 360 || timer == 420)
				{
					SoundEngine.PlaySound(((ModNPC)this).Mod.GetLegacySoundSlot((SoundType)50, "Sounds/CosmicStarAttack")?.WithVolume(1f), -1, -1);
					Projectile.NewProjectile(this.player.Center + Main.rand.NextVector2Square(-750f, 750f), Main.rand.NextVector2Square(-1f, 1f), ((ModNPC)this).Mod.Find<ModProjectile>("CosmicRitual").Type, 0, 6f, this.player.whoAmI, 0f, 0f);
				}
				if (timer == 190 || timer == 250 || timer == 310 || timer == 370 || timer == 430)
				{
					float num15 = 18f;
					int num16 = ((ModNPC)this).Mod.Find<ModProjectile>("CosmicStar").Type;
					SoundEngine.PlaySound(SoundID.Item20, new Vector2(((ModNPC)this).NPC.position.X, ((ModNPC)this).NPC.position.Y));
					float num17 = (float)Math.Atan2(((ModNPC)this).NPC.Center.Y - this.player.Center.Y, ((ModNPC)this).NPC.Center.X - this.player.Center.X);
					Projectile.NewProjectile(((ModNPC)this).NPC.Center.X, ((ModNPC)this).NPC.Center.Y, (float)(Math.Cos(num17) * (double)num15 * -1.0), (float)(Math.Sin(num17) * (double)num15 * -1.0), num16, num, 0f, Main.myPlayer, 0f, 0f);
				}
				if (timer == 490)
				{
					Projectile.NewProjectile(((ModNPC)this).NPC.Center.X, ((ModNPC)this).NPC.Center.Y, 0f, 0f, ((ModNPC)this).Mod.Find<ModProjectile>("CosmicStarHuge").Type, num, 1f, Main.myPlayer, 0f, 0f);
				}
				if (timer >= 700)
				{
					timer = 120;
					AttackType++;
				}
			}
			if (AttackType == 1)
			{
				if (timer == 180)
				{
					Projectile.NewProjectile(this.player.Center.X - 650f, this.player.Center.Y, 0f, 0f, ((ModNPC)this).Mod.Find<ModProjectile>("AldinTelegraph").Type, 0, 1f, Main.myPlayer, 0f, 0f);
				}
				if (timer == 240 || timer == 280 || timer == 320 || timer == 360)
				{
					int num18 = 0;
					if (timer == 240)
					{
						num18 = 13;
					}
					if (timer == 280)
					{
						num18 = 14;
					}
					if (timer == 320)
					{
						num18 = 15;
					}
					if (timer == 360)
					{
						num18 = 16;
					}
					float num19 = 2200f;
					float num20 = num19 / (float)num18;
					for (int n = 0; n < num18; n++)
					{
						Vector2 vector8 = new Vector2(this.player.Center.X - 1500f, this.player.Center.Y - num19 / 2f + num20 * (float)n);
						_ = Main.projectile[Projectile.NewProjectile(vector8.X, vector8.Y, 15f, 0f, ((ModNPC)this).Mod.Find<ModProjectile>("CosmosBolt").Type, num, 0f, Main.myPlayer, 0f, 0f)];
					}
				}
				if (timer >= 450)
				{
					timer = 120;
					AttackType++;
				}
			}
			if (AttackType == 2)
			{
				if (timer == 180 || timer == 240 || timer == 300)
				{
					SoundEngine.PlaySound(((ModNPC)this).Mod.GetLegacySoundSlot((SoundType)50, "Sounds/CosmicCharge")?.WithVolume(1f), -1, -1);
					float num21 = 10f;
					int num22 = ((ModNPC)this).Mod.Find<ModProjectile>("HomingStar").Type;
					SoundEngine.PlaySound(SoundID.Item20, new Vector2(((ModNPC)this).NPC.position.X, ((ModNPC)this).NPC.position.Y));
					float num23 = (float)Math.Atan2(((ModNPC)this).NPC.Center.Y - this.player.Center.Y, ((ModNPC)this).NPC.Center.X - this.player.Center.X);
					Projectile.NewProjectile(((ModNPC)this).NPC.Center.X, ((ModNPC)this).NPC.Center.Y, (float)(Math.Cos(num23) * (double)num21 * -1.0), (float)(Math.Sin(num23) * (double)num21 * -1.0), num22, num, 0f, Main.myPlayer, 0f, 0f);
				}
				if (timer == 600)
				{
					timer = 120;
					AttackType++;
				}
			}
			if (AttackType == 3)
			{
				if (timer > 180)
				{
					for (int num24 = 0; num24 < 20; num24++)
					{
						Vector2 vector9 = default(Vector2);
						double num25 = Main.rand.NextDouble() * 2.0 * Math.PI;
						vector9.X += (float)(Math.Sin(num25) * 1200.0);
						vector9.Y += (float)(Math.Cos(num25) * 1200.0);
						Dust obj = Main.dust[Dust.NewDust(((ModNPC)this).NPC.Center + vector9 - new Vector2(4f, 4f), 0, 0, 62, 0f, 0f, 100, Color.White)];
						obj.velocity *= 0f;
						obj.noGravity = true;
						obj.scale = 2.5f;
					}
					Player player = Main.player[((ModNPC)this).NPC.target];
					if (((Entity)player).active && !player.dead && player.Distance(((ModNPC)this).NPC.Center) > 1200f)
					{
						Vector2 vector10 = ((ModNPC)this).NPC.Center - player.Center;
						float num26 = vector10.Length() - 600f;
						vector10.Normalize();
						vector10 *= ((num26 < 25f) ? num26 : 25f);
						player.position += vector10;
					}
				}
				if (timer == 200)
				{
					SoundEngine.PlaySound(((ModNPC)this).Mod.GetLegacySoundSlot((SoundType)50, "Sounds/CosmicRay")?.WithVolume(1f), -1, -1);
					float num27 = 2f;
					float num28 = 8f;
					float num29 = MathHelper.ToRadians(360f);
					int num30 = 1;
					for (int num31 = 0; (float)num31 <= num28; num31++)
					{
						Vector2 vector11 = Vector2.One.RotatedBy(MathHelper.Lerp(0f - num29, num29, (float)num31 / num28)) * num27;
						Projectile.NewProjectile(((ModNPC)this).NPC.Center.X, ((ModNPC)this).NPC.Center.Y, vector11.X, vector11.Y, ((ModNPC)this).Mod.Find<ModProjectile>("CosmicRayArena").Type, num + 30, 2f, Main.myPlayer, (float)num30, 0f);
					}
				}
				if (timer == 300 || timer == 420 || timer == 540)
				{
					float num32 = 10f;
					float num33 = 21f;
					float num34 = MathHelper.ToRadians(360f);
					int num35 = -1;
					for (int num36 = 0; (float)num36 <= num33; num36++)
					{
						Vector2 vector12 = Vector2.One.RotatedBy(MathHelper.Lerp(0f - num34, num34, (float)num36 / num33)) * num32;
						Projectile.NewProjectile(((ModNPC)this).NPC.Center.X, ((ModNPC)this).NPC.Center.Y, vector12.X, vector12.Y, ((ModNPC)this).Mod.Find<ModProjectile>("CosmicBlastSpiral").Type, num, 2f, Main.myPlayer, (float)num35, 0f);
					}
				}
				if (timer == 360 || timer == 480 || timer == 600)
				{
					float num37 = 10f;
					float num38 = 21f;
					float num39 = MathHelper.ToRadians(360f);
					int num40 = 1;
					for (int num41 = 0; (float)num41 <= num38; num41++)
					{
						Vector2 vector13 = Vector2.One.RotatedBy(MathHelper.Lerp(0f - num39, num39, (float)num41 / num38)) * num37;
						Projectile.NewProjectile(((ModNPC)this).NPC.Center.X, ((ModNPC)this).NPC.Center.Y, vector13.X, vector13.Y, ((ModNPC)this).Mod.Find<ModProjectile>("CosmicBlastSpiral").Type, num, 2f, Main.myPlayer, (float)num40, 0f);
					}
				}
				if (timer == 700)
				{
					AttackType++;
					timer = 120;
				}
			}
			if (AttackType == 4)
			{
				if (timer == 180)
				{
					Projectile.NewProjectile(this.player.Center.X + 650f, this.player.Center.Y, 0f, 0f, ((ModNPC)this).Mod.Find<ModProjectile>("AldinTelegraph").Type, 0, 1f, Main.myPlayer, 0f, 0f);
				}
				if (timer == 240 || timer == 280 || timer == 320 || timer == 360)
				{
					int num42 = 0;
					if (timer == 240)
					{
						num42 = 13;
					}
					if (timer == 280)
					{
						num42 = 14;
					}
					if (timer == 320)
					{
						num42 = 15;
					}
					if (timer == 360)
					{
						num42 = 16;
					}
					float num43 = 2200f;
					float num44 = num43 / (float)num42;
					for (int num45 = 0; num45 < num42; num45++)
					{
						Vector2 vector14 = new Vector2(this.player.Center.X + 1500f, this.player.Center.Y - num43 / 2f + num44 * (float)num45);
						_ = Main.projectile[Projectile.NewProjectile(vector14.X, vector14.Y, -15f, 0f, ((ModNPC)this).Mod.Find<ModProjectile>("CosmosBolt").Type, num, 0f, Main.myPlayer, 0f, 0f)];
					}
				}
				if (timer >= 450)
				{
					timer = 120;
					AttackType++;
				}
			}
			if (AttackType == 5)
			{
				if (timer == 180)
				{
					Projectile.NewProjectile(this.player.Center.X, this.player.Center.Y - 250f, 0f, 0f, ((ModNPC)this).Mod.Find<ModProjectile>("AldinTelegraph").Type, 0, 1f, Main.myPlayer, 0f, 0f);
				}
				if (timer == 240 || timer == 280 || timer == 320 || timer == 360)
				{
					SoundEngine.PlaySound(((ModNPC)this).Mod.GetLegacySoundSlot((SoundType)50, "Sounds/CosmicCharge")?.WithVolume(1f), -1, -1);
					Projectile.NewProjectile(this.player.Center.X, this.player.Center.Y - 900f, 0f, 0f, ((ModNPC)this).Mod.Find<ModProjectile>("CosmicWrathBolt").Type, num, 1f, Main.myPlayer, 0f, 0f);
				}
				if (timer >= 450)
				{
					timer = 120;
					AttackType = 0;
				}
			}
		}
		if ((double)((ModNPC)this).NPC.life < (double)((ModNPC)this).NPC.lifeMax / 3.5 && !Phase3)
		{
			Transition = true;
			Phase3 = true;
		}
		if (Transition && (double)((ModNPC)this).NPC.life < (double)((ModNPC)this).NPC.lifeMax / 3.5)
		{
			TransitionTimer2++;
			timer = 120;
			((ModNPC)this).NPC.immortal = true;
			((ModNPC)this).NPC.dontTakeDamage = true;
			if (TransitionTimer < 180)
			{
				((ModNPC)this).NPC.velocity *= 0f;
			}
			if (TransitionTimer2 == 180)
			{
				Ultranium.seizureAmount = 20f;
			}
			if (TransitionTimer2 < 780 && TransitionTimer2 > 180)
			{
				((ModNPC)this).NPC.position.X = this.player.position.X - 170f;
				((ModNPC)this).NPC.position.Y = this.player.position.Y - 400f;
				if (Main.rand.Next(10) == 0)
				{
					int num46 = Main.rand.Next(-200, 200) * 6;
					int num47 = -1200;
					Projectile.NewProjectile(this.player.Center.X + (float)num46, this.player.Center.Y + (float)num47, 0f, 17f, ((ModNPC)this).Mod.Find<ModProjectile>("CosmicStar").Type, num, 1f, Main.myPlayer, 0f, 0f);
				}
			}
			if (TransitionTimer2 == 800 || TransitionTimer2 == 820 || TransitionTimer2 == 840)
			{
				Projectile.NewProjectile(this.player.Center.X, this.player.Center.Y - 1200f, 0f, 17f, ((ModNPC)this).Mod.Find<ModProjectile>("CosmicStarHuge").Type, num + 20, 1f, Main.myPlayer, 0f, 0f);
			}
			if (TransitionTimer2 == 1020)
			{
				TransitionTimer2 = 0;
				timer = 0;
				AttackType = 0;
				Transition = false;
			}
		}
		if ((double)((ModNPC)this).NPC.life < (double)((ModNPC)this).NPC.lifeMax / 3.5 && Phase3 && !Transition)
		{
			((ModNPC)this).NPC.immortal = false;
			((ModNPC)this).NPC.dontTakeDamage = false;
			if (AttackType == 0)
			{
				if (timer == 180)
				{
					for (int num48 = 0; num48 < 6; num48++)
					{
						Vector2 vector15 = ((float)Math.PI / 3f * (float)num48).ToRotationVector2();
						vector15.Normalize();
						vector15 *= 3f;
						Projectile.NewProjectile(((ModNPC)this).NPC.Center.X, ((ModNPC)this).NPC.Center.Y, vector15.X, vector15.Y, ((ModNPC)this).Mod.Find<ModProjectile>("StationaryStar").Type, num, 1f, Main.myPlayer, 0f, 0f);
					}
				}
				if (timer >= 360)
				{
					timer = 120;
					AttackType++;
				}
			}
			if (AttackType == 1)
			{
				if (timer > 180 && timer < 450)
				{
					for (int num49 = 0; num49 < 20; num49++)
					{
						Vector2 vector16 = default(Vector2);
						double num50 = Main.rand.NextDouble() * 2.0 * Math.PI;
						vector16.X += (float)(Math.Sin(num50) * 1200.0);
						vector16.Y += (float)(Math.Cos(num50) * 1200.0);
						Dust obj2 = Main.dust[Dust.NewDust(((ModNPC)this).NPC.Center + vector16 - new Vector2(4f, 4f), 0, 0, 62, 0f, 0f, 100, Color.White)];
						obj2.velocity *= 0f;
						obj2.noGravity = true;
						obj2.scale = 2.5f;
					}
					Player player2 = Main.player[((ModNPC)this).NPC.target];
					if (((Entity)player2).active && !player2.dead && player2.Distance(((ModNPC)this).NPC.Center) > 1200f)
					{
						Vector2 vector17 = ((ModNPC)this).NPC.Center - player2.Center;
						float num51 = vector17.Length() - 600f;
						vector17.Normalize();
						vector17 *= ((num51 < 25f) ? num51 : 25f);
						player2.position += vector17;
					}
					SoundEngine.PlaySound(((ModNPC)this).Mod.GetLegacySoundSlot((SoundType)50, "Sounds/CosmicRay")?.WithVolume(1f), -1, -1);
					Vector2 spinningpoint2 = new Vector2(400f, 0f);
					if (timer < 240 && timer > 180)
					{
						Spin += 0.01f;
					}
					else
					{
						Spin += 0.035f;
					}
					int num52 = 1;
					for (int num53 = 0; num53 < num52; num53++)
					{
						float num54 = 360 / num52;
						Vector2 vector18 = ((ModNPC)this).NPC.Center + spinningpoint2.RotatedBy((double)Spin + (double)(num54 * (float)num53) * (Math.PI / 2.0));
						float num55 = (float)Math.Atan2(((ModNPC)this).NPC.Center.Y - vector18.Y, ((ModNPC)this).NPC.Center.X - vector18.X);
						Projectile.NewProjectile(((ModNPC)this).NPC.Center.X, ((ModNPC)this).NPC.Center.Y, (float)(Math.Cos(num55) * 2.0 * -1.0), (float)(Math.Sin(num55) * 2.0 * -1.0), ((ModNPC)this).Mod.Find<ModProjectile>("CosmicRayQuick").Type, num, 0f, Main.myPlayer, 0f, (float)num53);
					}
				}
				if (timer >= 510)
				{
					timer = 120;
					AttackType++;
				}
			}
			if (AttackType == 2)
			{
				if (timer == 180)
				{
					Projectile.NewProjectile(this.player.Center.X - 650f, this.player.Center.Y, 0f, 0f, ((ModNPC)this).Mod.Find<ModProjectile>("AldinTelegraph").Type, 0, 1f, Main.myPlayer, 0f, 0f);
					Projectile.NewProjectile(this.player.Center.X + 650f, this.player.Center.Y, 0f, 0f, ((ModNPC)this).Mod.Find<ModProjectile>("AldinTelegraph").Type, 0, 1f, Main.myPlayer, 0f, 0f);
				}
				if (timer == 240 || timer == 280 || timer == 320 || timer == 360)
				{
					int num56 = 0;
					if (timer == 240)
					{
						num56 = 13;
					}
					if (timer == 280)
					{
						num56 = 14;
					}
					if (timer == 320)
					{
						num56 = 15;
					}
					if (timer == 360)
					{
						num56 = 16;
					}
					float num57 = 2200f;
					float num58 = num57 / (float)num56;
					for (int num59 = 0; num59 < num56; num59++)
					{
						Vector2 vector19 = new Vector2(this.player.Center.X - 1500f, this.player.Center.Y - num57 / 2f + num58 * (float)num59);
						_ = Main.projectile[Projectile.NewProjectile(vector19.X, vector19.Y, 17f, 0f, ((ModNPC)this).Mod.Find<ModProjectile>("CosmosBolt").Type, num, 0f, Main.myPlayer, 0f, 0f)];
					}
					for (int num60 = 0; num60 < num56; num60++)
					{
						Vector2 vector20 = new Vector2(this.player.Center.X + 1500f, this.player.Center.Y - num57 / 2f + num58 * (float)num60);
						_ = Main.projectile[Projectile.NewProjectile(vector20.X, vector20.Y, -17f, 0f, ((ModNPC)this).Mod.Find<ModProjectile>("CosmosBolt").Type, num, 0f, Main.myPlayer, 0f, 0f)];
					}
				}
				if (timer >= 450)
				{
					timer = 120;
					AttackType++;
				}
			}
			if (AttackType == 3)
			{
				if (timer == 180)
				{
					Projectile.NewProjectile(this.player.Center.X, this.player.Center.Y - 250f, 0f, 0f, ((ModNPC)this).Mod.Find<ModProjectile>("AldinTelegraph").Type, 0, 1f, Main.myPlayer, 0f, 0f);
					Projectile.NewProjectile(this.player.Center.X, this.player.Center.Y + 250f, 0f, 0f, ((ModNPC)this).Mod.Find<ModProjectile>("AldinTelegraph").Type, 0, 1f, Main.myPlayer, 0f, 0f);
				}
				if (timer == 240 || timer == 330 || timer == 420)
				{
					SoundEngine.PlaySound(((ModNPC)this).Mod.GetLegacySoundSlot((SoundType)50, "Sounds/CosmicCharge")?.WithVolume(1f), -1, -1);
					Projectile.NewProjectile(this.player.Center.X, this.player.Center.Y - 900f, 0f, 0f, ((ModNPC)this).Mod.Find<ModProjectile>("CosmicWrathBolt").Type, num, 1f, Main.myPlayer, 0f, 0f);
				}
				if (timer == 285 || timer == 375 || timer == 465)
				{
					SoundEngine.PlaySound(((ModNPC)this).Mod.GetLegacySoundSlot((SoundType)50, "Sounds/CosmicCharge")?.WithVolume(1f), -1, -1);
					Projectile.NewProjectile(this.player.Center.X, this.player.Center.Y + 900f, 0f, 0f, ((ModNPC)this).Mod.Find<ModProjectile>("CosmicWrathBolt2").Type, num, 1f, Main.myPlayer, 0f, 0f);
				}
				if (timer >= 510)
				{
					timer = 120;
					AttackType++;
				}
			}
			if (AttackType == 4)
			{
				if (timer == 180)
				{
					int num61 = 0;
					if (timer == 180)
					{
						num61 = 4;
					}
					float num62 = 2200f;
					float num63 = 2.5f;
					float num64 = num62 / (float)num61;
					for (int num65 = 0; num65 < num61; num65++)
					{
						Vector2 vector21 = new Vector2(((ModNPC)this).NPC.Center.X - num62 / num63 + num64 * (float)num65, ((ModNPC)this).NPC.Center.Y - 950f);
						_ = Main.projectile[Projectile.NewProjectile(vector21.X, vector21.Y, 0f, 14f, ((ModNPC)this).Mod.Find<ModProjectile>("CosmicStarHuge").Type, num, 0f, Main.myPlayer, 0f, 0f)];
					}
				}
				if (timer >= 360)
				{
					timer = 120;
					AttackType++;
				}
			}
			if (AttackType == 5)
			{
				if (timer == 180)
				{
					for (int num66 = 0; num66 < 4; num66++)
					{
						Vector2 vector22 = ((float)Math.PI / 2f * (float)num66).ToRotationVector2();
						vector22.Normalize();
						vector22 *= 7f;
						Projectile.NewProjectile(((ModNPC)this).NPC.Center.X, ((ModNPC)this).NPC.Center.Y, vector22.X, vector22.Y, ((ModNPC)this).Mod.Find<ModProjectile>("HomingStar").Type, num, 1f, Main.myPlayer, 0f, 0f);
					}
				}
				if (timer >= 385)
				{
					timer = 120;
					AttackType++;
				}
			}
			if (AttackType == 6)
			{
				if (timer == 180 || timer == 240 || timer == 300)
				{
					for (int num67 = 0; num67 < 15; num67++)
					{
						Vector2 vector23 = ((float)Math.PI * 2f / 15f * (float)num67).ToRotationVector2();
						vector23.Normalize();
						vector23 *= 2f;
						Projectile.NewProjectile(((ModNPC)this).NPC.Center.X, ((ModNPC)this).NPC.Center.Y, vector23.X, vector23.Y, ((ModNPC)this).Mod.Find<ModProjectile>("CosmosBoltHome").Type, num, 1f, Main.myPlayer, 0f, 0f);
					}
				}
				if (timer >= 360)
				{
					timer = 120;
					AttackType++;
				}
			}
			if (AttackType == 7)
			{
				if (timer > 180 && timer < 720)
				{
					for (int num68 = 0; num68 < 20; num68++)
					{
						Vector2 vector24 = default(Vector2);
						double num69 = Main.rand.NextDouble() * 2.0 * Math.PI;
						vector24.X += (float)(Math.Sin(num69) * 1200.0);
						vector24.Y += (float)(Math.Cos(num69) * 1200.0);
						Dust obj3 = Main.dust[Dust.NewDust(((ModNPC)this).NPC.Center + vector24 - new Vector2(4f, 4f), 0, 0, 62, 0f, 0f, 100, Color.White)];
						obj3.velocity *= 0f;
						obj3.noGravity = true;
						obj3.scale = 2.5f;
					}
					Player player3 = Main.player[((ModNPC)this).NPC.target];
					if (((Entity)player3).active && !player3.dead && player3.Distance(((ModNPC)this).NPC.Center) > 1200f)
					{
						Vector2 vector25 = ((ModNPC)this).NPC.Center - player3.Center;
						float num70 = vector25.Length() - 600f;
						vector25.Normalize();
						vector25 *= ((num70 < 25f) ? num70 : 25f);
						player3.position += vector25;
					}
				}
				if (timer == 180)
				{
					Projectile.NewProjectile(((ModNPC)this).NPC.Center.X, ((ModNPC)this).NPC.Center.Y, 0f, 0f, ((ModNPC)this).Mod.Find<ModProjectile>("CosmicRitualHuge").Type, 0, 1f, Main.myPlayer, 0f, 0f);
				}
				if (timer == 240)
				{
					float num71 = 2f;
					float num72 = 16f;
					float num73 = MathHelper.ToRadians(360f);
					int num74 = 1;
					for (int num75 = 0; (float)num75 <= num72; num75++)
					{
						Vector2 vector26 = Vector2.One.RotatedBy(MathHelper.Lerp(0f - num73, num73, (float)num75 / num72)) * num71;
						Projectile.NewProjectile(((ModNPC)this).NPC.Center.X, ((ModNPC)this).NPC.Center.Y, vector26.X, vector26.Y, ((ModNPC)this).Mod.Find<ModProjectile>("CosmicRayArena").Type, num + 30, 2f, Main.myPlayer, (float)num74, 0f);
					}
				}
				if (timer > 240 && timer < 720)
				{
					((ModNPC)this).NPC.ai[1] += 1f;
					if (((ModNPC)this).NPC.ai[2] > 2f)
					{
						SoundEngine.PlaySound(SoundID.Item9, ((ModNPC)this).NPC.Center);
						((ModNPC)this).NPC.ai[1] = 0f;
						((ModNPC)this).NPC.ai[2] += (float)Math.PI * 15f / 128f;
						if (((ModNPC)this).NPC.ai[1] > (float)Math.PI)
						{
							((ModNPC)this).NPC.ai[2] -= (float)Math.PI * 5f;
						}
						if (Main.netMode != 1)
						{
							int num76 = 3;
							for (int num77 = 0; num77 < num76; num77++)
							{
								Projectile.NewProjectile(((ModNPC)this).NPC.Center, new Vector2(10f, 0f).RotatedBy((double)((ModNPC)this).NPC.ai[2] - Math.PI * 2.0 / (double)num76 * (double)num77), ((ModNPC)this).Mod.Find<ModProjectile>("CosmicBlast").Type, num - 20, 0f, Main.myPlayer, 0f, 0f);
							}
						}
					}
				}
				if (timer >= 870)
				{
					timer = 120;
					AttackType = 0;
				}
			}
		}
		if (!Keeper.CanSpawnAldin)
		{
			((Entity)((ModNPC)this).NPC).active = false;
		}
	}

	private void Move(Vector2 offset)
	{
		speed = 35f;
		Vector2 vector = player.Center + offset - ((ModNPC)this).NPC.Center;
		float num = Magnitude(vector);
		if (num > speed)
		{
			vector *= speed / num;
		}
		float num2 = 10f;
		vector = (((ModNPC)this).NPC.velocity * num2 + vector) / (num2 + 1f);
		num = Magnitude(vector);
		if (num > speed)
		{
			vector *= speed / num;
		}
		((ModNPC)this).NPC.velocity = vector;
	}

	private float Magnitude(Vector2 mag)
	{
		return (float)Math.Sqrt(mag.X * mag.X + mag.Y * mag.Y);
	}

	public override void BossLoot(ref string name, ref int potionType)
	{
		potionType = 3544;
	}

	public override void OnKill()
	{
		if (Main.expertMode)
		{
			((ModNPC)this).NPC.DropBossBags();
		}
		else
		{
			int num = Main.rand.Next(3);
			if (num == 0)
			{
				Item.NewItem((int)((ModNPC)this).NPC.position.X, (int)((ModNPC)this).NPC.position.Y, ((ModNPC)this).NPC.width, ((ModNPC)this).NPC.height, ((ModNPC)this).Mod.Find<ModItem>("CosmicBlade").Type, 1, false, 0, false, false);
			}
			if (num == 1)
			{
				Item.NewItem((int)((ModNPC)this).NPC.position.X, (int)((ModNPC)this).NPC.position.Y, ((ModNPC)this).NPC.width, ((ModNPC)this).NPC.height, ((ModNPC)this).Mod.Find<ModItem>("CosmicBow").Type, 1, false, 0, false, false);
			}
			if (num == 2)
			{
				Item.NewItem((int)((ModNPC)this).NPC.position.X, (int)((ModNPC)this).NPC.position.Y, ((ModNPC)this).NPC.width, ((ModNPC)this).NPC.height, ((ModNPC)this).Mod.Find<ModItem>("CosmicStaff").Type, 1, false, 0, false, false);
			}
			int num2 = Main.rand.Next(3);
			if (num2 == 0)
			{
				Item.NewItem((int)((ModNPC)this).NPC.position.X, (int)((ModNPC)this).NPC.position.Y, ((ModNPC)this).NPC.width, ((ModNPC)this).NPC.height, ((ModNPC)this).Mod.Find<ModItem>("AldinHood").Type, 1, false, 0, false, false);
			}
			if (num2 == 1)
			{
				Item.NewItem((int)((ModNPC)this).NPC.position.X, (int)((ModNPC)this).NPC.position.Y, ((ModNPC)this).NPC.width, ((ModNPC)this).NPC.height, ((ModNPC)this).Mod.Find<ModItem>("AldinBody").Type, 1, false, 0, false, false);
			}
			if (num2 == 2)
			{
				Item.NewItem((int)((ModNPC)this).NPC.position.X, (int)((ModNPC)this).NPC.position.Y, ((ModNPC)this).NPC.width, ((ModNPC)this).NPC.height, ((ModNPC)this).Mod.Find<ModItem>("AldinRobe").Type, 1, false, 0, false, false);
			}
		}
		if (Main.rand.Next(10) == 0)
		{
			Item.NewItem((int)((ModNPC)this).NPC.position.X, (int)((ModNPC)this).NPC.position.Y, ((ModNPC)this).NPC.width, ((ModNPC)this).NPC.height, ((ModNPC)this).Mod.Find<ModItem>("AldinTrophyItem").Type, 1, false, 0, false, false);
		}
		Keeper.CanSpawnAldin = false;
		NPC.NewNPC((int)((ModNPC)this).NPC.Center.X, (int)((ModNPC)this).NPC.Center.Y, ((ModNPC)this).Mod.Find<ModNPC>("Keeper").Type, 0, 0f, 0f, 0f, 0f, 255);
		if (!UltraniumWorld.downedAldin)
		{
			UltraniumWorld.downedAldin = true;
			if (Main.netMode == 2)
			{
				NetMessage.SendData(7);
			}
		}
	}
}
