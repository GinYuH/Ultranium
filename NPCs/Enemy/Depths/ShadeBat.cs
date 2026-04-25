using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;
using Ultranium.Items.Shade;

namespace Ultranium.NPCs.Enemy.Depths;

public class ShadeBat : ModNPC
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Shade Bat");
		Main.npcFrameCount[NPC.type] = Main.npcFrameCount[152];
	}

	public override void SetDefaults()
	{
		NPC.npcSlots = 1f;
		NPC.width = 42;
		NPC.height = 52;
		NPC.damage = 40;
		NPC.defense = 60;
		NPC.lifeMax = 150;
		NPC.knockBackResist = 0.1f;
		NPC.aiStyle = NPCAIStyleID.Bat;
		AIType = NPCID.GiantFlyingFox;
		AnimationType = NPCID.GiantFlyingFox;
		NPC.HitSound = SoundID.NPCHit6;
		NPC.DeathSound = SoundID.NPCDeath8;
		NPC.knockBackResist = 0.5f;
		Banner = NPC.type;
		BannerItem = Mod.Find<ModItem>("ShadeBatBanner").Type;
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		if (NPC.life <= 0)
		{
			Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity, Mod.Find<ModGore>("DepthsBatGore1").Type);
			Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity, Mod.Find<ModGore>("DepthsBatGore2").Type);
			Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity, Mod.Find<ModGore>("DepthsBatGore3").Type);
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
			if (!spawnInfo.Player.GetModPlayer<UltraniumPlayer>().ZoneDepth)
			{
				return 0f;
			}
			return 20f;
		}
		return 0f;
    }

    public override void ModifyNPCLoot(NPCLoot npcLoot)
    {
        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ShadowEssence>(), 5));
    }
}
