using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Ultranium.ShadowEvent;

namespace Ultranium.Items.BossSummon;

public class DarkResonator : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Dark Resonator");
		((ModItem)this).Tooltip.SetDefault("A long lost relic, it's said it can open the rift to the nightmare world...\nStarts the abyssal armageddon event on use\nCan only be used during the night");
	}

	public override void SetDefaults()
	{
		((Entity)(object)((ModItem)this).item).width = 28;
		((Entity)(object)((ModItem)this).item).height = 18;
		((ModItem)this).item.maxStack = 1;
		((ModItem)this).item.rare = 11;
		((ModItem)this).item.useAnimation = 45;
		((ModItem)this).item.useTime = 45;
		((ModItem)this).item.useStyle = 4;
		((ModItem)this).item.consumable = false;
	}

	public override Vector2? HoldoutOffset()
	{
		return new Vector2(-7f, -4f);
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		tooltips[0].overrideColor = new Color(34, 166, 118);
	}

	public override bool UseItem(Player player)
	{
		if (ShadowEventWorld.ShadowEventActive && !Main.dayTime)
		{
			Main.PlaySound(((ModItem)this).mod.GetLegacySoundSlot((SoundType)50, "Sounds/ShadowAwakening")?.WithVolume(0.6f)?.WithPitchVariance(0.1f), -1, -1);
			Main.NewText("Theres no stopping the darkness now...", (byte)61, byte.MaxValue, (byte)142, false);
			return true;
		}
		if (!ShadowEventWorld.ShadowEventActive && !Main.dayTime)
		{
			Main.PlaySound(((ModItem)this).mod.GetLegacySoundSlot((SoundType)50, "Sounds/ShadowAwakening")?.WithVolume(0.6f)?.WithPitchVariance(0.1f), -1, -1);
			Main.NewText("The Abyssal Armageddon has begun", (byte)61, byte.MaxValue, (byte)142, false);
			ShadowEventWorld.ShadowEventActive = true;
			return true;
		}
		if (Main.dayTime)
		{
			Main.PlaySound(((ModItem)this).mod.GetLegacySoundSlot((SoundType)50, "Sounds/ShadowAwakening")?.WithVolume(0.6f)?.WithPitchVariance(0.1f), -1, -1);
			Main.NewText("You can only use the artifact under the darkness of the moon", (byte)61, byte.MaxValue, (byte)142, false);
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
		ModRecipe val = new ModRecipe(((ModItem)this).mod);
		val.AddIngredient((Mod)null, "DarkResonatorBroken", 1);
		val.AddIngredient((Mod)null, "NightmareBar", 6);
		val.AddIngredient((Mod)null, "NightmareFuel", 5);
		val.AddTile(412);
		val.SetResult((ModItem)(object)this, 1);
		val.AddRecipe();
	}
}
