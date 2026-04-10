using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Ranged;

public class SolSpear : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).Tooltip.SetDefault("Throws a fast moving sol spear\nThe spear will explode into short lived sparks when it touches an enemy or tile");
		((ModItem)this).DisplayName.SetDefault("Spear of the Sol");
	}

	public override void SetDefaults()
	{
		((ModItem)this).item.damage = 60;
		((ModItem)this).item.ranged = true;
		((Entity)(object)((ModItem)this).item).width = 56;
		((Entity)(object)((ModItem)this).item).height = 56;
		((ModItem)this).item.useTime = 30;
		((ModItem)this).item.useAnimation = 30;
		((ModItem)this).item.useStyle = 1;
		((ModItem)this).item.knockBack = 8f;
		((ModItem)this).item.noUseGraphic = true;
		((ModItem)this).item.value = Item.buyPrice(0, 50);
		((ModItem)this).item.rare = 8;
		((ModItem)this).item.UseSound = SoundID.Item60;
		((ModItem)this).item.autoReuse = true;
		((ModItem)this).item.shoot = ((ModItem)this).mod.ProjectileType("SolSpear");
		((ModItem)this).item.shootSpeed = 15f;
	}
}
