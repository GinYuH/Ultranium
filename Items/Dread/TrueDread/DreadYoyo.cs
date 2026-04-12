using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Dread.TrueDread;

public class DreadYoyo : ModItem
{
	public override void SetStaticDefaults()
	{
		DisplayName.SetDefault("The Toothball");
		Tooltip.SetDefault("Has a chance to shoot circles of dread teeth on enemy hits");
	}

	public override void SetDefaults()
	{
		Item.damage = 230;
		Item.rare = 11;
		Item.width = 24;
		Item.height = 24;
		Item.useStyle = 5;
		Item.useAnimation = 25;
		Item.useTime = 25;
		Item.UseSound = SoundID.Item1;
		Item.noUseGraphic = true;
		Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
		Item.noMelee = true;
		Item.channel = true;
		Item.value = Item.buyPrice(1);
		Item.shoot = Mod.Find<ModProjectile>("DreadYoyo").Type;
		Item.shootSpeed = 16f;
		Item.knockBack = 2.5f;
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		tooltips[0].OverrideColor = new Color(200, 0, 0);
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create(Type, 1);
		val.AddIngredient((Mod)null, "NightmareFuel", 10);
		val.AddIngredient((Mod)null, "DreadScale", 6);
		val.AddTile(412);
		val.Register();
	}
}
