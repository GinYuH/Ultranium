using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Ultranium.ShadowEvent;

namespace Ultranium.Items.BossSummon;

public class DarkResonator : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Dark Resonator");
		//Tooltip.SetDefault("A long lost relic, it's said it can open the rift to the nightmare world...\nStarts the abyssal armageddon event on use\nCan only be used during the night");
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
		if (ShadowEventWorld.ShadowEventActive && !Main.dayTime)
		{
			SoundEngine.PlaySound(new SoundStyle("Ultranium/Sounds/ShadowAwakening") with { PitchVariance = 0.1f });
			Main.NewText("Theres no stopping the darkness now...", (byte)61, byte.MaxValue, (byte)142);
			return true;
		}
		if (!ShadowEventWorld.ShadowEventActive && !Main.dayTime)
		{
			SoundEngine.PlaySound(new SoundStyle("Ultranium/Sounds/ShadowAwakening") with { PitchVariance = 0.1f });
            Main.NewText("The Abyssal Armageddon has begun", (byte)61, byte.MaxValue, (byte)142);
			ShadowEventWorld.ShadowEventActive = true;
			return true;
		}
		if (Main.dayTime)
		{
			SoundEngine.PlaySound(new SoundStyle("Ultranium/Sounds/ShadowAwakening") with { PitchVariance = 0.1f });
            Main.NewText("You can only use the artifact under the darkness of the moon", (byte)61, byte.MaxValue, (byte)142);
			ShadowEventWorld.StartShadowEvent = false;
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
