using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Ultranium.ShadowEvent;

namespace Ultranium.Items.BossSummon;

public class DarkResonatorDisabler : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Abyssal Spawn Disabler");
		//Tooltip.SetDefault("Using this during the abyssal armageddon event will toggle enemy spawns on and off");
	}

	public override void SetDefaults()
	{
		Item.width = 28;
		Item.height = 18;
		Item.maxStack = 1;
		Item.rare = ItemRarityID.Purple;
		Item.useAnimation = 45;
		Item.useTime = 45;
		Item.useStyle = ItemUseStyleID.HoldUp;
		Item.consumable = false;
	}

	public override Vector2? HoldoutOffset()
	{
		return new Vector2(-7f, -4f);
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		tooltips[0].OverrideColor = new Color(34, 166, 118);
	}

	public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
	{
		if (ShadowEventWorld.ShadowEventActive && !ShadowEventSpawns.DisabledSpawns)
		{
			Main.NewText("Spawns have been disabled!", (byte)61, byte.MaxValue, (byte)142);
			ShadowEventSpawns.DisabledSpawns = true;
			return true;
		}
		if (ShadowEventWorld.ShadowEventActive && ShadowEventSpawns.DisabledSpawns)
		{
			Main.NewText("Spawns have been enabled!", (byte)61, byte.MaxValue, (byte)142);
			ShadowEventSpawns.DisabledSpawns = false;
			return true;
		}
		return false;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create(Type, 1);
		val.AddIngredient((Mod)null, "DarkResonatorBroken", 1);
		val.AddIngredient((Mod)null, "NightmareBar", 6);
		val.AddIngredient((Mod)null, "NightmareFuel", 5);
		val.AddTile(TileID.LunarCraftingStation);
		val.Register();
	}
}
