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
		//DisplayName.SetDefault("Nether Beacon");
		//Tooltip.SetDefault("Summons The guardian deity of hell\nCan only be used in the underworld\nNot Consumable");
	}

	public override void SetDefaults()
	{
		Item.width = 42;
		Item.height = 46;
		Item.maxStack = 1;
		Item.rare = ItemRarityID.Purple;
		Item.useAnimation = 45;
		Item.value = Item.buyPrice(0, 10);
		Item.useTime = 45;
		Item.useStyle = ItemUseStyleID.HoldUp;
		Item.UseSound = SoundID.Item44;
		Item.consumable = false;
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		tooltips[0].OverrideColor = new Color(241, 166, 0);
	}

	public override bool CanUseItem(Player player)
	{
		if (!(player.position.Y / 16f < (float)(Main.maxTilesY - 200)))
		{
			return !NPC.AnyNPCs(Mod.Find<ModNPC>("Ignodium").Type);
		}
		return false;
	}

	public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
	{
		NPC.NewNPC(player.GetSource_ItemUse(Item), (int)player.Center.X, (int)player.Center.Y - 150, Mod.Find<ModNPC>("Ignodium").Type, 0, 0f, 0f, 0f, 0f, 255);
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
		Recipe val = Recipe.Create(Type, 1);
		val.AddIngredient(ItemID.LunarBar, 5);
		val.AddIngredient(null, "BrokenIgnodiumSummon", 1);
		val.AddTile(TileID.LunarCraftingStation);
		val.Register();
	}
}
