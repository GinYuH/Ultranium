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
		Tooltip.SetDefault("Imagine Pew pew in Terraria, HHHHHHHHHHHHHH im smart as frick.\n~Developer Item~");
		DisplayName.SetDefault("Ray Gun");
	}

	public override void SetDefaults()
	{
		Item.damage = 350;
		Item.DamageType = DamageClass.Ranged;
		Item.width = 20;
		Item.height = 40;
		Item.useTime = 12;
		Item.useAnimation = 12;
		Item.useStyle = 5;
		Item.knockBack = 6f;
		Item.rare = 11;
		Item.value = Item.buyPrice(2);
		Item.UseSound = SoundID.Item11;
		Item.autoReuse = true;
		Item.shoot = Mod.Find<ModProjectile>("RayGunLaser").Type;
		Item.shootSpeed = 20f;
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
