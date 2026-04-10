using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.NPCs.IceDragon;

[AutoloadBossHead]
public class IceDragon : ModNPC
{
	private int MoveSpeed;

	private int MoveSpeedY;

	private float HomeY = 150f;

	private int Timer1;

	private int Timer2;

	private int AttackType;

	private int AttackType2;

	private int FlyDirection;

	private int TransitionTimer;

	public static bool Transition;

	public static bool Phase2;

	public static bool BlizzardEffect;

	public override void SetStaticDefaults()
	{
		((ModNPC)this).DisplayName.SetDefault("Glacieron");
		Main.npcFrameCount[((ModNPC)this).npc.type] = 8;
		NPCID.Sets.TrailCacheLength[((ModNPC)this).npc.type] = 10;
		NPCID.Sets.TrailingMode[((ModNPC)this).npc.type] = 0;
	}

	public override void SetDefaults()
	{
		((ModNPC)this).npc.width = 244;
		((ModNPC)this).npc.height = 244;
		((ModNPC)this).npc.damage = 32;
		((ModNPC)this).npc.defense = 20;
		((ModNPC)this).npc.lifeMax = 5200;
		((ModNPC)this).npc.HitSound = SoundID.NPCHit56;
		((ModNPC)this).npc.DeathSound = SoundID.NPCDeath60;
		((ModNPC)this).npc.value = Item.buyPrice(0, 8);
		((ModNPC)this).npc.knockBackResist = 0f;
		((ModNPC)this).npc.aiStyle = -1;
		((ModNPC)this).npc.boss = true;
		((ModNPC)this).npc.noGravity = true;
		((ModNPC)this).npc.noTileCollide = true;
		base.music = ((ModNPC)this).mod.GetSoundSlot((SoundType)51, "Sounds/Music/IceDragon");
		base.bossBag = ((ModNPC)this).mod.ItemType("IceDragonBag");
	}

