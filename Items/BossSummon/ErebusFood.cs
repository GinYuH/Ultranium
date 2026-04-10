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
		// ((ModItem)this).DisplayName.SetDefault("Eldritch Worm Food");
		// ((ModItem)this).Tooltip.SetDefault("Summons Erebus upon use\nCan only be used during the Abyssal Armageddon event\nNot consumable");
	}

	public override void SetDefaults()
	{
		((Entity)(object)((ModItem)this).Item).width = 44;
		((Entity)(object)((ModItem)this).Item).height = 40;
		((ModItem)this).Item.maxStack = 1;
		((ModItem)this).Item.rare = 11;
		((ModItem)this).Item.useAnimation = 45;
		((ModItem)this).Item.useTime = 45;
		((ModItem)this).Item.useStyle = 4;
		((ModItem)this).Item.UseSound = ((ModItem)this).Mod.GetLegacySoundSlot((SoundType)2, "Sounds/ShadowAwakening");
		((ModItem)this).Item.consumable = false;
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		tooltips[0].OverrideColor = new Color(34, 166, 118);
	}

	public override bool CanUseItem(Player player)
	{
		if (!NPC.AnyNPCs(((ModItem)this).Mod.Find<ModNPC>("ErebusHead").Type))
		{
			return ShadowEventWorld.ShadowEventActive;
		}
		return false;
	}

	public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
	{
		NPC.SpawnOnPlayer(player.whoAmI, ((ModItem)this).Mod.Find<ModNPC>("ErebusHead").Type);
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
		Recipe val = /* ((ModItem)this) */Recipe.Create((ModItem)(object)this.Type, 1);
		val.AddIngredient((Mod)null, "DarkMatter", 35);
		val.AddIngredient((Mod)null, "EldritchBlood", 10);
		val.AddTile(412);
		val.Register();
	}
}
