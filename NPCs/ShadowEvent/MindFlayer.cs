using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Ultranium.ShadowEvent;

namespace Ultranium.NPCs.ShadowEvent;

[AutoloadBossHead]
public class MindFlayer : ModNPC
{
	private int timer;

	private int MoveSpeed;

	private int MoveSpeedY;

	private float HomeY = 130f;

	public int RoarTimer;

	private int TeleportTimer;

	private int TeleportNum;

	private bool HentaiFace;

	public override void SetStaticDefaults()
	{
		((ModNPC)this).DisplayName.SetDefault("Mind Flayer");
		Main.npcFrameCount[((ModNPC)this).npc.type] = 12;
		NPCID.Sets.TrailCacheLength[((ModNPC)this).npc.type] = 15;
		NPCID.Sets.TrailingMode[((ModNPC)this).npc.type] = 0;
	}

	public override void SetDefaults()
	{
		((ModNPC)this).npc.scale = 1f;
		((ModNPC)this).npc.width = 176;
		((ModNPC)this).npc.height = 252;
		((ModNPC)this).npc.damage = 90;
		((ModNPC)this).npc.lifeMax = 100000;
		((ModNPC)this).npc.defense = 75;
		((ModNPC)this).npc.knockBackResist = 0f;
		((ModNPC)this).npc.noGravity = true;
		((ModNPC)this).npc.noTileCollide = true;
		((ModNPC)this).npc.HitSound = SoundID.NPCHit55;
		((ModNPC)this).npc.DeathSound = ((ModNPC)this).mod.GetLegacySoundSlot((SoundType)50, "Sounds/MindFlayerRoar")?.WithVolume(1.2f)?.WithPitchVariance(0.5f);
		base.music = ((ModNPC)this).mod.GetSoundSlot((SoundType)51, "Sounds/Music/MindFlayer");
		((ModNPC)this).npc.defense = 60;
		((ModNPC)this).npc.npcSlots = 1f;
		((ModNPC)this).npc.lavaImmune = true;
		((ModNPC)this).npc.noGravity = true;
		((ModNPC)this).npc.netAlways = true;
		((ModNPC)this).npc.aiStyle = 0;
		((ModNPC)this).npc.alpha = 255;
		for (int i = 0; i < 206; i++)
		{
			((ModNPC)this).npc.buffImmune[i] = true;
		}
	}

