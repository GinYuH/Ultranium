using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Eldritch.Accessory;

public class EldritchScroll : ModItem
{
	public override void SetStaticDefaults()
	{
		SetStaticDefaults();
		DisplayName.SetDefault("Scroll of the Abyss");
		Tooltip.SetDefault("15% increased summon damage and minion knockback\n+2 max minions\nMinions inflict the eldritch decay debuff");
	}

	public override void SetDefaults()
	{
		Item.width = 38;
		Item.height = 46;
		Item.rare = 11;
		Item.value = Item.buyPrice(0, 80);
		Item.accessory = true;
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		tooltips[0].OverrideColor = new Color(34, 166, 118);
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.GetModPlayer<UltraniumPlayer>().EldritchScroll = true;
		player.GetDamage(DamageClass.Summon) += 0.15f;
		player.GetKnockback(DamageClass.Summon).Base *= 1.15f;
		player.maxMinions += 2;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create(Type, 1);
		val.AddIngredient((Mod)null, "DarkMatter", 12);
		val.AddIngredient((Mod)null, "NightmareScale", 8);
		val.AddIngredient(1864, 1);
		val.AddTile(412);
		val.Register();
	}
}
