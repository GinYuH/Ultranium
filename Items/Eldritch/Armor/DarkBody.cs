using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Eldritch.Armor;

[AutoloadEquip(/*Could not decode attribute arguments.*/)]
public class DarkBody : ModItem
{
	public override void SetStaticDefaults()
	{
		// ((ModItem)this).DisplayName.SetDefault("Darkmatter Chestmail");
		// ((ModItem)this).Tooltip.SetDefault("12% increased critical strike chance\n+10 max life and mana, and +2 max minions\n5% increased damage reduction\nEnemies are more likely to target you");
	}

	public override void SetDefaults()
	{
		((Entity)(object)((ModItem)this).Item).width = 18;
		((Entity)(object)((ModItem)this).Item).height = 18;
		((ModItem)this).Item.value = Item.buyPrice(0, 80);
		((ModItem)this).Item.rare = 11;
		((ModItem)this).Item.defense = 42;
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		tooltips[0].OverrideColor = new Color(34, 166, 118);
	}

	public override void UpdateEquip(Player player)
	{
		player.statLifeMax2 += 10;
		player.statManaMax2 += 10;
		player.maxMinions += 2;
		player.GetCritChance(DamageClass.Magic) += 10;
		player.GetCritChance(DamageClass.Melee) += 10;
		player.GetCritChance(DamageClass.Ranged) += 10;
		player.endurance += 0.05f;
		player.aggro += 550;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create((ModItem)(object)this.Type, 1);
		val.AddIngredient((Mod)null, "NightmareScale", 16);
		val.AddIngredient((Mod)null, "NightmareBar", 15);
		val.AddIngredient((Mod)null, "DarkMatter", 20);
		val.AddTile(412);
		val.Register();
	}
}
