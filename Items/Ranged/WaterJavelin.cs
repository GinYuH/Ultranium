using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Ranged;

public class WaterJavelin : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Ancient Javelin");
		((ModItem)this).Tooltip.SetDefault("");
	}

	public override void SetDefaults()
	{
		((ModItem)this).item.damage = 18;
		((ModItem)this).item.ranged = true;
		((Entity)(object)((ModItem)this).item).width = 56;
		((Entity)(object)((ModItem)this).item).height = 56;
		((ModItem)this).item.useTime = 28;
		((ModItem)this).item.useAnimation = 28;
		((ModItem)this).item.useStyle = 1;
		((ModItem)this).item.knockBack = 6f;
		((ModItem)this).item.value = Item.buyPrice(0, 20);
		((ModItem)this).item.rare = 2;
		((ModItem)this).item.autoReuse = true;
		((ModItem)this).item.shoot = ((ModItem)this).mod.ProjectileType("WaterJavelin");
		((ModItem)this).item.shootSpeed = 9f;
		((ModItem)this).item.useTurn = true;
		((ModItem)this).item.maxStack = 1;
		((ModItem)this).item.noUseGraphic = true;
	}
}
