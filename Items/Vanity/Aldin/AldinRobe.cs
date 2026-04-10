using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Vanity.Aldin;

[AutoloadEquip(/*Could not decode attribute arguments.*/)]
public class AldinRobe : ModItem
{
	public override void SetStaticDefaults()
	{
		SetStaticDefaults();
		// DisplayName.SetDefault("Cosmic Mage's Robe");
		// Tooltip.SetDefault("");
	}

	public override void SetDefaults()
	{
		((Entity)(object)Item).width = 18;
		((Entity)(object)Item).height = 18;
		Item.vanity = true;
		Item.rare = 9;
	}
}
