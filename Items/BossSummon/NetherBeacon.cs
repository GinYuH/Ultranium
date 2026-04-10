using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.BossSummon;

public class NetherBeacon : ModItem
{
	public override void SetStaticDefaults()
	{
		// ((ModItem)this).DisplayName.SetDefault("Nether Beacon");
		// ((ModItem)this).Tooltip.SetDefault("Summons The guardian deity of hell\nCan only be used in the underworld\nNot Consumable");
	}

	public override void SetDefaults()
	{
		((Entity)(object)((ModItem)this).Item).width = 42;
		((Entity)(object)((ModItem)this).Item).height = 46;
		((ModItem)this).Item.maxStack = 1;
		((ModItem)this).Item.rare = 11;
		((ModItem)this).Item.useAnimation = 45;
		((ModItem)this).Item.value = Item.buyPrice(0, 10);
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
		if (!(player.position.Y / 16f < (float)(Main.maxTilesY - 200)))
		{
			return !NPC.AnyNPCs(((ModItem)this).Mod.Find<ModNPC>("Ignodium").Type);
		}
		return false;
	}

	public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
	{
		NPC.NewNPC((int)player.Center.X, (int)player.Center.Y - 150, ((ModItem)this).Mod.Find<ModNPC>("Ignodium").Type, 0, 0f, 0f, 0f, 0f, 255);
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
		val.AddIngredient((Mod)null, "BrokenIgnodiumSummon", 1);
		val.AddTile(412);
		val.Register();
	}
}
