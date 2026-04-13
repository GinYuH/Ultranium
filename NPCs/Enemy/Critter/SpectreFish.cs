using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.NPCs.Enemy.Critter;

public class SpectreFish : ModNPC
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Spectre Fish");
		Main.npcFrameCount[NPC.type] = 4;
	}

	public override void SetDefaults()
	{
		NPC.width = 34;
		NPC.height = 24;
		NPC.damage = 0;
		NPC.defense = 0;
		NPC.lifeMax = 12;
		Main.npcCatchable[NPC.type] = true;
		NPC.catchItem = (short)ModContent.ItemType<SpectreFishItem>();
		NPC.HitSound = SoundID.NPCHit1;
		NPC.DeathSound = SoundID.NPCDeath1;
		NPC.knockBackResist = 0.35f;
		NPC.aiStyle = NPCAIStyleID.Piranha;
		NPC.noGravity = true;
		NPC.npcSlots = 0f;
		base.AIType = NPCID.Goldfish;
		NPC.dontCountMe = true;
	}

	public override void FindFrame(int frameHeight)
	{
		NPC.frameCounter += 0.15000000596046448;
		NPC.frameCounter %= Main.npcFrameCount[NPC.type];
		int num = (int)NPC.frameCounter;
		NPC.frame.Y = num * frameHeight;
	}

	public override void AI()
	{
		NPC.spriteDirection = NPC.direction;
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
