using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.NPCs.Enemy.Critter;

public class SpectreFish : ModNPC
{
	public override void SetStaticDefaults()
	{
		((ModNPC)this).DisplayName.SetDefault("Spectre Fish");
		Main.npcFrameCount[((ModNPC)this).npc.type] = 4;
	}

	public override void SetDefaults()
	{
		((ModNPC)this).npc.width = 34;
		((ModNPC)this).npc.height = 24;
		((ModNPC)this).npc.damage = 0;
		((ModNPC)this).npc.defense = 0;
		((ModNPC)this).npc.lifeMax = 12;
		Main.npcCatchable[((ModNPC)this).npc.type] = true;
		((ModNPC)this).npc.catchItem = (short)ModContent.ItemType<SpectreFishItem>();
		((ModNPC)this).npc.HitSound = SoundID.NPCHit1;
		((ModNPC)this).npc.DeathSound = SoundID.NPCDeath1;
		((ModNPC)this).npc.knockBackResist = 0.35f;
		((ModNPC)this).npc.aiStyle = 16;
		((ModNPC)this).npc.noGravity = true;
		((ModNPC)this).npc.npcSlots = 0f;
		base.aiType = 55;
		((ModNPC)this).npc.dontCountMe = true;
	}

	public override void FindFrame(int frameHeight)
	{
		((ModNPC)this).npc.frameCounter += 0.15000000596046448;
		((ModNPC)this).npc.frameCounter %= Main.npcFrameCount[((ModNPC)this).npc.type];
		int num = (int)((ModNPC)this).npc.frameCounter;
		((ModNPC)this).npc.frame.Y = num * frameHeight;
	}

	public override void AI()
	{
		((ModNPC)this).npc.spriteDirection = ((ModNPC)this).npc.direction;
	}

	public override float SpawnChance(NPCSpawnInfo spawnInfo)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		if (!spawnInfo.player.GetModPlayer<UltraniumPlayer>().ZoneShadow || !spawnInfo.water)
		{
			return 0f;
		}
		return 100f;
	}
}
