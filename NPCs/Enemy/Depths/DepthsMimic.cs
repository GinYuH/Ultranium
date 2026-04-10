using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.NPCs.Enemy.Depths;

public class DepthsMimic : ModNPC
{
	public override void SetStaticDefaults()
	{
		((ModNPC)this).DisplayName.SetDefault("Depths Mimic");
		Main.npcFrameCount[((ModNPC)this).npc.type] = Main.npcFrameCount[473];
	}

	public override void SetDefaults()
	{
		((ModNPC)this).npc.npcSlots = 1f;
		((ModNPC)this).npc.width = 42;
		((ModNPC)this).npc.height = 52;
		((ModNPC)this).npc.damage = 90;
		((ModNPC)this).npc.defense = 34;
		((ModNPC)this).npc.lifeMax = 3500;
		((ModNPC)this).npc.knockBackResist = 0.1f;
		((ModNPC)this).npc.aiStyle = 87;
		base.aiType = 473;
		base.animationType = 473;
		((ModNPC)this).npc.HitSound = SoundID.NPCHit4;
		((ModNPC)this).npc.DeathSound = SoundID.NPCDeath6;
		base.banner = ((ModNPC)this).npc.type;
		base.bannerItem = ((ModNPC)this).mod.ItemType("DepthsMimicBanner");
	}

	public override void HitEffect(int hitDirection, double damage)
	{
		if (((ModNPC)this).npc.life > 0)
		{
			return;
		}
		((ModNPC)this).npc.position.X = ((ModNPC)this).npc.position.X + (float)(((ModNPC)this).npc.width / 2);
		((ModNPC)this).npc.position.Y = ((ModNPC)this).npc.position.Y + (float)(((ModNPC)this).npc.height / 2);
		((ModNPC)this).npc.width = 30;
		((ModNPC)this).npc.height = 30;
		((ModNPC)this).npc.position.X = ((ModNPC)this).npc.position.X - (float)(((ModNPC)this).npc.width / 2);
		((ModNPC)this).npc.position.Y = ((ModNPC)this).npc.position.Y - (float)(((ModNPC)this).npc.height / 2);
		for (int i = 0; i < 20; i++)
		{
			int num = Dust.NewDust(new Vector2(((ModNPC)this).npc.position.X, ((ModNPC)this).npc.position.Y), ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, ((ModNPC)this).mod.DustType("ShadowDustBlack"), 0f, 0f, 100, default(Color), 2f);
			Main.dust[num].velocity *= 3f;
			if (Main.rand.Next(2) == 0)
			{
				Main.dust[num].scale = 0.5f;
				Main.dust[num].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
			}
		}
	}

	public override float SpawnChance(NPCSpawnInfo spawnInfo)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		Player player = spawnInfo.player;
		if (!player.ZoneTowerSolar && !player.ZoneTowerVortex && !player.ZoneTowerNebula && !player.ZoneTowerStardust && ((!Main.pumpkinMoon && !Main.snowMoon) || (double)spawnInfo.spawnTileY > Main.worldSurface || Main.dayTime) && (!Main.eclipse || (double)spawnInfo.spawnTileY > Main.worldSurface || !Main.dayTime) && SpawnCondition.GoblinArmy.Chance == 0f && !NPC.AnyNPCs(ModContent.NPCType<DepthsMimic>()))
		{
			if (!spawnInfo.player.GetModPlayer<UltraniumPlayer>().ZoneDepth)
			{
				return 0f;
			}
			return 1.5f;
		}
		return 0f;
	}

	public override void NPCLoot()
	{
		int num = Main.rand.Next(4);
		if (num == 0)
		{
			Item.NewItem((int)((ModNPC)this).npc.position.X, (int)((ModNPC)this).npc.position.Y, ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, ((ModNPC)this).mod.ItemType("DepthsFlail"), 1, false, 0, false, false);
		}
		if (num == 1)
		{
			Item.NewItem((int)((ModNPC)this).npc.position.X, (int)((ModNPC)this).npc.position.Y, ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, ((ModNPC)this).mod.ItemType("DepthsBow"), 1, false, 0, false, false);
		}
		if (num == 2)
		{
			Item.NewItem((int)((ModNPC)this).npc.position.X, (int)((ModNPC)this).npc.position.Y, ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, ((ModNPC)this).mod.ItemType("DepthsTome"), 1, false, 0, false, false);
		}
		if (num == 3)
		{
			Item.NewItem((int)((ModNPC)this).npc.position.X, (int)((ModNPC)this).npc.position.Y, ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, ((ModNPC)this).mod.ItemType("DepthsStaff"), 1, false, 0, false, false);
		}
		Item.NewItem((int)((ModNPC)this).npc.position.X, (int)((ModNPC)this).npc.position.Y, ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, 499, Main.rand.Next(5, 10), false, 0, false, false);
		Item.NewItem((int)((ModNPC)this).npc.position.X, (int)((ModNPC)this).npc.position.Y, ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, 500, Main.rand.Next(5, 15), false, 0, false, false);
	}
}
