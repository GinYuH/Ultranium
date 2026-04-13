using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Ultranium.Items.Ice;

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
		//DisplayName.SetDefault("Zephyr Squid");
		Main.npcFrameCount[NPC.type] = 5;
		NPCID.Sets.TrailCacheLength[NPC.type] = 3;
		NPCID.Sets.TrailingMode[NPC.type] = 0;
	}

	public override void SetDefaults()
	{
		NPC.width = 98;
		NPC.height = 256;
		NPC.damage = 20;
		NPC.lifeMax = 3800;
		NPC.defense = 20;
		NPC.knockBackResist = 0f;
		NPC.boss = true;
		NPC.noGravity = true;
		NPC.noTileCollide = true;
		NPC.HitSound = SoundID.NPCHit7;
		NPC.DeathSound = SoundID.NPCDeath1;
		NPC.value = Item.buyPrice(0, 5);
		NPC.npcSlots = 1f;
		NPC.boss = true;
		NPC.lavaImmune = true;
		NPC.noGravity = true;
		NPC.noTileCollide = true;
		NPC.buffImmune[24] = true;
		base.Music = MusicLoader.GetMusicSlot(Mod, "Sounds/Music/ZephyrSquid");
		NPC.netAlways = true;
		NPC.aiStyle = -1;
		players = 1;
	}

	public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
	{
		players = numPlayers;
		NPC.lifeMax = 4300 + numPlayers * 430;
		NPC.damage = 35;
		NPC.defense = 30;
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		if (NPC.velocity != Vector2.Zero)
		{
			Vector2 vector = new Vector2((float)ModContent.Request<Texture2D>("Ultranium/NPCs/Ocean/ZephyrSquidTrail").Width() * 0.2f, (float)NPC.height * 0.2f);
			for (int i = 0; i < NPC.oldPos.Length; i++)
			{
				Vector2 position = NPC.oldPos[i] - Main.screenPosition + vector + new Vector2(0f, NPC.gfxOffY);
				Color color = NPC.GetAlpha(drawColor) * ((float)(NPC.oldPos.Length - i) / (float)NPC.oldPos.Length / 2f);
				spriteBatch.Draw(ModContent.Request<Texture2D>("Ultranium/NPCs/Ocean/ZephyrSquidTrail").Value, position, NPC.frame, color, NPC.rotation, vector, NPC.scale, SpriteEffects.None, 0f);
			}
		}
		return true;
	}

	public override bool PreAI()
	{
		NPC.rotation = NPC.velocity.X * 0.02f;
		Player player = Main.player[NPC.target];
		int num = (Main.expertMode ? 14 : 20);
		if (!((Entity)player).active || player.dead)
		{
			NPC.TargetClosest(faceTarget: false);
			NPC.velocity.Y = -100f;
		}
		NPC.netUpdate = true;
		NPC.TargetClosest();
		if (Main.player[NPC.target].dead || Main.player[NPC.target].dead)
		{
			NPC.velocity.Y = 30f;
			NPC.ai[0] += 1f;
			if (NPC.ai[0] >= 120f)
			{
				((Entity)NPC).active = false;
			}
		}
		if (NPC.ai[0] == 0f)
		{
			if (NPC.Center.X >= player.Center.X && moveSpeed >= -53)
			{
				moveSpeed--;
			}
			else if (NPC.Center.X <= player.Center.X && moveSpeed <= 53)
			{
				moveSpeed++;
			}
			NPC.velocity.X = (float)moveSpeed * 0.09f;
			if (NPC.Center.Y >= player.Center.Y - HomeY && moveSpeedY >= -30)
			{
				moveSpeedY--;
				HomeY = 150f;
			}
			else if (NPC.Center.Y <= player.Center.Y - HomeY && moveSpeedY <= 30)
			{
				moveSpeedY++;
			}
			NPC.velocity.Y = (float)moveSpeedY * 0.1f;
		}
		if (NPC.ai[0] == 1f)
		{
			if (NPC.Center.X >= player.Center.X && moveSpeed >= -100)
			{
				moveSpeed--;
			}
			else if (NPC.Center.X <= player.Center.X && moveSpeed <= 100)
			{
				moveSpeed++;
			}
			NPC.velocity.X = (float)moveSpeed * 0.09f;
			if (NPC.Center.Y >= player.Center.Y - HomeY && moveSpeedY >= -30)
			{
				moveSpeedY--;
				HomeY = 150f;
			}
			else if (NPC.Center.Y <= player.Center.Y - HomeY && moveSpeedY <= 30)
			{
				moveSpeedY++;
			}
			NPC.velocity.Y = (float)moveSpeedY * 0.1f;
		}
		if (NPC.ai[0] == 2f)
		{
			NPC.velocity.X *= 0f;
			NPC.velocity.Y = -13f;
		}
		if (NPC.ai[0] == 3f)
		{
			NPC.velocity *= 0f;
		}
		timer++;
		if (timer == 100 || timer == 200 || timer == 300 || timer == 400 || timer == 500)
		{
			SoundEngine.PlaySound(SoundID.Item112, NPC.position);
			Vector2 vector = Main.player[NPC.target].Center - NPC.Center;
			vector.Normalize();
			vector.X *= 5f;
			vector.Y *= 5f;
			int num2 = Main.rand.Next(2, 4);
			for (int i = 0; i < num2; i++)
			{
				float num3 = (float)Main.rand.Next(-100, 100) * 0.01f;
				Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, vector.X + num3, vector.Y + num3, Mod.Find<ModProjectile>("Bubble").Type, num, 1f, Main.myPlayer, 0f, 0f);
			}
		}
		if (timer == 600)
		{
			NPC.ai[0] = 1f;
		}
		if (timer == 630 || timer == 660 || timer == 690 || timer == 720 || timer == 750 || timer == 780 || timer == 810 || timer == 840)
		{
			SoundEngine.PlaySound(SoundID.Item111, NPC.position);
			float num4 = 6f;
			int num5 = Mod.Find<ModProjectile>("InkGlob").Type;
			float num6 = (float)Math.Atan2(NPC.Center.Y - player.Center.Y, NPC.Center.X - player.Center.X);
			Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, (float)(Math.Cos(num6) * (double)num4 * -1.0), (float)(Math.Sin(num6) * (double)num4 * -1.0), num5, num, 0f, Main.myPlayer, 0f, 0f);
		}
		if (timer == 960)
		{
			for (int j = 0; j < 60; j++)
			{
				int num7 = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.SpookyWood);
				Main.dust[num7].scale = 1.5f;
			}
			Vector2 spinningpoint = new Vector2(8f, 0f).RotatedByRandom(Math.PI * 2.0);
			Vector2 spinningpoint2 = new Vector2(3f, 0f).RotatedByRandom(Math.PI * 2.0);
			for (int k = 0; k < 10; k++)
			{
				Vector2 vector2 = spinningpoint.RotatedBy(Math.PI * ((double)k + Main.rand.NextDouble() - 0.5));
				Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, vector2, Mod.Find<ModProjectile>("InkCloud").Type, num, 0f, Main.myPlayer, 0f, 0f);
			}
			for (int l = 0; l < 10; l++)
			{
				Vector2 vector3 = spinningpoint2.RotatedBy(Math.PI * ((double)l + Main.rand.NextDouble() - 0.5));
				Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, vector3, Mod.Find<ModProjectile>("InkBubble").Type, num, 0f, Main.myPlayer, 0f, 0f);
			}
			NPC.position.X = player.position.X - 100f;
			NPC.position.Y = player.position.Y + 300f;
		}
		if (timer > 960 && timer < 1000)
		{
			NPC.velocity *= 0f;
		}
		if (timer == 980)
		{
			Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("SquidChargeTelegraph").Type, 0, 0f, Main.myPlayer, 0f, 0f);
		}
		if (timer == 1000)
		{
			NPC.ai[0] = 2f;
		}
		if (timer == 1060)
		{
			NPC.ai[0] = 3f;
		}
		if (timer == 1100)
		{
			Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.position.X + 55f, NPC.position.Y, -4f, -6f, Mod.Find<ModProjectile>("AquaBall").Type, num, 0.4f, Main.myPlayer, 0f, 0f);
			Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.position.X + 55f, NPC.position.Y, -2f, -6f, Mod.Find<ModProjectile>("AquaBall").Type, num, 0.4f, Main.myPlayer, 0f, 0f);
			Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.position.X + 55f, NPC.position.Y, 0f, -6f, Mod.Find<ModProjectile>("AquaBall").Type, num, 0.4f, Main.myPlayer, 0f, 0f);
			Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.position.X + 55f, NPC.position.Y, 2f, -6f, Mod.Find<ModProjectile>("AquaBall").Type, num, 0.4f, Main.myPlayer, 0f, 0f);
			Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.position.X + 55f, NPC.position.Y, 4f, -6f, Mod.Find<ModProjectile>("AquaBall").Type, num, 0.4f, Main.myPlayer, 0f, 0f);
		}
		if (timer >= 1150)
		{
			timer = 0;
			NPC.ai[0] = 0f;
		}
		if (NPC.life < NPC.lifeMax / 2 && NPC.ai[0] < 2f)
		{
			BoltTimer++;
			if (BoltTimer == 300)
			{
				float num8 = 11f;
				float num9 = (float)Math.Atan2(NPC.Center.Y - player.Center.Y, NPC.Center.X - player.Center.X);
				Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, (float)(Math.Cos(num9) * (double)num8 * -1.0), (float)(Math.Sin(num9) * (double)num8 * -1.0), Mod.Find<ModProjectile>("WaterHelix1").Type, num, 0f, 0, 0f, 0f);
				Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, (float)(Math.Cos(num9) * (double)num8 * -1.0), (float)(Math.Sin(num9) * (double)num8 * -1.0), Mod.Find<ModProjectile>("WaterHelix2").Type, num, 0f, 0, 0f, 0f);
				BoltTimer = 0;
			}
		}
		return true;
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

	public override bool CheckDead()
	{
		Gore.NewGore(null, NPC.position, NPC.velocity, Mod.Find<ModGore>("SquidGore1").Type);
		Gore.NewGore(null, NPC.position, NPC.velocity, Mod.Find<ModGore>("SquidGore2").Type);
		Gore.NewGore(null, NPC.position, NPC.velocity, Mod.Find<ModGore>("SquidGore3").Type);
		Gore.NewGore(null, NPC.position, NPC.velocity, Mod.Find<ModGore>("SquidGore4").Type);
		return true;
	}

    public override void ModifyNPCLoot(NPCLoot npcLoot)
    {
        npcLoot.Add(ItemDropRule.BossBag(Mod.Find<ModItem>("SquidBag").Type));
		npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), Mod.Find<ModItem>("OceanScale").Type, 1, 8, 11));
        LeadingConditionRule notExpert = new LeadingConditionRule(new Conditions.NotExpert());
        notExpert.OnSuccess(ItemDropRule.OneFromOptions(1, Mod.Find<ModItem>("ZephyrBlade").Type, Mod.Find<ModItem>("ZephyrKnife").Type, Mod.Find<ModItem>("ZephyrTrident").Type));
        npcLoot.Add(notExpert);
		npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), Mod.Find<ModItem>("WormPet").Type, 20));
		npcLoot.Add(ItemDropRule.Common(Mod.Find<ModItem>("SquidMask").Type, 7));
		npcLoot.Add(ItemDropRule.Common(Mod.Find<ModItem>("SquidTrophyItem").Type, 10));

    }

	public override void OnKill()
	{
		if (!UltraniumWorld.downedSquid)
		{
			UltraniumWorld.downedSquid = true;
			if (Main.netMode == NetmodeID.Server)
			{
				NetMessage.SendData(MessageID.WorldData);
			}
		}
	}

	public override void BossLoot(ref int potionType)
	{
		potionType = 28;
	}
}
