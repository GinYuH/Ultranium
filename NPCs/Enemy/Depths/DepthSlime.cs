using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace Ultranium.NPCs.Enemy.Depths;

public class DepthSlime : ModNPC
{
	public override void SetStaticDefaults()
	{
		// ((ModNPC)this).DisplayName.SetDefault("Tenebris Mass");
		Main.npcFrameCount[((ModNPC)this).NPC.type] = 2;
	}

	public override void SetDefaults()
	{
		((ModNPC)this).NPC.width = 40;
		((ModNPC)this).NPC.height = 28;
		((ModNPC)this).NPC.damage = 35;
		((ModNPC)this).NPC.defense = 20;
		((ModNPC)this).NPC.lifeMax = 220;
		((ModNPC)this).NPC.HitSound = SoundID.NPCHit1;
		((ModNPC)this).NPC.DeathSound = SoundID.NPCDeath1;
		((ModNPC)this).NPC.value = 60f;
		((ModNPC)this).NPC.value = Item.buyPrice(0, 0, 1);
		((ModNPC)this).NPC.knockBackResist = 0.5f;
		((ModNPC)this).NPC.aiStyle = 1;
		base.Banner = ((ModNPC)this).NPC.type;
		base.BannerItem = ((ModNPC)this).Mod.Find<ModItem>("DepthSlimeBanner").Type;
	}

	public override void FindFrame(int frameHeight)
	{
		((ModNPC)this).NPC.frameCounter += 1.0;
		if (((ModNPC)this).NPC.frameCounter >= 6.0)
		{
			((ModNPC)this).NPC.frame.Y = (((ModNPC)this).NPC.frame.Y + frameHeight) % (Main.npcFrameCount[((ModNPC)this).NPC.type] * frameHeight);
			((ModNPC)this).NPC.frameCounter = 1.0;
		}
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		if (((ModNPC)this).NPC.life > 0)
		{
			return;
		}
		((ModNPC)this).NPC.position.X = ((ModNPC)this).NPC.position.X + (float)(((ModNPC)this).NPC.width / 2);
		((ModNPC)this).NPC.position.Y = ((ModNPC)this).NPC.position.Y + (float)(((ModNPC)this).NPC.height / 2);
		((ModNPC)this).NPC.width = 30;
		((ModNPC)this).NPC.height = 30;
		((ModNPC)this).NPC.position.X = ((ModNPC)this).NPC.position.X - (float)(((ModNPC)this).NPC.width / 2);
		((ModNPC)this).NPC.position.Y = ((ModNPC)this).NPC.position.Y - (float)(((ModNPC)this).NPC.height / 2);
		for (int i = 0; i < 20; i++)
		{
			int num = Dust.NewDust(new Vector2(((ModNPC)this).NPC.position.X, ((ModNPC)this).NPC.position.Y), ((ModNPC)this).NPC.width, ((ModNPC)this).NPC.height, ((ModNPC)this).Mod.Find<ModDust>("ShadowDustBlack").Type, 0f, 0f, 100, default(Color), 2f);
			Main.dust[num].velocity *= 3f;
			if (Main.rand.Next(2) == 0)
			{
				Main.dust[num].scale = 0.5f;
				Main.dust[num].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
			}
		}
	}

	public override void OnKill()
	{
		Item.NewItem(((ModNPC)this).NPC.getRect(), ((ModNPC)this).Mod.Find<ModItem>("ShadowOre").Type, Main.rand.Next(3, 6), false, 0, false, false);
		if (Main.rand.Next(5) == 0)
		{
			Item.NewItem((int)((ModNPC)this).NPC.position.X, (int)((ModNPC)this).NPC.position.Y, ((ModNPC)this).NPC.width, ((ModNPC)this).NPC.height, ((ModNPC)this).Mod.Find<ModItem>("ShadowEssence").Type, 1, false, 0, false, false);
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
}
