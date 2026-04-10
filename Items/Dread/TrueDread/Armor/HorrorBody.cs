using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Dread.TrueDread.Armor;

[AutoloadEquip(/*Could not decode attribute arguments.*/)]
public class HorrorBody : ModItem
{
	public override void SetStaticDefaults()
	{
		// DisplayName.SetDefault("Horror Breastplate");
		// Tooltip.SetDefault("7% increased damage and critical strike chance\n+20 max life and +1 max minions\n5% increased damage reduction");
	}

	public override void SetDefaults()
	{
		Item.width = 18;
		Item.height = 18;
		Item.value = Item.buyPrice(1);
		Item.rare = 11;
		Item.defense = 38;
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		tooltips[0].OverrideColor = new Color(200, 0, 0);
	}

	public override void UpdateEquip(Player player)
	{
		player.statLifeMax2 += 20;
		player.maxMinions++;
		player.GetDamage(DamageClass.Melee) += 0.07f;
		player.GetDamage(DamageClass.Ranged) += 0.07f;
		player.GetDamage(DamageClass.Magic) += 0.07f;
		player.GetDamage(DamageClass.Summon) += 0.07f;
		player.endurance += 0.05f;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create((ModItem)(object)this.Type, 1);
		val.AddIngredient((Mod)null, "NightmareFuel", 16);
		val.AddTile(412);
		val.Register();
	}
}
