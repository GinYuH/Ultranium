using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Eldritch.Accessory;

public class VoidPouch : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Dimensional Ammo Pouch");
		//Tooltip.SetDefault("15% increased ranged damage\n50% chance to not consume ammo\nRanged projectiles inflict the eldritch decay debuff");
	}

	public override void SetDefaults()
	{
		Item.width = 38;
		Item.height = 46;
		Item.rare = ItemRarityID.Purple;
		Item.value = Item.buyPrice(0, 80);
		Item.accessory = true;
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		tooltips[0].OverrideColor = new Color(34, 166, 118);
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.GetModPlayer<UltraniumPlayer>().VoidPouch = true;
		player.GetDamage(DamageClass.Melee) += 0.15f;
		player.GetAttackSpeed(DamageClass.Melee) += 0.12f;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = Recipe.Create(Type, 1);
		val.AddIngredient(null, "DarkMatter", 12);
		val.AddIngredient(null, "NightmareScale", 8);
		val.AddIngredient(ItemID.MagicQuiver, 1);
		val.AddTile(TileID.LunarCraftingStation);
		val.Register();
	}
}
