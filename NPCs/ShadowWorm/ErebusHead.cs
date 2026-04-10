using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Ultranium.NPCs.ShadowWorm.Projectiles;
using Ultranium.ShadowEvent;

namespace Ultranium.NPCs.ShadowWorm;

[AutoloadBossHead]
public class ErebusHead : ModNPC
{
	public static int CircleTimer;

	public static int CircleShootTimer;

	public static int Timer1;

	public static int Timer2;

	public static int Timer3;

	public int ChaseTimer;

	public int MovementAI;

	public float speed = (Main.expertMode ? 32f : 31f);

	public float acceleration = (Main.expertMode ? 0.5f : 0.5f);

	public int players;

	public static bool Circling;

	public static bool TeleportVortex;

	public int TeleportVortexTimer;

	private int Vortex;

	public float rotate;

	public float SpinX;

	public float SpinY;

	public int Spin;

	private int dpsCap = 250;

	private int damageDealt;

	private int dpsTime;

	private int noDamageTime;

	public override void SetStaticDefaults()
	{
		((ModNPC)this).DisplayName.SetDefault("Erebus");
		Main.npcFrameCount[((ModNPC)this).npc.type] = 2;
	}

	public override void SetDefaults()
	{
		((ModNPC)this).npc.lifeMax = 385000;
		((ModNPC)this).npc.damage = 120;
		((ModNPC)this).npc.defense = 85;
		((ModNPC)this).npc.knockBackResist = 0f;
		((ModNPC)this).npc.width = 80;
		((ModNPC)this).npc.height = 80;
		((ModNPC)this).npc.boss = true;
		((ModNPC)this).npc.lavaImmune = true;
		((ModNPC)this).npc.noGravity = true;
		((ModNPC)this).npc.noTileCollide = true;
		((ModNPC)this).npc.HitSound = SoundID.NPCHit52?.WithVolume(5f);
		((ModNPC)this).npc.DeathSound = ((ModNPC)this).mod.GetLegacySoundSlot((SoundType)50, "Sounds/ErebusRoar")?.WithVolume(1f);
		((ModNPC)this).npc.behindTiles = true;
		((ModNPC)this).npc.value = Item.buyPrice(0, 25, 50);
		((ModNPC)this).npc.npcSlots = 1f;
		((ModNPC)this).npc.netAlways = true;
		base.music = ((ModNPC)this).mod.GetSoundSlot((SoundType)51, "Sounds/Music/ErebusTheme");
		base.bossBag = ((ModNPC)this).mod.ItemType("ErebusBag");
		players = 1;
		for (int i = 0; i < 206; i++)
		{
			((ModNPC)this).npc.buffImmune[i] = true;
		}
	}

	public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
	{
		players = numPlayers;
		((ModNPC)this).npc.lifeMax = 450000 + numPlayers * 45000;
		((ModNPC)this).npc.damage = 200;
		((ModNPC)this).npc.defense = 90;
	}

	public override void BossHeadRotation(ref float rotation)
	{
		rotation = ((ModNPC)this).npc.rotation;
	}

	public override void FindFrame(int frameHeight)
	{
		Player player = Main.player[((ModNPC)this).npc.target];
		if ((Timer1 > 670 && Timer1 < 1015) || (Timer2 > 820 && Timer2 < 960) || (Timer2 >= 1335 && Timer2 <= 1575) || (Timer2 > 1575 && Vector2.Distance(((ModNPC)this).npc.Center, player.Center) <= 500f) || (Timer2 >= 2076 && Timer2 <= 2150) || Circling || ((ModNPC)this).npc.life < ((ModNPC)this).npc.lifeMax / 5)
		{
			((ModNPC)this).npc.frame.Y = frameHeight;
		}
		else
		{
			((ModNPC)this).npc.frame.Y = 0;
		}
	}

