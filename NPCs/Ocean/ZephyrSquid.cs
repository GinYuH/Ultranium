using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
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
		// ((ModNPC)this).DisplayName.SetDefault("Zephyr Squid");
		Main.npcFrameCount[((ModNPC)this).NPC.type] = 5;
		NPCID.Sets.TrailCacheLength[((ModNPC)this).NPC.type] = 3;
		NPCID.Sets.TrailingMode[((ModNPC)this).NPC.type] = 0;
	}

	public override void SetDefaults()
	{
		((ModNPC)this).NPC.width = 98;
		((ModNPC)this).NPC.height = 256;
		((ModNPC)this).NPC.damage = 20;
		((ModNPC)this).NPC.lifeMax = 3800;
		((ModNPC)this).NPC.defense = 20;
		((ModNPC)this).NPC.knockBackResist = 0f;
		((ModNPC)this).NPC.boss = true;
		((ModNPC)this).NPC.noGravity = true;
		((ModNPC)this).NPC.noTileCollide = true;
		((ModNPC)this).NPC.HitSound = SoundID.NPCHit7;
		((ModNPC)this).NPC.DeathSound = SoundID.NPCDeath1;
		((ModNPC)this).NPC.value = Item.buyPrice(0, 5);
		((ModNPC)this).NPC.npcSlots = 1f;
		((ModNPC)this).NPC.boss = true;
		((ModNPC)this).NPC.lavaImmune = true;
		((ModNPC)this).NPC.noGravity = true;
		((ModNPC)this).NPC.noTileCollide = true;
		base.bossBag/* tModPorter Note: Removed. Spawn the treasure bag alongside other loot via npcLoot.Add(ItemDropRule.BossBag(type)) */ = ((ModNPC)this).Mod.Find<ModItem>("SquidBag").Type;
		((ModNPC)this).NPC.buffImmune[24] = true;
		base.Music = ((ModNPC)this).Mod.GetSoundSlot((SoundType)51, "Sounds/Music/ZephyrSquid");
		((ModNPC)this).NPC.netAlways = true;
		((ModNPC)this).NPC.aiStyle = -1;
		players = 1;
	}

	public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
	{
		players = numPlayers;
		((ModNPC)this).NPC.lifeMax = 4300 + numPlayers * 430;
		((ModNPC)this).NPC.damage = 35;
		((ModNPC)this).NPC.defense = 30;
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		if (((ModNPC)this).NPC.velocity != Vector2.Zero)
		{
			Vector2 vector = new Vector2((float)ModContent.GetTexture("Ultranium/NPCs/Ocean/ZephyrSquidTrail").Width * 0.2f, (float)((ModNPC)this).NPC.height * 0.2f);
			for (int i = 0; i < ((ModNPC)this).NPC.oldPos.Length; i++)
			{
				Vector2 position = ((ModNPC)this).NPC.oldPos[i] - Main.screenPosition + vector + new Vector2(0f, ((ModNPC)this).NPC.gfxOffY);
				Color color = ((ModNPC)this).NPC.GetAlpha(drawColor) * ((float)(((ModNPC)this).NPC.oldPos.Length - i) / (float)((ModNPC)this).NPC.oldPos.Length / 2f);
				spriteBatch.Draw(ModContent.GetTexture("Ultranium/NPCs/Ocean/ZephyrSquidTrail"), position, ((ModNPC)this).NPC.frame, color, ((ModNPC)this).NPC.rotation, vector, ((ModNPC)this).NPC.scale, SpriteEffects.None, 0f);
			}
		}
		return true;
	}

	public override bool PreAI()
	{
		((ModNPC)this).NPC.rotation = ((ModNPC)this).NPC.velocity.X * 0.02f;
		Player player = Main.player[((ModNPC)this).NPC.target];
		int num = (Main.expertMode ? 14 : 20);
		if (!((Entity)player).active || player.dead)
		{
			((ModNPC)this).NPC.TargetClosest(faceTarget: false);
			((ModNPC)this).NPC.velocity.Y = -100f;
		}
		((ModNPC)this).NPC.netUpdate = true;
		((ModNPC)this).NPC.TargetClosest();
		if (Main.player[((ModNPC)this).NPC.target].dead || Main.player[((ModNPC)this).NPC.target].dead)
		{
			((ModNPC)this).NPC.velocity.Y = 30f;
			((ModNPC)this).NPC.ai[0] += 1f;
			if (((ModNPC)this).NPC.ai[0] >= 120f)
			{
				((Entity)((ModNPC)this).NPC).active = false;
			}
		}
		if (((ModNPC)this).NPC.ai[0] == 0f)
		{
			if (((ModNPC)this).NPC.Center.X >= player.Center.X && moveSpeed >= -53)
			{
				moveSpeed--;
			}
			else if (((ModNPC)this).NPC.Center.X <= player.Center.X && moveSpeed <= 53)
			{
				moveSpeed++;
			}
			((ModNPC)this).NPC.velocity.X = (float)moveSpeed * 0.09f;
			if (((ModNPC)this).NPC.Center.Y >= player.Center.Y - HomeY && moveSpeedY >= -30)
			{
				moveSpeedY--;
				HomeY = 150f;
			}
			else if (((ModNPC)this).NPC.Center.Y <= player.Center.Y - HomeY && moveSpeedY <= 30)
			{
				moveSpeedY++;
			}
			((ModNPC)this).NPC.velocity.Y = (float)moveSpeedY * 0.1f;
		}
		if (((ModNPC)this).NPC.ai[0] == 1f)
		{
			if (((ModNPC)this).NPC.Center.X >= player.Center.X && moveSpeed >= -100)
			{
				moveSpeed--;
			}
			else if (((ModNPC)this).NPC.Center.X <= player.Center.X && moveSpeed <= 100)
			{
				moveSpeed++;
			}
			((ModNPC)this).NPC.velocity.X = (float)moveSpeed * 0.09f;
			if (((ModNPC)this).NPC.Center.Y >= player.Center.Y - HomeY && moveSpeedY >= -30)
			{
				moveSpeedY--;
				HomeY = 150f;
			}
			else if (((ModNPC)this).NPC.Center.Y <= player.Center.Y - HomeY && moveSpeedY <= 30)
			{
				moveSpeedY++;
			}
			((ModNPC)this).NPC.velocity.Y = (float)moveSpeedY * 0.1f;
		}
		if (((ModNPC)this).NPC.ai[0] == 2f)
		{
			((ModNPC)this).NPC.velocity.X *= 0f;
			((ModNPC)this).NPC.velocity.Y = -13f;
		}
		if (((ModNPC)this).NPC.ai[0] == 3f)
		{
			((ModNPC)this).NPC.velocity *= 0f;
		}
		timer++;
		if (timer == 100 || timer == 200 || timer == 300 || timer == 400 || timer == 500)
		{
			SoundEngine.PlaySound(SoundID.Item112, ((ModNPC)this).NPC.position);
			Vector2 vector = Main.player[((ModNPC)this).NPC.target].Center - ((ModNPC)this).NPC.Center;
			vector.Normalize();
			vector.X *= 5f;
			vector.Y *= 5f;
			int num2 = Main.rand.Next(2, 4);
			for (int i = 0; i < num2; i++)
			{
				float num3 = (float)Main.rand.Next(-100, 100) * 0.01f;
				Projectile.NewProjectile(((ModNPC)this).NPC.Center.X, ((ModNPC)this).NPC.Center.Y, vector.X + num3, vector.Y + num3, ((ModNPC)this).Mod.Find<ModProjectile>("Bubble").Type, num, 1f, Main.myPlayer, 0f, 0f);
			}
		}
		if (timer == 600)
		{
			((ModNPC)this).NPC.ai[0] = 1f;
		}
		if (timer == 630 || timer == 660 || timer == 690 || timer == 720 || timer == 750 || timer == 780 || timer == 810 || timer == 840)
		{
			SoundEngine.PlaySound(SoundID.Item111, ((ModNPC)this).NPC.position);
			float num4 = 6f;
			int num5 = ((ModNPC)this).Mod.Find<ModProjectile>("InkGlob").Type;
			float num6 = (float)Math.Atan2(((ModNPC)this).NPC.Center.Y - player.Center.Y, ((ModNPC)this).NPC.Center.X - player.Center.X);
			Projectile.NewProjectile(((ModNPC)this).NPC.Center.X, ((ModNPC)this).NPC.Center.Y, (float)(Math.Cos(num6) * (double)num4 * -1.0), (float)(Math.Sin(num6) * (double)num4 * -1.0), num5, num, 0f, Main.myPlayer, 0f, 0f);
		}
		if (timer == 960)
		{
			for (int j = 0; j < 60; j++)
			{
				int num7 = Dust.NewDust(((ModNPC)this).NPC.position, ((ModNPC)this).NPC.width, ((ModNPC)this).NPC.height, 191);
				Main.dust[num7].scale = 1.5f;
			}
			Vector2 spinningpoint = new Vector2(8f, 0f).RotatedByRandom(Math.PI * 2.0);
			Vector2 spinningpoint2 = new Vector2(3f, 0f).RotatedByRandom(Math.PI * 2.0);
			for (int k = 0; k < 10; k++)
			{
				Vector2 vector2 = spinningpoint.RotatedBy(Math.PI * ((double)k + Main.rand.NextDouble() - 0.5));
				Projectile.NewProjectile(((ModNPC)this).NPC.Center, vector2, ((ModNPC)this).Mod.Find<ModProjectile>("InkCloud").Type, num, 0f, Main.myPlayer, 0f, 0f);
			}
			for (int l = 0; l < 10; l++)
			{
				Vector2 vector3 = spinningpoint2.RotatedBy(Math.PI * ((double)l + Main.rand.NextDouble() - 0.5));
				Projectile.NewProjectile(((ModNPC)this).NPC.Center, vector3, ((ModNPC)this).Mod.Find<ModProjectile>("InkBubble").Type, num, 0f, Main.myPlayer, 0f, 0f);
			}
			((ModNPC)this).NPC.position.X = player.position.X - 100f;
			((ModNPC)this).NPC.position.Y = player.position.Y + 300f;
		}
		if (timer > 960 && timer < 1000)
		{
			((ModNPC)this).NPC.velocity *= 0f;
		}
		if (timer == 980)
		{
			Projectile.NewProjectile(((ModNPC)this).NPC.Center.X, ((ModNPC)this).NPC.Center.Y, 0f, 0f, ((ModNPC)this).Mod.Find<ModProjectile>("SquidChargeTelegraph").Type, 0, 0f, Main.myPlayer, 0f, 0f);
		}
		if (timer == 1000)
		{
			((ModNPC)this).NPC.ai[0] = 2f;
		}
		if (timer == 1060)
		{
			((ModNPC)this).NPC.ai[0] = 3f;
		}
		if (timer == 1100)
		{
			Projectile.NewProjectile(((ModNPC)this).NPC.position.X + 55f, ((ModNPC)this).NPC.position.Y, -4f, -6f, ((ModNPC)this).Mod.Find<ModProjectile>("AquaBall").Type, num, 0.4f, Main.myPlayer, 0f, 0f);
			Projectile.NewProjectile(((ModNPC)this).NPC.position.X + 55f, ((ModNPC)this).NPC.position.Y, -2f, -6f, ((ModNPC)this).Mod.Find<ModProjectile>("AquaBall").Type, num, 0.4f, Main.myPlayer, 0f, 0f);
			Projectile.NewProjectile(((ModNPC)this).NPC.position.X + 55f, ((ModNPC)this).NPC.position.Y, 0f, -6f, ((ModNPC)this).Mod.Find<ModProjectile>("AquaBall").Type, num, 0.4f, Main.myPlayer, 0f, 0f);
			Projectile.NewProjectile(((ModNPC)this).NPC.position.X + 55f, ((ModNPC)this).NPC.position.Y, 2f, -6f, ((ModNPC)this).Mod.Find<ModProjectile>("AquaBall").Type, num, 0.4f, Main.myPlayer, 0f, 0f);
			Projectile.NewProjectile(((ModNPC)this).NPC.position.X + 55f, ((ModNPC)this).NPC.position.Y, 4f, -6f, ((ModNPC)this).Mod.Find<ModProjectile>("AquaBall").Type, num, 0.4f, Main.myPlayer, 0f, 0f);
		}
		if (timer >= 1150)
		{
			timer = 0;
			((ModNPC)this).NPC.ai[0] = 0f;
		}
		if (((ModNPC)this).NPC.life < ((ModNPC)this).NPC.lifeMax / 2 && ((ModNPC)this).NPC.ai[0] < 2f)
		{
			BoltTimer++;
			if (BoltTimer == 300)
			{
				float num8 = 11f;
				float num9 = (float)Math.Atan2(((ModNPC)this).NPC.Center.Y - player.Center.Y, ((ModNPC)this).NPC.Center.X - player.Center.X);
				Projectile.NewProjectile(((ModNPC)this).NPC.Center.X, ((ModNPC)this).NPC.Center.Y, (float)(Math.Cos(num9) * (double)num8 * -1.0), (float)(Math.Sin(num9) * (double)num8 * -1.0), ((ModNPC)this).Mod.Find<ModProjectile>("WaterHelix1").Type, num, 0f, 0, 0f, 0f);
				Projectile.NewProjectile(((ModNPC)this).NPC.Center.X, ((ModNPC)this).NPC.Center.Y, (float)(Math.Cos(num9) * (double)num8 * -1.0), (float)(Math.Sin(num9) * (double)num8 * -1.0), ((ModNPC)this).Mod.Find<ModProjectile>("WaterHelix2").Type, num, 0f, 0, 0f, 0f);
				BoltTimer = 0;
			}
		}
		return true;
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

	public override bool CheckDead()
	{
		Gore.NewGore(((ModNPC)this).NPC.position, ((ModNPC)this).NPC.velocity, ((ModNPC)this).Mod.GetGoreSlot("Gores/SquidGore1"));
		Gore.NewGore(((ModNPC)this).NPC.position, ((ModNPC)this).NPC.velocity, ((ModNPC)this).Mod.GetGoreSlot("Gores/SquidGore2"));
		Gore.NewGore(((ModNPC)this).NPC.position, ((ModNPC)this).NPC.velocity, ((ModNPC)this).Mod.GetGoreSlot("Gores/SquidGore3"));
		Gore.NewGore(((ModNPC)this).NPC.position, ((ModNPC)this).NPC.velocity, ((ModNPC)this).Mod.GetGoreSlot("Gores/SquidGore4"));
		return true;
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
				Item.NewItem((int)((ModNPC)this).NPC.position.X, (int)((ModNPC)this).NPC.position.Y, ((ModNPC)this).NPC.width, ((ModNPC)this).NPC.height, ((ModNPC)this).Mod.Find<ModItem>("ZephyrBlade").Type, 1, false, 0, false, false);
			}
			if (num == 1)
			{
				Item.NewItem((int)((ModNPC)this).NPC.position.X, (int)((ModNPC)this).NPC.position.Y, ((ModNPC)this).NPC.width, ((ModNPC)this).NPC.height, ((ModNPC)this).Mod.Find<ModItem>("ZephyrKnife").Type, 1, false, 0, false, false);
			}
			if (num == 2)
			{
				Item.NewItem((int)((ModNPC)this).NPC.position.X, (int)((ModNPC)this).NPC.position.Y, ((ModNPC)this).NPC.width, ((ModNPC)this).NPC.height, ((ModNPC)this).Mod.Find<ModItem>("ZephyrTrident").Type, 1, false, 0, false, false);
			}
			if (Main.rand.Next(20) == 0)
			{
				Item.NewItem((int)((ModNPC)this).NPC.position.X, (int)((ModNPC)this).NPC.position.Y, ((ModNPC)this).NPC.width, ((ModNPC)this).NPC.height, ((ModNPC)this).Mod.Find<ModItem>("WormPet").Type, 1, false, 0, false, false);
			}
			Item.NewItem(((ModNPC)this).NPC.getRect(), ((ModNPC)this).Mod.Find<ModItem>("OceanScale").Type, Main.rand.Next(8, 12), false, 0, false, false);
		}
		if (Main.rand.Next(7) == 0)
		{
			Item.NewItem(((ModNPC)this).NPC.getRect(), ((ModNPC)this).Mod.Find<ModItem>("SquidMask").Type, 1, false, 0, false, false);
		}
		if (Main.rand.Next(10) == 0)
		{
			Item.NewItem(((ModNPC)this).NPC.getRect(), ((ModNPC)this).Mod.Find<ModItem>("SquidTrophyItem").Type, 1, false, 0, false, false);
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
