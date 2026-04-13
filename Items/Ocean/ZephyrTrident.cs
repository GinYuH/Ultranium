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
		Item.staff[Item.type] = true;
		//DisplayName.SetDefault("Zephyr Trident");
		//Tooltip.SetDefault("A magical spear that works like a normal melee spear\nHitting an enemy with the spear itself restores small amounts of mana");
	}

	public override void SetDefaults()
	{
		Item.damage = 18;
		Item.DamageType = DamageClass.Magic;
		Item.mana = 13;
		Item.width = 80;
		Item.height = 80;
		Item.autoReuse = true;
		Item.useTime = 30;
		Item.useAnimation = 30;
		Item.useStyle = ItemUseStyleID.Shoot;
		Item.knockBack = 2f;
		Item.value = Item.buyPrice(0, 35, 45);
		Item.rare = ItemRarityID.Green;
		Item.noMelee = true;
		Item.autoReuse = true;
		Item.noUseGraphic = true;
		Item.UseSound = SoundID.Item45;
		Item.shoot = Mod.Find<ModProjectile>("ZephyrTrident").Type;
		Item.shootSpeed = 8f;
		currentHit = 0;
	}

	public override bool CanUseItem(Player player)
	{
		if (player.ownedProjectileCounts[Item.shoot] > 0)
		{
			return false;
		}
		return true;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		Vector2 spinningpoint = new Vector2(velocity.X, velocity.Y);
		Vector2 zero = Vector2.Zero;
		zero = ((Main.rand.Next(2) != 1) ? spinningpoint.RotatedBy(Math.PI / (double)(Main.rand.Next(82, 1800) / 10)) : spinningpoint.RotatedBy(Math.PI / (double)(Main.rand.Next(82, 1800) / 10)));
		velocity.X = zero.X;
		velocity.Y = zero.Y;
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
		Recipe val = Recipe.Create(Type, 1);
		val.AddIngredient(null, "OceanScale", 8);
		val.AddIngredient(ItemID.Coral, 5);
		val.AddTile(TileID.Anvils);
		val.Register();
	}
}
