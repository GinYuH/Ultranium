using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Ultranium.ShadowEvent;

namespace Ultranium.Items.BossSummon;

public class ErebusFood : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Eldritch Worm Food");
		((ModItem)this).Tooltip.SetDefault("Summons Erebus upon use\nCan only be used during the Abyssal Armageddon event\nNot consumable");
	}

	public override void SetDefaults()
	{
		((Entity)(object)((ModItem)this).item).width = 44;
		((Entity)(object)((ModItem)this).item).height = 40;
		((ModItem)this).item.maxStack = 1;
		((ModItem)this).item.rare = 11;
		((ModItem)this).item.useAnimation = 45;
		((ModItem)this).item.useTime = 45;
		((ModItem)this).item.useStyle = 4;
		((ModItem)this).item.UseSound = ((ModItem)this).mod.GetLegacySoundSlot((SoundType)2, "Sounds/ShadowAwakening");
		((ModItem)this).item.consumable = false;
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		tooltips[0].overrideColor = new Color(34, 166, 118);
	}

	public override bool CanUseItem(Player player)
	{
		if (!NPC.AnyNPCs(((ModItem)this).mod.NPCType("ErebusHead")))
		{
			return ShadowEventWorld.ShadowEventActive;
		}
		return false;
	}

	public override bool UseItem(Player player)
	{
		NPC.SpawnOnPlayer(player.whoAmI, ((ModItem)this).mod.NPCType("ErebusHead"));
		Main.PlaySound(15, player.position, 0);
		return true;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		ModRecipe val = new ModRecipe(((ModItem)this).mod);
		val.AddIngredient((Mod)null, "DarkMatter", 35);
		val.AddIngredient((Mod)null, "EldritchBlood", 10);
		val.AddTile(412);
		val.SetResult((ModItem)(object)this, 1);
		val.AddRecipe();
	}
}
