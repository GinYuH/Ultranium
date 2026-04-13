using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Dread.TrueDread;

public class DreadSpear : ModItem
{
	private int currentHit;

	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Inquietude Impaler");
		//Tooltip.SetDefault("Fires slightly inaccurate dread scythes");
	}

	public override void SetDefaults()
	{
		Item.damage = 200;
		Item.width = 64;
		Item.height = 64;
		Item.rare = ItemRarityID.Purple;
		Item.knockBack = 9f;
		Item.useStyle = ItemUseStyleID.Shoot;
		Item.useTime = 17;
		Item.useAnimation = 17;
		Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
		Item.noMelee = true;
		Item.autoReuse = true;
		Item.noUseGraphic = true;
		Item.useStyle = ItemUseStyleID.Shoot;
		Item.value = Item.buyPrice(1);
		Item.shoot = Mod.Find<ModProjectile>("DreadSpear").Type;
		Item.shootSpeed = 15f;
		Item.UseSound = SoundID.Item1;
		currentHit = 0;
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		tooltips[0].OverrideColor = new Color(200, 0, 0);
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
		zero = ((Main.rand.Next(2) != 1) ? spinningpoint.RotatedBy(-Math.PI / (double)(Main.rand.Next(82, 1800) / 10)) : spinningpoint.RotatedBy(Math.PI / (double)(Main.rand.Next(82, 1800) / 10)));
		velocity.X = zero.X;
		velocity.Y = zero.Y;
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
		Recipe val = Recipe.Create(Type, 1);
		val.AddIngredient(null, "NightmareFuel", 10);
		val.AddIngredient(null, "DreadScale", 6);
		val.AddTile(TileID.LunarCraftingStation);
		val.Register();
	}
}
