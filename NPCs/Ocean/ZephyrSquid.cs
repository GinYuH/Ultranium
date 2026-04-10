using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.NPCs.Ocean;

[AutoloadBossHead]
public class ZephyrSquid : ModNPC
{
	private int timer;

	private int BoltTimer;

	private int moveSpeed;

	private int moveSpeedY;

	private float HomeY = 100f;

	public int players;

	public override void SetStaticDefaults()
	{
		((ModNPC)this).DisplayName.SetDefault("Zephyr Squid");
		Main.npcFrameCount[((ModNPC)this).npc.type] = 5;
		NPCID.Sets.TrailCacheLength[((ModNPC)this).npc.type] = 3;
		NPCID.Sets.TrailingMode[((ModNPC)this).npc.type] = 0;
	}

	public override void SetDefaults()
	{
		((ModNPC)this).npc.width = 98;
		((ModNPC)this).npc.height = 256;
		((ModNPC)this).npc.damage = 20;
		((ModNPC)this).npc.lifeMax = 3800;
		((ModNPC)this).npc.defense = 20;
		((ModNPC)this).npc.knockBackResist = 0f;
		((ModNPC)this).npc.boss = true;
		((ModNPC)this).npc.noGravity = true;
		((ModNPC)this).npc.noTileCollide = true;
		((ModNPC)this).npc.HitSound = SoundID.NPCHit7;
		((ModNPC)this).npc.DeathSound = SoundID.NPCDeath1;
		((ModNPC)this).npc.value = Item.buyPrice(0, 5);
		((ModNPC)this).npc.npcSlots = 1f;
		((ModNPC)this).npc.boss = true;
		((ModNPC)this).npc.lavaImmune = true;
		((ModNPC)this).npc.noGravity = true;
		((ModNPC)this).npc.noTileCollide = true;
		base.bossBag = ((ModNPC)this).mod.ItemType("SquidBag");
		((ModNPC)this).npc.buffImmune[24] = true;
		base.music = ((ModNPC)this).mod.GetSoundSlot((SoundType)51, "Sounds/Music/ZephyrSquid");
		((ModNPC)this).npc.netAlways = true;
		((ModNPC)this).npc.aiStyle = -1;
		players = 1;
	}

