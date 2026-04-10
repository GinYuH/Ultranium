using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.NPCs.Enemy.Dread;

public class DreadChaser : ModNPC
{
	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[((ModNPC)this).npc.type] = 2;
		((ModNPC)this).DisplayName.SetDefault("Dread Chaser");
	}

	public override void SetDefaults()
	{
		((ModNPC)this).npc.width = 48;
		((ModNPC)this).npc.height = 48;
		((ModNPC)this).npc.aiStyle = -1;
		((ModNPC)this).npc.damage = 25;
		((ModNPC)this).npc.defense = 30;
		((ModNPC)this).npc.lifeMax = 175;
		((ModNPC)this).npc.HitSound = SoundID.NPCHit1;
		((ModNPC)this).npc.DeathSound = SoundID.NPCDeath1;
		((ModNPC)this).npc.knockBackResist = 0f;
		Main.npcFrameCount[((ModNPC)this).npc.type] = 2;
		((ModNPC)this).npc.lavaImmune = true;
		base.banner = ((ModNPC)this).npc.type;
		base.bannerItem = ((ModNPC)this).mod.ItemType("DreadChaserBanner");
		((ModNPC)this).npc.noGravity = true;
		((ModNPC)this).npc.noTileCollide = false;
		((ModNPC)this).npc.buffImmune[189] = true;
		((ModNPC)this).npc.buffImmune[39] = true;
		((ModNPC)this).npc.buffImmune[24] = true;
		((ModNPC)this).npc.buffImmune[46] = true;
		((ModNPC)this).npc.buffImmune[47] = true;
		((ModNPC)this).npc.buffImmune[70] = true;
		((ModNPC)this).npc.buffImmune[186] = true;
	}

	public override float SpawnChance(NPCSpawnInfo spawnInfo)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		Player player = spawnInfo.player;
		if (!player.ZoneTowerSolar && !player.ZoneTowerVortex && !player.ZoneTowerNebula && !player.ZoneTowerStardust && ((!Main.pumpkinMoon && !Main.snowMoon) || (double)spawnInfo.spawnTileY > Main.worldSurface || Main.dayTime) && (!Main.eclipse || (double)spawnInfo.spawnTileY > Main.worldSurface || !Main.dayTime) && SpawnCondition.GoblinArmy.Chance == 0f)
		{
			if (!spawnInfo.player.ZoneSkyHeight || !Main.hardMode || UltraniumWorld.downedDread)
			{
				return 0f;
			}
			return 0.06f;
		}
		return 0f;
	}

	public override void FindFrame(int frameHeight)
	{
		((ModNPC)this).npc.frameCounter += 1.0;
		if (((ModNPC)this).npc.frameCounter >= 6.0)
		{
			((ModNPC)this).npc.frame.Y = (((ModNPC)this).npc.frame.Y + frameHeight) % (Main.npcFrameCount[((ModNPC)this).npc.type] * frameHeight);
			((ModNPC)this).npc.frameCounter = 1.0;
		}
	}

	public override void NPCLoot()
	{
		Item.NewItem((int)((ModNPC)this).npc.position.X, (int)((ModNPC)this).npc.position.Y, ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, ((ModNPC)this).mod.ItemType("DreadFlame"), Main.rand.Next(1, 3), false, 0, false, false);
	}

	public override void HitEffect(int hitDirection, double damage)
	{
		if (((ModNPC)this).npc.life > 0)
		{
			return;
		}
		((ModNPC)this).npc.position.X = ((ModNPC)this).npc.position.X + (float)(((ModNPC)this).npc.width / 2);
		((ModNPC)this).npc.position.Y = ((ModNPC)this).npc.position.Y + (float)(((ModNPC)this).npc.height / 2);
		((ModNPC)this).npc.width = 30;
		((ModNPC)this).npc.height = 30;
		((ModNPC)this).npc.position.X = ((ModNPC)this).npc.position.X - (float)(((ModNPC)this).npc.width / 2);
		((ModNPC)this).npc.position.Y = ((ModNPC)this).npc.position.Y - (float)(((ModNPC)this).npc.height / 2);
		for (int i = 0; i < 20; i++)
		{
			int num = Dust.NewDust(new Vector2(((ModNPC)this).npc.position.X, ((ModNPC)this).npc.position.Y), ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, 90, 0f, 0f, 100, default(Color), 2f);
			Main.dust[num].velocity *= 3f;
			if (Main.rand.Next(2) == 0)
			{
				Main.dust[num].scale = 0.5f;
				Main.dust[num].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
			}
		}
	}

	public override void AI()
	{
		_ = Main.player[((ModNPC)this).npc.target];
		if (((ModNPC)this).npc.target < 0 || ((ModNPC)this).npc.target == 255 || Main.player[((ModNPC)this).npc.target].dead)
		{
			((ModNPC)this).npc.TargetClosest();
		}
		float num = 8f;
		float num2 = 0.05f;
		Vector2 vector = new Vector2(((ModNPC)this).npc.position.X + (float)((ModNPC)this).npc.width * 0.5f, ((ModNPC)this).npc.position.Y + (float)((ModNPC)this).npc.height * 0.5f);
		float num3 = Main.player[((ModNPC)this).npc.target].position.X + (float)(Main.player[((ModNPC)this).npc.target].width / 2);
		float num4 = Main.player[((ModNPC)this).npc.target].position.Y + (float)(Main.player[((ModNPC)this).npc.target].height / 2);
		num3 = (int)(num3 / 8f) * 8;
		num4 = (int)(num4 / 8f) * 8;
		vector.X = (int)(vector.X / 8f) * 8;
		vector.Y = (int)(vector.Y / 8f) * 8;
		num3 -= vector.X;
		num4 -= vector.Y;
		float num5 = (float)Math.Sqrt(num3 * num3 + num4 * num4);
		float num6 = num5;
		bool flag = false;
		if (num5 > 600f)
		{
			flag = true;
		}
		if (num5 == 0f)
		{
			num3 = ((ModNPC)this).npc.velocity.X;
			num4 = ((ModNPC)this).npc.velocity.Y;
		}
		else
		{
			num5 = num / num5;
			num3 *= num5;
			num4 *= num5;
		}
		if (num6 > 100f)
		{
			((ModNPC)this).npc.ai[0] += 1f;
			if (((ModNPC)this).npc.ai[0] > 0f)
			{
				((ModNPC)this).npc.velocity.Y = ((ModNPC)this).npc.velocity.Y + 0.012f;
			}
			else
			{
				((ModNPC)this).npc.velocity.Y = ((ModNPC)this).npc.velocity.Y - 0.012f;
			}
			if (((ModNPC)this).npc.ai[0] < -100f || ((ModNPC)this).npc.ai[0] > 100f)
			{
				((ModNPC)this).npc.velocity.X = ((ModNPC)this).npc.velocity.X + 0.012f;
			}
			else
			{
				((ModNPC)this).npc.velocity.X = ((ModNPC)this).npc.velocity.X - 0.012f;
			}
			if (((ModNPC)this).npc.ai[0] > 200f)
			{
				((ModNPC)this).npc.ai[0] = -200f;
			}
		}
		if (Main.player[((ModNPC)this).npc.target].dead)
		{
			num3 = (float)((ModNPC)this).npc.direction * num / 2f;
			num4 = (0f - num) / 2f;
		}
		if (((ModNPC)this).npc.velocity.X < num3)
		{
			((ModNPC)this).npc.velocity.X = ((ModNPC)this).npc.velocity.X + num2;
		}
		else if (((ModNPC)this).npc.velocity.X > num3)
		{
			((ModNPC)this).npc.velocity.X = ((ModNPC)this).npc.velocity.X - num2;
		}
		if (((ModNPC)this).npc.velocity.Y < num4)
		{
			((ModNPC)this).npc.velocity.Y = ((ModNPC)this).npc.velocity.Y + num2;
		}
		else if (((ModNPC)this).npc.velocity.Y > num4)
		{
			((ModNPC)this).npc.velocity.Y = ((ModNPC)this).npc.velocity.Y - num2;
		}
		int num7 = (int)((ModNPC)this).npc.position.X + ((ModNPC)this).npc.width / 2;
		int num8 = (int)((ModNPC)this).npc.position.Y + ((ModNPC)this).npc.height / 2;
		num7 /= 16;
		_ = num8 / 16;
		if (num3 > 0f)
		{
			((ModNPC)this).npc.spriteDirection = 1;
			((ModNPC)this).npc.rotation = (float)Math.Atan2(num4, num3) + MathHelper.ToRadians(-90f);
		}
		if (num3 < 0f)
		{
			((ModNPC)this).npc.spriteDirection = -1;
			((ModNPC)this).npc.rotation = (float)Math.Atan2(num4, num3) + 3.14f + MathHelper.ToRadians(90f);
		}
		float num9 = 0.7f;
		if (((ModNPC)this).npc.collideX)
		{
			((ModNPC)this).npc.netUpdate = true;
			((ModNPC)this).npc.velocity.X = ((ModNPC)this).npc.oldVelocity.X * (0f - num9);
			if (((ModNPC)this).npc.direction == -1 && ((ModNPC)this).npc.velocity.X > 0f && ((ModNPC)this).npc.velocity.X < 2f)
			{
				((ModNPC)this).npc.velocity.X = 2f;
			}
			if (((ModNPC)this).npc.direction == 1 && ((ModNPC)this).npc.velocity.X < 0f && ((ModNPC)this).npc.velocity.X > -2f)
			{
				((ModNPC)this).npc.velocity.X = -2f;
			}
		}
		if (((ModNPC)this).npc.collideY)
		{
			((ModNPC)this).npc.netUpdate = true;
			((ModNPC)this).npc.velocity.Y = ((ModNPC)this).npc.oldVelocity.Y * (0f - num9);
			if (((ModNPC)this).npc.velocity.Y > 0f && (double)((ModNPC)this).npc.velocity.Y < 1.5)
			{
				((ModNPC)this).npc.velocity.Y = 2f;
			}
			if (((ModNPC)this).npc.velocity.Y < 0f && (double)((ModNPC)this).npc.velocity.Y > -1.5)
			{
				((ModNPC)this).npc.velocity.Y = -2f;
			}
		}
		if (flag)
		{
			if ((((ModNPC)this).npc.velocity.X > 0f && num3 > 0f) || (((ModNPC)this).npc.velocity.X < 0f && num3 < 0f))
			{
				if (Math.Abs(((ModNPC)this).npc.velocity.X) < 12f)
				{
					((ModNPC)this).npc.velocity.X = ((ModNPC)this).npc.velocity.X * 1.05f;
				}
			}
			else
			{
				((ModNPC)this).npc.velocity.X = ((ModNPC)this).npc.velocity.X * 0.9f;
			}
		}
		if (((((ModNPC)this).npc.velocity.X > 0f && ((ModNPC)this).npc.oldVelocity.X < 0f) || (((ModNPC)this).npc.velocity.X < 0f && ((ModNPC)this).npc.oldVelocity.X > 0f) || (((ModNPC)this).npc.velocity.Y > 0f && ((ModNPC)this).npc.oldVelocity.Y < 0f) || (((ModNPC)this).npc.velocity.Y < 0f && ((ModNPC)this).npc.oldVelocity.Y > 0f)) && !((ModNPC)this).npc.justHit)
		{
			((ModNPC)this).npc.netUpdate = true;
		}
	}
}
