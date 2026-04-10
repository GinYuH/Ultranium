using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.NPCs.Enemy.Dread;

public class DreadApparition : ModNPC
{
	public override void SetStaticDefaults()
	{
		((ModNPC)this).DisplayName.SetDefault("Dread Apparition");
		Main.npcFrameCount[((ModNPC)this).npc.type] = 6;
	}

	public override void SetDefaults()
	{
		((ModNPC)this).npc.width = 48;
		((ModNPC)this).npc.height = 34;
		((ModNPC)this).npc.damage = 35;
		((ModNPC)this).npc.defense = 15;
		((ModNPC)this).npc.lifeMax = 200;
		((ModNPC)this).npc.HitSound = SoundID.NPCHit1;
		((ModNPC)this).npc.DeathSound = SoundID.NPCDeath6;
		((ModNPC)this).npc.aiStyle = 86;
		((ModNPC)this).npc.noGravity = true;
		((ModNPC)this).npc.lavaImmune = true;
		base.aiType = 472;
		base.animationType = 472;
		base.banner = ((ModNPC)this).npc.type;
		base.bannerItem = ((ModNPC)this).mod.ItemType("DreadApparitionBanner");
	}

	public override float SpawnChance(NPCSpawnInfo spawnInfo)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		Player player = spawnInfo.player;
		if (!player.ZoneTowerSolar && !player.ZoneTowerVortex && !player.ZoneTowerNebula && !player.ZoneTowerStardust && ((!Main.pumpkinMoon && !Main.snowMoon) || (double)spawnInfo.spawnTileY > Main.worldSurface || Main.dayTime) && (!Main.eclipse || (double)spawnInfo.spawnTileY > Main.worldSurface || !Main.dayTime) && SpawnCondition.GoblinArmy.Chance == 0f)
		{
			if (!spawnInfo.player.ZoneSkyHeight || !Main.hardMode || UltraniumWorld.downedDread)
			{
				return 0f;
			}
			return 0.06f;
		}
		return 0f;
	}

	public override void NPCLoot()
	{
		Item.NewItem((int)((ModNPC)this).npc.position.X, (int)((ModNPC)this).npc.position.Y, ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, ((ModNPC)this).mod.ItemType("DreadFlame"), Main.rand.Next(1, 3), false, 0, false, false);
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
			int num = Dust.NewDust(new Vector2(((ModNPC)this).npc.position.X, ((ModNPC)this).npc.position.Y), ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, 90, 0f, 0f, 100, default(Color), 2f);
			Main.dust[num].velocity *= 3f;
			if (Main.rand.Next(2) == 0)
			{
				Main.dust[num].scale = 0.5f;
				Main.dust[num].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
			}
		}
	}
}
