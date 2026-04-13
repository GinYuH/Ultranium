using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Eldritch;

public class Exitium : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Exitium");
		//Tooltip.SetDefault("Fires tentacles when enemies are nearby");
	}

	public override void SetDefaults()
	{
		Item.damage = 260;
		Item.rare = ItemRarityID.Purple;
		Item.width = 24;
		Item.height = 24;
		Item.useStyle = ItemUseStyleID.Shoot;
		Item.useAnimation = 25;
		Item.useTime = 25;
		Item.UseSound = SoundID.Item1;
		Item.noUseGraphic = true;
		Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
		Item.noMelee = true;
		Item.channel = true;
		Item.value = Item.buyPrice(1, 50);
		Item.shoot = Mod.Find<ModProjectile>("Exitium").Type;
		Item.shootSpeed = 16f;
		Item.knockBack = 2.5f;
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		tooltips[0].OverrideColor = new Color(34, 166, 118);
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = Recipe.Create(Type, 1);
		val.AddIngredient(null, "NightmareScale", 8);
		val.AddIngredient(null, "NightmareBar", 12);
		val.AddIngredient(null, "DarkMatter", 10);
		val.AddTile(TileID.LunarCraftingStation);
		val.Register();
	}
}
