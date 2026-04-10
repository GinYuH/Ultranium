using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Guardians.Nature;

public class UltraFlail : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Nature Power Flail");
		((ModItem)this).Tooltip.SetDefault("Has a chance to create lingering ultranium energy bolts upon striking an enemy");
	}

	public override void SetDefaults()
	{
		((Entity)(object)((ModItem)this).item).width = 38;
		((Entity)(object)((ModItem)this).item).height = 54;
		((ModItem)this).item.damage = 220;
		((ModItem)this).item.noMelee = true;
		((ModItem)this).item.noUseGraphic = true;
		((ModItem)this).item.channel = true;
		((ModItem)this).item.autoReuse = true;
		((ModItem)this).item.melee = true;
		((ModItem)this).item.useAnimation = 10;
		((ModItem)this).item.useTime = 10;
		((ModItem)this).item.useStyle = 5;
		((ModItem)this).item.knockBack = 2f;
		((ModItem)this).item.rare = 11;
		((ModItem)this).item.value = Item.buyPrice(1);
		((ModItem)this).item.UseSound = SoundID.Item116;
		((ModItem)this).item.shoot = ((ModItem)this).mod.ProjectileType("UltraFlail");
		((ModItem)this).item.shootSpeed = 22f;
	}

	public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
	{
		float num = (Main.rand.NextFloat() - 0.75f) * ((float)Math.PI / 4f);
		float num2 = 0.783f;
		float num3 = (float)Math.Sqrt(speedX * speedX + speedY * speedY);
		double num4 = Math.Atan2(speedX, speedY) - 0.1;
		double num5 = num2 / 6f;
		double num6 = num4 + num5 * 1.0;
		Projectile.NewProjectile(position.X, position.Y, num3 * (float)Math.Sin(num6), num3 * (float)Math.Cos(num6), ((ModItem)this).mod.ProjectileType("UltraFlail"), damage, knockBack, player.whoAmI, 0f, num);
		return false;
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		tooltips[0].overrideColor = new Color(241, 166, 0);
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		ModRecipe val = new ModRecipe(((ModItem)this).mod);
		val.AddIngredient((Mod)null, "UltrumShard", 10);
		val.AddTile(412);
		val.SetResult((ModItem)(object)this, 1);
		val.AddRecipe();
	}
}
