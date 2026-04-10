using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Dread.TrueDread;

public class DreadSpear : ModItem
{
	private int currentHit;

	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Inquietude Impaler");
		((ModItem)this).Tooltip.SetDefault("Fires slightly inaccurate dread scythes");
	}

	public override void SetDefaults()
	{
		((ModItem)this).item.damage = 200;
		((Entity)(object)((ModItem)this).item).width = 64;
		((Entity)(object)((ModItem)this).item).height = 64;
		((ModItem)this).item.rare = 11;
		((ModItem)this).item.knockBack = 9f;
		((ModItem)this).item.useStyle = 5;
		((ModItem)this).item.useTime = 17;
		((ModItem)this).item.useAnimation = 17;
		((ModItem)this).item.melee = true;
		((ModItem)this).item.noMelee = true;
		((ModItem)this).item.autoReuse = true;
		((ModItem)this).item.noUseGraphic = true;
		((ModItem)this).item.useStyle = 5;
		((ModItem)this).item.value = Item.buyPrice(1);
		((ModItem)this).item.shoot = ((ModItem)this).mod.ProjectileType("DreadSpear");
		((ModItem)this).item.shootSpeed = 15f;
		((ModItem)this).item.UseSound = SoundID.Item1;
		currentHit = 0;
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		tooltips[0].overrideColor = new Color(200, 0, 0);
	}

	public override bool CanUseItem(Player player)
	{
		if (player.ownedProjectileCounts[((ModItem)this).item.shoot] > 0)
		{
			return false;
		}
		return true;
	}

	public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
	{
		Vector2 spinningpoint = new Vector2(speedX, speedY);
		Vector2 zero = Vector2.Zero;
		zero = ((Main.rand.Next(2) != 1) ? spinningpoint.RotatedBy(-Math.PI / (double)(Main.rand.Next(82, 1800) / 10)) : spinningpoint.RotatedBy(Math.PI / (double)(Main.rand.Next(82, 1800) / 10)));
		speedX = zero.X;
		speedY = zero.Y;
		currentHit++;
		return true;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		ModRecipe val = new ModRecipe(((ModItem)this).mod);
		val.AddIngredient((Mod)null, "NightmareFuel", 10);
		val.AddIngredient((Mod)null, "DreadScale", 6);
		val.AddTile(412);
		val.SetResult((ModItem)(object)this, 1);
		val.AddRecipe();
	}
}
