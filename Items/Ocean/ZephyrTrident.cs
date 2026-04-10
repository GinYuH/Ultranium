using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Ocean;

public class ZephyrTrident : ModItem
{
	private int currentHit;

	public override void SetStaticDefaults()
	{
		Item.staff[((ModItem)this).Item.type] = true;
		// ((ModItem)this).DisplayName.SetDefault("Zephyr Trident");
		// ((ModItem)this).Tooltip.SetDefault("A magical spear that works like a normal melee spear\nHitting an enemy with the spear itself restores small amounts of mana");
	}

	public override void SetDefaults()
	{
		((ModItem)this).Item.damage = 18;
		((ModItem)this).Item.DamageType = DamageClass.Magic;
		((ModItem)this).Item.mana = 13;
		((Entity)(object)((ModItem)this).Item).width = 80;
		((Entity)(object)((ModItem)this).Item).height = 80;
		((ModItem)this).Item.autoReuse = true;
		((ModItem)this).Item.useTime = 30;
		((ModItem)this).Item.useAnimation = 30;
		((ModItem)this).Item.useStyle = 5;
		((ModItem)this).Item.knockBack = 2f;
		((ModItem)this).Item.value = Item.buyPrice(0, 35, 45);
		((ModItem)this).Item.rare = 2;
		((ModItem)this).Item.noMelee = true;
		((ModItem)this).Item.autoReuse = true;
		((ModItem)this).Item.noUseGraphic = true;
		((ModItem)this).Item.UseSound = SoundID.Item45;
		((ModItem)this).Item.shoot = ((ModItem)this).Mod.Find<ModProjectile>("ZephyrTrident").Type;
		((ModItem)this).Item.shootSpeed = 8f;
		currentHit = 0;
	}

	public override bool CanUseItem(Player player)
	{
		if (player.ownedProjectileCounts[((ModItem)this).Item.shoot] > 0)
		{
			return false;
		}
		return true;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
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
		Recipe val = /* ((ModItem)this) */Recipe.Create((ModItem)(object)this.Type, 1);
		val.AddIngredient((Mod)null, "OceanScale", 8);
		val.AddIngredient(275, 5);
		val.AddTile(16);
		val.Register();
	}
}
