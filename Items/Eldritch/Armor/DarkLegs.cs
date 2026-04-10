using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Eldritch.Armor;

[AutoloadEquip(/*Could not decode attribute arguments.*/)]
public class DarkLegs : ModItem
{
	public override void SetStaticDefaults()
	{
		// ((ModItem)this).DisplayName.SetDefault("Darkmatter Leggings");
		// ((ModItem)this).Tooltip.SetDefault("+10 max mana and +1 max minions\n8% increased melee speed and 12% increased movement speed");
	}

	public override void SetDefaults()
	{
		((Entity)(object)((ModItem)this).Item).width = 18;
		((Entity)(object)((ModItem)this).Item).height = 18;
		((ModItem)this).Item.value = Item.buyPrice(0, 80);
		((ModItem)this).Item.rare = 11;
		((ModItem)this).Item.defense = 33;
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
		Recipe val = /* ((ModItem)this) */Recipe.Create((ModItem)(object)this.Type, 1);
		val.AddIngredient((Mod)null, "NightmareScale", 12);
		val.AddIngredient((Mod)null, "NightmareBar", 12);
		val.AddIngredient((Mod)null, "DarkMatter", 15);
		val.AddTile(412);
		val.Register();
	}
}
