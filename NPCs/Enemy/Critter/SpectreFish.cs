using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.NPCs.Enemy.Critter;

public class SpectreFish : ModNPC
{
	public override void SetStaticDefaults()
	{
		// ((ModNPC)this).DisplayName.SetDefault("Spectre Fish");
		Main.npcFrameCount[((ModNPC)this).NPC.type] = 4;
	}

	public override void SetDefaults()
	{
		((ModNPC)this).NPC.width = 34;
		((ModNPC)this).NPC.height = 24;
		((ModNPC)this).NPC.damage = 0;
		((ModNPC)this).NPC.defense = 0;
		((ModNPC)this).NPC.lifeMax = 12;
		Main.npcCatchable[((ModNPC)this).NPC.type] = true;
		((ModNPC)this).NPC.catchItem = (short)ModContent.ItemType<SpectreFishItem>();
		((ModNPC)this).NPC.HitSound = SoundID.NPCHit1;
		((ModNPC)this).NPC.DeathSound = SoundID.NPCDeath1;
		((ModNPC)this).NPC.knockBackResist = 0.35f;
		((ModNPC)this).NPC.aiStyle = 16;
		((ModNPC)this).NPC.noGravity = true;
		((ModNPC)this).NPC.npcSlots = 0f;
		base.AIType = 55;
		((ModNPC)this).NPC.dontCountMe = true;
	}

	public override void FindFrame(int frameHeight)
	{
		((ModNPC)this).NPC.frameCounter += 0.15000000596046448;
		((ModNPC)this).NPC.frameCounter %= Main.npcFrameCount[((ModNPC)this).NPC.type];
		int num = (int)((ModNPC)this).NPC.frameCounter;
		((ModNPC)this).NPC.frame.Y = num * frameHeight;
	}

	public override void AI()
	{
		((ModNPC)this).NPC.spriteDirection = ((ModNPC)this).NPC.direction;
	}

	public override float SpawnChance(NPCSpawnInfo spawnInfo)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		if (!spawnInfo.Player.GetModPlayer<UltraniumPlayer>().ZoneShadow || !spawnInfo.Water)
		{
			return 0f;
		}
		return 100f;
	}
}
