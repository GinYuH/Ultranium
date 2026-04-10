using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Dread.Materials;

public class DreadScale : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Dread Scale");
		((ModItem)this).Tooltip.SetDefault("");
	}

	public override void SetDefaults()
	{
		Item item = new Item();
		((Entity)(object)((ModItem)this).item).width = ((Entity)(object)item).width;
		((Entity)(object)((ModItem)this).item).height = ((Entity)(object)item).height;
		((ModItem)this).item.maxStack = 999;
		((ModItem)this).item.value = Item.buyPrice(0, 0, 15);
		((ModItem)this).item.rare = 4;
	}
}
