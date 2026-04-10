using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Guardians;

[AutoloadEquip(/*Could not decode attribute arguments.*/)]
public class GuardianShield : ModItem
{
	public override void SetStaticDefaults()
	{
		SetStaticDefaults();
		// DisplayName.SetDefault("Shield of the Deities");
		// Tooltip.SetDefault("Grants immunity to most debuffs and knockback\nDamage taken from lava is reduced and you can walk on fire blocks\nGrants you the ability to dash\nDisabling the visibility will disable the dash");
	}

	public override void SetDefaults()
	{
		Item.width = 42;
		Item.height = 42;
		Item.value = Item.buyPrice(1);
		Item.rare = 11;
		Item.accessory = true;
		Item.defense = 5;
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		tooltips[0].OverrideColor = new Color(241, 166, 0);
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		if (!hideVisual)
		{
			player.dash = 1;
		}
		player.noKnockback = true;
		player.lavaRose = true;
		player.fireWalk = true;
		player.noKnockback = true;
		player.buffImmune[46] = true;
		player.buffImmune[33] = true;
		player.buffImmune[36] = true;
		player.buffImmune[30] = true;
		player.buffImmune[20] = true;
		player.buffImmune[32] = true;
		player.buffImmune[31] = true;
		player.buffImmune[35] = true;
		player.buffImmune[23] = true;
		player.buffImmune[22] = true;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create(Type, 1);
		val.AddIngredient(1613, 1);
		val.AddIngredient(1323, 1);
		val.AddIngredient((Mod)null, "UltrumShard", 5);
		val.AddIngredient((Mod)null, "HellShard", 5);
		val.AddTile(412);
		val.Register();
	}
}
