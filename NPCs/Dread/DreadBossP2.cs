using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.NPCs.Dread;

[AutoloadBossHead]
public class DreadBossP2 : ModNPC
{
	private Player player;

	private float speed;

	private float RotationAccel = 0.5f;

	private int timer;

	public int players;

	public override void SetStaticDefaults()
	{
		// DisplayName.SetDefault("Dread");
		Main.npcFrameCount[NPC.type] = 4;
	}

	public override void SetDefaults()
	{
		NPC.width = 250;
		NPC.height = 190;
		NPC.scale = 1.2f;
		NPC.lifeMax = 18500;
		NPC.damage = 60;
		NPC.defense = 65;
		NPC.knockBackResist = 0f;
		NPC.boss = true;
		NPC.lavaImmune = true;
		NPC.noGravity = true;
		NPC.noTileCollide = true;
		NPC.netAlways = true;
		NPC.HitSound = SoundID.NPCHit7;
		NPC.DeathSound = SoundID.NPCDeath60;
		NPC.value = Item.buyPrice(0, 15);
		NPC.npcSlots = 1f;
		base.Music = MusicLoader.GetMusicSlot(Mod, "Sounds/Music/Dread");
		NPC.aiStyle = -1;
		players = 1;
	}

	public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
	{
		players = numPlayers;
		NPC.lifeMax = 22000 + numPlayers * 2200;
		NPC.damage = 75;
		NPC.defense = 70;
	}

	public override void BossHeadRotation(ref float rotation)
	{
		rotation = NPC.rotation;
	}

	public override void ModifyHitByProjectile(Projectile projectile, ref NPC.HitModifiers modifiers)
	{
		if (projectile.type == 92 || projectile.type == 91)
		{
			modifiers.SourceDamage /= 3;
		}
	}

