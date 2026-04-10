using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace Ultranium.NPCs.Enemy.Shadow;

public class ShadeGhoul : ModNPC
{
	public override void SetStaticDefaults()
	{
		// ((ModNPC)this).DisplayName.SetDefault("Shade Ghoul");
		Main.npcFrameCount[((ModNPC)this).NPC.type] = Main.npcFrameCount[524];
	}

	public override void SetDefaults()
	{
		((ModNPC)this).NPC.npcSlots = 1f;
		((ModNPC)this).NPC.width = 42;
		((ModNPC)this).NPC.height = 52;
		((ModNPC)this).NPC.damage = 18;
		((ModNPC)this).NPC.defense = 15;
		((ModNPC)this).NPC.lifeMax = 60;
		((ModNPC)this).NPC.knockBackResist = 0.1f;
		((ModNPC)this).NPC.aiStyle = 3;
		base.AIType = 524;
		base.AnimationType = 524;
		((ModNPC)this).NPC.HitSound = SoundID.NPCHit6;
		((ModNPC)this).NPC.DeathSound = SoundID.NPCDeath8;
		base.Banner = ((ModNPC)this).NPC.type;
		base.BannerItem = ((ModNPC)this).Mod.Find<ModItem>("ShadeGhoulBanner").Type;
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		if (((ModNPC)this).NPC.life <= 0)
		{
			Gore.NewGore(((ModNPC)this).NPC.position, ((ModNPC)this).NPC.velocity, ((ModNPC)this).Mod.GetGoreSlot("Gores/ShadowBiome/GhoulGore1"));
			Gore.NewGore(((ModNPC)this).NPC.position, ((ModNPC)this).NPC.velocity, ((ModNPC)this).Mod.GetGoreSlot("Gores/ShadowBiome/GhoulGore2"));
			Gore.NewGore(((ModNPC)this).NPC.position, ((ModNPC)this).NPC.velocity, ((ModNPC)this).Mod.GetGoreSlot("Gores/ShadowBiome/GhoulGore3"));
		}
	}

	public override float SpawnChance(NPCSpawnInfo spawnInfo)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		Player player = spawnInfo.Player;
		if (!player.ZoneTowerSolar && !player.ZoneTowerVortex && !player.ZoneTowerNebula && !player.ZoneTowerStardust && ((!Main.pumpkinMoon && !Main.snowMoon) || (double)spawnInfo.SpawnTileY > Main.worldSurface || Main.dayTime) && (!Main.eclipse || (double)spawnInfo.SpawnTileY > Main.worldSurface || !Main.dayTime) && SpawnCondition.GoblinArmy.Chance == 0f)
		{
			if (!spawnInfo.Player.GetModPlayer<UltraniumPlayer>().ZoneShadow || Main.dayTime)
			{
				return 0f;
			}
			return 20f;
		}
		return 0f;
	}

	public override void OnKill()
	{
		if (Main.rand.Next(2) == 1)
		{
			Item.NewItem((int)((ModNPC)this).NPC.position.X, (int)((ModNPC)this).NPC.position.Y, ((ModNPC)this).NPC.width, ((ModNPC)this).NPC.height, ((ModNPC)this).Mod.Find<ModItem>("ShadowEssence").Type, 1, false, 0, false, false);
		}
	}
}