	public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
	{
		((ModNPC)this).npc.lifeMax = 200000;
		((ModNPC)this).npc.damage = 120;
		((ModNPC)this).npc.defense = 75;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
	{
		if (((ModNPC)this).npc.velocity != Vector2.Zero)
		{
			Vector2 vector = new Vector2((float)ModContent.GetTexture("Ultranium/NPCs/ShadowEvent/MindFlayerTrail").Width * 0.5f, (float)((ModNPC)this).npc.height * 0.5f);
			for (int i = 0; i < ((ModNPC)this).npc.oldPos.Length; i++)
			{
				SpriteEffects effects = ((((ModNPC)this).npc.direction != -1) ? SpriteEffects.FlipHorizontally : SpriteEffects.None);
				Vector2 position = ((ModNPC)this).npc.oldPos[i] - Main.screenPosition + vector + new Vector2(0f, ((ModNPC)this).npc.gfxOffY);
				Color color = ((ModNPC)this).npc.GetAlpha(drawColor) * ((float)(((ModNPC)this).npc.oldPos.Length - i) / (float)((ModNPC)this).npc.oldPos.Length / 2f);
				spriteBatch.Draw(ModContent.GetTexture("Ultranium/NPCs/ShadowEvent/MindFlayerTrail"), position, ((ModNPC)this).npc.frame, color, ((ModNPC)this).npc.rotation, vector, ((ModNPC)this).npc.scale, effects, 0f);
			}
		}
		return true;
	}

	public override bool CheckDead()
	{
		Gore.NewGore(((ModNPC)this).npc.position, ((ModNPC)this).npc.velocity, ((ModNPC)this).mod.GetGoreSlot("Gores/ShadowEvent/MindFlayerGore1"));
		Gore.NewGore(((ModNPC)this).npc.position, ((ModNPC)this).npc.velocity, ((ModNPC)this).mod.GetGoreSlot("Gores/ShadowEvent/MindFlayerGore2"));
		Gore.NewGore(((ModNPC)this).npc.position, ((ModNPC)this).npc.velocity, ((ModNPC)this).mod.GetGoreSlot("Gores/ShadowEvent/MindFlayerGore3"));
		return true;
	}

	public override void OnHitPlayer(Player player, int damage, bool crit)
	{
		player.AddBuff(163, 60, fromNetPvP: true);
		player.AddBuff(((ModNPC)this).mod.BuffType("DarkDebuff"), 300);
	}

	public override void FindFrame(int frameHeight)
	{
		if (!HentaiFace)
		{
			((ModNPC)this).npc.frameCounter += 1.0;
			if (((ModNPC)this).npc.frameCounter > 6.0)
			{
				((ModNPC)this).npc.frame.Y = ((ModNPC)this).npc.frame.Y + frameHeight;
				((ModNPC)this).npc.frameCounter = 0.0;
			}
			if (((ModNPC)this).npc.frame.Y >= frameHeight * 6)
			{
				((ModNPC)this).npc.frame.Y = 0;
			}
		}
		if (HentaiFace)
		{
			((ModNPC)this).npc.frameCounter += 1.0;
			if (((ModNPC)this).npc.frameCounter > 6.0)
			{
				((ModNPC)this).npc.frame.Y = ((ModNPC)this).npc.frame.Y + frameHeight;
				((ModNPC)this).npc.frameCounter = 0.0;
			}
			if (((ModNPC)this).npc.frame.Y >= frameHeight * 12)
			{
				((ModNPC)this).npc.frame.Y = frameHeight * 6;
			}
		}
	}

	public override bool PreAI()
	{
		((ModNPC)this).npc.rotation = ((ModNPC)this).npc.velocity.X * 0.02f;
		Player player = Main.player[((ModNPC)this).npc.target];
		bool expertMode = Main.expertMode;
		((ModNPC)this).npc.netUpdate = true;
		((ModNPC)this).npc.TargetClosest();
		int num = (expertMode ? 40 : 55);
		if (((ModNPC)this).npc.ai[0] == 0f)
		{
			if (((ModNPC)this).npc.Center.X >= player.Center.X && MoveSpeed >= -75)
			{
				MoveSpeed--;
			}
			else if (((ModNPC)this).npc.Center.X <= player.Center.X && MoveSpeed <= 75)
			{
				MoveSpeed++;
			}
			((ModNPC)this).npc.velocity.X = (float)MoveSpeed * 0.1f;
			if (((ModNPC)this).npc.Center.Y >= player.Center.Y - HomeY && MoveSpeedY >= -75)
			{
				MoveSpeedY--;
				HomeY = 150f;
			}
			else if (((ModNPC)this).npc.Center.Y <= player.Center.Y - HomeY && MoveSpeedY <= 75)
			{
				MoveSpeedY++;
			}
			((ModNPC)this).npc.velocity.Y = (float)MoveSpeedY * 0.12f;
		}
		if (((ModNPC)this).npc.ai[0] == 1f)
		{
			if (((ModNPC)this).npc.Center.X >= player.Center.X && MoveSpeed >= -150)
			{
				MoveSpeed--;
			}
			else if (((ModNPC)this).npc.Center.X <= player.Center.X && MoveSpeed <= 150)
			{
				MoveSpeed++;
			}
			((ModNPC)this).npc.velocity.X = (float)MoveSpeed * 0.1f;
			if (((ModNPC)this).npc.Center.Y >= player.Center.Y - HomeY && MoveSpeedY >= -150)
			{
				MoveSpeedY--;
				HomeY = 150f;
			}
			else if (((ModNPC)this).npc.Center.Y <= player.Center.Y - HomeY && MoveSpeedY <= 150)
			{
				MoveSpeedY++;
			}
			((ModNPC)this).npc.velocity.Y = (float)MoveSpeedY * 0.12f;
		}
		RoarTimer++;
		if (RoarTimer == 360)
		{
			Main.PlaySound(((ModNPC)this).mod.GetLegacySoundSlot((SoundType)50, "Sounds/MindFlayerGrowl").WithVolume(2f).WithPitchVariance(0.5f), -1, -1);
			RoarTimer = 0;
		}
		if (TeleportTimer <= 0)
		{
			timer++;
		}
		if (timer < 100)
		{
			((ModNPC)this).npc.velocity *= 0f;
			((ModNPC)this).npc.alpha -= 5;
		}
		if (timer == 120 || timer == 170 || timer == 220 || timer == 270 || timer == 320 || timer == 370)
		{
			Vector2 vector = Main.player[((ModNPC)this).npc.target].Center - ((ModNPC)this).npc.Center;
			vector.Normalize();
			vector.X *= 9.5f;
			vector.Y *= 9.5f;
			int num2 = Main.rand.Next(3, 5);
			for (int i = 0; i < num2; i++)
			{
				float num3 = (float)Main.rand.Next(-150, 150) * 0.04f;
				Projectile.NewProjectile(((ModNPC)this).npc.Center.X, ((ModNPC)this).npc.Center.Y, vector.X + num3, vector.Y + num3, ((ModNPC)this).mod.ProjectileType("FlayerScythe"), num, 1f, Main.myPlayer, 0f, 0f);
			}
		}
		if (timer >= 430 && timer <= 730)
		{
			((ModNPC)this).npc.velocity *= 0f;
			HentaiFace = true;
		}
		if (timer == 470 || timer == 510 || timer == 550 || timer == 590 || timer == 630 || timer == 670 || timer == 710)
		{
			Main.PlaySound(SoundID.Item78, ((ModNPC)this).npc.Center);
			float num4 = 26f;
			float num5 = (float)Math.Atan2(((ModNPC)this).npc.Center.Y - player.Center.Y, ((ModNPC)this).npc.Center.X - player.Center.X);
			Vector2 spinninpoint = new Vector2((float)(Math.Cos(num5) * (double)num4 * -1.0), (float)(Math.Sin(num5) * (double)num4 * -1.0));
			spinninpoint = spinninpoint.RotatedByRandom(MathHelper.ToRadians(30f));
			Main.projectile[Projectile.NewProjectile(((ModNPC)this).npc.Center, spinninpoint, ((ModNPC)this).mod.ProjectileType("FlayerTentacleBody"), num, 0f, Main.myPlayer, 0f, 0f)].localAI[1] = 200f;
		}
		if (timer == 730)
		{
			((ModNPC)this).npc.ai[0] = 1f;
			HentaiFace = false;
		}
		if (timer == 790 || timer == 880)
		{
			Vector2 vector2 = Main.player[((ModNPC)this).npc.target].Center - ((ModNPC)this).npc.Center;
			vector2.Normalize();
			vector2.X *= 10.5f;
			vector2.Y *= 10.5f;
			Projectile.NewProjectile(((ModNPC)this).npc.Center.X, ((ModNPC)this).npc.Center.Y, vector2.X, vector2.Y, ((ModNPC)this).mod.ProjectileType("FlayerVortex"), num, 1f, ((ModNPC)this).npc.target, 0f, 0f);
		}
		if (timer >= 1010)
		{
			if (TeleportNum < 6)
			{
				TeleportTimer++;
				((ModNPC)this).npc.velocity *= 0f;
				if (TeleportTimer == 1)
				{
					Projectile.NewProjectile(player.Center + Main.rand.NextVector2Square(-600f, 600f), Main.rand.NextVector2Square(-1f, 1f), ((ModNPC)this).mod.ProjectileType("FlayerTelegraph"), 0, 6f, player.whoAmI, 0f, 0f);
				}
				if (TeleportTimer == 100 || TeleportTimer == 110 || TeleportTimer == 120 || TeleportTimer == 130 || TeleportTimer == 140)
				{
					Vector2 vector3 = Main.player[((ModNPC)this).npc.target].Center - ((ModNPC)this).npc.Center;
					vector3.Normalize();
					vector3.X *= 8.5f;
					vector3.Y *= 8.5f;
					Projectile.NewProjectile(((ModNPC)this).npc.Center.X, ((ModNPC)this).npc.Center.Y, vector3.X, vector3.Y, ((ModNPC)this).mod.ProjectileType("FlayerSpit"), num, 1f, ((ModNPC)this).npc.target, 0f, 0f);
					HentaiFace = true;
				}
				if (TeleportTimer >= 160)
				{
					TeleportTimer = 0;
					TeleportNum++;
					HentaiFace = false;
				}
			}
			if (timer == 1120)
			{
				Projectile.NewProjectile(((ModNPC)this).npc.Center.X, ((ModNPC)this).npc.Center.Y, 0f, 0f, ((ModNPC)this).mod.ProjectileType("FlayerAuraBase"), num + 30, 1f, Main.myPlayer, 0f, 0f);
			}
			if (timer > 1120 && timer < 1660)
			{
				((ModNPC)this).npc.velocity *= 0f;
				HentaiFace = true;
			}
			if (timer > 1120 && timer < 1660)
			{
				((ModNPC)this).npc.immortal = true;
				((ModNPC)this).npc.dontTakeDamage = true;
				if (Main.rand.Next(7) == 0)
				{
					Vector2 vector4 = Main.player[((ModNPC)this).npc.target].Center - ((ModNPC)this).npc.Center;
					vector4.Normalize();
					vector4.X *= 8.5f;
					vector4.Y *= 8.5f;
					Projectile.NewProjectile(((ModNPC)this).npc.Center.X, ((ModNPC)this).npc.Center.Y, vector4.X, vector4.Y, ((ModNPC)this).mod.ProjectileType("FlayerSpit"), num, 1f, ((ModNPC)this).npc.target, 0f, 0f);
				}
			}
			else
			{
				((ModNPC)this).npc.immortal = false;
				((ModNPC)this).npc.dontTakeDamage = false;
			}
		}
		if (timer == 1720)
		{
			if (((ModNPC)this).npc.life <= ((ModNPC)this).npc.lifeMax / 2 && NPC.CountNPCS(((ModNPC)this).mod.NPCType("MindFlayerClone")) < 1)
			{
				NPC.NewNPC((int)((ModNPC)this).npc.Center.X, (int)((ModNPC)this).npc.Center.Y + 1000, ((ModNPC)this).mod.NPCType("MindFlayerClone"), 0, 0f, 0f, 0f, 0f, 255);
				NPC.NewNPC((int)((ModNPC)this).npc.Center.X, (int)((ModNPC)this).npc.Center.Y - 1000, ((ModNPC)this).mod.NPCType("MindFlayerClone"), 0, 0f, 0f, 0f, 0f, 255);
			}
			timer = 100;
			((ModNPC)this).npc.ai[0] = 0f;
			TeleportTimer = 0;
			TeleportNum = 0;
			HentaiFace = false;
		}
		return true;
	}

	public override void NPCLoot()
	{
		if (Main.expertMode)
		{
			Item.NewItem((int)((ModNPC)this).npc.position.X, (int)((ModNPC)this).npc.position.Y, ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, ((ModNPC)this).mod.ItemType("FlayerBrain"), 1, false, 0, false, false);
		}
		int num = Main.rand.Next(3);
		if (num == 0)
		{
			Item.NewItem((int)((ModNPC)this).npc.position.X, (int)((ModNPC)this).npc.position.Y, ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, ((ModNPC)this).mod.ItemType("FlayerBlade"), 1, false, 0, false, false);
		}
		if (num == 1)
		{
			Item.NewItem((int)((ModNPC)this).npc.position.X, (int)((ModNPC)this).npc.position.Y, ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, ((ModNPC)this).mod.ItemType("FlayerStaff"), 1, false, 0, false, false);
		}
		if (num == 2)
		{
			Item.NewItem((int)((ModNPC)this).npc.position.X, (int)((ModNPC)this).npc.position.Y, ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, ((ModNPC)this).mod.ItemType("FlayerBow"), 1, false, 0, false, false);
		}
		if (Main.rand.Next(7) == 0)
		{
			Item.NewItem((int)((ModNPC)this).npc.position.X, (int)((ModNPC)this).npc.position.Y, ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, ((ModNPC)this).mod.ItemType("MindFlayerMask"), 1, false, 0, false, false);
		}
		if (Main.rand.Next(10) == 0)
		{
			Item.NewItem((int)((ModNPC)this).npc.position.X, (int)((ModNPC)this).npc.position.Y, ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, ((ModNPC)this).mod.ItemType("FlayerTrophyItem"), 1, false, 0, false, false);
		}
		Item.NewItem(((ModNPC)this).npc.getRect(), ((ModNPC)this).mod.ItemType("DarkMatter"), Main.rand.Next(5, 12), false, 0, false, false);
		Item.NewItem(((ModNPC)this).npc.getRect(), ((ModNPC)this).mod.ItemType("EldritchBlood"), Main.rand.Next(15, 25), false, 0, false, false);
		if (ShadowEventWorld.ShadowEventActive)
		{
			ShadowEventWorld.Phase2 = true;
			string text = "The darkness thickens...";
			if (Main.netMode == 0)
			{
				Main.NewText(text, (byte)61, byte.MaxValue, (byte)142, false);
			}
			if (Main.netMode == 2)
			{
				NetMessage.SendData(25, -1, -1, NetworkText.FromLiteral(text), 255, 175f, 75f, 255f);
			}
		}
	}

	public override void BossLoot(ref string name, ref int potionType)
	{
		potionType = 3544;
	}
}