	public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
	{
		Texture2D texture = ((ModNPC)this).mod.GetTexture("NPCs/ShadowWorm/Glow/ErebusHeadGlow");
		Rectangle value = new Rectangle(0, ((ModNPC)this).npc.frame.Y, texture.Width, texture.Height / Main.npcFrameCount[((ModNPC)this).npc.type]);
		Vector2 origin = new Vector2((float)texture.Width * 0.5f, (float)texture.Height * 0.3f);
		SpriteEffects effects = SpriteEffects.None;
		spriteBatch.Draw(texture, ((ModNPC)this).npc.Center - Main.screenPosition + new Vector2(0f, ((ModNPC)this).npc.gfxOffY), value, new Color(255, 255, 255, 0), ((ModNPC)this).npc.rotation, origin, ((ModNPC)this).npc.scale, effects, 0f);
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
	{
		Texture2D texture2D = Main.npcTexture[((ModNPC)this).npc.type];
		Rectangle value = new Rectangle(0, ((ModNPC)this).npc.frame.Y, texture2D.Width, texture2D.Height / Main.npcFrameCount[((ModNPC)this).npc.type]);
		Vector2 origin = new Vector2((float)texture2D.Width * 0.5f, (float)texture2D.Height * 0.3f);
		Main.spriteBatch.Draw(texture2D, ((ModNPC)this).npc.Center - Main.screenPosition, value, drawColor, ((ModNPC)this).npc.rotation, origin, ((ModNPC)this).npc.scale, SpriteEffects.None, 0f);
		return false;
	}

	public override void ModifyHitByItem(Player player, Item item, ref int damage, ref float knockback, ref bool crit)
	{
		if (noDamageTime > 0)
		{
			damage = 0;
		}
	}

	public override void ModifyHitByProjectile(Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
	{
		if (noDamageTime > 0)
		{
			damage = 0;
		}
		if (projectile.type == 634 || projectile.type == 617 || projectile.type == 620 || projectile.type == 632 || projectile.type == 631 || projectile.type == 639 || projectile.type == 616 || projectile.type == 502 || projectile.type == 503 || projectile.type == 636)
		{
			damage /= 5;
		}
	}

	public override bool CheckDead()
	{
		Gore.NewGore(((ModNPC)this).npc.position, ((ModNPC)this).npc.velocity, ((ModNPC)this).mod.GetGoreSlot("Gores/ErebusHeadGore1"));
		Gore.NewGore(((ModNPC)this).npc.position, ((ModNPC)this).npc.velocity, ((ModNPC)this).mod.GetGoreSlot("Gores/ErebusHeadGore2"));
		return true;
	}

	public override void HitEffect(int hitDirection, double damage)
	{
		if (!((ModNPC)this).npc.immortal)
		{
			damageDealt += (int)damage;
		}
	}

	public override bool PreAI()
	{
		dpsTime++;
		if (noDamageTime >= 1)
		{
			noDamageTime--;
		}
		if (dpsTime >= 60)
		{
			dpsTime = 0;
			damageDealt = 0;
		}
		if (damageDealt >= dpsCap)
		{
			dpsTime = 0;
			damageDealt = 0;
			noDamageTime = 60;
		}
		if (noDamageTime != 0)
		{
			((ModNPC)this).npc.defense = 100000;
		}
		else
		{
			((ModNPC)this).npc.defense = 90;
		}
		Player player = Main.player[((ModNPC)this).npc.target];
		int num = (Main.expertMode ? 48 : 65);
		((ModNPC)this).npc.rotation = (float)Math.Atan2(((ModNPC)this).npc.velocity.Y, ((ModNPC)this).npc.velocity.X) + 1.57f;
		if (Main.player[((ModNPC)this).npc.target].dead || Main.dayTime)
		{
			((ModNPC)this).npc.velocity.Y = 80f;
			((ModNPC)this).npc.ai[3] += 1f;
			if (((ModNPC)this).npc.ai[3] >= 120f)
			{
				((Entity)((ModNPC)this).npc).active = false;
			}
		}
		if (NPC.AnyNPCs(((ModNPC)this).mod.NPCType("RestlessSoul")) || player.ownedProjectileCounts[((ModNPC)this).mod.ProjectileType("ExpandingVortex")] > 0)
		{
			((ModNPC)this).npc.immortal = true;
			((ModNPC)this).npc.dontTakeDamage = true;
		}
		else
		{
			((ModNPC)this).npc.immortal = false;
			((ModNPC)this).npc.dontTakeDamage = false;
		}
		if (player.ownedProjectileCounts[((ModNPC)this).mod.ProjectileType("ExpandingVortex")] > 0 && ExpandingVortex.Timer < 550)
		{
			TeleportVortex = true;
			if (ExpandingVortex.Timer < 500)
			{
				((ModNPC)this).npc.scale = 0.0001f;
			}
			else
			{
				((ModNPC)this).npc.scale = 1f;
			}
		}
		else
		{
			TeleportVortex = false;
			((ModNPC)this).npc.scale = 1f;
		}
		if ((double)((ModNPC)this).npc.life <= (double)((ModNPC)this).npc.lifeMax * 0.75 && Vortex == 0 && ((Entity)((ModNPC)this).npc).active)
		{
			Vector2 vector = Main.player[((ModNPC)this).npc.target].Center - ((ModNPC)this).npc.Center;
			vector.Normalize();
			vector.X *= 10.5f;
			vector.Y *= 10.5f;
			Main.PlaySound(SoundID.Item84, ((ModNPC)this).npc.position);
			Projectile.NewProjectile(((ModNPC)this).npc.Center.X, ((ModNPC)this).npc.Center.Y, vector.X, vector.Y, ((ModNPC)this).mod.ProjectileType("ExpandingVortex"), num, 1f, Main.myPlayer, 0f, 0f);
			Vortex++;
		}
		if ((double)((ModNPC)this).npc.life <= (double)((ModNPC)this).npc.lifeMax * 0.5 && Vortex == 1 && ((Entity)((ModNPC)this).npc).active)
		{
			Vector2 vector2 = Main.player[((ModNPC)this).npc.target].Center - ((ModNPC)this).npc.Center;
			vector2.Normalize();
			vector2.X *= 10.5f;
			vector2.Y *= 10.5f;
			Main.PlaySound(SoundID.Item84, ((ModNPC)this).npc.position);
			Projectile.NewProjectile(((ModNPC)this).npc.Center.X, ((ModNPC)this).npc.Center.Y, vector2.X, vector2.Y, ((ModNPC)this).mod.ProjectileType("ExpandingVortex"), num, 1f, Main.myPlayer, 0f, 0f);
			Vortex++;
		}
		if ((double)((ModNPC)this).npc.life <= (double)((ModNPC)this).npc.lifeMax * 0.25 && Vortex == 2 && ((Entity)((ModNPC)this).npc).active)
		{
			Vector2 vector3 = Main.player[((ModNPC)this).npc.target].Center - ((ModNPC)this).npc.Center;
			vector3.Normalize();
			vector3.X *= 10.5f;
			vector3.Y *= 10.5f;
			Main.PlaySound(SoundID.Item84, ((ModNPC)this).npc.position);
			Projectile.NewProjectile(((ModNPC)this).npc.Center.X, ((ModNPC)this).npc.Center.Y, vector3.X, vector3.Y, ((ModNPC)this).mod.ProjectileType("ExpandingVortex"), num, 1f, Main.myPlayer, 0f, 0f);
			Vortex++;
		}
		if (((ModNPC)this).npc.life > ((ModNPC)this).npc.lifeMax / 2 && ((ModNPC)this).npc.life > ((ModNPC)this).npc.lifeMax / 5 && !Circling && !TeleportVortex)
		{
			Timer1++;
			if (Timer1 == 250 || Timer1 == 370 || Timer1 == 490)
			{
				Vector2 vector4 = Main.player[((ModNPC)this).npc.target].Center - ((ModNPC)this).npc.Center;
				vector4.Normalize();
				vector4.X *= 18.5f;
				vector4.Y *= 18.5f;
				int num2 = Main.rand.Next(3, 5);
				for (int i = 0; i < num2; i++)
				{
					float num3 = (float)Main.rand.Next(-300, 300) * 0.01f;
					Projectile.NewProjectile(((ModNPC)this).npc.Center.X, ((ModNPC)this).npc.Center.Y, vector4.X + num3, vector4.Y + num3, ((ModNPC)this).mod.ProjectileType("ErebusBlast"), num, 1f, Main.myPlayer, 0f, 0f);
				}
			}
			if (Timer1 < 790 && Timer1 > 670)
			{
				((ModNPC)this).npc.velocity *= 0.9f;
				int num4 = 36;
				for (int j = 0; j < num4; j++)
				{
					Vector2 vector5 = (Vector2.One * new Vector2((float)((ModNPC)this).npc.width / 7f, (float)((ModNPC)this).npc.height / 7f) * 0.75f * 0.5f).RotatedBy((float)(j - (num4 / 2 - 1)) * ((float)Math.PI * 2f) / (float)num4) + ((ModNPC)this).npc.Center;
					Vector2 vector6 = vector5 - ((ModNPC)this).npc.Center;
					Dust obj = Main.dust[Dust.NewDust(vector5 + vector6, 0, 0, 89, vector6.X * 2f, vector6.Y * 2f, 100, default(Color), 1.4f)];
					obj.noGravity = true;
					obj.noLight = false;
					obj.velocity = Vector2.Normalize(vector6) * 3f;
					obj.fadeIn = 1.3f;
				}
			}
			if (Timer1 >= 790)
			{
				((ModNPC)this).npc.velocity *= 0.988f;
			}
			else
			{
				((ModNPC)this).npc.velocity *= 1f;
			}
			if (Timer1 == 790)
			{
				Main.PlaySound(((ModNPC)this).mod.GetLegacySoundSlot((SoundType)50, "Sounds/ErebusRoar")?.WithVolume(1f), -1, -1);
			}
			if (Timer1 == 790 || Timer1 == 865 || Timer1 == 940)
			{
				int num5 = 24;
				for (int k = 0; k < num5; k++)
				{
					Vector2 vector7 = (Vector2.One * new Vector2((float)((ModNPC)this).npc.width / 7f, (float)((ModNPC)this).npc.height / 7f) * 0.75f * 0.5f).RotatedBy((float)(k - (num5 / 2 - 1)) * ((float)Math.PI * 2f) / (float)num5) + ((ModNPC)this).npc.Center;
					Vector2 vector8 = vector7 - ((ModNPC)this).npc.Center;
					Dust obj2 = Main.dust[Dust.NewDust(vector7 + vector8, 0, 0, 89, vector8.X * 2f, vector8.Y * 2f, 100, default(Color), 1.4f)];
					obj2.noGravity = true;
					obj2.noLight = false;
					obj2.velocity = Vector2.Normalize(vector8) * 6f;
					obj2.fadeIn = 1.3f;
				}
				Vector2 vector9 = Main.player[((ModNPC)this).npc.target].Center - ((ModNPC)this).npc.Center;
				vector9.Normalize();
				vector9.X *= 50f;
				vector9.Y *= 50f;
				((ModNPC)this).npc.velocity.X = vector9.X;
				((ModNPC)this).npc.velocity.Y = vector9.Y;
			}
			if (Timer1 >= 1015)
			{
				Timer1 = 0;
				MovementAI = 0;
			}
		}
		if (((ModNPC)this).npc.life <= ((ModNPC)this).npc.lifeMax / 2 && ((ModNPC)this).npc.life > ((ModNPC)this).npc.lifeMax / 5 && !Circling && !TeleportVortex)
		{
			Timer1 = 0;
			Timer2++;
			if (Timer2 >= 300 && Timer2 <= 500 && Main.rand.Next(20) == 0)
			{
				Projectile.NewProjectile(player.Center + Main.rand.NextVector2Square(-750f, 750f), Main.rand.NextVector2Square(-1f, 1f), ((ModNPC)this).mod.ProjectileType("EldritchEyeTelegraph"), 0, 6f, player.whoAmI, 0f, 0f);
			}
			if (Timer2 > 820 && Timer2 < 960)
			{
				((ModNPC)this).npc.velocity *= 0.9f;
				int num6 = 36;
				for (int l = 0; l < num6; l++)
				{
					Vector2 vector10 = (Vector2.One * new Vector2((float)((ModNPC)this).npc.width / 7f, (float)((ModNPC)this).npc.height / 7f) * 0.75f * 0.5f).RotatedBy((float)(l - (num6 / 2 - 1)) * ((float)Math.PI * 2f) / (float)num6) + ((ModNPC)this).npc.Center;
					Vector2 vector11 = vector10 - ((ModNPC)this).npc.Center;
					Dust obj3 = Main.dust[Dust.NewDust(vector10 + vector11, 0, 0, ((ModNPC)this).mod.DustType("ShadowDustPurple"), vector11.X * 2f, vector11.Y * 2f, 100, default(Color), 1.4f)];
					obj3.noGravity = true;
					obj3.noLight = false;
					obj3.velocity = Vector2.Normalize(vector11) * 3f;
					obj3.fadeIn = 1.3f;
				}
			}
			if (Timer2 == 900)
			{
				Main.PlaySound(((ModNPC)this).mod.GetLegacySoundSlot((SoundType)50, "Sounds/ErebusRoar")?.WithVolume(1f), -1, -1);
				Vector2 vector12 = Main.player[((ModNPC)this).npc.target].Center - ((ModNPC)this).npc.Center;
				vector12.Normalize();
				vector12.X *= 12.5f;
				vector12.Y *= 12.5f;
				Main.PlaySound(SoundID.Item84, ((ModNPC)this).npc.position);
				Projectile.NewProjectile(((ModNPC)this).npc.Center.X, ((ModNPC)this).npc.Center.Y, vector12.X, vector12.Y, ((ModNPC)this).mod.ProjectileType("ErebusVortex"), num, 1f, Main.myPlayer, 0f, 0f);
			}
			if (Timer2 == 1275)
			{
				MovementAI = 1;
			}
			if (Timer2 == 1270 && ((ModNPC)this).npc.life <= ((ModNPC)this).npc.lifeMax / 3)
			{
				Circling = true;
			}
			if (Timer2 == 1335 || Timer2 == 1395 || Timer2 == 1455 || Timer2 == 1515 || Timer2 == 1575)
			{
				Main.PlaySound(SoundID.NPCDeath13, ((ModNPC)this).npc.position);
				float num7 = 2f;
				int num8 = ((ModNPC)this).mod.ProjectileType("ErebusToothBall");
				Main.PlaySound(2, (int)((ModNPC)this).npc.position.X, (int)((ModNPC)this).npc.position.Y, 20, 1f, 0f);
				float num9 = (float)Math.Atan2(((ModNPC)this).npc.Center.Y - player.Center.Y, ((ModNPC)this).npc.Center.X - player.Center.X);
				Projectile.NewProjectile(((ModNPC)this).npc.Center.X, ((ModNPC)this).npc.Center.Y, (float)(Math.Cos(num9) * (double)num7 * -1.0), (float)(Math.Sin(num9) * (double)num7 * -1.0), num8, num, 0f, Main.myPlayer, 0f, 0f);
			}
			if (Vector2.Distance(((ModNPC)this).npc.Center, player.Center) <= 500f && MovementAI == 1 && Timer2 >= 1575)
			{
				Vector2 vector13 = new Vector2(((ModNPC)this).npc.position.X + (float)(((ModNPC)this).npc.width / 2), ((ModNPC)this).npc.position.Y + (float)(((ModNPC)this).npc.height / 2));
				if (Main.rand.Next(5) == 0)
				{
					Projectile.NewProjectile(vector13.X + ((ModNPC)this).npc.velocity.X, vector13.Y + ((ModNPC)this).npc.velocity.Y, ((ModNPC)this).npc.velocity.X * 0.5f + Utils.NextFloat(Main.rand, -0.6f, 0.6f) * 1f, ((ModNPC)this).npc.velocity.Y * 0.5f + Utils.NextFloat(Main.rand, -0.6f, 0.6f) * 1f, ((ModNPC)this).mod.ProjectileType("ShadowFlameBreath"), num, 0f, 0, 0f, 0f);
					if (Main.rand.Next(3) == 0)
					{
						Main.PlaySound(SoundID.DD2_BetsyFlameBreath, ((ModNPC)this).npc.position);
					}
				}
			}
			if (Timer2 >= 1995 && Timer2 < 2075)
			{
				int num10 = 36;
				for (int m = 0; m < num10; m++)
				{
					Vector2 vector14 = (Vector2.One * new Vector2((float)((ModNPC)this).npc.width / 7f, (float)((ModNPC)this).npc.height / 7f) * 0.75f * 0.5f).RotatedBy((float)(m - (num10 / 2 - 1)) * ((float)Math.PI * 2f) / (float)num10) + ((ModNPC)this).npc.Center;
					Vector2 vector15 = vector14 - ((ModNPC)this).npc.Center;
					Dust obj4 = Main.dust[Dust.NewDust(vector14 + vector15, 0, 0, 89, vector15.X * 2f, vector15.Y * 2f, 100, default(Color), 1.4f)];
					obj4.noGravity = true;
					obj4.noLight = false;
					obj4.velocity = Vector2.Normalize(vector15) * 3f;
					obj4.fadeIn = 1.3f;
				}
			}
			if (Timer2 == 2075)
			{
				for (int n = 0; n < 100; n++)
				{
					int num11 = Dust.NewDust(((ModNPC)this).npc.position, ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, 89, 0f, -2f, 0, default(Color), 1.5f);
					Main.dust[num11].noGravity = false;
					Main.dust[num11].scale = 3.5f;
					Main.dust[num11].position.X += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
					Main.dust[num11].position.Y += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
					if (Main.dust[num11].position != ((ModNPC)this).npc.Center)
					{
						Main.dust[num11].velocity = ((ModNPC)this).npc.DirectionTo(Main.dust[num11].position) * 10f;
					}
				}
				((ModNPC)this).npc.position.X = player.position.X - 100f;
				((ModNPC)this).npc.position.Y = player.position.Y + 5000f;
				for (int num12 = 0; num12 < 200; num12++)
				{
					if (((Entity)Main.npc[num12]).active && (Main.npc[num12].type == ModContent.NPCType<ErebusBody>() || Main.npc[num12].type == ModContent.NPCType<ErebusTail>()))
					{
						Main.npc[num12].Center = ((ModNPC)this).npc.Center;
					}
				}
				MovementAI = 0;
			}
			if (Timer2 == 2076)
			{
				Vector2 vector16 = Main.player[((ModNPC)this).npc.target].Center - ((ModNPC)this).npc.Center;
				vector16.Normalize();
				vector16.X *= 65f;
				vector16.Y *= 65f;
				((ModNPC)this).npc.velocity.X = vector16.X;
				((ModNPC)this).npc.velocity.Y = vector16.Y;
			}
			if (Timer2 >= 2076 && Timer2 <= 2226 && Main.rand.Next(3) == 0)
			{
				for (int num13 = 0; num13 < 2; num13++)
				{
					Vector2 vector17 = new Vector2(0f, 1f).RotatedBy(((ModNPC)this).npc.rotation + MathHelper.ToRadians((num13 == 0) ? 90 : 270));
					vector17.Normalize();
					vector17 *= 10f;
					_ = Main.projectile[Projectile.NewProjectile(((ModNPC)this).npc.Center, vector17, ((ModNPC)this).mod.ProjectileType("EldritchBlast"), num, 0f, Main.myPlayer, 0f, 0f)];
				}
			}
			if (Timer2 == 2226)
			{
				Timer2 = 0;
				MovementAI = 0;
				((ModNPC)this).npc.velocity *= 0f;
			}
		}
		if (((ModNPC)this).npc.life < ((ModNPC)this).npc.lifeMax / 5 && !Circling && !TeleportVortex)
		{
			Timer1 = 0;
			Timer2 = 0;
			((ModNPC)this).npc.velocity *= 0.988f;
			Timer3++;
			if (Timer3 < 100)
			{
				((ModNPC)this).npc.velocity *= 0.9f;
				int num14 = 36;
				for (int num15 = 0; num15 < num14; num15++)
				{
					Vector2 vector18 = (Vector2.One * new Vector2((float)((ModNPC)this).npc.width / 7f, (float)((ModNPC)this).npc.height / 7f) * 0.75f * 0.5f).RotatedBy((float)(num15 - (num14 / 2 - 1)) * ((float)Math.PI * 2f) / (float)num14) + ((ModNPC)this).npc.Center;
					Vector2 vector19 = vector18 - ((ModNPC)this).npc.Center;
					Dust obj5 = Main.dust[Dust.NewDust(vector18 + vector19, 0, 0, 89, vector19.X * 2f, vector19.Y * 2f, 100, default(Color), 1.4f)];
					obj5.noGravity = true;
					obj5.noLight = false;
					obj5.velocity = Vector2.Normalize(vector19) * 3f;
					obj5.fadeIn = 1.3f;
				}
			}
			if (Timer3 == 100 || Timer3 == 160 || Timer3 == 220 || Timer3 == 280 || Timer3 == 340)
			{
				Main.PlaySound(((ModNPC)this).mod.GetLegacySoundSlot((SoundType)50, "Sounds/ErebusCharge")?.WithVolume(1f), -1, -1);
				int num16 = 24;
				for (int num17 = 0; num17 < num16; num17++)
				{
					Vector2 vector20 = (Vector2.One * new Vector2((float)((ModNPC)this).npc.width / 7f, (float)((ModNPC)this).npc.height / 7f) * 0.75f * 0.5f).RotatedBy((float)(num17 - (num16 / 2 - 1)) * ((float)Math.PI * 2f) / (float)num16) + ((ModNPC)this).npc.Center;
					Vector2 vector21 = vector20 - ((ModNPC)this).npc.Center;
					Dust obj6 = Main.dust[Dust.NewDust(vector20 + vector21, 0, 0, 89, vector21.X * 2f, vector21.Y * 2f, 100, default(Color), 1.4f)];
					obj6.noGravity = true;
					obj6.noLight = false;
					obj6.velocity = Vector2.Normalize(vector21) * 6f;
					obj6.fadeIn = 1.3f;
				}
				Vector2 vector22 = Main.player[((ModNPC)this).npc.target].Center - ((ModNPC)this).npc.Center;
				vector22.Normalize();
				vector22.X *= 50f;
				vector22.Y *= 50f;
				((ModNPC)this).npc.velocity.X = vector22.X;
				((ModNPC)this).npc.velocity.Y = vector22.Y;
			}
			if (Timer3 == 400)
			{
				Timer3 = 40;
				Circling = true;
			}
		}
		if (TeleportVortex)
		{
			Timer1 = 0;
			Timer2 = 0;
			Timer3 = 0;
			MovementAI = 0;
			TeleportVortexTimer++;
			if (ExpandingVortex.Timer == 500)
			{
				Main.PlaySound(((ModNPC)this).mod.GetLegacySoundSlot((SoundType)50, "Sounds/ErebusRoar")?.WithVolume(1f), -1, -1);
				Vector2 vector23 = Main.player[((ModNPC)this).npc.target].Center - ((ModNPC)this).npc.Center;
				vector23.Normalize();
				vector23.X *= 50f;
				vector23.Y *= 50f;
				((ModNPC)this).npc.velocity.X = vector23.X;
				((ModNPC)this).npc.velocity.Y = vector23.Y;
			}
		}
		if (((ModNPC)this).npc.localAI[1] == 0f && ((ModNPC)this).npc.life <= ((ModNPC)this).npc.lifeMax / 3)
		{
			int num18 = 5;
			for (int num19 = 0; num19 < num18; num19++)
			{
				int num20 = 360 / num18;
				NPC.NewNPC((int)((ModNPC)this).npc.Center.X, (int)((ModNPC)this).npc.Center.Y, ModContent.NPCType<RestlessSoul>(), ((ModNPC)this).npc.whoAmI, (float)((ModNPC)this).npc.whoAmI, (float)(num19 * num20), 0f, 0f, 255);
			}
			((ModNPC)this).npc.localAI[1] += 1f;
		}
		if (Circling && !TeleportVortex)
		{
			((ModNPC)this).npc.velocity = new Vector2(((ModNPC)this).npc.velocity.X, ((ModNPC)this).npc.velocity.Y).RotatedBy(MathHelper.ToRadians(Spin - 30));
			((ModNPC)this).npc.TargetClosest(faceTarget: false);
			CircleTimer++;
			rotate += 2f;
			if (CircleTimer < 580)
			{
				Vector2 vector24 = new Vector2(2000f, 0f).RotatedBy(MathHelper.ToRadians(rotate * 1.57f));
				SpinX = player.Center.X + vector24.X - ((ModNPC)this).npc.Center.X;
				SpinY = player.Center.Y + vector24.Y - ((ModNPC)this).npc.Center.Y;
				float num21 = (float)Math.Sqrt(SpinX * SpinX + SpinY * SpinY);
				if (num21 > 52f)
				{
					num21 = 7.5f / num21;
					SpinX *= num21 * 7f;
					SpinY *= num21 * 7f;
					((ModNPC)this).npc.velocity.X = SpinX;
					((ModNPC)this).npc.velocity.Y = SpinY;
				}
				else
				{
					((ModNPC)this).npc.position.X = player.Center.X + vector24.X - (float)(((ModNPC)this).npc.height / 2);
					((ModNPC)this).npc.position.Y = player.Center.Y + vector24.Y - (float)(((ModNPC)this).npc.width / 2);
					num21 = 7f / num21;
					SpinX *= num21 * 7f;
					SpinY *= num21 * 7f;
					((ModNPC)this).npc.velocity.X = 0f;
					((ModNPC)this).npc.velocity.Y = 0f;
					((ModNPC)this).npc.rotation = (float)Math.Atan2(((ModNPC)this).npc.velocity.X, ((ModNPC)this).npc.velocity.Y) + 1.57f;
				}
				CircleShootTimer++;
				if (CircleShootTimer == 60)
				{
					for (int num22 = -1; num22 <= 2; num22++)
					{
						Projectile.NewProjectile(((ModNPC)this).npc.Center, 15f * ((ModNPC)this).npc.DirectionTo(player.Center).RotatedBy(MathHelper.ToRadians(8f) * (float)num22), ((ModNPC)this).mod.ProjectileType("ErebusBlast"), num, 0f, Main.myPlayer, 0f, 0f);
					}
				}
				if (CircleShootTimer == 70)
				{
					CircleShootTimer = 0;
				}
			}
			else
			{
				((ModNPC)this).npc.velocity *= 0f;
			}
			if (CircleTimer == 590)
			{
				Circling = false;
				CircleTimer = 0;
				CircleShootTimer = 0;
			}
		}
		if (Main.netMode != 1 && ((ModNPC)this).npc.ai[0] == 0f)
		{
			((ModNPC)this).npc.realLife = ((ModNPC)this).npc.whoAmI;
			int num23 = ((ModNPC)this).npc.whoAmI;
			int num24 = 26;
			for (int num25 = 1; num25 < num24; num25++)
			{
				int num26 = 0;
				num26 = ((ModNPC)this).mod.NPCType("ErebusBody");
				num23 = NPC.NewNPC((int)((ModNPC)this).npc.Center.X, (int)((ModNPC)this).npc.Center.Y, num26, ((ModNPC)this).npc.whoAmI, 0f, (float)num23, 0f, 0f, 255);
				Main.npc[num23].realLife = ((ModNPC)this).npc.whoAmI;
				Main.npc[num23].ai[3] = ((ModNPC)this).npc.whoAmI;
			}
			num23 = NPC.NewNPC((int)((ModNPC)this).npc.Center.X, (int)((ModNPC)this).npc.Center.Y, ((ModNPC)this).mod.NPCType("ErebusTail"), ((ModNPC)this).npc.whoAmI, 0f, (float)num23, 0f, 0f, 255);
			Main.npc[num23].realLife = ((ModNPC)this).npc.whoAmI;
			Main.npc[num23].ai[3] = ((ModNPC)this).npc.whoAmI;
			((ModNPC)this).npc.ai[0] = 1f;
			((ModNPC)this).npc.netUpdate = true;
		}
		if (!Circling && Timer2 < 2075 && Timer1 < 790 && ((ModNPC)this).npc.life > ((ModNPC)this).npc.lifeMax / 5 && !TeleportVortex)
		{
			int num27 = (int)((double)((ModNPC)this).npc.position.X / 16.0) - 1;
			int num28 = (int)((double)(((ModNPC)this).npc.position.X + (float)((ModNPC)this).npc.width) / 16.0) + 2;
			int num29 = (int)((double)((ModNPC)this).npc.position.Y / 16.0) - 1;
			int num30 = (int)((double)(((ModNPC)this).npc.position.Y + (float)((ModNPC)this).npc.height) / 16.0) + 2;
			if (num27 < 0)
			{
				num27 = 0;
			}
			if (num28 > Main.maxTilesX)
			{
				num28 = Main.maxTilesX;
			}
			if (num29 < 0)
			{
				num29 = 0;
			}
			if (num30 > Main.maxTilesY)
			{
				num30 = Main.maxTilesY;
			}
			bool flag = false;
			Vector2 vector25 = default(Vector2);
			for (int num31 = num27; num31 < num28; num31++)
			{
				for (int num32 = num29; num32 < num30; num32++)
				{
					if (Main.tile[num31, num32] == null || ((!Main.tile[num31, num32].nactive() || (!Main.tileSolid[Main.tile[num31, num32].type] && Main.tile[num31, num32].frameY != 0)) && Main.tile[num31, num32].liquid <= 64))
					{
						continue;
					}
					vector25.X = num31 * 16;
					vector25.Y = num32 * 16;
					if (((ModNPC)this).npc.position.X + (float)((ModNPC)this).npc.width > vector25.X && (double)((ModNPC)this).npc.position.X < (double)vector25.X + 16.0 && (double)(((ModNPC)this).npc.position.Y + (float)((ModNPC)this).npc.height) > (double)vector25.Y && (double)((ModNPC)this).npc.position.Y < (double)vector25.Y + 16.0)
					{
						flag = true;
						Ultranium.seizureAmount = 12f;
						if (Main.rand.Next(100) == 0 && Main.tile[num31, num32].nactive())
						{
							WorldGen.KillTile(num31, num32, fail: true, effectOnly: true);
						}
					}
				}
			}
			if (!flag)
			{
				Rectangle rectangle = new Rectangle((int)((ModNPC)this).npc.position.X, (int)((ModNPC)this).npc.position.Y, ((ModNPC)this).npc.width, ((ModNPC)this).npc.height);
				int num33 = 0;
				if (MovementAI == 0)
				{
					num33 = 900;
				}
				if (MovementAI == 1)
				{
					num33 = 400;
				}
				bool flag2 = true;
				for (int num34 = 0; num34 < 255; num34++)
				{
					if (!((Entity)Main.player[num34]).active)
					{
						continue;
					}
					Rectangle value = new Rectangle((int)Main.player[num34].position.X - num33, (int)Main.player[num34].position.Y - num33, num33 * 2, num33 * 2);
					if (rectangle.Intersects(value))
					{
						if (MovementAI == 0)
						{
							flag2 = false;
							ChaseTimer = 0;
							speed = (Main.expertMode ? 32f : 32f);
							acceleration = (Main.expertMode ? 0.5f : 0.5f);
						}
						if (MovementAI == 1)
						{
							flag2 = false;
							speed = (Main.expertMode ? 25f : 25f);
							acceleration = (Main.expertMode ? 0.46f : 0.46f);
						}
						break;
					}
				}
				if (flag2)
				{
					flag = true;
				}
			}
			Vector2 vector26 = new Vector2(((ModNPC)this).npc.position.X + (float)((ModNPC)this).npc.width * 0.5f, ((ModNPC)this).npc.position.Y + (float)((ModNPC)this).npc.height * 0.5f);
			float num35 = Main.player[((ModNPC)this).npc.target].position.X + (float)(Main.player[((ModNPC)this).npc.target].width / 2);
			float num36 = Main.player[((ModNPC)this).npc.target].position.Y + (float)(Main.player[((ModNPC)this).npc.target].height / 2);
			float num37 = (int)((double)num35 / 16.0) * 16;
			float num38 = (int)((double)num36 / 16.0) * 16;
			vector26.X = (int)((double)vector26.X / 16.0) * 16;
			vector26.Y = (int)((double)vector26.Y / 16.0) * 16;
			float num39 = num37 - vector26.X;
			float num40 = num38 - vector26.Y;
			float num41 = (float)Math.Sqrt(num39 * num39 + num40 * num40);
			if (!flag)
			{
				((ModNPC)this).npc.TargetClosest();
				((ModNPC)this).npc.velocity.Y = ((ModNPC)this).npc.velocity.Y + 0.22f;
				if (((ModNPC)this).npc.velocity.Y > speed)
				{
					((ModNPC)this).npc.velocity.Y = speed;
				}
				if ((double)(Math.Abs(((ModNPC)this).npc.velocity.X) + Math.Abs(((ModNPC)this).npc.velocity.Y)) < (double)speed * 0.4)
				{
					if ((double)((ModNPC)this).npc.velocity.X < 0.0)
					{
						((ModNPC)this).npc.velocity.X = ((ModNPC)this).npc.velocity.X - acceleration * 1.1f;
					}
					else
					{
						((ModNPC)this).npc.velocity.X = ((ModNPC)this).npc.velocity.X + acceleration * 1.1f;
					}
				}
				else if (((ModNPC)this).npc.velocity.Y == speed)
				{
					if (((ModNPC)this).npc.velocity.X < num39)
					{
						((ModNPC)this).npc.velocity.X = ((ModNPC)this).npc.velocity.X + acceleration;
					}
					else if (((ModNPC)this).npc.velocity.X > num39)
					{
						((ModNPC)this).npc.velocity.X = ((ModNPC)this).npc.velocity.X - acceleration;
					}
				}
				else if ((double)((ModNPC)this).npc.velocity.Y > 4.0)
				{
					if ((double)((ModNPC)this).npc.velocity.X < 0.0)
					{
						((ModNPC)this).npc.velocity.X = ((ModNPC)this).npc.velocity.X + acceleration * 0.9f;
					}
					else
					{
						((ModNPC)this).npc.velocity.X = ((ModNPC)this).npc.velocity.X - acceleration * 0.9f;
					}
				}
			}
			else
			{
				float num42 = Math.Abs(num39);
				float num43 = Math.Abs(num40);
				float num44 = speed / num41;
				num39 *= num44;
				num40 *= num44;
				if (((double)((ModNPC)this).npc.velocity.X > 0.0 && (double)num39 > 0.0) || ((double)((ModNPC)this).npc.velocity.X < 0.0 && (double)num39 < 0.0) || ((double)((ModNPC)this).npc.velocity.Y > 0.0 && (double)num40 > 0.0) || ((double)((ModNPC)this).npc.velocity.Y < 0.0 && (double)num40 < 0.0))
				{
					if (((ModNPC)this).npc.velocity.X < num39)
					{
						((ModNPC)this).npc.velocity.X = ((ModNPC)this).npc.velocity.X + acceleration;
					}
					else if (((ModNPC)this).npc.velocity.X > num39)
					{
						((ModNPC)this).npc.velocity.X = ((ModNPC)this).npc.velocity.X - acceleration;
					}
					if (((ModNPC)this).npc.velocity.Y < num40)
					{
						((ModNPC)this).npc.velocity.Y = ((ModNPC)this).npc.velocity.Y + acceleration;
					}
					else if (((ModNPC)this).npc.velocity.Y > num40)
					{
						((ModNPC)this).npc.velocity.Y = ((ModNPC)this).npc.velocity.Y - acceleration;
					}
					if ((double)Math.Abs(num40) < (double)speed * 0.2 && (((double)((ModNPC)this).npc.velocity.X > 0.0 && (double)num39 < 0.0) || ((double)((ModNPC)this).npc.velocity.X < 0.0 && (double)num39 > 0.0)))
					{
						if ((double)((ModNPC)this).npc.velocity.Y > 0.0)
						{
							((ModNPC)this).npc.velocity.Y = ((ModNPC)this).npc.velocity.Y + acceleration * 2f;
						}
						else
						{
							((ModNPC)this).npc.velocity.Y = ((ModNPC)this).npc.velocity.Y - acceleration * 2f;
						}
					}
					if ((double)Math.Abs(num39) < (double)speed * 0.2 && (((double)((ModNPC)this).npc.velocity.Y > 0.0 && (double)num40 < 0.0) || ((double)((ModNPC)this).npc.velocity.Y < 0.0 && (double)num40 > 0.0)))
					{
						if ((double)((ModNPC)this).npc.velocity.X > 0.0)
						{
							((ModNPC)this).npc.velocity.X = ((ModNPC)this).npc.velocity.X + acceleration * 2f;
						}
						else
						{
							((ModNPC)this).npc.velocity.X = ((ModNPC)this).npc.velocity.X - acceleration * 2f;
						}
					}
				}
				else if (num42 > num43)
				{
					if (((ModNPC)this).npc.velocity.X < num39)
					{
						((ModNPC)this).npc.velocity.X = ((ModNPC)this).npc.velocity.X + acceleration * 1.1f;
					}
					else if (((ModNPC)this).npc.velocity.X > num39)
					{
						((ModNPC)this).npc.velocity.X = ((ModNPC)this).npc.velocity.X - acceleration * 1.1f;
					}
					if ((double)(Math.Abs(((ModNPC)this).npc.velocity.X) + Math.Abs(((ModNPC)this).npc.velocity.Y)) < (double)speed * 0.5)
					{
						if ((double)((ModNPC)this).npc.velocity.Y > 0.0)
						{
							((ModNPC)this).npc.velocity.Y = ((ModNPC)this).npc.velocity.Y + acceleration;
						}
						else
						{
							((ModNPC)this).npc.velocity.Y = ((ModNPC)this).npc.velocity.Y - acceleration;
						}
					}
				}
				else
				{
					if (((ModNPC)this).npc.velocity.Y < num40)
					{
						((ModNPC)this).npc.velocity.Y = ((ModNPC)this).npc.velocity.Y + acceleration * 1.1f;
					}
					else if (((ModNPC)this).npc.velocity.Y > num40)
					{
						((ModNPC)this).npc.velocity.Y = ((ModNPC)this).npc.velocity.Y - acceleration * 1.1f;
					}
					if ((double)(Math.Abs(((ModNPC)this).npc.velocity.X) + Math.Abs(((ModNPC)this).npc.velocity.Y)) < (double)speed * 0.5)
					{
						if ((double)((ModNPC)this).npc.velocity.X > 0.0)
						{
							((ModNPC)this).npc.velocity.X = ((ModNPC)this).npc.velocity.X + acceleration;
						}
						else
						{
							((ModNPC)this).npc.velocity.X = ((ModNPC)this).npc.velocity.X - acceleration;
						}
					}
				}
			}
			((ModNPC)this).npc.rotation = (float)Math.Atan2(((ModNPC)this).npc.velocity.Y, ((ModNPC)this).npc.velocity.X) + 1.57f;
			if (flag)
			{
				if (((ModNPC)this).npc.localAI[0] != 1f)
				{
					((ModNPC)this).npc.netUpdate = true;
				}
				((ModNPC)this).npc.localAI[0] = 1f;
			}
			else
			{
				if ((double)((ModNPC)this).npc.localAI[0] != 0.0)
				{
					((ModNPC)this).npc.netUpdate = true;
				}
				((ModNPC)this).npc.localAI[0] = 0f;
			}
			if ((((double)((ModNPC)this).npc.velocity.X > 0.0 && (double)((ModNPC)this).npc.oldVelocity.X < 0.0) || ((double)((ModNPC)this).npc.velocity.X < 0.0 && (double)((ModNPC)this).npc.oldVelocity.X > 0.0) || ((double)((ModNPC)this).npc.velocity.Y > 0.0 && (double)((ModNPC)this).npc.oldVelocity.Y < 0.0) || ((double)((ModNPC)this).npc.velocity.Y < 0.0 && (double)((ModNPC)this).npc.oldVelocity.Y > 0.0)) && !((ModNPC)this).npc.justHit)
			{
				((ModNPC)this).npc.netUpdate = true;
			}
		}
		if (!((Entity)((ModNPC)this).npc).active)
		{
			Timer1 = 0;
			Timer2 = 0;
			Timer3 = 0;
			Circling = false;
			CircleTimer = 0;
			CircleShootTimer = 0;
			MovementAI = 0;
		}
		return false;
	}

	private void Movement(Vector2 targetPos, float speedModifier, float cap = 12f, bool fastY = false)
	{
		if (((ModNPC)this).npc.Center.X < targetPos.X)
		{
			((ModNPC)this).npc.velocity.X += speedModifier;
			if (((ModNPC)this).npc.velocity.X < 0f)
			{
				((ModNPC)this).npc.velocity.X += speedModifier * 2f;
			}
		}
		else
		{
			((ModNPC)this).npc.velocity.X -= speedModifier;
			if (((ModNPC)this).npc.velocity.X > 0f)
			{
				((ModNPC)this).npc.velocity.X -= speedModifier * 2f;
			}
		}
		if (((ModNPC)this).npc.Center.Y < targetPos.Y)
		{
			((ModNPC)this).npc.velocity.Y += (fastY ? (speedModifier * 2f) : speedModifier);
			if (((ModNPC)this).npc.velocity.Y < 0f)
			{
				((ModNPC)this).npc.velocity.Y += speedModifier * 2f;
			}
		}
		else
		{
			((ModNPC)this).npc.velocity.Y -= (fastY ? (speedModifier * 2f) : speedModifier);
			if (((ModNPC)this).npc.velocity.Y > 0f)
			{
				((ModNPC)this).npc.velocity.Y -= speedModifier * 2f;
			}
		}
		if (Math.Abs(((ModNPC)this).npc.velocity.X) > cap)
		{
			((ModNPC)this).npc.velocity.X = cap * (float)Math.Sign(((ModNPC)this).npc.velocity.X);
		}
		if (Math.Abs(((ModNPC)this).npc.velocity.Y) > cap)
		{
			((ModNPC)this).npc.velocity.Y = cap * (float)Math.Sign(((ModNPC)this).npc.velocity.Y);
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
			int num = Main.rand.Next(9);
			if (num == 0)
			{
				Item.NewItem((int)((ModNPC)this).npc.position.X, (int)((ModNPC)this).npc.position.Y, ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, ((ModNPC)this).mod.ItemType("Noctis"), 1, false, 0, false, false);
			}
			if (num == 1)
			{
				Item.NewItem((int)((ModNPC)this).npc.position.X, (int)((ModNPC)this).npc.position.Y, ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, ((ModNPC)this).mod.ItemType("SolibusOrba"), 1, false, 0, false, false);
			}
			if (num == 2)
			{
				Item.NewItem((int)((ModNPC)this).npc.position.X, (int)((ModNPC)this).npc.position.Y, ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, ((ModNPC)this).mod.ItemType("Crepus"), 1, false, 0, false, false);
			}
			if (num == 3)
			{
				Item.NewItem((int)((ModNPC)this).npc.position.X, (int)((ModNPC)this).npc.position.Y, ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, ((ModNPC)this).mod.ItemType("Inanis"), 1, false, 0, false, false);
			}
			if (num == 4)
			{
				Item.NewItem((int)((ModNPC)this).npc.position.X, (int)((ModNPC)this).npc.position.Y, ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, ((ModNPC)this).mod.ItemType("CavumNigrum"), 1, false, 0, false, false);
			}
			if (num == 5)
			{
				Item.NewItem((int)((ModNPC)this).npc.position.X, (int)((ModNPC)this).npc.position.Y, ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, ((ModNPC)this).mod.ItemType("Exitium"), 1, false, 0, false, false);
			}
			if (num == 6)
			{
				Item.NewItem((int)((ModNPC)this).npc.position.X, (int)((ModNPC)this).npc.position.Y, ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, ((ModNPC)this).mod.ItemType("Umbra"), 1, false, 0, false, false);
			}
			if (num == 7)
			{
				Item.NewItem((int)((ModNPC)this).npc.position.X, (int)((ModNPC)this).npc.position.Y, ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, ((ModNPC)this).mod.ItemType("Nihil"), 1, false, 0, false, false);
			}
			if (num == 8)
			{
				Item.NewItem((int)((ModNPC)this).npc.position.X, (int)((ModNPC)this).npc.position.Y, ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, ((ModNPC)this).mod.ItemType("Caliginus"), 1, false, 0, false, false);
			}
			Item.NewItem((int)((ModNPC)this).npc.position.X, (int)((ModNPC)this).npc.position.Y, ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, ((ModNPC)this).mod.ItemType("NightmareScale"), Main.rand.Next(20, 35), false, 0, false, false);
			Item.NewItem((int)((ModNPC)this).npc.position.X, (int)((ModNPC)this).npc.position.Y, ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, ((ModNPC)this).mod.ItemType("DarkMatter"), Main.rand.Next(10, 15), false, 0, false, false);
			if (Main.rand.Next(20) == 0)
			{
				Item.NewItem((int)((ModNPC)this).npc.position.X, (int)((ModNPC)this).npc.position.Y, ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, ((ModNPC)this).mod.ItemType("ErebusGuitar"), 1, false, 0, false, false);
			}
		}
		if (Main.rand.Next(7) == 0)
		{
			Item.NewItem((int)((ModNPC)this).npc.position.X, (int)((ModNPC)this).npc.position.Y, ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, ((ModNPC)this).mod.ItemType("ErebusMask"), 1, false, 0, false, false);
		}
		if (Main.rand.Next(10) == 0)
		{
			Item.NewItem((int)((ModNPC)this).npc.position.X, (int)((ModNPC)this).npc.position.Y, ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, ((ModNPC)this).mod.ItemType("ErebusTrophyItem"), 1, false, 0, false, false);
		}
		if (!UltraniumWorld.downedErebus)
		{
			UltraniumWorld.downedErebus = true;
			if (Main.netMode == 2)
			{
				NetMessage.SendData(7);
			}
		}
		if (ShadowEventWorld.ShadowEventActive)
		{
			ShadowEventWorld.EventTimer = 25200;
		}
		Timer1 = 0;
		Timer2 = 0;
		Timer3 = 0;
		Circling = false;
		CircleTimer = 0;
		CircleShootTimer = 0;
		MovementAI = 0;
	}

	public override void BossLoot(ref string name, ref int potionType)
	{
		potionType = 3544;
	}

	public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
	{
		scale = 1.9f;
		return null;
	}

	public override bool CheckActive()
	{
		return false;
	}
}
