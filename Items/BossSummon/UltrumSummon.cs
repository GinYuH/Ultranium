using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.BossSummon;

public class UltrumSummon : ModItem
{
	public override void SetStaticDefaults()
	{
		// ((ModItem)this).DisplayName.SetDefault("Ultranium Sigil");
		// ((ModItem)this).Tooltip.SetDefault("Summons the guardian deity of nature\nCan only be used on the surface\nNot Consumable");
	}

	public override void SetDefaults()
	{
		((Entity)(object)((ModItem)this).Item).width = 20;
		((Entity)(object)((ModItem)this).Item).height = 20;
		((ModItem)this).Item.maxStack = 1;
		((ModItem)this).Item.rare = 11;
		((ModItem)this).Item.value = Item.buyPrice(0, 10);
		((ModItem)this).Item.useAnimation = 45;
		((ModItem)this).Item.useTime = 45;
		((ModItem)this).Item.useStyle = 4;
		((ModItem)this).Item.UseSound = SoundID.Item44;
		((ModItem)this).Item.consumable = false;
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		tooltips[0].OverrideColor = new Color(241, 166, 0);
	}

	public override bool CanUseItem(Player player)
	{
		if (!NPC.AnyNPCs(((ModItem)this).Mod.Find<ModNPC>("Ultrum").Type))
		{
			return player.ZoneOverworldHeight;
		}
		return false;
	}

	public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
	{
		NPC.NewNPC((int)player.Center.X, (int)player.Center.Y - 150, ((ModItem)this).Mod.Find<ModNPC>("Ultrum").Type, 0, 0f, 0f, 0f, 0f, 255);
		SoundEngine.PlaySound(SoundID.Roar, player.position);
		return true;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create((ModItem)(object)this.Type, 1);
		val.AddIngredient(3467, 5);
		val.AddIngredient((Mod)null, "BrokenUltrumSummon", 1);
		val.AddTile(412);
		val.Register();
	}
}
