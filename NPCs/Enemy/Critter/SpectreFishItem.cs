using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.NPCs.Enemy.Critter;

public class SpectreFishItem : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Spectre Fish");
		//Tooltip.SetDefault("'It seems to be staring into nothing'");
	}

	public override void SetDefaults()
	{
		Item.CloneDefaults(ItemID.Goldfish);
		Item.makeNPC = (short)ModContent.NPCType<SpectreFish>();
	}
}
