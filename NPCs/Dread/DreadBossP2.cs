using System;
using Microsoft.Xna.Framework;
using Terraria;
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
		((ModNPC)this).DisplayName.SetDefault("Dread");
		Main.npcFrameCount[((ModNPC)this).npc.type] = 4;
	}

	public override void SetDefaults()
	{
		((ModNPC)this).npc.width = 250;
		((ModNPC)this).npc.height = 190;
		((ModNPC)this).npc.scale = 1.2f;
		((ModNPC)this).npc.lifeMax = 18500;
		((ModNPC)this).npc.damage = 60;
		((ModNPC)this).npc.defense = 65;
		((ModNPC)this).npc.knockBackResist = 0f;
		((ModNPC)this).npc.boss = true;
		((ModNPC)this).npc.lavaImmune = true;
		((ModNPC)this).npc.noGravity = true;
		((ModNPC)this).npc.noTileCollide = true;
		((ModNPC)this).npc.netAlways = true;
		((ModNPC)this).npc.HitSound = SoundID.NPCHit7;
		((ModNPC)this).npc.DeathSound = SoundID.NPCDeath60;
		((ModNPC)this).npc.value = Item.buyPrice(0, 15);
		((ModNPC)this).npc.npcSlots = 1f;
		base.music = ((ModNPC)this).mod.GetSoundSlot((SoundType)51, "Sounds/Music/Dread");
		base.bossBag = ((ModNPC)this).mod.ItemType("DreadBag");
		((ModNPC)this).npc.aiStyle = -1;
		players = 1;
	}

	public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
	{
		players = numPlayers;
		((ModNPC)this).npc.lifeMax = 22000 + numPlayers * 2200;
		((ModNPC)this).npc.damage = 75;
		((ModNPC)this).npc.defense = 70;
	}

	public override void BossHeadRotation(ref float rotation)
	{
		rotation = ((ModNPC)this).npc.rotation;
	}

	public override void ModifyHitByProjectile(Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
	{
		if (projectile.type == 92 || projectile.type == 91)
		{
			damage /= 3;
		}
	}

	public override void AI()
	{
		player = Main.player[((ModNPC)this).npc.target];
		Vector2 vector = Main.player[((ModNPC)this).npc.target].Center - ((ModNPC)this).npc.Center;
		vector.Normalize();
		int num = (Main.expertMode ? 25 : 45);
		if (!((Entity)player).active || player.dead || Main.dayTime)
		{
			((ModNPC)this).npc.TargetClosest(faceTarget: false);
			player = Main.player[((ModNPC)this).npc.target];
			if (!((Entity)player).active || player.dead || Main.dayTime)
			{
				((ModNPC)this).npc.velocity = new Vector2(0f, -10f);
				if (((ModNPC)this).npc.timeLeft > 180)
				{
					((ModNPC)this).npc.timeLeft = 180;
				}
				return;
			}
		}
		float num2 = (float)Math.Atan2(vector.Y, vector.X);
		if (timer > 120)
		{
			Vector2 vector2 = new Vector2(((ModNPC)this).npc.Center.X, ((ModNPC)this).npc.Center.Y);
			float num3 = Main.player[((ModNPC)this).npc.target].Center.X - vector2.X;
			float num4 = Main.player[((ModNPC)this).npc.target].Center.Y - vector2.Y;
			((ModNPC)this).npc.rotation = (float)Math.Atan2(num4, num3) + 4.71f;
		}
		timer++;
		if (timer < 120)
		{
			((ModNPC)this).npc.velocity *= 0f;
			((ModNPC)this).npc.rotation += RotationAccel;
		}
		if ((timer > 180 && timer < 480) || (timer > 1360 && timer < 1420))
		{
			((ModNPC)this).npc.velocity *= 0.988f;
		}
		if (timer == 180 || timer == 240 || timer == 300 || timer == 360 || timer == 420 || timer == 1360 || timer == 1865 || timer == 2010)
		{
			Vector2 vector3 = new Vector2(((ModNPC)this).npc.position.X + (float)((ModNPC)this).npc.width * 0.5f, ((ModNPC)this).npc.position.Y + (float)((ModNPC)this).npc.height * 0.5f);
			num2 = (float)Math.Atan2(vector3.Y - (Main.player[((ModNPC)this).npc.target].position.Y + (float)Main.player[((ModNPC)this).npc.target].height * 0.5f), vector3.X - (Main.player[((ModNPC)this).npc.target].position.X + (float)Main.player[((ModNPC)this).npc.target].width * 0.5f));
			((ModNPC)this).npc.velocity.X = (float)(Math.Cos(num2) * 15.0) * -1f;
			((ModNPC)this).npc.velocity.Y = (float)(Math.Sin(num2) * 15.0) * -1f;
			int num5 = 45;
			for (int i = 0; i < num5; i++)
			{
				Vector2 vector4 = (Vector2.One * new Vector2((float)((ModNPC)this).npc.width / 7f, (float)((ModNPC)this).npc.height / 7f) * 0.75f * 0.5f).RotatedBy((float)(i - (num5 / 2 - 1)) * ((float)Math.PI * 2f) / (float)num5) + ((ModNPC)this).npc.Center;
				Vector2 vector5 = vector4 - ((ModNPC)this).npc.Center;
				Dust obj = Main.dust[Dust.NewDust(vector4 + vector5, 0, 0, 90, vector5.X * 2f, vector5.Y * 2f, 100, default(Color), 1.4f)];
				obj.noGravity = true;
				obj.noLight = false;
				obj.velocity = Vector2.Normalize(vector5) * 3f;
				obj.fadeIn = 1.3f;
			}
		}
		if (timer == 1360 || timer == 1865 || timer == 2010)
		{
			Vector2 vector6 = new Vector2(((ModNPC)this).npc.position.X + (float)((ModNPC)this).npc.width * 0.5f, ((ModNPC)this).npc.position.Y + (float)((ModNPC)this).npc.height * 0.5f);
			num2 = (float)Math.Atan2(vector6.Y - (Main.player[((ModNPC)this).npc.target].position.Y + (float)Main.player[((ModNPC)this).npc.target].height * 0.5f), vector6.X - (Main.player[((ModNPC)this).npc.target].position.X + (float)Main.player[((ModNPC)this).npc.target].width * 0.5f));
			((ModNPC)this).npc.velocity.X = (float)(Math.Cos(num2) * 19.0) * -1f;
			((ModNPC)this).npc.velocity.Y = (float)(Math.Sin(num2) * 19.0) * -1f;
			int num6 = 45;
			for (int j = 0; j < num6; j++)
			{
				Vector2 vector7 = (Vector2.One * new Vector2((float)((ModNPC)this).npc.width / 7f, (float)((ModNPC)this).npc.height / 7f) * 0.75f * 0.5f).RotatedBy((float)(j - (num6 / 2 - 1)) * ((float)Math.PI * 2f) / (float)num6) + ((ModNPC)this).npc.Center;
				Vector2 vector8 = vector7 - ((ModNPC)this).npc.Center;
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
			Vector2 vector9 = Main.player[((ModNPC)this).npc.target].Center - ((ModNPC)this).npc.Center;
			vector9.Normalize();
			vector9.X *= 4f;
			vector9.Y *= 4f;
			int num7 = 1;
			for (int k = 0; k < num7; k++)
			{
				float num8 = (float)Main.rand.Next(-100, 100) * 0.01f;
				float num9 = (float)Main.rand.Next(-100, 100) * 0.01f;
				Projectile.NewProjectile(((ModNPC)this).npc.Center.X, ((ModNPC)this).npc.Center.Y, vector9.X + num8, vector9.Y + num9, ((ModNPC)this).mod.ProjectileType("DreadFlames"), num, 1f, Main.myPlayer, 0f, 0f);
				if (Main.rand.Next(3) == 0)
				{
					Main.PlaySound(SoundID.DD2_BetsyFlameBreath, ((ModNPC)this).npc.position);
				}
			}
		}
		if ((timer > 800 && timer < 1360) || (timer > 1420 && timer < 1740) || timer >= 2070)
		{
			Move(new Vector2(0f, 0f));
		}
		if (timer == 860 || timer == 920 || timer == 980 || timer == 1040)
		{
			Vector2 vector10 = Main.player[((ModNPC)this).npc.target].Center - ((ModNPC)this).npc.Center;
			vector10.Normalize();
			vector10.X *= 8.5f;
			vector10.Y *= 8.5f;
			int num10 = Main.rand.Next(3, 3);
			for (int l = 0; l < num10; l++)
			{
				float num11 = (float)Main.rand.Next(-300, 300) * 0.01f;
				Projectile.NewProjectile(((ModNPC)this).npc.Center.X, ((ModNPC)this).npc.Center.Y, vector10.X + num11, vector10.Y + num11, ((ModNPC)this).mod.ProjectileType("DreadSpit"), num, 1f, Main.myPlayer, 0f, 0f);
			}
		}
		if ((timer >= 1120 && timer <= 1140) || (timer >= 1160 && timer <= 1180) || (timer >= 1200 && timer <= 1220))
		{
			float num12 = 8f;
			int num13 = ((ModNPC)this).mod.ProjectileType("DreadBolt");
			Main.PlaySound(2, (int)((ModNPC)this).npc.position.X, (int)((ModNPC)this).npc.position.Y, 20, 1f, 0f);
			num2 = (float)Math.Atan2(((ModNPC)this).npc.Center.Y - player.Center.Y, ((ModNPC)this).npc.Center.X - player.Center.X);
			Projectile.NewProjectile(((ModNPC)this).npc.Center.X, ((ModNPC)this).npc.Center.Y, (float)(Math.Cos(num2) * (double)num12 * -1.0), (float)(Math.Sin(num2) * (double)num12 * -1.0), num13, num, 0f, Main.myPlayer, 0f, 0f);
		}
		if (timer == 1480 || timer == 1540 || timer == 1600 || timer == 1660 || timer == 1720)
		{
			float num14 = 5f;
			int num15 = ((ModNPC)this).mod.ProjectileType("ToothBall2");
			Main.PlaySound(SoundID.NPCDeath13, ((ModNPC)this).npc.position);
			num2 = (float)Math.Atan2(((ModNPC)this).npc.Center.Y - player.Center.Y, ((ModNPC)this).npc.Center.X - player.Center.X);
			Projectile.NewProjectile(((ModNPC)this).npc.Center.X, ((ModNPC)this).npc.Center.Y, (float)(Math.Cos(num2) * (double)num14 * -1.0), (float)(Math.Sin(num2) * (double)num14 * -1.0), num15, num, 0f, Main.myPlayer, 0f, 0f);
		}
		if (timer >= 1780 && timer < 1865)
		{
			Move(new Vector2(550f, 0f));
			if (timer == 1815 || timer == 1835 || timer == 1855)
			{
				float num16 = 8.5f;
				int num17 = ((ModNPC)this).mod.ProjectileType("DreadBolt");
				Main.PlaySound(2, (int)((ModNPC)this).npc.position.X, (int)((ModNPC)this).npc.position.Y, 20, 1f, 0f);
				num2 = (float)Math.Atan2(((ModNPC)this).npc.Center.Y - player.Center.Y, ((ModNPC)this).npc.Center.X - player.Center.X);
				Projectile.NewProjectile(((ModNPC)this).npc.Center.X, ((ModNPC)this).npc.Center.Y, (float)(Math.Cos(num2) * (double)num16 * -1.0), (float)(Math.Sin(num2) * (double)num16 * -1.0), num17, num, 0f, Main.myPlayer, 0f, 0f);
			}
		}
		if (timer >= 1925 && timer < 2010)
		{
			Move(new Vector2(-550f, 0f));
			if (timer == 1945 || timer == 1965 || timer == 1985)
			{
				float num18 = 8.5f;
				int num19 = ((ModNPC)this).mod.ProjectileType("DreadBolt");
				Main.PlaySound(2, (int)((ModNPC)this).npc.position.X, (int)((ModNPC)this).npc.position.Y, 20, 1f, 0f);
				num2 = (float)Math.Atan2(((ModNPC)this).npc.Center.Y - player.Center.Y, ((ModNPC)this).npc.Center.X - player.Center.X);
				Projectile.NewProjectile(((ModNPC)this).npc.Center.X, ((ModNPC)this).npc.Center.Y, (float)(Math.Cos(num2) * (double)num18 * -1.0), (float)(Math.Sin(num2) * (double)num18 * -1.0), num19, num, 0f, Main.myPlayer, 0f, 0f);
			}
		}
		if (timer == 2070 || timer == 2130)
		{
			int num20 = 6;
			for (int m = 0; m < num20; m++)
			{
				int num21 = 360 / num20;
				NPC.NewNPC((int)((ModNPC)this).npc.Center.X, (int)((ModNPC)this).npc.Center.Y, ((ModNPC)this).mod.NPCType("DreadOrbiter"), ((ModNPC)this).npc.whoAmI, (float)(m * num21), (float)((ModNPC)this).npc.whoAmI, 0f, 0f, 255);
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
		Vector2 vector = player.Center + offset - ((ModNPC)this).npc.Center;
		float num = Magnitude(vector);
		if (num > speed)
		{
			vector *= speed / num;
		}
		float num2 = 10f;
		vector = (((ModNPC)this).npc.velocity * num2 + vector) / (num2 + 1f);
		num = Magnitude(vector);
		if (num > speed)
		{
			vector *= speed / num;
		}
		((ModNPC)this).npc.velocity = vector;
	}

	private float Magnitude(Vector2 mag)
	{
		return (float)Math.Sqrt(mag.X * mag.X + mag.Y * mag.Y);
	}

	public override void NPCLoot()
	{
		if (Main.expertMode)
		{
			((ModNPC)this).npc.DropBossBags();
		}
		else
		{
			int num = Main.rand.Next(4);
			if (num == 0)
			{
				Item.NewItem((int)((ModNPC)this).npc.position.X, (int)((ModNPC)this).npc.position.Y, ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, ((ModNPC)this).mod.ItemType("DreadSword"), 1, false, 0, false, false);
			}
			if (num == 1)
			{
				Item.NewItem((int)((ModNPC)this).npc.position.X, (int)((ModNPC)this).npc.position.Y, ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, ((ModNPC)this).mod.ItemType("DreadBow"), 1, false, 0, false, false);
			}
			if (num == 2)
			{
				Item.NewItem((int)((ModNPC)this).npc.position.X, (int)((ModNPC)this).npc.position.Y, ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, ((ModNPC)this).mod.ItemType("DreadStaff"), 1, false, 0, false, false);
			}
			if (num == 3)
			{
				Item.NewItem((int)((ModNPC)this).npc.position.X, (int)((ModNPC)this).npc.position.Y, ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, ((ModNPC)this).mod.ItemType("DreadSummon"), 1, false, 0, false, false);
			}
			if (Main.rand.Next(3) == 0)
			{
				Item.NewItem(((ModNPC)this).npc.getRect(), ((ModNPC)this).mod.ItemType("DreadTooth"), 1, false, 0, false, false);
			}
			if (Main.rand.Next(15) == 0)
			{
				Item.NewItem(((ModNPC)this).npc.getRect(), ((ModNPC)this).mod.ItemType("DreadBreadItem"), 1, false, 0, false, false);
			}
			Item.NewItem(((ModNPC)this).npc.getRect(), ((ModNPC)this).mod.ItemType("DreadFlame"), Main.rand.Next(10, 18), false, 0, false, false);
			Item.NewItem(((ModNPC)this).npc.getRect(), ((ModNPC)this).mod.ItemType("DreadScale"), Main.rand.Next(6, 12), false, 0, false, false);
		}
		if (Main.rand.Next(7) == 0)
		{
			Item.NewItem(((ModNPC)this).npc.getRect(), ((ModNPC)this).mod.ItemType("DreadMask"), 1, false, 0, false, false);
		}
		if (Main.rand.Next(10) == 0)
		{
			Item.NewItem(((ModNPC)this).npc.getRect(), ((ModNPC)this).mod.ItemType("DreadTrophyItem"), 1, false, 0, false, false);
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
			int num = Dust.NewDust(((ModNPC)this).npc.position, ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, 90, 0f, -2f, 0, default(Color), 1.5f);
			Main.dust[num].noGravity = false;
			Main.dust[num].position.X += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
			Main.dust[num].position.Y += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
			if (Main.dust[num].position != ((ModNPC)this).npc.Center)
			{
				Main.dust[num].velocity = ((ModNPC)this).npc.DirectionTo(Main.dust[num].position) * 3f;
			}
		}
		for (int j = 0; j < 80; j++)
		{
			int num2 = Dust.NewDust(((ModNPC)this).npc.position, ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, 90, 0f, -2f, 0, default(Color), 2f);
			Main.dust[num2].noGravity = false;
			Main.dust[num2].position.X += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
			Main.dust[num2].position.Y += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
			if (Main.dust[num2].position != ((ModNPC)this).npc.Center)
			{
				Main.dust[num2].velocity = ((ModNPC)this).npc.DirectionTo(Main.dust[num2].position) * 8f;
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
		((ModNPC)this).npc.frameCounter += 1.0;
		if (((ModNPC)this).npc.frameCounter >= 9.0)
		{
			((ModNPC)this).npc.frame.Y = (((ModNPC)this).npc.frame.Y + frameHeight) % (Main.npcFrameCount[((ModNPC)this).npc.type] * frameHeight);
			((ModNPC)this).npc.frameCounter = 1.0;
		}
	}
}