	public override void AI()
	{
		player = Main.player[NPC.target];
		Vector2 vector = Main.player[NPC.target].Center - NPC.Center;
		vector.Normalize();
		int num = (Main.expertMode ? 25 : 45);
		if (!((Entity)player).active || player.dead || Main.dayTime)
		{
			NPC.TargetClosest(faceTarget: false);
			player = Main.player[NPC.target];
			if (!((Entity)player).active || player.dead || Main.dayTime)
			{
				NPC.velocity = new Vector2(0f, -10f);
				if (NPC.timeLeft > 180)
				{
					NPC.timeLeft = 180;
				}
				return;
			}
		}
		float num2 = (float)Math.Atan2(vector.Y, vector.X);
		if (timer > 120)
		{
			Vector2 vector2 = new Vector2(NPC.Center.X, NPC.Center.Y);
			float num3 = Main.player[NPC.target].Center.X - vector2.X;
			float num4 = Main.player[NPC.target].Center.Y - vector2.Y;
			NPC.rotation = (float)Math.Atan2(num4, num3) + 4.71f;
		}
		timer++;
		if (timer < 120)
		{
			NPC.velocity *= 0f;
			NPC.rotation += RotationAccel;
		}
		if ((timer > 180 && timer < 480) || (timer > 1360 && timer < 1420))
		{
			NPC.velocity *= 0.988f;
		}
		if (timer == 180 || timer == 240 || timer == 300 || timer == 360 || timer == 420 || timer == 1360 || timer == 1865 || timer == 2010)
		{
			Vector2 vector3 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
			num2 = (float)Math.Atan2(vector3.Y - (Main.player[NPC.target].position.Y + (float)Main.player[NPC.target].height * 0.5f), vector3.X - (Main.player[NPC.target].position.X + (float)Main.player[NPC.target].width * 0.5f));
			NPC.velocity.X = (float)(Math.Cos(num2) * 15.0) * -1f;
			NPC.velocity.Y = (float)(Math.Sin(num2) * 15.0) * -1f;
			int num5 = 45;
			for (int i = 0; i < num5; i++)
			{
				Vector2 vector4 = (Vector2.One * new Vector2((float)NPC.width / 7f, (float)NPC.height / 7f) * 0.75f * 0.5f).RotatedBy((float)(i - (num5 / 2 - 1)) * ((float)Math.PI * 2f) / (float)num5) + NPC.Center;
				Vector2 vector5 = vector4 - NPC.Center;
				Dust obj = Main.dust[Dust.NewDust(vector4 + vector5, 0, 0, 90, vector5.X * 2f, vector5.Y * 2f, 100, default(Color), 1.4f)];
				obj.noGravity = true;
				obj.noLight = false;
				obj.velocity = Vector2.Normalize(vector5) * 3f;
				obj.fadeIn = 1.3f;
			}
		}
		if (timer == 1360 || timer == 1865 || timer == 2010)
		{
			Vector2 vector6 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
			num2 = (float)Math.Atan2(vector6.Y - (Main.player[NPC.target].position.Y + (float)Main.player[NPC.target].height * 0.5f), vector6.X - (Main.player[NPC.target].position.X + (float)Main.player[NPC.target].width * 0.5f));
			NPC.velocity.X = (float)(Math.Cos(num2) * 19.0) * -1f;
			NPC.velocity.Y = (float)(Math.Sin(num2) * 19.0) * -1f;
			int num6 = 45;
			for (int j = 0; j < num6; j++)
			{
				Vector2 vector7 = (Vector2.One * new Vector2((float)NPC.width / 7f, (float)NPC.height / 7f) * 0.75f * 0.5f).RotatedBy((float)(j - (num6 / 2 - 1)) * ((float)Math.PI * 2f) / (float)num6) + NPC.Center;
				Vector2 vector8 = vector7 - NPC.Center;
				Dust obj2 = Main.dust[Dust.NewDust(vector7 + vector8, 0, 0, 90, vector8.X * 2f, vector8.Y * 2f, 100, default(Color), 1.4f)];
				obj2.noGravity = true;
				obj2.noLight = false;
				obj2.velocity = Vector2.Normalize(vector8) * 3f;
				obj2.fadeIn = 1.3f;
			}
		}
		if (timer > 480 && timer < 780)
		{
			Move(new Vector2(0f, -360f));
			Vector2 vector9 = Main.player[NPC.target].Center - NPC.Center;
			vector9.Normalize();
			vector9.X *= 4f;
			vector9.Y *= 4f;
			int num7 = 1;
			for (int k = 0; k < num7; k++)
			{
				float num8 = (float)Main.rand.Next(-100, 100) * 0.01f;
				float num9 = (float)Main.rand.Next(-100, 100) * 0.01f;
				Projectile.NewProjectile(null, NPC.Center.X, NPC.Center.Y, vector9.X + num8, vector9.Y + num9, Mod.Find<ModProjectile>("DreadFlames").Type, num, 1f, Main.myPlayer, 0f, 0f);
				if (Main.rand.Next(3) == 0)
				{
					SoundEngine.PlaySound(SoundID.DD2_BetsyFlameBreath, NPC.position);
				}
			}
		}
		if ((timer > 800 && timer < 1360) || (timer > 1420 && timer < 1740) || timer >= 2070)
		{
			Move(new Vector2(0f, 0f));
		}
		if (timer == 860 || timer == 920 || timer == 980 || timer == 1040)
		{
			Vector2 vector10 = Main.player[NPC.target].Center - NPC.Center;
			vector10.Normalize();
			vector10.X *= 8.5f;
			vector10.Y *= 8.5f;
			int num10 = Main.rand.Next(3, 3);
			for (int l = 0; l < num10; l++)
			{
				float num11 = (float)Main.rand.Next(-300, 300) * 0.01f;
				Projectile.NewProjectile(null, NPC.Center.X, NPC.Center.Y, vector10.X + num11, vector10.Y + num11, Mod.Find<ModProjectile>("DreadSpit").Type, num, 1f, Main.myPlayer, 0f, 0f);
			}
		}
		if ((timer >= 1120 && timer <= 1140) || (timer >= 1160 && timer <= 1180) || (timer >= 1200 && timer <= 1220))
		{
			float num12 = 8f;
			int num13 = Mod.Find<ModProjectile>("DreadBolt").Type;
			SoundEngine.PlaySound(SoundID.Item20, new Vector2(NPC.position.X, NPC.position.Y));
			num2 = (float)Math.Atan2(NPC.Center.Y - player.Center.Y, NPC.Center.X - player.Center.X);
			Projectile.NewProjectile(null, NPC.Center.X, NPC.Center.Y, (float)(Math.Cos(num2) * (double)num12 * -1.0), (float)(Math.Sin(num2) * (double)num12 * -1.0), num13, num, 0f, Main.myPlayer, 0f, 0f);
		}
		if (timer == 1480 || timer == 1540 || timer == 1600 || timer == 1660 || timer == 1720)
		{
			float num14 = 5f;
			int num15 = Mod.Find<ModProjectile>("ToothBall2").Type;
			SoundEngine.PlaySound(SoundID.NPCDeath13, NPC.position);
			num2 = (float)Math.Atan2(NPC.Center.Y - player.Center.Y, NPC.Center.X - player.Center.X);
			Projectile.NewProjectile(null, NPC.Center.X, NPC.Center.Y, (float)(Math.Cos(num2) * (double)num14 * -1.0), (float)(Math.Sin(num2) * (double)num14 * -1.0), num15, num, 0f, Main.myPlayer, 0f, 0f);
		}
		if (timer >= 1780 && timer < 1865)
		{
			Move(new Vector2(550f, 0f));
			if (timer == 1815 || timer == 1835 || timer == 1855)
			{
				float num16 = 8.5f;
				int num17 = Mod.Find<ModProjectile>("DreadBolt").Type;
				SoundEngine.PlaySound(SoundID.Item20, new Vector2(NPC.position.X, NPC.position.Y));
				num2 = (float)Math.Atan2(NPC.Center.Y - player.Center.Y, NPC.Center.X - player.Center.X);
				Projectile.NewProjectile(null, NPC.Center.X, NPC.Center.Y, (float)(Math.Cos(num2) * (double)num16 * -1.0), (float)(Math.Sin(num2) * (double)num16 * -1.0), num17, num, 0f, Main.myPlayer, 0f, 0f);
			}
		}
		if (timer >= 1925 && timer < 2010)
		{
			Move(new Vector2(-550f, 0f));
			if (timer == 1945 || timer == 1965 || timer == 1985)
			{
				float num18 = 8.5f;
				int num19 = Mod.Find<ModProjectile>("DreadBolt").Type;
				SoundEngine.PlaySound(SoundID.Item20, new Vector2(NPC.position.X, NPC.position.Y));
				num2 = (float)Math.Atan2(NPC.Center.Y - player.Center.Y, NPC.Center.X - player.Center.X);
				Projectile.NewProjectile(null, NPC.Center.X, NPC.Center.Y, (float)(Math.Cos(num2) * (double)num18 * -1.0), (float)(Math.Sin(num2) * (double)num18 * -1.0), num19, num, 0f, Main.myPlayer, 0f, 0f);
			}
		}
		if (timer == 2070 || timer == 2130)
		{
			int num20 = 6;
			for (int m = 0; m < num20; m++)
			{
				int num21 = 360 / num20;
				NPC.NewNPC(null, (int)NPC.Center.X, (int)NPC.Center.Y, Mod.Find<ModNPC>("DreadOrbiter").Type, NPC.whoAmI, (float)(m * num21), (float)NPC.whoAmI, 0f, 0f, 255);
			}
		}
		if (timer >= 2190)
		{
			timer = 120;
		}
	}

