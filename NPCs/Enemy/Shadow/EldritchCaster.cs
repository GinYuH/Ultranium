using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace Ultranium.NPCs.Enemy.Shadow;

public class EldritchCaster : ModNPC
{
	public bool Shooting;

	private float Timer
	{
		get
		{
			return ((ModNPC)this).NPC.ai[0];
		}
		set
		{
			((ModNPC)this).NPC.ai[0] = value;
		}
	}

	private float TeleportTimer
	{
		get
		{
			return ((ModNPC)this).NPC.ai[1];
		}
		set
		{
			((ModNPC)this).NPC.ai[1] = value;
		}
	}

	private float TeleportX
	{
		get
		{
			return ((ModNPC)this).NPC.ai[2];
		}
		set
		{
			((ModNPC)this).NPC.ai[2] = value;
		}
	}

	private float TeleportY
	{
		get
		{
			return ((ModNPC)this).NPC.ai[3];
		}
		set
		{
			((ModNPC)this).NPC.ai[3] = value;
		}
	}

	public override void SetStaticDefaults()
	{
		// ((ModNPC)this).DisplayName.SetDefault("Eldritch Caster");
		Main.npcFrameCount[((ModNPC)this).NPC.type] = 2;
	}

	public override void SetDefaults()
	{
		((ModNPC)this).NPC.width = 30;
		((ModNPC)this).NPC.height = 48;
		((ModNPC)this).NPC.damage = 45;
		((ModNPC)this).NPC.defense = 25;
		((ModNPC)this).NPC.lifeMax = 50;
		((ModNPC)this).NPC.knockBackResist = 0f;
		((ModNPC)this).NPC.value = Item.buyPrice(0, 0, 1);
		((ModNPC)this).NPC.HitSound = SoundID.NPCHit2;
		((ModNPC)this).NPC.DeathSound = SoundID.NPCDeath2;
		base.Banner = ((ModNPC)this).NPC.type;
		base.BannerItem = ((ModNPC)this).Mod.Find<ModItem>("EldritchCasterBanner").Type;
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		if (((ModNPC)this).NPC.life <= 0)
		{
			Gore.NewGore(((ModNPC)this).NPC.position, ((ModNPC)this).NPC.velocity, ((ModNPC)this).Mod.GetGoreSlot("Gores/ShadowBiome/CasterGore1"));
			Gore.NewGore(((ModNPC)this).NPC.position, ((ModNPC)this).NPC.velocity, ((ModNPC)this).Mod.GetGoreSlot("Gores/ShadowBiome/CasterGore2"));
			Gore.NewGore(((ModNPC)this).NPC.position, ((ModNPC)this).NPC.velocity, ((ModNPC)this).Mod.GetGoreSlot("Gores/ShadowBiome/CasterGore3"));
		}
	}

	public override float SpawnChance(NPCSpawnInfo spawnInfo)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		Player player = spawnInfo.Player;
		if (!player.ZoneTowerSolar && !player.ZoneTowerVortex && !player.ZoneTowerNebula && !player.ZoneTowerStardust && ((!Main.pumpkinMoon && !Main.snowMoon) || (double)spawnInfo.SpawnTileY > Main.worldSurface || Main.dayTime) && (!Main.eclipse || (double)spawnInfo.SpawnTileY > Main.worldSurface || !Main.dayTime) && SpawnCondition.GoblinArmy.Chance == 0f)
		{
			if (!spawnInfo.Player.GetModPlayer<UltraniumPlayer>().ZoneShadow)
			{
				return 0f;
			}
			return 1f;
		}
		return 0f;
	}

	public override void AI()
	{
		((ModNPC)this).NPC.TargetClosest();
		Player player = Main.player[((ModNPC)this).NPC.target];
		Vector2 vector = player.Center - ((ModNPC)this).NPC.Center;
		((ModNPC)this).NPC.spriteDirection = Math.Sign(vector.X);
		((ModNPC)this).NPC.velocity.X = 0f;
		if (Timer > 0f)
		{
			Timer -= 1f;
		}
		Shooting = Timer <= 30f;
		if (Timer <= 0f)
		{
			SoundEngine.PlaySound(SoundID.Item20, new Vector2(((ModNPC)this).NPC.position.X, ((ModNPC)this).NPC.position.Y));
			float num = 3f;
			float num2 = (float)Math.Atan2(((ModNPC)this).NPC.Center.Y - player.Center.Y, ((ModNPC)this).NPC.Center.X - player.Center.X);
			Projectile.NewProjectile(((ModNPC)this).NPC.Center.X, ((ModNPC)this).NPC.Center.Y, (float)(Math.Cos(num2) * (double)num * -1.0), (float)(Math.Sin(num2) * (double)num * -1.0), ((ModNPC)this).Mod.Find<ModProjectile>("CasterBolt").Type, 20, 0f, 0, 0f, 0f);
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
		int num3 = (int)((ModNPC)this).NPC.position.X / 16;
		int num4 = (int)((ModNPC)this).NPC.position.Y / 16;
		int num5 = 20;
		bool flag = false;
		int num6 = Main.rand.Next(num - num5, num + num5);
		for (int i = Main.rand.Next(num2 - num5, num2 + num5); i < num2 + num5; i++)
		{
			if ((i < num2 - 4 || i > num2 + 4 || num6 < num - 4 || num6 > num + 4) && (i < num4 - 1 || i > num4 + 1 || num6 < num3 - 1 || num6 > num3 + 1) && Main.tile[num6, i].HasUnactuatedTile)
			{
				bool flag2 = true;
				if ((Main.tile[num6, i - 1].LiquidType == LiquidID.Lava))
				{
					flag2 = false;
				}
				if (flag2 && Main.tileSolid[Main.tile[num6, i].TileType] && !Collision.SolidTiles(num6 - 1, num6 + 1, i - 4, i - 1))
				{
					TeleportX = num6;
					TeleportY = i;
					flag = true;
					break;
				}
			}
		}
		SoundEngine.PlaySound(SoundID.Item8, ((ModNPC)this).NPC.position);
		if (TeleportX != 0f && TeleportY != 0f && flag)
		{
			((ModNPC)this).NPC.position.X = (float)((double)TeleportX * 16.0 - (double)(((ModNPC)this).NPC.width / 2) + 8.0);
			((ModNPC)this).NPC.position.Y = TeleportY * 16f - (float)((ModNPC)this).NPC.height;
			((ModNPC)this).NPC.netUpdate = true;
			for (int j = 0; j < 20; j++)
			{
				Dust obj = Main.dust[Dust.NewDust(((ModNPC)this).NPC.position, ((ModNPC)this).NPC.width, ((ModNPC)this).NPC.height, 89)];
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
			((ModNPC)this).NPC.frame.Y = 0;
		}
		if (Shooting)
		{
			((ModNPC)this).NPC.frame.Y = frameHeight;
		}
	}

	public override void OnKill()
	{
		if (Main.rand.Next(3) == 0)
		{
			Item.NewItem((int)((ModNPC)this).NPC.position.X, (int)((ModNPC)this).NPC.position.Y, ((ModNPC)this).NPC.width, ((ModNPC)this).NPC.height, ((ModNPC)this).Mod.Find<ModItem>("ShadowEssence").Type, 1, false, 0, false, false);
		}
	}
}
