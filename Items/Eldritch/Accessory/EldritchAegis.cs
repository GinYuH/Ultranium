using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Eldritch.Accessory;

[AutoloadEquip(/*Could not decode attribute arguments.*/)]
public class EldritchAegis : ModItem
{
	public override void SetStaticDefaults()
	{
		SetStaticDefaults();
		DisplayName.SetDefault("Erebus Bulwark");
		Tooltip.SetDefault("Grants immunity to most debuffs and knockback\nDamage taken from lava is reduced and you can walk on fire blocks\nGrants you the ice barrier buff when below 25% hp, which reduces damage taken by 25%\nGrants you the ability to dash\nDisabling the visibility will disable the dash");
	}

	public override void SetDefaults()
	{
		Item.width = 38;
		Item.height = 46;
		Item.rare = 11;
		Item.value = Item.buyPrice(0, 80);
		Item.accessory = true;
		Item.defense = 7;
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		tooltips[0].OverrideColor = new Color(34, 166, 118);
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
		if (player.statLife < (int)((float)player.statLifeMax2 * 0.25f))
		{
			player.AddBuff(62, 2, quiet: false);
		}
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create(Type, 1);
		val.AddIngredient((Mod)null, "DarkMatter", 12);
		val.AddIngredient((Mod)null, "NightmareScale", 8);
		val.AddIngredient((Mod)null, "GuardianShield", 1);
		val.AddIngredient(1253, 1);
		val.AddTile(412);
		val.Register();
	}
}