	public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
	{
		((ModNPC)this).npc.lifeMax = 6300;
		((ModNPC)this).npc.damage = 42;
		((ModNPC)this).npc.defense = 35;
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
	{
		if (((ModNPC)this).npc.velocity != Vector2.Zero)
		{
			Vector2 vector = new Vector2((float)ModContent.GetTexture("Ultranium/NPCs/IceDragon/IceDragonTrail").Width * 0.45f, (float)((ModNPC)this).npc.height * 0.5f);
			for (int i = 0; i < ((ModNPC)this).npc.oldPos.Length; i++)
			{
				SpriteEffects effects = ((((ModNPC)this).npc.direction != -1) ? SpriteEffects.FlipHorizontally : SpriteEffects.None);
				Vector2 position = ((ModNPC)this).npc.oldPos[i] - Main.screenPosition + vector + new Vector2(0f, ((ModNPC)this).npc.gfxOffY);
				Color color = ((ModNPC)this).npc.GetAlpha(drawColor) * ((float)(((ModNPC)this).npc.oldPos.Length - i) / (float)((ModNPC)this).npc.oldPos.Length / 2f);
				spriteBatch.Draw(ModContent.GetTexture("Ultranium/NPCs/IceDragon/IceDragonTrail"), position, ((ModNPC)this).npc.frame, color, ((ModNPC)this).npc.rotation, vector, ((ModNPC)this).npc.scale, effects, 0f);
			}
		}
		return true;
	}

	public override bool PreAI()
	{
		if (Main.player[((ModNPC)this).npc.target].dead)
		{
			((ModNPC)this).npc.velocity.Y = -30f;
			((ModNPC)this).npc.ai[3] += 1f;
			if (((ModNPC)this).npc.ai[3] >= 120f)
			{
				((Entity)((ModNPC)this).npc).active = false;
			}
		}
		((ModNPC)this).npc.rotation = ((ModNPC)this).npc.velocity.X * 0.02f;
		((ModNPC)this).npc.spriteDirection = ((ModNPC)this).npc.direction;
		Player player = Main.player[((ModNPC)this).npc.target];
		((ModNPC)this).npc.TargetClosest();
		Vector2 vector = Main.player[((ModNPC)this).npc.target].Center - ((ModNPC)this).npc.Center;
		vector.Normalize();
		int num = (Main.expertMode ? 20 : 35);
		if (((ModNPC)this).npc.life > ((ModNPC)this).npc.lifeMax / 2)
		{
			((ModNPC)this).npc.velocity *= 0.985f;
			Timer1++;
			if (Timer1 == 60 || Timer1 == 120 || Timer1 == 180)
			{
				vector.X *= 12f;
				vector.Y *= 12f;
				((ModNPC)this).npc.velocity.X = vector.X;
				((ModNPC)this).npc.velocity.Y = vector.Y;
				Vector2 vector2 = Main.player[((ModNPC)this).npc.target].Center - ((ModNPC)this).npc.Center;
				vector2.Normalize();
				vector2.X *= 12f;
				vector2.Y *= 12f;
			}
			if (Timer1 > 270)
			{
				if (((ModNPC)this).npc.Center.X >= player.Center.X && MoveSpeed >= -53)
				{
					MoveSpeed--;
				}
				else if (((ModNPC)this).npc.Center.X <= player.Center.X && MoveSpeed <= 53)
				{
					MoveSpeed++;
				}
				((ModNPC)this).npc.velocity.X = (float)MoveSpeed * 0.07f;
				if (((ModNPC)this).npc.Center.Y >= player.Center.Y - HomeY && MoveSpeedY >= -30)
				{
					MoveSpeedY--;
					HomeY = 100f;
				}
				else if (((ModNPC)this).npc.Center.Y <= player.Center.Y - HomeY && MoveSpeedY <= 30)
				{
					MoveSpeedY++;
				}
				((ModNPC)this).npc.velocity.Y = (float)MoveSpeedY * 0.07f;
				if (Main.rand.Next(220) == 6)
				{
					HomeY = -34f;
				}
			}
			if (AttackType == 0 && (Timer1 == 280 || Timer1 == 300 || Timer1 == 320))
			{
				float num2 = 15f;
				int num3 = 128;
				Main.PlaySound(2, (int)((ModNPC)this).npc.position.X, (int)((ModNPC)this).npc.position.Y, 20, 1f, 0f);
				float num4 = (float)Math.Atan2(((ModNPC)this).npc.Center.Y - player.Center.Y, ((ModNPC)this).npc.Center.X - player.Center.X);
				Projectile.NewProjectile(((ModNPC)this).npc.Center.X, ((ModNPC)this).npc.Center.Y, (float)(Math.Cos(num4) * (double)num2 * -1.0), (float)(Math.Sin(num4) * (double)num2 * -1.0), num3, num, 0f, Main.myPlayer, 0f, 0f);
			}
			if (AttackType == 1 && (Timer1 == 280 || Timer1 == 320))
			{
				for (int i = -1; i <= 1; i++)
				{
					Projectile.NewProjectile(((ModNPC)this).npc.Center, 7f * ((ModNPC)this).npc.DirectionTo(player.Center).RotatedBy(MathHelper.ToRadians(5f) * (float)i), ((ModNPC)this).mod.ProjectileType("IceSpike"), num, 0f, Main.myPlayer, 0f, 0f);
				}
			}
			if (AttackType == 2)
			{
				if (Timer1 == 280)
				{
					Main.PlaySound(((ModNPC)this).mod.GetLegacySoundSlot((SoundType)50, "Sounds/DragonRoar")?.WithVolume(7f)?.WithPitchVariance(0.6f), -1, -1);
				}
				if (Timer1 == 300)
				{
					float num5 = 14f;
					int num6 = ((ModNPC)this).mod.ProjectileType("IceTwisterSmall");
					Main.PlaySound(2, (int)((ModNPC)this).npc.position.X, (int)((ModNPC)this).npc.position.Y, 20, 1f, 0f);
					float num7 = (float)Math.Atan2(((ModNPC)this).npc.Center.Y - player.Center.Y, ((ModNPC)this).npc.Center.X - player.Center.X);
					Projectile.NewProjectile(((ModNPC)this).npc.Center.X, ((ModNPC)this).npc.Center.Y, (float)(Math.Cos(num7) * (double)num5 * -1.0), (float)(Math.Sin(num7) * (double)num5 * -1.0), num6, num, 0f, Main.myPlayer, 0f, 0f);
				}
			}
			if (Timer1 >= 360)
			{
				Timer1 = 0;
				if (AttackType <= 2)
				{
					AttackType++;
				}
				if (AttackType > 2)
				{
					AttackType = 0;
				}
			}
		}
		else
		{
			if (((ModNPC)this).npc.life < ((ModNPC)this).npc.lifeMax / 2 && !Phase2)
			{
				Phase2 = true;
				Transition = true;
			}
			if (Transition)
			{
				((ModNPC)this).npc.immortal = true;
				((ModNPC)this).npc.dontTakeDamage = true;
				((ModNPC)this).npc.rotation = ((ModNPC)this).npc.velocity.X * 0.02f;
				Timer1 = 0;
				TransitionTimer++;
				if (TransitionTimer < 300)
				{
					((ModNPC)this).npc.velocity *= 0f;
				}
				if (TransitionTimer == 250)
				{
					Main.PlaySound(((ModNPC)this).mod.GetLegacySoundSlot((SoundType)50, "Sounds/DragonRoar")?.WithVolume(7f)?.WithPitchVariance(0.6f), -1, -1);
					Ultranium.seizureAmount = 12f;
					BlizzardEffect = true;
				}
				if (TransitionTimer == 300)
				{
					TransitionTimer = 0;
					Transition = false;
				}
			}
			if (Phase2 && !Transition)
			{
				((ModNPC)this).npc.velocity *= 0.989f;
				((ModNPC)this).npc.immortal = false;
				((ModNPC)this).npc.dontTakeDamage = false;
				Timer2++;
				if (Timer2 == 60 || Timer2 == 120 || Timer2 == 180)
				{
					vector.X *= 13f;
					vector.Y *= 13f;
					((ModNPC)this).npc.velocity.X = vector.X;
					((ModNPC)this).npc.velocity.Y = vector.Y;
					Vector2 vector3 = Main.player[((ModNPC)this).npc.target].Center - ((ModNPC)this).npc.Center;
					vector3.Normalize();
					vector3.X *= 13f;
					vector3.Y *= 13f;
				}
				if (Timer2 == 240)
				{
					for (int j = 0; j < 50; j++)
					{
						int num8 = Dust.NewDust(((ModNPC)this).npc.position, ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, 45);
						Main.dust[num8].scale = 1.5f;
					}
					if (FlyDirection == 0)
					{
						((ModNPC)this).npc.position.X = player.position.X + 400f;
						((ModNPC)this).npc.position.Y = player.position.Y - 500f;
					}
					if (FlyDirection == 1)
					{
						((ModNPC)this).npc.position.X = player.position.X - 600f;
						((ModNPC)this).npc.position.Y = player.position.Y - 500f;
					}
					for (int k = 0; k < 50; k++)
					{
						int num9 = Dust.NewDust(((ModNPC)this).npc.position, ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, 45);
						Main.dust[num9].scale = 1.5f;
					}
				}
				if (Timer2 > 240 && Timer2 < 320)
				{
					if (FlyDirection == 0)
					{
						((ModNPC)this).npc.velocity.X = -14f;
						((ModNPC)this).npc.velocity.Y = 0f;
					}
					if (FlyDirection == 1)
					{
						((ModNPC)this).npc.velocity.X = 14f;
						((ModNPC)this).npc.velocity.Y = 0f;
					}
					if (Main.rand.Next(8) == 0)
					{
						int num10 = 128;
						Main.PlaySound(2, (int)((ModNPC)this).npc.position.X, (int)((ModNPC)this).npc.position.Y, 20, 1f, 0f);
						Math.Atan2(((ModNPC)this).npc.Center.Y - player.Center.Y, ((ModNPC)this).npc.Center.X - player.Center.X);
						Projectile.NewProjectile(((ModNPC)this).npc.Center.X, ((ModNPC)this).npc.Center.Y, 0f, 7f, num10, num + 10, 0f, Main.myPlayer, 0f, 0f);
					}
				}
				if (Timer2 == 320)
				{
					((ModNPC)this).npc.velocity *= 0f;
				}
				if (AttackType2 == 0 && (Timer2 == 360 || Timer2 == 390 || Timer2 == 420 || Timer2 == 450 || Timer2 == 480))
				{
					float num11 = 6f;
					int num12 = ((ModNPC)this).mod.ProjectileType("IceWave");
					Main.PlaySound(2, (int)((ModNPC)this).npc.position.X, (int)((ModNPC)this).npc.position.Y, 20, 1f, 0f);
					float num13 = (float)Math.Atan2(((ModNPC)this).npc.Center.Y - player.Center.Y, ((ModNPC)this).npc.Center.X - player.Center.X);
					Projectile.NewProjectile(((ModNPC)this).npc.Center.X, ((ModNPC)this).npc.Center.Y, (float)(Math.Cos(num13) * (double)num11 * -1.0), (float)(Math.Sin(num13) * (double)num11 * -1.0), num12, num + 10, 0f, Main.myPlayer, 0f, 0f);
				}
				if (AttackType2 == 1 && (Timer2 == 360 || Timer2 == 420 || Timer2 == 480))
				{
					for (int l = -2; l <= 2; l++)
					{
						Projectile.NewProjectile(((ModNPC)this).npc.Center, 6f * ((ModNPC)this).npc.DirectionTo(player.Center).RotatedBy(MathHelper.ToRadians(5f) * (float)l), ((ModNPC)this).mod.ProjectileType("IceSpike"), num + 10, 0f, Main.myPlayer, 0f, 0f);
					}
				}
				if (AttackType2 == 2 && Timer2 == 400)
				{
					Main.PlaySound(((ModNPC)this).mod.GetLegacySoundSlot((SoundType)50, "Sounds/DragonRoar")?.WithVolume(7f)?.WithPitchVariance(0.6f), -1, -1);
					float num14 = 4f;
					int num15 = ((ModNPC)this).mod.ProjectileType("IceVortex");
					Main.PlaySound(2, (int)((ModNPC)this).npc.position.X, (int)((ModNPC)this).npc.position.Y, 20, 1f, 0f);
					float num16 = (float)Math.Atan2(((ModNPC)this).npc.Center.Y - player.Center.Y, ((ModNPC)this).npc.Center.X - player.Center.X);
					Projectile.NewProjectile(((ModNPC)this).npc.Center.X, ((ModNPC)this).npc.Center.Y, (float)(Math.Cos(num16) * (double)num14 * -1.0), (float)(Math.Sin(num16) * (double)num14 * -1.0), num15, num + 10, 0f, Main.myPlayer, 0f, 0f);
				}
				if (Timer2 >= 520)
				{
					Timer2 = 0;
					if (AttackType2 <= 2)
					{
						AttackType2++;
					}
					if (AttackType2 > 2)
					{
						AttackType2 = 0;
					}
					if (FlyDirection <= 1)
					{
						FlyDirection++;
					}
					if (FlyDirection > 1)
					{
						FlyDirection = 0;
					}
				}
			}
		}
		if (!((Entity)((ModNPC)this).npc).active)
		{
			BlizzardEffect = false;
			Transition = false;
			Phase2 = false;
		}
		return true;
	}

	public override bool CheckDead()
	{
		Gore.NewGore(((ModNPC)this).npc.position, ((ModNPC)this).npc.velocity, ((ModNPC)this).mod.GetGoreSlot("Gores/DragonGore1"));
		Gore.NewGore(((ModNPC)this).npc.position, ((ModNPC)this).npc.velocity, ((ModNPC)this).mod.GetGoreSlot("Gores/DragonGore2"));
		Gore.NewGore(((ModNPC)this).npc.position, ((ModNPC)this).npc.velocity, ((ModNPC)this).mod.GetGoreSlot("Gores/DragonGore3"));
		Gore.NewGore(((ModNPC)this).npc.position, ((ModNPC)this).npc.velocity, ((ModNPC)this).mod.GetGoreSlot("Gores/DragonGore4"));
		Gore.NewGore(((ModNPC)this).npc.position, ((ModNPC)this).npc.velocity, ((ModNPC)this).mod.GetGoreSlot("Gores/DragonGore5"));
		Gore.NewGore(((ModNPC)this).npc.position, ((ModNPC)this).npc.velocity, ((ModNPC)this).mod.GetGoreSlot("Gores/DragonGore6"));
		Gore.NewGore(((ModNPC)this).npc.position, ((ModNPC)this).npc.velocity, ((ModNPC)this).mod.GetGoreSlot("Gores/DragonGore7"));
		return true;
	}

	public override void FindFrame(int frameHeight)
	{
		((ModNPC)this).npc.frameCounter += 1.0;
		if (((ModNPC)this).npc.frameCounter > 5.0)
		{
			((ModNPC)this).npc.frame.Y = ((ModNPC)this).npc.frame.Y + frameHeight;
			((ModNPC)this).npc.frameCounter = 0.0;
		}
		if (((ModNPC)this).npc.frame.Y >= frameHeight * 7)
		{
			((ModNPC)this).npc.frame.Y = 0;
		}
		if (Timer1 > 240 || TransitionTimer >= 250 || (Timer2 >= 360 && Timer2 < 520))
		{
			((ModNPC)this).npc.frame.Y = frameHeight * 7;
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
			int num = Main.rand.Next(3);
			if (num == 0)
			{
				Item.NewItem((int)((ModNPC)this).npc.position.X, (int)((ModNPC)this).npc.position.Y, ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, ((ModNPC)this).mod.ItemType("GlacialFlail"), 1, false, 0, false, false);
			}
			if (num == 1)
			{
				Item.NewItem((int)((ModNPC)this).npc.position.X, (int)((ModNPC)this).npc.position.Y, ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, ((ModNPC)this).mod.ItemType("GlacialGun"), 1, false, 0, false, false);
			}
			if (num == 2)
			{
				Item.NewItem((int)((ModNPC)this).npc.position.X, (int)((ModNPC)this).npc.position.Y, ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, ((ModNPC)this).mod.ItemType("GlacialWand"), 1, false, 0, false, false);
			}
			Item.NewItem((int)((ModNPC)this).npc.position.X, (int)((ModNPC)this).npc.position.Y, ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, ((ModNPC)this).mod.ItemType("IcePelt"), Main.rand.Next(12, 18), false, 0, false, false);
		}
		if (Main.rand.Next(7) == 0)
		{
			Item.NewItem(((ModNPC)this).npc.getRect(), ((ModNPC)this).mod.ItemType("IceDragonMask"), 1, false, 0, false, false);
		}
		if (Main.rand.Next(10) == 0)
		{
			Item.NewItem(((ModNPC)this).npc.getRect(), ((ModNPC)this).mod.ItemType("IceDragonTrophyItem"), 1, false, 0, false, false);
		}
		if (!UltraniumWorld.downedDragon)
		{
			UltraniumWorld.downedDragon = true;
			if (Main.netMode == 2)
			{
				NetMessage.SendData(7);
			}
		}
		BlizzardEffect = false;
		Transition = false;
		Phase2 = false;
	}

	public override void BossLoot(ref string name, ref int potionType)
	{
		potionType = 188;
	}
}
