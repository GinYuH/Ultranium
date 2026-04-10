using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.NPCs.Enemy;

public class Orca : ModNPC
{
	public override void SetStaticDefaults()
	{
		// ((ModNPC)this).DisplayName.SetDefault("Orca");
	}

	public override void SetDefaults()
	{
		((ModNPC)this).NPC.damage = 40;
		((ModNPC)this).NPC.lifeMax = 250;
		((ModNPC)this).NPC.defense = 20;
		((ModNPC)this).NPC.knockBackResist = 0.1f;
		((ModNPC)this).NPC.HitSound = SoundID.NPCHit1;
		((ModNPC)this).NPC.DeathSound = SoundID.NPCDeath1;
		((ModNPC)this).NPC.width = 120;
		((ModNPC)this).NPC.height = 50;
		((ModNPC)this).NPC.scale = 1.1f;
		((ModNPC)this).NPC.noGravity = true;
		((ModNPC)this).NPC.value = Item.buyPrice(0, 0, 20);
		((ModNPC)this).NPC.aiStyle = 16;
		base.AIType = 65;
		Main.npcFrameCount[((ModNPC)this).NPC.type] = Main.npcFrameCount[65];
		base.AnimationType = 65;
		((ModNPC)this).NPC.buffImmune[31] = true;
	}

	public override void OnKill()
	{
		if (Main.rand.Next(20) == 0)
		{
			Item.NewItem((int)((ModNPC)this).NPC.position.X, (int)((ModNPC)this).NPC.position.Y, ((ModNPC)this).NPC.width, ((ModNPC)this).NPC.height, 268, 1, false, 0, false, false);
		}
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
