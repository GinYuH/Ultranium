using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Guardians.Nature;

public class UltraFlail : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Nature Power Flail");
		//Tooltip.SetDefault("Has a chance to create lingering ultranium energy bolts upon striking an enemy");
	}

	public override void SetDefaults()
	{
		Item.width = 38;
		Item.height = 54;
		Item.damage = 220;
		Item.noMelee = true;
		Item.noUseGraphic = true;
		Item.channel = true;
		Item.autoReuse = true;
		Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
		Item.useAnimation = 10;
		Item.useTime = 10;
		Item.useStyle = ItemUseStyleID.Shoot;
		Item.knockBack = 2f;
		Item.rare = ItemRarityID.Purple;
		Item.value = Item.buyPrice(1);
		Item.UseSound = SoundID.Item116;
		Item.shoot = Mod.Find<ModProjectile>("UltraFlail").Type;
		Item.shootSpeed = 22f;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		float num = (Main.rand.NextFloat() - 0.75f) * ((float)Math.PI / 4f);
		float num2 = 0.783f;
		float num3 = (float)Math.Sqrt(velocity.X * velocity.X + velocity.Y * velocity.Y);
		double num4 = Math.Atan2(velocity.X, velocity.Y) - 0.1;
		double num5 = num2 / 6f;
		double num6 = num4 + num5 * 1.0;
		Projectile.NewProjectile(source, position.X, position.Y, num3 * (float)Math.Sin(num6), num3 * (float)Math.Cos(num6), Mod.Find<ModProjectile>("UltraFlail").Type, damage, knockback, player.whoAmI, 0f, num);
		return false;
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		tooltips[0].OverrideColor = new Color(241, 166, 0);
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = Recipe.Create(Type, 1);
		val.AddIngredient(null, "UltrumShard", 10);
		val.AddTile(TileID.LunarCraftingStation);
		val.Register();
	}
}
