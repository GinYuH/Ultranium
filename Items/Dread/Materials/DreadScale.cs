using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Dread.Materials;

public class DreadScale : ModItem
{
	public override void SetStaticDefaults()
	{
		// ((ModItem)this).DisplayName.SetDefault("Dread Scale");
		// ((ModItem)this).Tooltip.SetDefault("");
	}

	public override void SetDefaults()
	{
		Item item = new Item();
		((Entity)(object)((ModItem)this).Item).width = ((Entity)(object)item).width;
		((Entity)(object)((ModItem)this).Item).height = ((Entity)(object)item).height;
		((ModItem)this).Item.maxStack = 999;
		((ModItem)this).Item.value = Item.buyPrice(0, 0, 15);
		((ModItem)this).Item.rare = 4;
	}
}
