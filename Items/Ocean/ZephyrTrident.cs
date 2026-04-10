using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Ocean;

public class ZephyrTrident : ModItem
{
	private int currentHit;

	public override void SetStaticDefaults()
	{
		Item.staff[((ModItem)this).item.type] = true;
		((ModItem)this).DisplayName.SetDefault("Zephyr Trident");
		((ModItem)this).Tooltip.SetDefault("A magical spear that works like a normal melee spear\nHitting an enemy with the spear itself restores small amounts of mana");
	}

	public override void SetDefaults()
	{
		((ModItem)this).item.damage = 18;
		((ModItem)this).item.magic = true;
		((ModItem)this).item.mana = 13;
		((Entity)(object)((ModItem)this).item).width = 80;
		((Entity)(object)((ModItem)this).item).height = 80;
		((ModItem)this).item.autoReuse = true;
		((ModItem)this).item.useTime = 30;
		((ModItem)this).item.useAnimation = 30;
		((ModItem)this).item.useStyle = 5;
		((ModItem)this).item.knockBack = 2f;
		((ModItem)this).item.value = Item.buyPrice(0, 35, 45);
		((ModItem)this).item.rare = 2;
		((ModItem)this).item.noMelee = true;
		((ModItem)this).item.autoReuse = true;
		((ModItem)this).item.noUseGraphic = true;
		((ModItem)this).item.UseSound = SoundID.Item45;
		((ModItem)this).item.shoot = ((ModItem)this).mod.ProjectileType("ZephyrTrident");
		((ModItem)this).item.shootSpeed = 8f;
		currentHit = 0;
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
		zero = ((Main.rand.Next(2) != 1) ? spinningpoint.RotatedBy(Math.PI / (double)(Main.rand.Next(82, 1800) / 10)) : spinningpoint.RotatedBy(Math.PI / (double)(Main.rand.Next(82, 1800) / 10)));
		speedX = zero.X;
		speedY = zero.Y;
		currentHit++;
		return true;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		ModRecipe val = new ModRecipe(((ModItem)this).mod);
		val.AddIngredient((Mod)null, "OceanScale", 8);
		val.AddIngredient(275, 5);
		val.AddTile(16);
		val.SetResult((ModItem)(object)this, 1);
		val.AddRecipe();
	}
}
