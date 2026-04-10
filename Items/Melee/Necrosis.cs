using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Melee;

public class Necrosis : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Necrosis");
		((ModItem)this).Tooltip.SetDefault("Fires Skulls upon swing");
	}

	public override void SetDefaults()
	{
		((ModItem)this).item.damage = 27;
		((ModItem)this).item.melee = true;
		((Entity)(object)((ModItem)this).item).width = 42;
		((Entity)(object)((ModItem)this).item).height = 42;
		((ModItem)this).item.useTime = 50;
		((ModItem)this).item.useAnimation = 25;
		((ModItem)this).item.useStyle = 1;
		((ModItem)this).item.knockBack = 6f;
		((ModItem)this).item.value = Item.buyPrice(0, 30);
		((ModItem)this).item.rare = 2;
		((ModItem)this).item.UseSound = SoundID.Item1;
		((ModItem)this).item.autoReuse = true;
		((ModItem)this).item.shoot = 270;
		((ModItem)this).item.shootSpeed = 15f;
	}
}
