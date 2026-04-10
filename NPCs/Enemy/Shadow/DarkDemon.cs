using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace Ultranium.NPCs.Enemy.Shadow;

public class DarkDemon : ModNPC
{
	private int Timer;

	public override void SetStaticDefaults()
	{
		// ((ModNPC)this).DisplayName.SetDefault("Dark Demon");
		Main.npcFrameCount[((ModNPC)this).NPC.type] = 4;
	}

	public override void SetDefaults()
	{
		((ModNPC)this).NPC.width = 30;
		((ModNPC)this).NPC.height = 30;
		((ModNPC)this).NPC.damage = 18;
		((ModNPC)this).NPC.defense = 10;
		((ModNPC)this).NPC.lifeMax = 55;
		((ModNPC)this).NPC.HitSound = SoundID.NPCHit21;
		((ModNPC)this).NPC.DeathSound = SoundID.NPCDeath24;
		((ModNPC)this).NPC.value = Item.buyPrice(0, 0, 1);
		((ModNPC)this).NPC.knockBackResist = 0.5f;
		((ModNPC)this).NPC.aiStyle = 14;
		((ModNPC)this).NPC.noGravity = true;
		base.Banner = ((ModNPC)this).NPC.type;
		base.BannerItem = ((ModNPC)this).Mod.Find<ModItem>("DarkDemonBanner").Type;
	}

	public override void AI()
	{
		((ModNPC)this).NPC.TargetClosest();
		Player player = Main.player[((ModNPC)this).NPC.target];
		((ModNPC)this).NPC.rotation = ((ModNPC)this).NPC.velocity.X * 0.03f;
		((ModNPC)this).NPC.spriteDirection = ((ModNPC)this).NPC.direction;
		Timer++;
		if (Timer > 600)
		{
			((ModNPC)this).NPC.velocity *= 0f;
		}
		if (Timer == 650)
		{
			float num = 6f;
			float num2 = (float)Math.Atan2(((ModNPC)this).NPC.Center.Y - player.Center.Y, ((ModNPC)this).NPC.Center.X - player.Center.X);
			Projectile.NewProjectile(((ModNPC)this).NPC.Center.X, ((ModNPC)this).NPC.Center.Y, (float)(Math.Cos(num2) * (double)num * -1.0), (float)(Math.Sin(num2) * (double)num * -1.0), ((ModNPC)this).Mod.Find<ModProjectile>("DarkDemonScythe").Type, 20, 0f, 0, 0f, 0f);
			Timer = 0;
		}
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		if (((ModNPC)this).NPC.life <= 0)
		{
			Gore.NewGore(((ModNPC)this).NPC.position, ((ModNPC)this).NPC.velocity, ((ModNPC)this).Mod.GetGoreSlot("Gores/ShadowBiome/DemonGore1"));
			Gore.NewGore(((ModNPC)this).NPC.position, ((ModNPC)this).NPC.velocity, ((ModNPC)this).Mod.GetGoreSlot("Gores/ShadowBiome/DemonGore2"));
			Gore.NewGore(((ModNPC)this).NPC.position, ((ModNPC)this).NPC.velocity, ((ModNPC)this).Mod.GetGoreSlot("Gores/ShadowBiome/DemonGore3"));
			Gore.NewGore(((ModNPC)this).NPC.position, ((ModNPC)this).NPC.velocity, ((ModNPC)this).Mod.GetGoreSlot("Gores/ShadowBiome/DemonGore4"));
		}
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

	public override void OnKill()
	{
		if (Utils.NextBool(Main.rand, 2))
		{
			Item.NewItem(((ModNPC)this).NPC.getRect(), ((ModNPC)this).Mod.Find<ModItem>("ShadowEssence").Type, Main.rand.Next(1, 4), false, 0, false, false);
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
			return 20f;
		}
		return 0f;
	}
}
