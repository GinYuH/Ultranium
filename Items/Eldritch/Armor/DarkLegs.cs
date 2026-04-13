using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Eldritch.Armor;

[AutoloadEquip(EquipType.Legs)]
public class DarkLegs : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Darkmatter Leggings");
		//Tooltip.SetDefault("+10 max mana and +1 max minions\n8% increased melee speed and 12% increased movement speed");
	}

	public override void SetDefaults()
	{
		Item.width = 18;
		Item.height = 18;
		Item.value = Item.buyPrice(0, 80);
		Item.rare = ItemRarityID.Purple;
		Item.defense = 33;
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		tooltips[0].OverrideColor = new Color(34, 166, 118);
	}

	public override void UpdateEquip(Player player)
	{
		player.statManaMax2 += 10;
		player.maxMinions++;
		player.GetAttackSpeed(DamageClass.Melee) += 0.08f;
		player.moveSpeed += 12f;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = Recipe.Create(Type, 1);
		val.AddIngredient(null, "NightmareScale", 12);
		val.AddIngredient(null, "NightmareBar", 12);
		val.AddIngredient(null, "DarkMatter", 15);
		val.AddTile(TileID.LunarCraftingStation);
		val.Register();
	}
}
