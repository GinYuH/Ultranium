using Terraria;
using Terraria.ModLoader;

namespace Ultranium.NPCs.Town.Shrooms;

public class TheFart : ModItem
{
	public override void SetStaticDefaults()
	{
		// ((ModItem)this).DisplayName.SetDefault("The Fart");
		// ((ModItem)this).Tooltip.SetDefault("A mystical fart cloud that is constantly taking the shape of a mushroom.\nIts smell is legendary, whether that is a good thing or not is up to you.");
	}

	public override void SetDefaults()
	{
		((Entity)(object)((ModItem)this).Item).width = 20;
		((Entity)(object)((ModItem)this).Item).height = 30;
		((ModItem)this).Item.rare = -11;
		((ModItem)this).Item.maxStack = 1;
	}
}
