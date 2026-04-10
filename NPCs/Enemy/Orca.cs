using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.NPCs.Enemy;

public class Orca : ModNPC
{
	public override void SetStaticDefaults()
	{
		((ModNPC)this).DisplayName.SetDefault("Orca");
	}

	public override void SetDefaults()
	{
		((ModNPC)this).npc.damage = 40;
		((ModNPC)this).npc.lifeMax = 250;
		((ModNPC)this).npc.defense = 20;
		((ModNPC)this).npc.knockBackResist = 0.1f;
		((ModNPC)this).npc.HitSound = SoundID.NPCHit1;
		((ModNPC)this).npc.DeathSound = SoundID.NPCDeath1;
		((ModNPC)this).npc.width = 120;
		((ModNPC)this).npc.height = 50;
		((ModNPC)this).npc.scale = 1.1f;
		((ModNPC)this).npc.noGravity = true;
		((ModNPC)this).npc.value = Item.buyPrice(0, 0, 20);
		((ModNPC)this).npc.aiStyle = 16;
		base.aiType = 65;
		Main.npcFrameCount[((ModNPC)this).npc.type] = Main.npcFrameCount[65];
		base.animationType = 65;
		((ModNPC)this).npc.buffImmune[31] = true;
	}

	public override void NPCLoot()
	{
		if (Main.rand.Next(20) == 0)
		{
			Item.NewItem((int)((ModNPC)this).npc.position.X, (int)((ModNPC)this).npc.position.Y, ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, 268, 1, false, 0, false, false);
		}
	}

	public override float SpawnChance(NPCSpawnInfo spawnInfo)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		int spawnTileX = spawnInfo.spawnTileX;
		int spawnTileY = spawnInfo.spawnTileY;
		_ = Main.tile[spawnTileX, spawnTileY].type;
		if (!spawnInfo.water || !((double)spawnTileY < Main.rockLayer) || (spawnTileX >= 250 && spawnTileX <= Main.maxTilesX - 250) || spawnInfo.playerSafe)
		{
			return 0f;
		}
		return 0.5f;
	}
}
