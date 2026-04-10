using Terraria.ModLoader;

namespace Ultranium.NPCs.Enemy.Critter;

public class DepthSnailItem : ModItem
{
	public override void SetStaticDefaults()
	{
		// ((ModItem)this).DisplayName.SetDefault("Depth Snail");
		// ((ModItem)this).Tooltip.SetDefault("'It's glow is almost hypnotic'");
	}

	public override void SetDefaults()
	{
		((ModItem)this).Item.CloneDefaults(2007);
		((ModItem)this).Item.bait = 40;
		((ModItem)this).Item.makeNPC = (short)ModContent.NPCType<DepthSnail>();
	}
}
