using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Developer;

public class RayGun : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).Tooltip.SetDefault("Imagine Pew pew in Terraria, HHHHHHHHHHHHHH im smart as frick.\n~Developer Item~");
		((ModItem)this).DisplayName.SetDefault("Ray Gun");
	}

	public override void SetDefaults()
	{
		((ModItem)this).item.damage = 350;
		((ModItem)this).item.ranged = true;
		((Entity)(object)((ModItem)this).item).width = 20;
		((Entity)(object)((ModItem)this).item).height = 40;
		((ModItem)this).item.useTime = 12;
		((ModItem)this).item.useAnimation = 12;
		((ModItem)this).item.useStyle = 5;
		((ModItem)this).item.knockBack = 6f;
		((ModItem)this).item.rare = 11;
		((ModItem)this).item.value = Item.buyPrice(2);
		((ModItem)this).item.UseSound = SoundID.Item11;
		((ModItem)this).item.autoReuse = true;
		((ModItem)this).item.shoot = ((ModItem)this).mod.ProjectileType("RayGunLaser");
		((ModItem)this).item.shootSpeed = 20f;
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		tooltips[0].overrideColor = new Color(255, 0, 0);
	}

	public override Vector2? HoldoutOffset()
	{
		return new Vector2(0f, 0f);
	}
}
