using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Vanity.BossMasks;

[AutoloadEquip(/*Could not decode attribute arguments.*/)]
public class DreadMask : ModItem
{
	public override void SetStaticDefaults()
	{
		// ((ModItem)this).DisplayName.SetDefault("Dread Mask");
		// ((ModItem)this).Tooltip.SetDefault("");
	}

	public override void SetDefaults()
	{
		((Entity)(object)((ModItem)this).Item).width = 28;
		((Entity)(object)((ModItem)this).Item).height = 26;
		((ModItem)this).Item.value = Item.sellPrice();
		((ModItem)this).Item.rare = 1;
		((ModItem)this).Item.vanity = true;
	}
}
