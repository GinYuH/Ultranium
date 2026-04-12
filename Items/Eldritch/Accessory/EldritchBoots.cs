using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Eldritch.Accessory;

[AutoloadEquip(EquipType.Wings)]
public class EldritchBoots : ModItem
{
	public override void SetStaticDefaults()
	{
		DisplayName.SetDefault("Eldritch Tracers");
		Tooltip.SetDefault("Allows flight and slow fall\nAllows super fast running and extra mobility on ice\n10% increased movement speed\nGrants the ability to walk on water and lava\nGrants immunity to fire blocks and 10 seconds of immunity to lava");
		ArmorIDs.Wing.Sets.Stats[Item.wingSlot] = new Terraria.DataStructures.WingStats(240, 10f, 5f);
    }

	public override void SetDefaults()
	{
		Item.width = 34;
		Item.height = 26;
		Item.value = Item.buyPrice(0, 80);
		Item.rare = 11;
		Item.accessory = true;
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		tooltips[0].OverrideColor = new Color(34, 166, 118);
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.waterWalk = true;
		player.fireWalk = true;
		player.lavaMax += 600;
		player.accRunSpeed = 8.75f;
		player.moveSpeed += 30f;
		player.iceSkate = true;
	}

	public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising, ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
	{
		ascentWhenFalling = 0.85f;
		ascentWhenRising = 0.15f;
		maxCanAscendMultiplier = 1.1f;
		maxAscentMultiplier = 3f;
		constantAscend = 0.095f;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create(Type, 1);
		val.AddIngredient((Mod)null, "DarkMatter", 12);
		val.AddIngredient((Mod)null, "NightmareScale", 8);
		val.AddIngredient(1862, 1);
		val.AddIngredient(908, 1);
		val.AddIngredient(575, 20);
		val.AddTile(412);
		val.Register();
	}
}