	private void Move(Vector2 offset)
	{
		if ((timer >= 1780 && timer < 1865) || (timer >= 1925 && timer < 2010))
		{
			speed = 30f;
		}
		else if (timer > 480 && timer < 780)
		{
			speed = 7.5f;
		}
		else
		{
			speed = 6.5f;
		}
		Vector2 vector = player.Center + offset - NPC.Center;
		float num = Magnitude(vector);
		if (num > speed)
		{
			vector *= speed / num;
		}
		float num2 = 10f;
		vector = (NPC.velocity * num2 + vector) / (num2 + 1f);
		num = Magnitude(vector);
		if (num > speed)
		{
			vector *= speed / num;
		}
		NPC.velocity = vector;
	}

	private float Magnitude(Vector2 mag)
	{
		return (float)Math.Sqrt(mag.X * mag.X + mag.Y * mag.Y);
	}

    public override void ModifyNPCLoot(NPCLoot npcLoot)
    {
        npcLoot.Add(ItemDropRule.BossBag(Mod.Find<ModItem>("DreadBag").Type));
		npcLoot.Add(ItemDropRule.Common(Mod.Find<ModItem>("DreadMask").Type, 7));
		npcLoot.Add(ItemDropRule.Common(Mod.Find<ModItem>("DreadTrophyItem").Type, 10));
		npcLoot.Add(new LeadingConditionRule(new Conditions.NotExpert()).OnSuccess(ItemDropRule.OneFromOptions(1, Mod.Find<ModItem>("DreadSword").Type, Mod.Find<ModItem>("DreadBow").Type, Mod.Find<ModItem>("DreadStaff").Type, Mod.Find<ModItem>("DreadSummon").Type)));
		npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), Mod.Find<ModItem>("DreadTooth").Type, 3));
		npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), Mod.Find<ModItem>("DreadBreadItem").Type, 15));
		npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), Mod.Find<ModItem>("DreadFlame").Type, 1, 10, 17));
		npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), Mod.Find<ModItem>("DreadScale").Type, 1, 6, 11));
    }

	public override void OnKill()
	{
		if (!UltraniumWorld.downedDread)
		{
			UltraniumWorld.downedDread = true;
			if (Main.netMode == 2)
			{
				NetMessage.SendData(7);
			}
		}
	}

	public override bool CheckDead()
	{
		for (int i = 0; i < 50; i++)
		{
			int num = Dust.NewDust(NPC.position, NPC.width, NPC.height, 90, 0f, -2f, 0, default(Color), 1.5f);
			Main.dust[num].noGravity = false;
			Main.dust[num].position.X += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
			Main.dust[num].position.Y += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
			if (Main.dust[num].position != NPC.Center)
			{
				Main.dust[num].velocity = NPC.DirectionTo(Main.dust[num].position) * 3f;
			}
		}
		for (int j = 0; j < 80; j++)
		{
			int num2 = Dust.NewDust(NPC.position, NPC.width, NPC.height, 90, 0f, -2f, 0, default(Color), 2f);
			Main.dust[num2].noGravity = false;
			Main.dust[num2].position.X += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
			Main.dust[num2].position.Y += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
			if (Main.dust[num2].position != NPC.Center)
			{
				Main.dust[num2].velocity = NPC.DirectionTo(Main.dust[num2].position) * 8f;
			}
		}
		return true;
	}

	public override void BossLoot(ref int potionType)
	{
		potionType = 499;
	}

	public override void FindFrame(int frameHeight)
	{
		NPC.frameCounter += 1.0;
		if (NPC.frameCounter >= 9.0)
		{
			NPC.frame.Y = (NPC.frame.Y + frameHeight) % (Main.npcFrameCount[NPC.type] * frameHeight);
			NPC.frameCounter = 1.0;
		}
	}
}
