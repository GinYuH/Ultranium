using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
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
		// ((ModNPC)this).DisplayName.SetDefault("Dread");
		Main.npcFrameCount[((ModNPC)this).NPC.type] = 4;
	}

	public override void SetDefaults()
	{
		((ModNPC)this).NPC.width = 250;
		((ModNPC)this).NPC.height = 190;
		((ModNPC)this).NPC.scale = 1.2f;
		((ModNPC)this).NPC.lifeMax = 18500;
		((ModNPC)this).NPC.damage = 60;
		((ModNPC)this).NPC.defense = 65;
		((ModNPC)this).NPC.knockBackResist = 0f;
		((ModNPC)this).NPC.boss = true;
		((ModNPC)this).NPC.lavaImmune = true;
		((ModNPC)this).NPC.noGravity = true;
		((ModNPC)this).NPC.noTileCollide = true;
		((ModNPC)this).NPC.netAlways = true;
		((ModNPC)this).NPC.HitSound = SoundID.NPCHit7;
		((ModNPC)this).NPC.DeathSound = SoundID.NPCDeath60;
		((ModNPC)this).NPC.value = Item.buyPrice(0, 15);
		((ModNPC)this).NPC.npcSlots = 1f;
		base.Music = ((ModNPC)this).Mod.GetSoundSlot((SoundType)51, "Sounds/Music/Dread");
		base.bossBag/* tModPorter Note: Removed. Spawn the treasure bag alongside other loot via npcLoot.Add(ItemDropRule.BossBag(type)) */ = ((ModNPC)this).Mod.Find<ModItem>("DreadBag").Type;
		((ModNPC)this).NPC.aiStyle = -1;
		players = 1;
	}

	public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
	{
		players = numPlayers;
		((ModNPC)this).NPC.lifeMax = 22000 + numPlayers * 2200;
		((ModNPC)this).NPC.damage = 75;
		((ModNPC)this).NPC.defense = 70;
	}

	public override void BossHeadRotation(ref float rotation)
	{
		rotation = ((ModNPC)this).NPC.rotation;
	}

	public override void ModifyHitByProjectile(Projectile projectile, ref NPC.HitModifiers modifiers)
	{
		if (projectile.type == 92 || projectile.type == 91)
		{
			damage /= 3;
		}
	}

	public override void AI()
	{
		player = Main.player[((ModNPC)this).NPC.target];
		Vector2 vector = Main.player[((ModNPC)this).NPC.target].Center - ((ModNPC)this).NPC.Center;
		vector.Normalize();
		int num = (Main.expertMode ? 25 : 45);
		if (!((Entity)player).active || player.dead || Main.dayTime)
		{
			((ModNPC)this).NPC.TargetClosest(faceTarget: false);
			player = Main.player[((ModNPC)this).NPC.target];
			if (!((Entity)player).active || player.dead || Main.dayTime)
			{
				((ModNPC)this).NPC.velocity = new Vector2(0f, -10f);
				if (((ModNPC)this).NPC.timeLeft > 180)
				{
					((ModNPC)this).NPC.timeLeft = 180;
				}
				return;
			}
		}
		float num2 = (float)Math.Atan2(vector.Y, vector.X);
		if (timer > 120)
		{
			Vector2 vector2 = new Vector2(((ModNPC)this).NPC.Center.X, ((ModNPC)this).NPC.Center.Y);
			float num3 = Main.player[((ModNPC)this).NPC.target].Center.X - vector2.X;
			float num4 = Main.player[((ModNPC)this).NPC.target].Center.Y - vector2.Y;
			((ModNPC)this).NPC.rotation = (float)Math.Atan2(num4, num3) + 4.71f;
		}
		timer++;
		if (timer < 120)
		{
			((ModNPC)this).NPC.velocity *= 0f;
			((ModNPC)this).NPC.rotation += RotationAccel;
		}
		if ((timer > 180 && timer < 480) || (timer > 1360 && timer < 1420))
		{
			((ModNPC)this).NPC.velocity *= 0.988f;
		}
		if (timer == 180 || timer == 240 || timer == 300 || timer == 360 || timer == 420 || timer == 1360 || timer == 1865 || timer == 2010)
		{
			Vector2 vector3 = new Vector2(((ModNPC)this).NPC.position.X + (float)((ModNPC)this).NPC.width * 0.5f, ((ModNPC)this).NPC.position.Y + (float)((ModNPC)this).NPC.height * 0.5f);
			num2 = (float)Math.Atan2(vector3.Y - (Main.player[((ModNPC)this).NPC.target].position.Y + (float)Main.player[((ModNPC)this).NPC.target].height * 0.5f), vector3.X - (Main.player[((ModNPC)this).NPC.target].position.X + (float)Main.player[((ModNPC)this).NPC.target].width * 0.5f));
			((ModNPC)this).NPC.velocity.X = (float)(Math.Cos(num2) * 15.0) * -1f;
			((ModNPC)this).NPC.velocity.Y = (float)(Math.Sin(num2) * 15.0) * -1f;
			int num5 = 45;
			for (int i = 0; i < num5; i++)
			{
				Vector2 vector4 = (Vector2.One * new Vector2((float)((ModNPC)this).NPC.width / 7f, (float)((ModNPC)this).NPC.height / 7f) * 0.75f * 0.5f).RotatedBy((float)(i - (num5 / 2 - 1)) * ((float)Math.PI * 2f) / (float)num5) + ((ModNPC)this).NPC.Center;
				Vector2 vector5 = vector4 - ((ModNPC)this).NPC.Center;
				Dust obj = Main.dust[Dust.NewDust(vector4 + vector5, 0, 0, 90, vector5.X * 2f, vector5.Y * 2f, 100, default(Color), 1.4f)];
				obj.noGravity = true;
				obj.noLight = false;
				obj.velocity = Vector2.Normalize(vector5) * 3f;
				obj.fadeIn = 1.3f;
			}
		}
		if (timer == 1360 || timer == 1865 || timer == 2010)
		{
			Vector2 vector6 = new Vector2(((ModNPC)this).NPC.position.X + (float)((ModNPC)this).NPC.width * 0.5f, ((ModNPC)this).NPC.position.Y + (float)((ModNPC)this).NPC.height * 0.5f);
			num2 = (float)Math.Atan2(vector6.Y - (Main.player[((ModNPC)this).NPC.target].position.Y + (float)Main.player[((ModNPC)this).NPC.target].height * 0.5f), vector6.X - (Main.player[((ModNPC)this).NPC.target].position.X + (float)Main.player[((ModNPC)this).NPC.target].width * 0.5f));
			((ModNPC)this).NPC.velocity.X = (float)(Math.Cos(num2) * 19.0) * -1f;
			((ModNPC)this).NPC.velocity.Y = (float)(Math.Sin(num2) * 19.0) * -1f;
			int num6 = 45;
			for (int j = 0; j < num6; j++)
			{
				Vector2 vector7 = (Vector2.One * new Vector2((float)((ModNPC)this).NPC.width / 7f, (float)((ModNPC)this).NPC.height / 7f) * 0.75f * 0.5f).RotatedBy((float)(j - (num6 / 2 - 1)) * ((float)Math.PI * 2f) / (float)num6) + ((ModNPC)this).NPC.Center;
				Vector2 vector8 = vector7 - ((ModNPC)this).NPC.Center;
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
			Vector2 vector9 = Main.player[((ModNPC)this).NPC.target].Center - ((ModNPC)this).NPC.Center;
			vector9.Normalize();
			vector9.X *= 4f;
			vector9.Y *= 4f;
			int num7 = 1;
			for (int k = 0; k < num7; k++)
			{
				float num8 = (float)Main.rand.Next(-100, 100) * 0.01f;
				float num9 = (float)Main.rand.Next(-100, 100) * 0.01f;
				Projectile.NewProjectile(((ModNPC)this).NPC.Center.X, ((ModNPC)this).NPC.Center.Y, vector9.X + num8, vector9.Y + num9, ((ModNPC)this).Mod.Find<ModProjectile>("DreadFlames").Type, num, 1f, Main.myPlayer, 0f, 0f);
				if (Main.rand.Next(3) == 0)
				{
					SoundEngine.PlaySound(SoundID.DD2_BetsyFlameBreath, ((ModNPC)this).NPC.position);
				}
			}
		}
		if ((timer > 800 && timer < 1360) || (timer > 1420 && timer < 1740) || timer >= 2070)
		{
			Move(new Vector2(0f, 0f));
		}
		if (timer == 860 || timer == 920 || timer == 980 || timer == 1040)
		{
			Vector2 vector10 = Main.player[((ModNPC)this).NPC.target].Center - ((ModNPC)this).NPC.Center;
			vector10.Normalize();
			vector10.X *= 8.5f;
			vector10.Y *= 8.5f;
			int num10 = Main.rand.Next(3, 3);
			for (int l = 0; l < num10; l++)
			{
				float num11 = (float)Main.rand.Next(-300, 300) * 0.01f;
				Projectile.NewProjectile(((ModNPC)this).NPC.Center.X, ((ModNPC)this).NPC.Center.Y, vector10.X + num11, vector10.Y + num11, ((ModNPC)this).Mod.Find<ModProjectile>("DreadSpit").Type, num, 1f, Main.myPlayer, 0f, 0f);
			}
		}
		if ((timer >= 1120 && timer <= 1140) || (timer >= 1160 && timer <= 1180) || (timer >= 1200 && timer <= 1220))
		{
			float num12 = 8f;
			int num13 = ((ModNPC)this).Mod.Find<ModProjectile>("DreadBolt").Type;
			SoundEngine.PlaySound(SoundID.Item20, new Vector2(((ModNPC)this).NPC.position.X, ((ModNPC)this).NPC.position.Y));
			num2 = (float)Math.Atan2(((ModNPC)this).NPC.Center.Y - player.Center.Y, ((ModNPC)this).NPC.Center.X - player.Center.X);
			Projectile.NewProjectile(((ModNPC)this).NPC.Center.X, ((ModNPC)this).NPC.Center.Y, (float)(Math.Cos(num2) * (double)num12 * -1.0), (float)(Math.Sin(num2) * (double)num12 * -1.0), num13, num, 0f, Main.myPlayer, 0f, 0f);
		}
		if (timer == 1480 || timer == 1540 || timer == 1600 || timer == 1660 || timer == 1720)
		{
			float num14 = 5f;
			int num15 = ((ModNPC)this).Mod.Find<ModProjectile>("ToothBall2").Type;
			SoundEngine.PlaySound(SoundID.NPCDeath13, ((ModNPC)this).NPC.position);
			num2 = (float)Math.Atan2(((ModNPC)this).NPC.Center.Y - player.Center.Y, ((ModNPC)this).NPC.Center.X - player.Center.X);
			Projectile.NewProjectile(((ModNPC)this).NPC.Center.X, ((ModNPC)this).NPC.Center.Y, (float)(Math.Cos(num2) * (double)num14 * -1.0), (float)(Math.Sin(num2) * (double)num14 * -1.0), num15, num, 0f, Main.myPlayer, 0f, 0f);
		}
		if (timer >= 1780 && timer < 1865)
		{
			Move(new Vector2(550f, 0f));
			if (timer == 1815 || timer == 1835 || timer == 1855)
			{
				float num16 = 8.5f;
				int num17 = ((ModNPC)this).Mod.Find<ModProjectile>("DreadBolt").Type;
				SoundEngine.PlaySound(SoundID.Item20, new Vector2(((ModNPC)this).NPC.position.X, ((ModNPC)this).NPC.position.Y));
				num2 = (float)Math.Atan2(((ModNPC)this).NPC.Center.Y - player.Center.Y, ((ModNPC)this).NPC.Center.X - player.Center.X);
				Projectile.NewProjectile(((ModNPC)this).NPC.Center.X, ((ModNPC)this).NPC.Center.Y, (float)(Math.Cos(num2) * (double)num16 * -1.0), (float)(Math.Sin(num2) * (double)num16 * -1.0), num17, num, 0f, Main.myPlayer, 0f, 0f);
			}
		}
		if (timer >= 1925 && timer < 2010)
		{
			Move(new Vector2(-550f, 0f));
			if (timer == 1945 || timer == 1965 || timer == 1985)
			{
				float num18 = 8.5f;
				int num19 = ((ModNPC)this).Mod.Find<ModProjectile>("DreadBolt").Type;
				SoundEngine.PlaySound(SoundID.Item20, new Vector2(((ModNPC)this).NPC.position.X, ((ModNPC)this).NPC.position.Y));
				num2 = (float)Math.Atan2(((ModNPC)this).NPC.Center.Y - player.Center.Y, ((ModNPC)this).NPC.Center.X - player.Center.X);
				Projectile.NewProjectile(((ModNPC)this).NPC.Center.X, ((ModNPC)this).NPC.Center.Y, (float)(Math.Cos(num2) * (double)num18 * -1.0), (float)(Math.Sin(num2) * (double)num18 * -1.0), num19, num, 0f, Main.myPlayer, 0f, 0f);
			}
		}
		if (timer == 2070 || timer == 2130)
		{
			int num20 = 6;
			for (int m = 0; m < num20; m++)
			{
				int num21 = 360 / num20;
				NPC.NewNPC((int)((ModNPC)this).NPC.Center.X, (int)((ModNPC)this).NPC.Center.Y, ((ModNPC)this).Mod.Find<ModNPC>("DreadOrbiter").Type, ((ModNPC)this).NPC.whoAmI, (float)(m * num21), (float)((ModNPC)this).NPC.whoAmI, 0f, 0f, 255);
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

	public override void OnKill()
	{
		if (Main.expertMode)
		{
			((ModNPC)this).NPC.DropBossBags();
		}
		else
		{
			int num = Main.rand.Next(4);
			if (num == 0)
			{
				Item.NewItem((int)((ModNPC)this).NPC.position.X, (int)((ModNPC)this).NPC.position.Y, ((ModNPC)this).NPC.width, ((ModNPC)this).NPC.height, ((ModNPC)this).Mod.Find<ModItem>("DreadSword").Type, 1, false, 0, false, false);
			}
			if (num == 1)
			{
				Item.NewItem((int)((ModNPC)this).NPC.position.X, (int)((ModNPC)this).NPC.position.Y, ((ModNPC)this).NPC.width, ((ModNPC)this).NPC.height, ((ModNPC)this).Mod.Find<ModItem>("DreadBow").Type, 1, false, 0, false, false);
			}
			if (num == 2)
			{
				Item.NewItem((int)((ModNPC)this).NPC.position.X, (int)((ModNPC)this).NPC.position.Y, ((ModNPC)this).NPC.width, ((ModNPC)this).NPC.height, ((ModNPC)this).Mod.Find<ModItem>("DreadStaff").Type, 1, false, 0, false, false);
			}
			if (num == 3)
			{
				Item.NewItem((int)((ModNPC)this).NPC.position.X, (int)((ModNPC)this).NPC.position.Y, ((ModNPC)this).NPC.width, ((ModNPC)this).NPC.height, ((ModNPC)this).Mod.Find<ModItem>("DreadSummon").Type, 1, false, 0, false, false);
			}
			if (Main.rand.Next(3) == 0)
			{
				Item.NewItem(((ModNPC)this).NPC.getRect(), ((ModNPC)this).Mod.Find<ModItem>("DreadTooth").Type, 1, false, 0, false, false);
			}
			if (Main.rand.Next(15) == 0)
			{
				Item.NewItem(((ModNPC)this).NPC.getRect(), ((ModNPC)this).Mod.Find<ModItem>("DreadBreadItem").Type, 1, false, 0, false, false);
			}
			Item.NewItem(((ModNPC)this).NPC.getRect(), ((ModNPC)this).Mod.Find<ModItem>("DreadFlame").Type, Main.rand.Next(10, 18), false, 0, false, false);
			Item.NewItem(((ModNPC)this).NPC.getRect(), ((ModNPC)this).Mod.Find<ModItem>("DreadScale").Type, Main.rand.Next(6, 12), false, 0, false, false);
		}
		if (Main.rand.Next(7) == 0)
		{
			Item.NewItem(((ModNPC)this).NPC.getRect(), ((ModNPC)this).Mod.Find<ModItem>("DreadMask").Type, 1, false, 0, false, false);
		}
		if (Main.rand.Next(10) == 0)
		{
			Item.NewItem(((ModNPC)this).NPC.getRect(), ((ModNPC)this).Mod.Find<ModItem>("DreadTrophyItem").Type, 1, false, 0, false, false);
		}
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
			int num = Dust.NewDust(((ModNPC)this).NPC.position, ((ModNPC)this).NPC.width, ((ModNPC)this).NPC.height, 90, 0f, -2f, 0, default(Color), 1.5f);
			Main.dust[num].noGravity = false;
			Main.dust[num].position.X += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
			Main.dust[num].position.Y += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
			if (Main.dust[num].position != ((ModNPC)this).NPC.Center)
			{
				Main.dust[num].velocity = ((ModNPC)this).NPC.DirectionTo(Main.dust[num].position) * 3f;
			}
		}
		for (int j = 0; j < 80; j++)
		{
			int num2 = Dust.NewDust(((ModNPC)this).NPC.position, ((ModNPC)this).NPC.width, ((ModNPC)this).NPC.height, 90, 0f, -2f, 0, default(Color), 2f);
			Main.dust[num2].noGravity = false;
			Main.dust[num2].position.X += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
			Main.dust[num2].position.Y += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
			if (Main.dust[num2].position != ((ModNPC)this).NPC.Center)
			{
				Main.dust[num2].velocity = ((ModNPC)this).NPC.DirectionTo(Main.dust[num2].position) * 8f;
			}
		}
		return true;
	}

	public override void BossLoot(ref string name, ref int potionType)
	{
		potionType = 499;
	}

	public override void FindFrame(int frameHeight)
	{
		((ModNPC)this).NPC.frameCounter += 1.0;
		if (((ModNPC)this).NPC.frameCounter >= 9.0)
		{
			((ModNPC)this).NPC.frame.Y = (((ModNPC)this).NPC.frame.Y + frameHeight) % (Main.npcFrameCount[((ModNPC)this).NPC.type] * frameHeight);
			((ModNPC)this).NPC.frameCounter = 1.0;
		}
	}
}
