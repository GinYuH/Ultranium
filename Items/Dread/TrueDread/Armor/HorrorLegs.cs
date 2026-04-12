using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Dread.TrueDread.Armor;

[AutoloadEquip(/*Could not decode attribute arguments.*/)]
public class HorrorLegs : ModItem
{
	public override void SetStaticDefaults()
	{
		DisplayName.SetDefault("Horror Leggings");
		Tooltip.SetDefault("8% increased damage, melee speed, and movement speed");
	}

	public override void SetDefaults()
	{
		Item.width = 18;
		Item.height = 18;
		Item.value = Item.buyPrice(1);
		Item.rare = 11;
		Item.defense = 26;
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		tooltips[0].OverrideColor = new Color(200, 0, 0);
	}

	public override void UpdateEquip(Player player)
	{
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create(Type, 1);
		val.AddIngredient((Mod)null, "NightmareFuel", 12);
		val.AddTile(412);
		val.Register();
	}
}
