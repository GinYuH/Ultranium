using Terraria.ModLoader;

namespace Ultranium.NPCs.Enemy.Critter;

public class SpectreFishItem : ModItem
{
	public override void SetStaticDefaults()
	{
		// ((ModItem)this).DisplayName.SetDefault("Spectre Fish");
		// ((ModItem)this).Tooltip.SetDefault("'It seems to be staring into nothing'");
	}

	public override void SetDefaults()
	{
		((ModItem)this).Item.CloneDefaults(261);
		((ModItem)this).Item.makeNPC = (short)ModContent.NPCType<SpectreFish>();
	}
}
