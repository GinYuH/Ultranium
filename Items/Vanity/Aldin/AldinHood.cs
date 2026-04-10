using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Vanity.Aldin;

[AutoloadEquip(/*Could not decode attribute arguments.*/)]
public class AldinHood : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).SetStaticDefaults();
		// ((ModItem)this).DisplayName.SetDefault("Cosmic Mage's Hood");
		// ((ModItem)this).Tooltip.SetDefault("");
	}

	public override void SetDefaults()
	{
		((Entity)(object)((ModItem)this).Item).width = 18;
		((Entity)(object)((ModItem)this).Item).height = 18;
		((ModItem)this).Item.vanity = true;
		((ModItem)this).Item.rare = 9;
	}
}
