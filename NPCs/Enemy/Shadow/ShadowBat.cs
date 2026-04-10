using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace Ultranium.NPCs.Enemy.Shadow;

public class ShadowBat : ModNPC
{
	public override void SetStaticDefaults()
	{
		// ((ModNPC)this).DisplayName.SetDefault("Shadow Bat");
		Main.npcFrameCount[((ModNPC)this).NPC.type] = 4;
	}

	public override void SetDefaults()
	{
		((ModNPC)this).NPC.width = 18;
		((ModNPC)this).NPC.height = 40;
		((ModNPC)this).NPC.damage = 12;
		((ModNPC)this).NPC.defense = 10;
		((ModNPC)this).NPC.lifeMax = 45;
		((ModNPC)this).NPC.value = Item.buyPrice(0, 0, 1);
		((ModNPC)this).NPC.HitSound = SoundID.NPCHit1;
		((ModNPC)this).NPC.DeathSound = SoundID.NPCDeath4;
		((ModNPC)this).NPC.value = 60f;
		((ModNPC)this).NPC.knockBackResist = 0.5f;
		((ModNPC)this).NPC.aiStyle = 14;
		base.AIType = 49;
		base.Banner = ((ModNPC)this).NPC.type;
		base.BannerItem = ((ModNPC)this).Mod.Find<ModItem>("ShadowBatBanner").Type;
	}

	public override void AI()
	{
		((ModNPC)this).NPC.rotation = ((ModNPC)this).NPC.velocity.X * 0.03f;
		((ModNPC)this).NPC.spriteDirection = ((ModNPC)this).NPC.direction;
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		if (((ModNPC)this).NPC.life <= 0)
		{
			Gore.NewGore(((ModNPC)this).NPC.position, ((ModNPC)this).NPC.velocity, ((ModNPC)this).Mod.GetGoreSlot("Gores/ShadowBiome/ShadowBatGore"));
		}
	}

	public override void OnKill()
	{
		if (Utils.NextBool(Main.rand, 2))
		{
			Item.NewItem(((ModNPC)this).NPC.getRect(), ((ModNPC)this).Mod.Find<ModItem>("ShadowEssence").Type, 1, false, 0, false, false);
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
