using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Ultranium.ShadowEvent;

namespace Ultranium.Items.BossSummon;

public class ErebusFood : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Eldritch Worm Food");
		//Tooltip.SetDefault("Summons Erebus upon use\nCan only be used during the Abyssal Armageddon event\nNot consumable");
	}

	public override void SetDefaults()
	{
		Item.width = 44;
		Item.height = 40;
		Item.maxStack = 1;
		Item.rare = ItemRarityID.Purple;
		Item.useAnimation = 45;
		Item.useTime = 45;
		Item.useStyle = ItemUseStyleID.HoldUp;
		Item.UseSound = new SoundStyle("Ultranium/Sounds/ShadowAwakening");
		Item.consumable = false;
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		tooltips[0].OverrideColor = new Color(34, 166, 118);
	}

	public override bool CanUseItem(Player player)
	{
		if (!NPC.AnyNPCs(Mod.Find<ModNPC>("ErebusHead").Type))
		{
			return ShadowEventWorld.ShadowEventActive;
		}
		return false;
	}

	public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
	{
		NPC.SpawnOnPlayer(player.whoAmI, Mod.Find<ModNPC>("ErebusHead").Type);
		SoundEngine.PlaySound(SoundID.Roar, player.position);
		return true;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = Recipe.Create(Type, 1);
		val.AddIngredient(null, "DarkMatter", 35);
		val.AddIngredient(null, "EldritchBlood", 10);
		val.AddTile(TileID.LunarCraftingStation);
		val.Register();
	}
}