	public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
	{
		players = numPlayers;
		((ModNPC)this).npc.lifeMax = 4300 + numPlayers * 430;
		((ModNPC)this).npc.damage = 35;
		((ModNPC)this).npc.defense = 30;
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
	{
		if (((ModNPC)this).npc.velocity != Vector2.Zero)
		{
			Vector2 vector = new Vector2((float)ModContent.GetTexture("Ultranium/NPCs/Ocean/ZephyrSquidTrail").Width * 0.2f, (float)((ModNPC)this).npc.height * 0.2f);
			for (int i = 0; i < ((ModNPC)this).npc.oldPos.Length; i++)
			{
				Vector2 position = ((ModNPC)this).npc.oldPos[i] - Main.screenPosition + vector + new Vector2(0f, ((ModNPC)this).npc.gfxOffY);
				Color color = ((ModNPC)this).npc.GetAlpha(drawColor) * ((float)(((ModNPC)this).npc.oldPos.Length - i) / (float)((ModNPC)this).npc.oldPos.Length / 2f);
				spriteBatch.Draw(ModContent.GetTexture("Ultranium/NPCs/Ocean/ZephyrSquidTrail"), position, ((ModNPC)this).npc.frame, color, ((ModNPC)this).npc.rotation, vector, ((ModNPC)this).npc.scale, SpriteEffects.None, 0f);
			}
		}
		return true;
	}

	public override bool PreAI()
	{
		((ModNPC)this).npc.rotation = ((ModNPC)this).npc.velocity.X * 0.02f;
		Player player = Main.player[((ModNPC)this).npc.target];
		int num = (Main.expertMode ? 14 : 20);
		if (!((Entity)player).active || player.dead)
		{
			((ModNPC)this).npc.TargetClosest(faceTarget: false);
			((ModNPC)this).npc.velocity.Y = -100f;
		}
		((ModNPC)this).npc.netUpdate = true;
		((ModNPC)this).npc.TargetClosest();
		if (Main.player[((ModNPC)this).npc.target].dead || Main.player[((ModNPC)this).npc.target].dead)
		{
			((ModNPC)this).npc.velocity.Y = 30f;
			((ModNPC)this).npc.ai[0] += 1f;
			if (((ModNPC)this).npc.ai[0] >= 120f)
			{
				((Entity)((ModNPC)this).npc).active = false;
			}
		}
		if (((ModNPC)this).npc.ai[0] == 0f)
		{
			if (((ModNPC)this).npc.Center.X >= player.Center.X && moveSpeed >= -53)
			{
				moveSpeed--;
			}
			else if (((ModNPC)this).npc.Center.X <= player.Center.X && moveSpeed <= 53)
			{
				moveSpeed++;
			}
			((ModNPC)this).npc.velocity.X = (float)moveSpeed * 0.09f;
			if (((ModNPC)this).npc.Center.Y >= player.Center.Y - HomeY && moveSpeedY >= -30)
			{
				moveSpeedY--;
				HomeY = 150f;
			}
			else if (((ModNPC)this).npc.Center.Y <= player.Center.Y - HomeY && moveSpeedY <= 30)
			{
				moveSpeedY++;
			}
			((ModNPC)this).npc.velocity.Y = (float)moveSpeedY * 0.1f;
		}
		if (((ModNPC)this).npc.ai[0] == 1f)
		{
			if (((ModNPC)this).npc.Center.X >= player.Center.X && moveSpeed >= -100)
			{
				moveSpeed--;
			}
			else if (((ModNPC)this).npc.Center.X <= player.Center.X && moveSpeed <= 100)
			{
				moveSpeed++;
			}
			((ModNPC)this).npc.velocity.X = (float)moveSpeed * 0.09f;
			if (((ModNPC)this).npc.Center.Y >= player.Center.Y - HomeY && moveSpeedY >= -30)
			{
				moveSpeedY--;
				HomeY = 150f;
			}
			else if (((ModNPC)this).npc.Center.Y <= player.Center.Y - HomeY && moveSpeedY <= 30)
			{
				moveSpeedY++;
			}
			((ModNPC)this).npc.velocity.Y = (float)moveSpeedY * 0.1f;
		}
		if (((ModNPC)this).npc.ai[0] == 2f)
		{
			((ModNPC)this).npc.velocity.X *= 0f;
			((ModNPC)this).npc.velocity.Y = -13f;
		}
		if (((ModNPC)this).npc.ai[0] == 3f)
		{
			((ModNPC)this).npc.velocity *= 0f;
		}
		timer++;
		if (timer == 100 || timer == 200 || timer == 300 || timer == 400 || timer == 500)
		{
			Main.PlaySound(SoundID.Item112, ((ModNPC)this).npc.position);
			Vector2 vector = Main.player[((ModNPC)this).npc.target].Center - ((ModNPC)this).npc.Center;
			vector.Normalize();
			vector.X *= 5f;
			vector.Y *= 5f;
			int num2 = Main.rand.Next(2, 4);
			for (int i = 0; i < num2; i++)
			{
				float num3 = (float)Main.rand.Next(-100, 100) * 0.01f;
				Projectile.NewProjectile(((ModNPC)this).npc.Center.X, ((ModNPC)this).npc.Center.Y, vector.X + num3, vector.Y + num3, ((ModNPC)this).mod.ProjectileType("Bubble"), num, 1f, Main.myPlayer, 0f, 0f);
			}
		}
		if (timer == 600)
		{
			((ModNPC)this).npc.ai[0] = 1f;
		}
		if (timer == 630 || timer == 660 || timer == 690 || timer == 720 || timer == 750 || timer == 780 || timer == 810 || timer == 840)
		{
			Main.PlaySound(SoundID.Item111, ((ModNPC)this).npc.position);
			float num4 = 6f;
			int num5 = ((ModNPC)this).mod.ProjectileType("InkGlob");
			float num6 = (float)Math.Atan2(((ModNPC)this).npc.Center.Y - player.Center.Y, ((ModNPC)this).npc.Center.X - player.Center.X);
			Projectile.NewProjectile(((ModNPC)this).npc.Center.X, ((ModNPC)this).npc.Center.Y, (float)(Math.Cos(num6) * (double)num4 * -1.0), (float)(Math.Sin(num6) * (double)num4 * -1.0), num5, num, 0f, Main.myPlayer, 0f, 0f);
		}
		if (timer == 960)
		{
			for (int j = 0; j < 60; j++)
			{
				int num7 = Dust.NewDust(((ModNPC)this).npc.position, ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, 191);
				Main.dust[num7].scale = 1.5f;
			}
			Vector2 spinningpoint = new Vector2(8f, 0f).RotatedByRandom(Math.PI * 2.0);
			Vector2 spinningpoint2 = new Vector2(3f, 0f).RotatedByRandom(Math.PI * 2.0);
			for (int k = 0; k < 10; k++)
			{
				Vector2 vector2 = spinningpoint.RotatedBy(Math.PI * ((double)k + Main.rand.NextDouble() - 0.5));
				Projectile.NewProjectile(((ModNPC)this).npc.Center, vector2, ((ModNPC)this).mod.ProjectileType("InkCloud"), num, 0f, Main.myPlayer, 0f, 0f);
			}
			for (int l = 0; l < 10; l++)
			{
				Vector2 vector3 = spinningpoint2.RotatedBy(Math.PI * ((double)l + Main.rand.NextDouble() - 0.5));
				Projectile.NewProjectile(((ModNPC)this).npc.Center, vector3, ((ModNPC)this).mod.ProjectileType("InkBubble"), num, 0f, Main.myPlayer, 0f, 0f);
			}
			((ModNPC)this).npc.position.X = player.position.X - 100f;
			((ModNPC)this).npc.position.Y = player.position.Y + 300f;
		}
		if (timer > 960 && timer < 1000)
		{
			((ModNPC)this).npc.velocity *= 0f;
		}
		if (timer == 980)
		{
			Projectile.NewProjectile(((ModNPC)this).npc.Center.X, ((ModNPC)this).npc.Center.Y, 0f, 0f, ((ModNPC)this).mod.ProjectileType("SquidChargeTelegraph"), 0, 0f, Main.myPlayer, 0f, 0f);
		}
		if (timer == 1000)
		{
			((ModNPC)this).npc.ai[0] = 2f;
		}
		if (timer == 1060)
		{
			((ModNPC)this).npc.ai[0] = 3f;
		}
		if (timer == 1100)
		{
			Projectile.NewProjectile(((ModNPC)this).npc.position.X + 55f, ((ModNPC)this).npc.position.Y, -4f, -6f, ((ModNPC)this).mod.ProjectileType("AquaBall"), num, 0.4f, Main.myPlayer, 0f, 0f);
			Projectile.NewProjectile(((ModNPC)this).npc.position.X + 55f, ((ModNPC)this).npc.position.Y, -2f, -6f, ((ModNPC)this).mod.ProjectileType("AquaBall"), num, 0.4f, Main.myPlayer, 0f, 0f);
			Projectile.NewProjectile(((ModNPC)this).npc.position.X + 55f, ((ModNPC)this).npc.position.Y, 0f, -6f, ((ModNPC)this).mod.ProjectileType("AquaBall"), num, 0.4f, Main.myPlayer, 0f, 0f);
			Projectile.NewProjectile(((ModNPC)this).npc.position.X + 55f, ((ModNPC)this).npc.position.Y, 2f, -6f, ((ModNPC)this).mod.ProjectileType("AquaBall"), num, 0.4f, Main.myPlayer, 0f, 0f);
			Projectile.NewProjectile(((ModNPC)this).npc.position.X + 55f, ((ModNPC)this).npc.position.Y, 4f, -6f, ((ModNPC)this).mod.ProjectileType("AquaBall"), num, 0.4f, Main.myPlayer, 0f, 0f);
		}
		if (timer >= 1150)
		{
			timer = 0;
			((ModNPC)this).npc.ai[0] = 0f;
		}
		if (((ModNPC)this).npc.life < ((ModNPC)this).npc.lifeMax / 2 && ((ModNPC)this).npc.ai[0] < 2f)
		{
			BoltTimer++;
			if (BoltTimer == 300)
			{
				float num8 = 11f;
				float num9 = (float)Math.Atan2(((ModNPC)this).npc.Center.Y - player.Center.Y, ((ModNPC)this).npc.Center.X - player.Center.X);
				Projectile.NewProjectile(((ModNPC)this).npc.Center.X, ((ModNPC)this).npc.Center.Y, (float)(Math.Cos(num9) * (double)num8 * -1.0), (float)(Math.Sin(num9) * (double)num8 * -1.0), ((ModNPC)this).mod.ProjectileType("WaterHelix1"), num, 0f, 0, 0f, 0f);
				Projectile.NewProjectile(((ModNPC)this).npc.Center.X, ((ModNPC)this).npc.Center.Y, (float)(Math.Cos(num9) * (double)num8 * -1.0), (float)(Math.Sin(num9) * (double)num8 * -1.0), ((ModNPC)this).mod.ProjectileType("WaterHelix2"), num, 0f, 0, 0f, 0f);
				BoltTimer = 0;
			}
		}
		return true;
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

	public override bool CheckDead()
	{
		Gore.NewGore(((ModNPC)this).npc.position, ((ModNPC)this).npc.velocity, ((ModNPC)this).mod.GetGoreSlot("Gores/SquidGore1"));
		Gore.NewGore(((ModNPC)this).npc.position, ((ModNPC)this).npc.velocity, ((ModNPC)this).mod.GetGoreSlot("Gores/SquidGore2"));
		Gore.NewGore(((ModNPC)this).npc.position, ((ModNPC)this).npc.velocity, ((ModNPC)this).mod.GetGoreSlot("Gores/SquidGore3"));
		Gore.NewGore(((ModNPC)this).npc.position, ((ModNPC)this).npc.velocity, ((ModNPC)this).mod.GetGoreSlot("Gores/SquidGore4"));
		return true;
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
				Item.NewItem((int)((ModNPC)this).npc.position.X, (int)((ModNPC)this).npc.position.Y, ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, ((ModNPC)this).mod.ItemType("ZephyrBlade"), 1, false, 0, false, false);
			}
			if (num == 1)
			{
				Item.NewItem((int)((ModNPC)this).npc.position.X, (int)((ModNPC)this).npc.position.Y, ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, ((ModNPC)this).mod.ItemType("ZephyrKnife"), 1, false, 0, false, false);
			}
			if (num == 2)
			{
				Item.NewItem((int)((ModNPC)this).npc.position.X, (int)((ModNPC)this).npc.position.Y, ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, ((ModNPC)this).mod.ItemType("ZephyrTrident"), 1, false, 0, false, false);
			}
			if (Main.rand.Next(20) == 0)
			{
				Item.NewItem((int)((ModNPC)this).npc.position.X, (int)((ModNPC)this).npc.position.Y, ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, ((ModNPC)this).mod.ItemType("WormPet"), 1, false, 0, false, false);
			}
			Item.NewItem(((ModNPC)this).npc.getRect(), ((ModNPC)this).mod.ItemType("OceanScale"), Main.rand.Next(8, 12), false, 0, false, false);
		}
		if (Main.rand.Next(7) == 0)
		{
			Item.NewItem(((ModNPC)this).npc.getRect(), ((ModNPC)this).mod.ItemType("SquidMask"), 1, false, 0, false, false);
		}
		if (Main.rand.Next(10) == 0)
		{
			Item.NewItem(((ModNPC)this).npc.getRect(), ((ModNPC)this).mod.ItemType("SquidTrophyItem"), 1, false, 0, false, false);
		}
		if (!UltraniumWorld.downedSquid)
		{
			UltraniumWorld.downedSquid = true;
			if (Main.netMode == 2)
			{
				NetMessage.SendData(7);
			}
		}
	}

	public override void BossLoot(ref string name, ref int potionType)
	{
		potionType = 28;
	}
}
