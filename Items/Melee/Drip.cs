using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Melee;

public class Drip : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("The Dripper");
		((ModItem)this).Tooltip.SetDefault("Shoots Water bolts in random directions");
	}

	public override void SetDefaults()
	{
		((ModItem)this).item.useStyle = 5;
		((Entity)(object)((ModItem)this).item).width = 30;
		((Entity)(object)((ModItem)this).item).height = 26;
		((ModItem)this).item.noUseGraphic = true;
		((ModItem)this).item.melee = true;
		((ModItem)this).item.noMelee = true;
		((ModItem)this).item.channel = true;
		((ModItem)this).item.UseSound = SoundID.Item1;
		((ModItem)this).item.useAnimation = 25;
		((ModItem)this).item.useTime = 25;
		((ModItem)this).item.shoot = ((ModItem)this).mod.ProjectileType("DripProjectile");
		((ModItem)this).item.shootSpeed = 16f;
		((ModItem)this).item.knockBack = 2.5f;
		((ModItem)this).item.damage = 23;
		((ModItem)this).item.value = Item.buyPrice(0, 20);
		((ModItem)this).item.rare = 3;
	}
}
