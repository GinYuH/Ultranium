using Terraria.ModLoader;

namespace Ultranium.NPCs.Enemy.Critter;

public class DepthSnailItem : ModItem
{
	public override void SetStaticDefaults()
	{
		DisplayName.SetDefault("Depth Snail");
		Tooltip.SetDefault("'It's glow is almost hypnotic'");
	}

	public override void SetDefaults()
	{
		Item.CloneDefaults(2007);
		Item.bait = 40;
		Item.makeNPC = (short)ModContent.NPCType<DepthSnail>();
	}
}
