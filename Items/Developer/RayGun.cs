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
		// ((ModItem)this).Tooltip.SetDefault("Imagine Pew pew in Terraria, HHHHHHHHHHHHHH im smart as frick.\n~Developer Item~");
		// ((ModItem)this).DisplayName.SetDefault("Ray Gun");
	}

	public override void SetDefaults()
	{
		((ModItem)this).Item.damage = 350;
		((ModItem)this).Item.DamageType = DamageClass.Ranged;
		((Entity)(object)((ModItem)this).Item).width = 20;
		((Entity)(object)((ModItem)this).Item).height = 40;
		((ModItem)this).Item.useTime = 12;
		((ModItem)this).Item.useAnimation = 12;
		((ModItem)this).Item.useStyle = 5;
		((ModItem)this).Item.knockBack = 6f;
		((ModItem)this).Item.rare = 11;
		((ModItem)this).Item.value = Item.buyPrice(2);
		((ModItem)this).Item.UseSound = SoundID.Item11;
		((ModItem)this).Item.autoReuse = true;
		((ModItem)this).Item.shoot = ((ModItem)this).Mod.Find<ModProjectile>("RayGunLaser").Type;
		((ModItem)this).Item.shootSpeed = 20f;
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		tooltips[0].OverrideColor = new Color(255, 0, 0);
	}

	public override Vector2? HoldoutOffset()
	{
		return new Vector2(0f, 0f);
	}
}
