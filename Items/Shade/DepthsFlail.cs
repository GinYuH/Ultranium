using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Shade;

public class DepthsFlail : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Azathoth");
	}

	public override void SetDefaults()
	{
		((Entity)(object)((ModItem)this).item).width = 30;
		((Entity)(object)((ModItem)this).item).height = 11;
		((ModItem)this).item.damage = 70;
		((ModItem)this).item.knockBack = 4f;
		((ModItem)this).item.rare = 7;
		((ModItem)this).item.useStyle = 5;
		((ModItem)this).item.useAnimation = 19;
		((ModItem)this).item.useTime = 19;
		((ModItem)this).item.UseSound = SoundID.Item1;
		((ModItem)this).item.value = Item.buyPrice(0, 68);
		((ModItem)this).item.noMelee = true;
		((ModItem)this).item.noUseGraphic = true;
		((ModItem)this).item.melee = true;
		((ModItem)this).item.autoReuse = true;
		((ModItem)this).item.noMelee = true;
		((ModItem)this).item.shoot = ((ModItem)this).mod.ProjectileType("EldritchFlail");
		((ModItem)this).item.shootSpeed = 15f;
	}

	public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
	{
		float num = (Main.rand.NextFloat() - 0.75f) * ((float)Math.PI / 4f);
		Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, knockBack, player.whoAmI, 0f, num);
		return false;
	}
}
