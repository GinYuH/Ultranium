using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Ultranium.Items.Shade;

namespace Ultranium.NPCs.Enemy;

public class Orca : ModNPC
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Orca");
	}

	public override void SetDefaults()
	{
		NPC.damage = 40;
		NPC.lifeMax = 250;
		NPC.defense = 20;
		NPC.knockBackResist = 0.1f;
		NPC.HitSound = SoundID.NPCHit1;
		NPC.DeathSound = SoundID.NPCDeath1;
		NPC.width = 120;
		NPC.height = 50;
		NPC.scale = 1.1f;
		NPC.noGravity = true;
		NPC.value = Item.buyPrice(0, 0, 20);
		NPC.aiStyle = NPCAIStyleID.Piranha;
		AIType = NPCID.Shark;
		Main.npcFrameCount[NPC.type] = Main.npcFrameCount[65];
		AnimationType = NPCID.Shark;
		NPC.buffImmune[31] = true;
	}


    public override void ModifyNPCLoot(NPCLoot npcLoot)
    {
        npcLoot.Add(ItemDropRule.Common(268, 20));
    }

	public override float SpawnChance(NPCSpawnInfo spawnInfo)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		int spawnTileX = spawnInfo.SpawnTileX;
		int spawnTileY = spawnInfo.SpawnTileY;
		_ = Main.tile[spawnTileX, spawnTileY].TileType;
		if (!spawnInfo.Water || !((double)spawnTileY < Main.rockLayer) || (spawnTileX >= 250 && spawnTileX <= Main.maxTilesX - 250) || spawnInfo.PlayerSafe)
		{
			return 0f;
		}
		return 0.5f;
	}
}
