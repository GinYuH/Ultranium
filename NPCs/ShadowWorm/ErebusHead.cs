using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
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
		// ((ModNPC)this).DisplayName.SetDefault("Erebus");
		Main.npcFrameCount[((ModNPC)this).NPC.type] = 2;
	}

	public override void SetDefaults()
	{
		((ModNPC)this).NPC.lifeMax = 385000;
		((ModNPC)this).NPC.damage = 120;
		((ModNPC)this).NPC.defense = 85;
		((ModNPC)this).NPC.knockBackResist = 0f;
		((ModNPC)this).NPC.width = 80;
		((ModNPC)this).NPC.height = 80;
		((ModNPC)this).NPC.boss = true;
		((ModNPC)this).NPC.lavaImmune = true;
		((ModNPC)this).NPC.noGravity = true;
		((ModNPC)this).NPC.noTileCollide = true;
		((ModNPC)this).NPC.HitSound = SoundID.NPCHit52?.WithVolume(5f);
		((ModNPC)this).NPC.DeathSound = ((ModNPC)this).Mod.GetLegacySoundSlot((SoundType)50, "Sounds/ErebusRoar")?.WithVolume(1f);
		((ModNPC)this).NPC.behindTiles = true;
		((ModNPC)this).NPC.value = Item.buyPrice(0, 25, 50);
		((ModNPC)this).NPC.npcSlots = 1f;
		((ModNPC)this).NPC.netAlways = true;
		base.Music = ((ModNPC)this).Mod.GetSoundSlot((SoundType)51, "Sounds/Music/ErebusTheme");
		base.bossBag/* tModPorter Note: Removed. Spawn the treasure bag alongside other loot via npcLoot.Add(ItemDropRule.BossBag(type)) */ = ((ModNPC)this).Mod.Find<ModItem>("ErebusBag").Type;
		players = 1;
		for (int i = 0; i < 206; i++)
		{
			((ModNPC)this).NPC.buffImmune[i] = true;
		}
	}

	public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
	{
		players = numPlayers;
		((ModNPC)this).NPC.lifeMax = 450000 + numPlayers * 45000;
		((ModNPC)this).NPC.damage = 200;
		((ModNPC)this).NPC.defense = 90;
	}

	public override void BossHeadRotation(ref float rotation)
	{
		rotation = ((ModNPC)this).NPC.rotation;
	}

	public override void FindFrame(int frameHeight)
	{
		Player player = Main.player[((ModNPC)this).NPC.target];
		if ((Timer1 > 670 && Timer1 < 1015) || (Timer2 > 820 && Timer2 < 960) || (Timer2 >= 1335 && Timer2 <= 1575) || (Timer2 > 1575 && Vector2.Distance(((ModNPC)this).NPC.Center, player.Center) <= 500f) || (Timer2 >= 2076 && Timer2 <= 2150) || Circling || ((ModNPC)this).NPC.life < ((ModNPC)this).NPC.lifeMax / 5)
		{
			((ModNPC)this).NPC.frame.Y = frameHeight;
		}
		else
		{
			((ModNPC)this).NPC.frame.Y = 0;
		}
	}

	public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		Texture2D texture = ((ModNPC)this).Mod.GetTexture("NPCs/ShadowWorm/Glow/ErebusHeadGlow");
		Rectangle value = new Rectangle(0, ((ModNPC)this).NPC.frame.Y, texture.Width, texture.Height / Main.npcFrameCount[((ModNPC)this).NPC.type]);
		Vector2 origin = new Vector2((float)texture.Width * 0.5f, (float)texture.Height * 0.3f);
		SpriteEffects effects = SpriteEffects.None;
		spriteBatch.Draw(texture, ((ModNPC)this).NPC.Center - Main.screenPosition + new Vector2(0f, ((ModNPC)this).NPC.gfxOffY), value, new Color(255, 255, 255, 0), ((ModNPC)this).NPC.rotation, origin, ((ModNPC)this).NPC.scale, effects, 0f);
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		Texture2D texture2D = TextureAssets.Npc[((ModNPC)this).NPC.type].Value;
		Rectangle value = new Rectangle(0, ((ModNPC)this).NPC.frame.Y, texture2D.Width, texture2D.Height / Main.npcFrameCount[((ModNPC)this).NPC.type]);
		Vector2 origin = new Vector2((float)texture2D.Width * 0.5f, (float)texture2D.Height * 0.3f);
		Main.spriteBatch.Draw(texture2D, ((ModNPC)this).NPC.Center - Main.screenPosition, value, drawColor, ((ModNPC)this).NPC.rotation, origin, ((ModNPC)this).NPC.scale, SpriteEffects.None, 0f);
		return false;
	}

	public override void ModifyHitByItem(Player player, Item item, ref NPC.HitModifiers modifiers)
	{
		if (noDamageTime > 0)
		{
			damage = 0;
		}
	}

	public override void ModifyHitByProjectile(Projectile projectile, ref NPC.HitModifiers modifiers)
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
		Gore.NewGore(((ModNPC)this).NPC.position, ((ModNPC)this).NPC.velocity, ((ModNPC)this).Mod.GetGoreSlot("Gores/ErebusHeadGore1"));
		Gore.NewGore(((ModNPC)this).NPC.position, ((ModNPC)this).NPC.velocity, ((ModNPC)this).Mod.GetGoreSlot("Gores/ErebusHeadGore2"));
		return true;
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		if (!((ModNPC)this).NPC.immortal)
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
			((ModNPC)this).NPC.defense = 100000;
		}
		else
		{
			((ModNPC)this).NPC.defense = 90;
		}
		Player player = Main.player[((ModNPC)this).NPC.target];
		int num = (Main.expertMode ? 48 : 65);
		((ModNPC)this).NPC.rotation = (float)Math.Atan2(((ModNPC)this).NPC.velocity.Y, ((ModNPC)this).NPC.velocity.X) + 1.57f;
		if (Main.player[((ModNPC)this).NPC.target].dead || Main.dayTime)
		{
			((ModNPC)this).NPC.velocity.Y = 80f;
			((ModNPC)this).NPC.ai[3] += 1f;
			if (((ModNPC)this).NPC.ai[3] >= 120f)
			{
				((Entity)((ModNPC)this).NPC).active = false;
			}
		}
		if (NPC.AnyNPCs(((ModNPC)this).Mod.Find<ModNPC>("RestlessSoul").Type) || player.ownedProjectileCounts[((ModNPC)this).Mod.Find<ModProjectile>("ExpandingVortex").Type] > 0)
		{
			((ModNPC)this).NPC.immortal = true;
			((ModNPC)this).NPC.dontTakeDamage = true;
		}
		else
		{
			((ModNPC)this).NPC.immortal = false;
			((ModNPC)this).NPC.dontTakeDamage = false;
		}
		if (player.ownedProjectileCounts[((ModNPC)this).Mod.Find<ModProjectile>("ExpandingVortex").Type] > 0 && ExpandingVortex.Timer < 550)
		{
			TeleportVortex = true;
			if (ExpandingVortex.Timer < 500)
			{
				((ModNPC)this).NPC.scale = 0.0001f;
			}
			else
			{
				((ModNPC)this).NPC.scale = 1f;
			}
		}
		else
		{
			TeleportVortex = false;
			((ModNPC)this).NPC.scale = 1f;
		}
		if ((double)((ModNPC)this).NPC.life <= (double)((ModNPC)this).NPC.lifeMax * 0.75 && Vortex == 0 && ((Entity)((ModNPC)this).NPC).active)
		{
			Vector2 vector = Main.player[((ModNPC)this).NPC.target].Center - ((ModNPC)this).NPC.Center;
			vector.Normalize();
			vector.X *= 10.5f;
			vector.Y *= 10.5f;
			SoundEngine.PlaySound(SoundID.Item84, ((ModNPC)this).NPC.position);
			Projectile.NewProjectile(((ModNPC)this).NPC.Center.X, ((ModNPC)this).NPC.Center.Y, vector.X, vector.Y, ((ModNPC)this).Mod.Find<ModProjectile>("ExpandingVortex").Type, num, 1f, Main.myPlayer, 0f, 0f);
			Vortex++;
		}
		if ((double)((ModNPC)this).NPC.life <= (double)((ModNPC)this).NPC.lifeMax * 0.5 && Vortex == 1 && ((Entity)((ModNPC)this).NPC).active)
		{
			Vector2 vector2 = Main.player[((ModNPC)this).NPC.target].Center - ((ModNPC)this).NPC.Center;
			vector2.Normalize();
			vector2.X *= 10.5f;
			vector2.Y *= 10.5f;
			SoundEngine.PlaySound(SoundID.Item84, ((ModNPC)this).NPC.position);
			Projectile.NewProjectile(((ModNPC)this).NPC.Center.X, ((ModNPC)this).NPC.Center.Y, vector2.X, vector2.Y, ((ModNPC)this).Mod.Find<ModProjectile>("ExpandingVortex").Type, num, 1f, Main.myPlayer, 0f, 0f);
			Vortex++;
		}
		if ((double)((ModNPC)this).NPC.life <= (double)((ModNPC)this).NPC.lifeMax * 0.25 && Vortex == 2 && ((Entity)((ModNPC)this).NPC).active)
		{
			Vector2 vector3 = Main.player[((ModNPC)this).NPC.target].Center - ((ModNPC)this).NPC.Center;
			vector3.Normalize();
			vector3.X *= 10.5f;
			vector3.Y *= 10.5f;
			SoundEngine.PlaySound(SoundID.Item84, ((ModNPC)this).NPC.position);
			Projectile.NewProjectile(((ModNPC)this).NPC.Center.X, ((ModNPC)this).NPC.Center.Y, vector3.X, vector3.Y, ((ModNPC)this).Mod.Find<ModProjectile>("ExpandingVortex").Type, num, 1f, Main.myPlayer, 0f, 0f);
			Vortex++;
		}
		if (((ModNPC)this).NPC.life > ((ModNPC)this).NPC.lifeMax / 2 && ((ModNPC)this).NPC.life > ((ModNPC)this).NPC.lifeMax / 5 && !Circling && !TeleportVortex)
		{
			Timer1++;
			if (Timer1 == 250 || Timer1 == 370 || Timer1 == 490)
			{
				Vector2 vector4 = Main.player[((ModNPC)this).NPC.target].Center - ((ModNPC)this).NPC.Center;
				vector4.Normalize();
				vector4.X *= 18.5f;
				vector4.Y *= 18.5f;
				int num2 = Main.rand.Next(3, 5);
				for (int i = 0; i < num2; i++)
				{
					float num3 = (float)Main.rand.Next(-300, 300) * 0.01f;
					Projectile.NewProjectile(((ModNPC)this).NPC.Center.X, ((ModNPC)this).NPC.Center.Y, vector4.X + num3, vector4.Y + num3, ((ModNPC)this).Mod.Find<ModProjectile>("ErebusBlast").Type, num, 1f, Main.myPlayer, 0f, 0f);
				}
			}
			if (Timer1 < 790 && Timer1 > 670)
			{
				((ModNPC)this).NPC.velocity *= 0.9f;
				int num4 = 36;
				for (int j = 0; j < num4; j++)
				{
					Vector2 vector5 = (Vector2.One * new Vector2((float)((ModNPC)this).NPC.width / 7f, (float)((ModNPC)this).NPC.height / 7f) * 0.75f * 0.5f).RotatedBy((float)(j - (num4 / 2 - 1)) * ((float)Math.PI * 2f) / (float)num4) + ((ModNPC)this).NPC.Center;
					Vector2 vector6 = vector5 - ((ModNPC)this).NPC.Center;
					Dust obj = Main.dust[Dust.NewDust(vector5 + vector6, 0, 0, 89, vector6.X * 2f, vector6.Y * 2f, 100, default(Color), 1.4f)];
					obj.noGravity = true;
					obj.noLight = false;
					obj.velocity = Vector2.Normalize(vector6) * 3f;
					obj.fadeIn = 1.3f;
				}
			}
			if (Timer1 >= 790)
			{
				((ModNPC)this).NPC.velocity *= 0.988f;
			}
			else
			{
				((ModNPC)this).NPC.velocity *= 1f;
			}
			if (Timer1 == 790)
			{
				SoundEngine.PlaySound(((ModNPC)this).Mod.GetLegacySoundSlot((SoundType)50, "Sounds/ErebusRoar")?.WithVolume(1f), -1, -1);
			}
			if (Timer1 == 790 || Timer1 == 865 || Timer1 == 940)
			{
				int num5 = 24;
				for (int k = 0; k < num5; k++)
				{
					Vector2 vector7 = (Vector2.One * new Vector2((float)((ModNPC)this).NPC.width / 7f, (float)((ModNPC)this).NPC.height / 7f) * 0.75f * 0.5f).RotatedBy((float)(k - (num5 / 2 - 1)) * ((float)Math.PI * 2f) / (float)num5) + ((ModNPC)this).NPC.Center;
					Vector2 vector8 = vector7 - ((ModNPC)this).NPC.Center;
					Dust obj2 = Main.dust[Dust.NewDust(vector7 + vector8, 0, 0, 89, vector8.X * 2f, vector8.Y * 2f, 100, default(Color), 1.4f)];
					obj2.noGravity = true;
					obj2.noLight = false;
					obj2.velocity = Vector2.Normalize(vector8) * 6f;
					obj2.fadeIn = 1.3f;
				}
				Vector2 vector9 = Main.player[((ModNPC)this).NPC.target].Center - ((ModNPC)this).NPC.Center;
				vector9.Normalize();
				vector9.X *= 50f;
				vector9.Y *= 50f;
				((ModNPC)this).NPC.velocity.X = vector9.X;
				((ModNPC)this).NPC.velocity.Y = vector9.Y;
			}
			if (Timer1 >= 1015)
			{
				Timer1 = 0;
				MovementAI = 0;
			}
		}
		if (((ModNPC)this).NPC.life <= ((ModNPC)this).NPC.lifeMax / 2 && ((ModNPC)this).NPC.life > ((ModNPC)this).NPC.lifeMax / 5 && !Circling && !TeleportVortex)
		{
			Timer1 = 0;
			Timer2++;
			if (Timer2 >= 300 && Timer2 <= 500 && Main.rand.Next(20) == 0)
			{
				Projectile.NewProjectile(player.Center + Main.rand.NextVector2Square(-750f, 750f), Main.rand.NextVector2Square(-1f, 1f), ((ModNPC)this).Mod.Find<ModProjectile>("EldritchEyeTelegraph").Type, 0, 6f, player.whoAmI, 0f, 0f);
			}
			if (Timer2 > 820 && Timer2 < 960)
			{
				((ModNPC)this).NPC.velocity *= 0.9f;
				int num6 = 36;
				for (int l = 0; l < num6; l++)
				{
					Vector2 vector10 = (Vector2.One * new Vector2((float)((ModNPC)this).NPC.width / 7f, (float)((ModNPC)this).NPC.height / 7f) * 0.75f * 0.5f).RotatedBy((float)(l - (num6 / 2 - 1)) * ((float)Math.PI * 2f) / (float)num6) + ((ModNPC)this).NPC.Center;
					Vector2 vector11 = vector10 - ((ModNPC)this).NPC.Center;
					Dust obj3 = Main.dust[Dust.NewDust(vector10 + vector11, 0, 0, ((ModNPC)this).Mod.Find<ModDust>("ShadowDustPurple").Type, vector11.X * 2f, vector11.Y * 2f, 100, default(Color), 1.4f)];
					obj3.noGravity = true;
					obj3.noLight = false;
					obj3.velocity = Vector2.Normalize(vector11) * 3f;
					obj3.fadeIn = 1.3f;
				}
			}
			if (Timer2 == 900)
			{
				SoundEngine.PlaySound(((ModNPC)this).Mod.GetLegacySoundSlot((SoundType)50, "Sounds/ErebusRoar")?.WithVolume(1f), -1, -1);
				Vector2 vector12 = Main.player[((ModNPC)this).NPC.target].Center - ((ModNPC)this).NPC.Center;
				vector12.Normalize();
				vector12.X *= 12.5f;
				vector12.Y *= 12.5f;
				SoundEngine.PlaySound(SoundID.Item84, ((ModNPC)this).NPC.position);
				Projectile.NewProjectile(((ModNPC)this).NPC.Center.X, ((ModNPC)this).NPC.Center.Y, vector12.X, vector12.Y, ((ModNPC)this).Mod.Find<ModProjectile>("ErebusVortex").Type, num, 1f, Main.myPlayer, 0f, 0f);
			}
			if (Timer2 == 1275)
			{
				MovementAI = 1;
			}
			if (Timer2 == 1270 && ((ModNPC)this).NPC.life <= ((ModNPC)this).NPC.lifeMax / 3)
			{
				Circling = true;
			}
			if (Timer2 == 1335 || Timer2 == 1395 || Timer2 == 1455 || Timer2 == 1515 || Timer2 == 1575)
			{
				SoundEngine.PlaySound(SoundID.NPCDeath13, ((ModNPC)this).NPC.position);
				float num7 = 2f;
				int num8 = ((ModNPC)this).Mod.Find<ModProjectile>("ErebusToothBall").Type;
				SoundEngine.PlaySound(SoundID.Item20, new Vector2(((ModNPC)this).NPC.position.X, ((ModNPC)this).NPC.position.Y));
				float num9 = (float)Math.Atan2(((ModNPC)this).NPC.Center.Y - player.Center.Y, ((ModNPC)this).NPC.Center.X - player.Center.X);
				Projectile.NewProjectile(((ModNPC)this).NPC.Center.X, ((ModNPC)this).NPC.Center.Y, (float)(Math.Cos(num9) * (double)num7 * -1.0), (float)(Math.Sin(num9) * (double)num7 * -1.0), num8, num, 0f, Main.myPlayer, 0f, 0f);
			}
			if (Vector2.Distance(((ModNPC)this).NPC.Center, player.Center) <= 500f && MovementAI == 1 && Timer2 >= 1575)
			{
				Vector2 vector13 = new Vector2(((ModNPC)this).NPC.position.X + (float)(((ModNPC)this).NPC.width / 2), ((ModNPC)this).NPC.position.Y + (float)(((ModNPC)this).NPC.height / 2));
				if (Main.rand.Next(5) == 0)
				{
					Projectile.NewProjectile(vector13.X + ((ModNPC)this).NPC.velocity.X, vector13.Y + ((ModNPC)this).NPC.velocity.Y, ((ModNPC)this).NPC.velocity.X * 0.5f + Utils.NextFloat(Main.rand, -0.6f, 0.6f) * 1f, ((ModNPC)this).NPC.velocity.Y * 0.5f + Utils.NextFloat(Main.rand, -0.6f, 0.6f) * 1f, ((ModNPC)this).Mod.Find<ModProjectile>("ShadowFlameBreath").Type, num, 0f, 0, 0f, 0f);
					if (Main.rand.Next(3) == 0)
					{
						SoundEngine.PlaySound(SoundID.DD2_BetsyFlameBreath, ((ModNPC)this).NPC.position);
					}
				}
			}
			if (Timer2 >= 1995 && Timer2 < 2075)
			{
				int num10 = 36;
				for (int m = 0; m < num10; m++)
				{
					Vector2 vector14 = (Vector2.One * new Vector2((float)((ModNPC)this).NPC.width / 7f, (float)((ModNPC)this).NPC.height / 7f) * 0.75f * 0.5f).RotatedBy((float)(m - (num10 / 2 - 1)) * ((float)Math.PI * 2f) / (float)num10) + ((ModNPC)this).NPC.Center;
					Vector2 vector15 = vector14 - ((ModNPC)this).NPC.Center;
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
					int num11 = Dust.NewDust(((ModNPC)this).NPC.position, ((ModNPC)this).NPC.width, ((ModNPC)this).NPC.height, 89, 0f, -2f, 0, default(Color), 1.5f);
					Main.dust[num11].noGravity = false;
					Main.dust[num11].scale = 3.5f;
					Main.dust[num11].position.X += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
					Main.dust[num11].position.Y += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
					if (Main.dust[num11].position != ((ModNPC)this).NPC.Center)
					{
						Main.dust[num11].velocity = ((ModNPC)this).NPC.DirectionTo(Main.dust[num11].position) * 10f;
					}
				}
				((ModNPC)this).NPC.position.X = player.position.X - 100f;
				((ModNPC)this).NPC.position.Y = player.position.Y + 5000f;
				for (int num12 = 0; num12 < 200; num12++)
				{
					if (((Entity)Main.npc[num12]).active && (Main.npc[num12].type == ModContent.NPCType<ErebusBody>() || Main.npc[num12].type == ModContent.NPCType<ErebusTail>()))
					{
						Main.npc[num12].Center = ((ModNPC)this).NPC.Center;
					}
				}
				MovementAI = 0;
			}
			if (Timer2 == 2076)
			{
				Vector2 vector16 = Main.player[((ModNPC)this).NPC.target].Center - ((ModNPC)this).NPC.Center;
				vector16.Normalize();
				vector16.X *= 65f;
				vector16.Y *= 65f;
				((ModNPC)this).NPC.velocity.X = vector16.X;
				((ModNPC)this).NPC.velocity.Y = vector16.Y;
			}
			if (Timer2 >= 2076 && Timer2 <= 2226 && Main.rand.Next(3) == 0)
			{
				for (int num13 = 0; num13 < 2; num13++)
				{
					Vector2 vector17 = new Vector2(0f, 1f).RotatedBy(((ModNPC)this).NPC.rotation + MathHelper.ToRadians((num13 == 0) ? 90 : 270));
					vector17.Normalize();
					vector17 *= 10f;
					_ = Main.projectile[Projectile.NewProjectile(((ModNPC)this).NPC.Center, vector17, ((ModNPC)this).Mod.Find<ModProjectile>("EldritchBlast").Type, num, 0f, Main.myPlayer, 0f, 0f)];
				}
			}
			if (Timer2 == 2226)
			{
				Timer2 = 0;
				MovementAI = 0;
				((ModNPC)this).NPC.velocity *= 0f;
			}
		}
		if (((ModNPC)this).NPC.life < ((ModNPC)this).NPC.lifeMax / 5 && !Circling && !TeleportVortex)
		{
			Timer1 = 0;
			Timer2 = 0;
			((ModNPC)this).NPC.velocity *= 0.988f;
			Timer3++;
			if (Timer3 < 100)
			{
				((ModNPC)this).NPC.velocity *= 0.9f;
				int num14 = 36;
				for (int num15 = 0; num15 < num14; num15++)
				{
					Vector2 vector18 = (Vector2.One * new Vector2((float)((ModNPC)this).NPC.width / 7f, (float)((ModNPC)this).NPC.height / 7f) * 0.75f * 0.5f).RotatedBy((float)(num15 - (num14 / 2 - 1)) * ((float)Math.PI * 2f) / (float)num14) + ((ModNPC)this).NPC.Center;
					Vector2 vector19 = vector18 - ((ModNPC)this).NPC.Center;
					Dust obj5 = Main.dust[Dust.NewDust(vector18 + vector19, 0, 0, 89, vector19.X * 2f, vector19.Y * 2f, 100, default(Color), 1.4f)];
					obj5.noGravity = true;
					obj5.noLight = false;
					obj5.velocity = Vector2.Normalize(vector19) * 3f;
					obj5.fadeIn = 1.3f;
				}
			}
			if (Timer3 == 100 || Timer3 == 160 || Timer3 == 220 || Timer3 == 280 || Timer3 == 340)
			{
				SoundEngine.PlaySound(((ModNPC)this).Mod.GetLegacySoundSlot((SoundType)50, "Sounds/ErebusCharge")?.WithVolume(1f), -1, -1);
				int num16 = 24;
				for (int num17 = 0; num17 < num16; num17++)
				{
					Vector2 vector20 = (Vector2.One * new Vector2((float)((ModNPC)this).NPC.width / 7f, (float)((ModNPC)this).NPC.height / 7f) * 0.75f * 0.5f).RotatedBy((float)(num17 - (num16 / 2 - 1)) * ((float)Math.PI * 2f) / (float)num16) + ((ModNPC)this).NPC.Center;
					Vector2 vector21 = vector20 - ((ModNPC)this).NPC.Center;
					Dust obj6 = Main.dust[Dust.NewDust(vector20 + vector21, 0, 0, 89, vector21.X * 2f, vector21.Y * 2f, 100, default(Color), 1.4f)];
					obj6.noGravity = true;
					obj6.noLight = false;
					obj6.velocity = Vector2.Normalize(vector21) * 6f;
					obj6.fadeIn = 1.3f;
				}
				Vector2 vector22 = Main.player[((ModNPC)this).NPC.target].Center - ((ModNPC)this).NPC.Center;
				vector22.Normalize();
				vector22.X *= 50f;
				vector22.Y *= 50f;
				((ModNPC)this).NPC.velocity.X = vector22.X;
				((ModNPC)this).NPC.velocity.Y = vector22.Y;
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
				SoundEngine.PlaySound(((ModNPC)this).Mod.GetLegacySoundSlot((SoundType)50, "Sounds/ErebusRoar")?.WithVolume(1f), -1, -1);
				Vector2 vector23 = Main.player[((ModNPC)this).NPC.target].Center - ((ModNPC)this).NPC.Center;
				vector23.Normalize();
				vector23.X *= 50f;
				vector23.Y *= 50f;
				((ModNPC)this).NPC.velocity.X = vector23.X;
				((ModNPC)this).NPC.velocity.Y = vector23.Y;
			}
		}
		if (((ModNPC)this).NPC.localAI[1] == 0f && ((ModNPC)this).NPC.life <= ((ModNPC)this).NPC.lifeMax / 3)
		{
			int num18 = 5;
			for (int num19 = 0; num19 < num18; num19++)
			{
				int num20 = 360 / num18;
				NPC.NewNPC((int)((ModNPC)this).NPC.Center.X, (int)((ModNPC)this).NPC.Center.Y, ModContent.NPCType<RestlessSoul>(), ((ModNPC)this).NPC.whoAmI, (float)((ModNPC)this).NPC.whoAmI, (float)(num19 * num20), 0f, 0f, 255);
			}
			((ModNPC)this).NPC.localAI[1] += 1f;
		}
		if (Circling && !TeleportVortex)
		{
			((ModNPC)this).NPC.velocity = new Vector2(((ModNPC)this).NPC.velocity.X, ((ModNPC)this).NPC.velocity.Y).RotatedBy(MathHelper.ToRadians(Spin - 30));
			((ModNPC)this).NPC.TargetClosest(faceTarget: false);
			CircleTimer++;
			rotate += 2f;
			if (CircleTimer < 580)
			{
				Vector2 vector24 = new Vector2(2000f, 0f).RotatedBy(MathHelper.ToRadians(rotate * 1.57f));
				SpinX = player.Center.X + vector24.X - ((ModNPC)this).NPC.Center.X;
				SpinY = player.Center.Y + vector24.Y - ((ModNPC)this).NPC.Center.Y;
				float num21 = (float)Math.Sqrt(SpinX * SpinX + SpinY * SpinY);
				if (num21 > 52f)
				{
					num21 = 7.5f / num21;
					SpinX *= num21 * 7f;
					SpinY *= num21 * 7f;
					((ModNPC)this).NPC.velocity.X = SpinX;
					((ModNPC)this).NPC.velocity.Y = SpinY;
				}
				else
				{
					((ModNPC)this).NPC.position.X = player.Center.X + vector24.X - (float)(((ModNPC)this).NPC.height / 2);
					((ModNPC)this).NPC.position.Y = player.Center.Y + vector24.Y - (float)(((ModNPC)this).NPC.width / 2);
					num21 = 7f / num21;
					SpinX *= num21 * 7f;
					SpinY *= num21 * 7f;
					((ModNPC)this).NPC.velocity.X = 0f;
					((ModNPC)this).NPC.velocity.Y = 0f;
					((ModNPC)this).NPC.rotation = (float)Math.Atan2(((ModNPC)this).NPC.velocity.X, ((ModNPC)this).NPC.velocity.Y) + 1.57f;
				}
				CircleShootTimer++;
				if (CircleShootTimer == 60)
				{
					for (int num22 = -1; num22 <= 2; num22++)
					{
						Projectile.NewProjectile(((ModNPC)this).NPC.Center, 15f * ((ModNPC)this).NPC.DirectionTo(player.Center).RotatedBy(MathHelper.ToRadians(8f) * (float)num22), ((ModNPC)this).Mod.Find<ModProjectile>("ErebusBlast").Type, num, 0f, Main.myPlayer, 0f, 0f);
					}
				}
				if (CircleShootTimer == 70)
				{
					CircleShootTimer = 0;
				}
			}
			else
			{
				((ModNPC)this).NPC.velocity *= 0f;
			}
			if (CircleTimer == 590)
			{
				Circling = false;
				CircleTimer = 0;
				CircleShootTimer = 0;
			}
		}
		if (Main.netMode != 1 && ((ModNPC)this).NPC.ai[0] == 0f)
		{
			((ModNPC)this).NPC.realLife = ((ModNPC)this).NPC.whoAmI;
			int num23 = ((ModNPC)this).NPC.whoAmI;
			int num24 = 26;
			for (int num25 = 1; num25 < num24; num25++)
			{
				int num26 = 0;
				num26 = ((ModNPC)this).Mod.Find<ModNPC>("ErebusBody").Type;
				num23 = NPC.NewNPC((int)((ModNPC)this).NPC.Center.X, (int)((ModNPC)this).NPC.Center.Y, num26, ((ModNPC)this).NPC.whoAmI, 0f, (float)num23, 0f, 0f, 255);
				Main.npc[num23].realLife = ((ModNPC)this).NPC.whoAmI;
				Main.npc[num23].ai[3] = ((ModNPC)this).NPC.whoAmI;
			}
			num23 = NPC.NewNPC((int)((ModNPC)this).NPC.Center.X, (int)((ModNPC)this).NPC.Center.Y, ((ModNPC)this).Mod.Find<ModNPC>("ErebusTail").Type, ((ModNPC)this).NPC.whoAmI, 0f, (float)num23, 0f, 0f, 255);
			Main.npc[num23].realLife = ((ModNPC)this).NPC.whoAmI;
			Main.npc[num23].ai[3] = ((ModNPC)this).NPC.whoAmI;
			((ModNPC)this).NPC.ai[0] = 1f;
			((ModNPC)this).NPC.netUpdate = true;
		}
		if (!Circling && Timer2 < 2075 && Timer1 < 790 && ((ModNPC)this).NPC.life > ((ModNPC)this).NPC.lifeMax / 5 && !TeleportVortex)
		{
			int num27 = (int)((double)((ModNPC)this).NPC.position.X / 16.0) - 1;
			int num28 = (int)((double)(((ModNPC)this).NPC.position.X + (float)((ModNPC)this).NPC.width) / 16.0) + 2;
			int num29 = (int)((double)((ModNPC)this).NPC.position.Y / 16.0) - 1;
			int num30 = (int)((double)(((ModNPC)this).NPC.position.Y + (float)((ModNPC)this).NPC.height) / 16.0) + 2;
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
					if (Main.tile[num31, num32] == null || ((!Main.tile[num31, num32].HasUnactuatedTile || (!Main.tileSolid[Main.tile[num31, num32].TileType] && Main.tile[num31, num32].TileFrameY != 0)) && Main.tile[num31, num32].LiquidAmount <= 64))
					{
						continue;
					}
					vector25.X = num31 * 16;
					vector25.Y = num32 * 16;
					if (((ModNPC)this).NPC.position.X + (float)((ModNPC)this).NPC.width > vector25.X && (double)((ModNPC)this).NPC.position.X < (double)vector25.X + 16.0 && (double)(((ModNPC)this).NPC.position.Y + (float)((ModNPC)this).NPC.height) > (double)vector25.Y && (double)((ModNPC)this).NPC.position.Y < (double)vector25.Y + 16.0)
					{
						flag = true;
						Ultranium.seizureAmount = 12f;
						if (Main.rand.Next(100) == 0 && Main.tile[num31, num32].HasUnactuatedTile)
						{
							WorldGen.KillTile(num31, num32, fail: true, effectOnly: true);
						}
					}
				}
			}
			if (!flag)
			{
				Rectangle rectangle = new Rectangle((int)((ModNPC)this).NPC.position.X, (int)((ModNPC)this).NPC.position.Y, ((ModNPC)this).NPC.width, ((ModNPC)this).NPC.height);
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
			Vector2 vector26 = new Vector2(((ModNPC)this).NPC.position.X + (float)((ModNPC)this).NPC.width * 0.5f, ((ModNPC)this).NPC.position.Y + (float)((ModNPC)this).NPC.height * 0.5f);
			float num35 = Main.player[((ModNPC)this).NPC.target].position.X + (float)(Main.player[((ModNPC)this).NPC.target].width / 2);
			float num36 = Main.player[((ModNPC)this).NPC.target].position.Y + (float)(Main.player[((ModNPC)this).NPC.target].height / 2);
			float num37 = (int)((double)num35 / 16.0) * 16;
			float num38 = (int)((double)num36 / 16.0) * 16;
			vector26.X = (int)((double)vector26.X / 16.0) * 16;
			vector26.Y = (int)((double)vector26.Y / 16.0) * 16;
			float num39 = num37 - vector26.X;
			float num40 = num38 - vector26.Y;
			float num41 = (float)Math.Sqrt(num39 * num39 + num40 * num40);
			if (!flag)
			{
				((ModNPC)this).NPC.TargetClosest();
				((ModNPC)this).NPC.velocity.Y = ((ModNPC)this).NPC.velocity.Y + 0.22f;
				if (((ModNPC)this).NPC.velocity.Y > speed)
				{
					((ModNPC)this).NPC.velocity.Y = speed;
				}
				if ((double)(Math.Abs(((ModNPC)this).NPC.velocity.X) + Math.Abs(((ModNPC)this).NPC.velocity.Y)) < (double)speed * 0.4)
				{
					if ((double)((ModNPC)this).NPC.velocity.X < 0.0)
					{
						((ModNPC)this).NPC.velocity.X = ((ModNPC)this).NPC.velocity.X - acceleration * 1.1f;
					}
					else
					{
						((ModNPC)this).NPC.velocity.X = ((ModNPC)this).NPC.velocity.X + acceleration * 1.1f;
					}
				}
				else if (((ModNPC)this).NPC.velocity.Y == speed)
				{
					if (((ModNPC)this).NPC.velocity.X < num39)
					{
						((ModNPC)this).NPC.velocity.X = ((ModNPC)this).NPC.velocity.X + acceleration;
					}
					else if (((ModNPC)this).NPC.velocity.X > num39)
					{
						((ModNPC)this).NPC.velocity.X = ((ModNPC)this).NPC.velocity.X - acceleration;
					}
				}
				else if ((double)((ModNPC)this).NPC.velocity.Y > 4.0)
				{
					if ((double)((ModNPC)this).NPC.velocity.X < 0.0)
					{
						((ModNPC)this).NPC.velocity.X = ((ModNPC)this).NPC.velocity.X + acceleration * 0.9f;
					}
					else
					{
						((ModNPC)this).NPC.velocity.X = ((ModNPC)this).NPC.velocity.X - acceleration * 0.9f;
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
				if (((double)((ModNPC)this).NPC.velocity.X > 0.0 && (double)num39 > 0.0) || ((double)((ModNPC)this).NPC.velocity.X < 0.0 && (double)num39 < 0.0) || ((double)((ModNPC)this).NPC.velocity.Y > 0.0 && (double)num40 > 0.0) || ((double)((ModNPC)this).NPC.velocity.Y < 0.0 && (double)num40 < 0.0))
				{
					if (((ModNPC)this).NPC.velocity.X < num39)
					{
						((ModNPC)this).NPC.velocity.X = ((ModNPC)this).NPC.velocity.X + acceleration;
					}
					else if (((ModNPC)this).NPC.velocity.X > num39)
					{
						((ModNPC)this).NPC.velocity.X = ((ModNPC)this).NPC.velocity.X - acceleration;
					}
					if (((ModNPC)this).NPC.velocity.Y < num40)
					{
						((ModNPC)this).NPC.velocity.Y = ((ModNPC)this).NPC.velocity.Y + acceleration;
					}
					else if (((ModNPC)this).NPC.velocity.Y > num40)
					{
						((ModNPC)this).NPC.velocity.Y = ((ModNPC)this).NPC.velocity.Y - acceleration;
					}
					if ((double)Math.Abs(num40) < (double)speed * 0.2 && (((double)((ModNPC)this).NPC.velocity.X > 0.0 && (double)num39 < 0.0) || ((double)((ModNPC)this).NPC.velocity.X < 0.0 && (double)num39 > 0.0)))
					{
						if ((double)((ModNPC)this).NPC.velocity.Y > 0.0)
						{
							((ModNPC)this).NPC.velocity.Y = ((ModNPC)this).NPC.velocity.Y + acceleration * 2f;
						}
						else
						{
							((ModNPC)this).NPC.velocity.Y = ((ModNPC)this).NPC.velocity.Y - acceleration * 2f;
						}
					}
					if ((double)Math.Abs(num39) < (double)speed * 0.2 && (((double)((ModNPC)this).NPC.velocity.Y > 0.0 && (double)num40 < 0.0) || ((double)((ModNPC)this).NPC.velocity.Y < 0.0 && (double)num40 > 0.0)))
					{
						if ((double)((ModNPC)this).NPC.velocity.X > 0.0)
						{
							((ModNPC)this).NPC.velocity.X = ((ModNPC)this).NPC.velocity.X + acceleration * 2f;
						}
						else
						{
							((ModNPC)this).NPC.velocity.X = ((ModNPC)this).NPC.velocity.X - acceleration * 2f;
						}
					}
				}
				else if (num42 > num43)
				{
					if (((ModNPC)this).NPC.velocity.X < num39)
					{
						((ModNPC)this).NPC.velocity.X = ((ModNPC)this).NPC.velocity.X + acceleration * 1.1f;
					}
					else if (((ModNPC)this).NPC.velocity.X > num39)
					{
						((ModNPC)this).NPC.velocity.X = ((ModNPC)this).NPC.velocity.X - acceleration * 1.1f;
					}
					if ((double)(Math.Abs(((ModNPC)this).NPC.velocity.X) + Math.Abs(((ModNPC)this).NPC.velocity.Y)) < (double)speed * 0.5)
					{
						if ((double)((ModNPC)this).NPC.velocity.Y > 0.0)
						{
							((ModNPC)this).NPC.velocity.Y = ((ModNPC)this).NPC.velocity.Y + acceleration;
						}
						else
						{
							((ModNPC)this).NPC.velocity.Y = ((ModNPC)this).NPC.velocity.Y - acceleration;
						}
					}
				}
				else
				{
					if (((ModNPC)this).NPC.velocity.Y < num40)
					{
						((ModNPC)this).NPC.velocity.Y = ((ModNPC)this).NPC.velocity.Y + acceleration * 1.1f;
					}
					else if (((ModNPC)this).NPC.velocity.Y > num40)
					{
						((ModNPC)this).NPC.velocity.Y = ((ModNPC)this).NPC.velocity.Y - acceleration * 1.1f;
					}
					if ((double)(Math.Abs(((ModNPC)this).NPC.velocity.X) + Math.Abs(((ModNPC)this).NPC.velocity.Y)) < (double)speed * 0.5)
					{
						if ((double)((ModNPC)this).NPC.velocity.X > 0.0)
						{
							((ModNPC)this).NPC.velocity.X = ((ModNPC)this).NPC.velocity.X + acceleration;
						}
						else
						{
							((ModNPC)this).NPC.velocity.X = ((ModNPC)this).NPC.velocity.X - acceleration;
						}
					}
				}
			}
			((ModNPC)this).NPC.rotation = (float)Math.Atan2(((ModNPC)this).NPC.velocity.Y, ((ModNPC)this).NPC.velocity.X) + 1.57f;
			if (flag)
			{
				if (((ModNPC)this).NPC.localAI[0] != 1f)
				{
					((ModNPC)this).NPC.netUpdate = true;
				}
				((ModNPC)this).NPC.localAI[0] = 1f;
			}
			else
			{
				if ((double)((ModNPC)this).NPC.localAI[0] != 0.0)
				{
					((ModNPC)this).NPC.netUpdate = true;
				}
				((ModNPC)this).NPC.localAI[0] = 0f;
			}
			if ((((double)((ModNPC)this).NPC.velocity.X > 0.0 && (double)((ModNPC)this).NPC.oldVelocity.X < 0.0) || ((double)((ModNPC)this).NPC.velocity.X < 0.0 && (double)((ModNPC)this).NPC.oldVelocity.X > 0.0) || ((double)((ModNPC)this).NPC.velocity.Y > 0.0 && (double)((ModNPC)this).NPC.oldVelocity.Y < 0.0) || ((double)((ModNPC)this).NPC.velocity.Y < 0.0 && (double)((ModNPC)this).NPC.oldVelocity.Y > 0.0)) && !((ModNPC)this).NPC.justHit)
			{
				((ModNPC)this).NPC.netUpdate = true;
			}
		}
		if (!((Entity)((ModNPC)this).NPC).active)
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
		if (((ModNPC)this).NPC.Center.X < targetPos.X)
		{
			((ModNPC)this).NPC.velocity.X += speedModifier;
			if (((ModNPC)this).NPC.velocity.X < 0f)
			{
				((ModNPC)this).NPC.velocity.X += speedModifier * 2f;
			}
		}
		else
		{
			((ModNPC)this).NPC.velocity.X -= speedModifier;
			if (((ModNPC)this).NPC.velocity.X > 0f)
			{
				((ModNPC)this).NPC.velocity.X -= speedModifier * 2f;
			}
		}
		if (((ModNPC)this).NPC.Center.Y < targetPos.Y)
		{
			((ModNPC)this).NPC.velocity.Y += (fastY ? (speedModifier * 2f) : speedModifier);
			if (((ModNPC)this).NPC.velocity.Y < 0f)
			{
				((ModNPC)this).NPC.velocity.Y += speedModifier * 2f;
			}
		}
		else
		{
			((ModNPC)this).NPC.velocity.Y -= (fastY ? (speedModifier * 2f) : speedModifier);
			if (((ModNPC)this).NPC.velocity.Y > 0f)
			{
				((ModNPC)this).NPC.velocity.Y -= speedModifier * 2f;
			}
		}
		if (Math.Abs(((ModNPC)this).NPC.velocity.X) > cap)
		{
			((ModNPC)this).NPC.velocity.X = cap * (float)Math.Sign(((ModNPC)this).NPC.velocity.X);
		}
		if (Math.Abs(((ModNPC)this).NPC.velocity.Y) > cap)
		{
			((ModNPC)this).NPC.velocity.Y = cap * (float)Math.Sign(((ModNPC)this).NPC.velocity.Y);
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
			int num = Main.rand.Next(9);
			if (num == 0)
			{
				Item.NewItem((int)((ModNPC)this).NPC.position.X, (int)((ModNPC)this).NPC.position.Y, ((ModNPC)this).NPC.width, ((ModNPC)this).NPC.height, ((ModNPC)this).Mod.Find<ModItem>("Noctis").Type, 1, false, 0, false, false);
			}
			if (num == 1)
			{
				Item.NewItem((int)((ModNPC)this).NPC.position.X, (int)((ModNPC)this).NPC.position.Y, ((ModNPC)this).NPC.width, ((ModNPC)this).NPC.height, ((ModNPC)this).Mod.Find<ModItem>("SolibusOrba").Type, 1, false, 0, false, false);
			}
			if (num == 2)
			{
				Item.NewItem((int)((ModNPC)this).NPC.position.X, (int)((ModNPC)this).NPC.position.Y, ((ModNPC)this).NPC.width, ((ModNPC)this).NPC.height, ((ModNPC)this).Mod.Find<ModItem>("Crepus").Type, 1, false, 0, false, false);
			}
			if (num == 3)
			{
				Item.NewItem((int)((ModNPC)this).NPC.position.X, (int)((ModNPC)this).NPC.position.Y, ((ModNPC)this).NPC.width, ((ModNPC)this).NPC.height, ((ModNPC)this).Mod.Find<ModItem>("Inanis").Type, 1, false, 0, false, false);
			}
			if (num == 4)
			{
				Item.NewItem((int)((ModNPC)this).NPC.position.X, (int)((ModNPC)this).NPC.position.Y, ((ModNPC)this).NPC.width, ((ModNPC)this).NPC.height, ((ModNPC)this).Mod.Find<ModItem>("CavumNigrum").Type, 1, false, 0, false, false);
			}
			if (num == 5)
			{
				Item.NewItem((int)((ModNPC)this).NPC.position.X, (int)((ModNPC)this).NPC.position.Y, ((ModNPC)this).NPC.width, ((ModNPC)this).NPC.height, ((ModNPC)this).Mod.Find<ModItem>("Exitium").Type, 1, false, 0, false, false);
			}
			if (num == 6)
			{
				Item.NewItem((int)((ModNPC)this).NPC.position.X, (int)((ModNPC)this).NPC.position.Y, ((ModNPC)this).NPC.width, ((ModNPC)this).NPC.height, ((ModNPC)this).Mod.Find<ModItem>("Umbra").Type, 1, false, 0, false, false);
			}
			if (num == 7)
			{
				Item.NewItem((int)((ModNPC)this).NPC.position.X, (int)((ModNPC)this).NPC.position.Y, ((ModNPC)this).NPC.width, ((ModNPC)this).NPC.height, ((ModNPC)this).Mod.Find<ModItem>("Nihil").Type, 1, false, 0, false, false);
			}
			if (num == 8)
			{
				Item.NewItem((int)((ModNPC)this).NPC.position.X, (int)((ModNPC)this).NPC.position.Y, ((ModNPC)this).NPC.width, ((ModNPC)this).NPC.height, ((ModNPC)this).Mod.Find<ModItem>("Caliginus").Type, 1, false, 0, false, false);
			}
			Item.NewItem((int)((ModNPC)this).NPC.position.X, (int)((ModNPC)this).NPC.position.Y, ((ModNPC)this).NPC.width, ((ModNPC)this).NPC.height, ((ModNPC)this).Mod.Find<ModItem>("NightmareScale").Type, Main.rand.Next(20, 35), false, 0, false, false);
			Item.NewItem((int)((ModNPC)this).NPC.position.X, (int)((ModNPC)this).NPC.position.Y, ((ModNPC)this).NPC.width, ((ModNPC)this).NPC.height, ((ModNPC)this).Mod.Find<ModItem>("DarkMatter").Type, Main.rand.Next(10, 15), false, 0, false, false);
			if (Main.rand.Next(20) == 0)
			{
				Item.NewItem((int)((ModNPC)this).NPC.position.X, (int)((ModNPC)this).NPC.position.Y, ((ModNPC)this).NPC.width, ((ModNPC)this).NPC.height, ((ModNPC)this).Mod.Find<ModItem>("ErebusGuitar").Type, 1, false, 0, false, false);
			}
		}
		if (Main.rand.Next(7) == 0)
		{
			Item.NewItem((int)((ModNPC)this).NPC.position.X, (int)((ModNPC)this).NPC.position.Y, ((ModNPC)this).NPC.width, ((ModNPC)this).NPC.height, ((ModNPC)this).Mod.Find<ModItem>("ErebusMask").Type, 1, false, 0, false, false);
		}
		if (Main.rand.Next(10) == 0)
		{
			Item.NewItem((int)((ModNPC)this).NPC.position.X, (int)((ModNPC)this).NPC.position.Y, ((ModNPC)this).NPC.width, ((ModNPC)this).NPC.height, ((ModNPC)this).Mod.Find<ModItem>("ErebusTrophyItem").Type, 1, false, 0, false, false);
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
