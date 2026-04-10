using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.NPCs.Enemy.Shadow;

public class EldritchCaster : ModNPC
{
	public bool Shooting;

	private float Timer
	{
		get
		{
			return ((ModNPC)this).npc.ai[0];
		}
		set
		{
			((ModNPC)this).npc.ai[0] = value;
		}
	}

	private float TeleportTimer
	{
		get
		{
			return ((ModNPC)this).npc.ai[1];
		}
		set
		{
			((ModNPC)this).npc.ai[1] = value;
		}
	}

	private float TeleportX
	{
		get
		{
			return ((ModNPC)this).npc.ai[2];
		}
		set
		{
			((ModNPC)this).npc.ai[2] = value;
		}
	}

	private float TeleportY
	{
		get
		{
			return ((ModNPC)this).npc.ai[3];
		}
		set
		{
			((ModNPC)this).npc.ai[3] = value;
		}
	}

	public override void SetStaticDefaults()
	{
		((ModNPC)this).DisplayName.SetDefault("Eldritch Caster");
		Main.npcFrameCount[((ModNPC)this).npc.type] = 2;
	}

	public override void SetDefaults()
	{
		((ModNPC)this).npc.width = 30;
		((ModNPC)this).npc.height = 48;
		((ModNPC)this).npc.damage = 45;
		((ModNPC)this).npc.defense = 25;
		((ModNPC)this).npc.lifeMax = 50;
		((ModNPC)this).npc.knockBackResist = 0f;
		((ModNPC)this).npc.value = Item.buyPrice(0, 0, 1);
		((ModNPC)this).npc.HitSound = SoundID.NPCHit2;
		((ModNPC)this).npc.DeathSound = SoundID.NPCDeath2;
		base.banner = ((ModNPC)this).npc.type;
		base.bannerItem = ((ModNPC)this).mod.ItemType("EldritchCasterBanner");
	}

	public override void HitEffect(int hitDirection, double damage)
	{
		if (((ModNPC)this).npc.life <= 0)
		{
			Gore.NewGore(((ModNPC)this).npc.position, ((ModNPC)this).npc.velocity, ((ModNPC)this).mod.GetGoreSlot("Gores/ShadowBiome/CasterGore1"));
			Gore.NewGore(((ModNPC)this).npc.position, ((ModNPC)this).npc.velocity, ((ModNPC)this).mod.GetGoreSlot("Gores/ShadowBiome/CasterGore2"));
			Gore.NewGore(((ModNPC)this).npc.position, ((ModNPC)this).npc.velocity, ((ModNPC)this).mod.GetGoreSlot("Gores/ShadowBiome/CasterGore3"));
		}
	}

	public override float SpawnChance(NPCSpawnInfo spawnInfo)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		Player player = spawnInfo.player;
		if (!player.ZoneTowerSolar && !player.ZoneTowerVortex && !player.ZoneTowerNebula && !player.ZoneTowerStardust && ((!Main.pumpkinMoon && !Main.snowMoon) || (double)spawnInfo.spawnTileY > Main.worldSurface || Main.dayTime) && (!Main.eclipse || (double)spawnInfo.spawnTileY > Main.worldSurface || !Main.dayTime) && SpawnCondition.GoblinArmy.Chance == 0f)
		{
			if (!spawnInfo.player.GetModPlayer<UltraniumPlayer>().ZoneShadow)
			{
				return 0f;
			}
			return 1f;
		}
		return 0f;
	}

	public override void AI()
	{
		((ModNPC)this).npc.TargetClosest();
		Player player = Main.player[((ModNPC)this).npc.target];
		Vector2 vector = player.Center - ((ModNPC)this).npc.Center;
		((ModNPC)this).npc.spriteDirection = Math.Sign(vector.X);
		((ModNPC)this).npc.velocity.X = 0f;
		if (Timer > 0f)
		{
			Timer -= 1f;
		}
		Shooting = Timer <= 30f;
		if (Timer <= 0f)
		{
			Main.PlaySound(2, (int)((ModNPC)this).npc.position.X, (int)((ModNPC)this).npc.position.Y, 20, 1f, 0f);
			float num = 3f;
			float num2 = (float)Math.Atan2(((ModNPC)this).npc.Center.Y - player.Center.Y, ((ModNPC)this).npc.Center.X - player.Center.X);
			Projectile.NewProjectile(((ModNPC)this).npc.Center.X, ((ModNPC)this).npc.Center.Y, (float)(Math.Cos(num2) * (double)num * -1.0), (float)(Math.Sin(num2) * (double)num * -1.0), ((ModNPC)this).mod.ProjectileType("CasterBolt"), 20, 0f, 0, 0f, 0f);
			Timer = 120f;
		}
		TeleportTimer--;
		if (TeleportTimer <= 0f)
		{
			TeleportTimer = 200f;
			Teleport(player, 0);
		}
	}

	private void Teleport(Player player, int attemptNum)
	{
		int num = (int)player.position.X / 16;
		int num2 = (int)player.position.Y / 16;
		int num3 = (int)((ModNPC)this).npc.position.X / 16;
		int num4 = (int)((ModNPC)this).npc.position.Y / 16;
		int num5 = 20;
		bool flag = false;
		int num6 = Main.rand.Next(num - num5, num + num5);
		for (int i = Main.rand.Next(num2 - num5, num2 + num5); i < num2 + num5; i++)
		{
			if ((i < num2 - 4 || i > num2 + 4 || num6 < num - 4 || num6 > num + 4) && (i < num4 - 1 || i > num4 + 1 || num6 < num3 - 1 || num6 > num3 + 1) && Main.tile[num6, i].nactive())
			{
				bool flag2 = true;
				if (Main.tile[num6, i - 1].lava())
				{
					flag2 = false;
				}
				if (flag2 && Main.tileSolid[Main.tile[num6, i].type] && !Collision.SolidTiles(num6 - 1, num6 + 1, i - 4, i - 1))
				{
					TeleportX = num6;
					TeleportY = i;
					flag = true;
					break;
				}
			}
		}
		Main.PlaySound(SoundID.Item8, ((ModNPC)this).npc.position);
		if (TeleportX != 0f && TeleportY != 0f && flag)
		{
			((ModNPC)this).npc.position.X = (float)((double)TeleportX * 16.0 - (double)(((ModNPC)this).npc.width / 2) + 8.0);
			((ModNPC)this).npc.position.Y = TeleportY * 16f - (float)((ModNPC)this).npc.height;
			((ModNPC)this).npc.netUpdate = true;
			for (int j = 0; j < 20; j++)
			{
				Dust obj = Main.dust[Dust.NewDust(((ModNPC)this).npc.position, ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, 89)];
				obj.noGravity = true;
				obj.scale = 1f;
				obj.velocity *= 0.1f;
			}
		}
		else if (attemptNum < 10)
		{
			Teleport(player, attemptNum + 1);
		}
	}

	public override void FindFrame(int frameHeight)
	{
		if (!Shooting)
		{
			((ModNPC)this).npc.frame.Y = 0;
		}
		if (Shooting)
		{
			((ModNPC)this).npc.frame.Y = frameHeight;
		}
	}

	public override void NPCLoot()
	{
		if (Main.rand.Next(3) == 0)
		{
			Item.NewItem((int)((ModNPC)this).npc.position.X, (int)((ModNPC)this).npc.position.Y, ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, ((ModNPC)this).mod.ItemType("ShadowEssence"), 1, false, 0, false, false);
		}
	}
}
