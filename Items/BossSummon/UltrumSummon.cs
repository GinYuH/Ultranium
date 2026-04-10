using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.BossSummon;

public class UltrumSummon : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Ultranium Sigil");
		((ModItem)this).Tooltip.SetDefault("Summons the guardian deity of nature\nCan only be used on the surface\nNot Consumable");
	}

	public override void SetDefaults()
	{
		((Entity)(object)((ModItem)this).item).width = 20;
		((Entity)(object)((ModItem)this).item).height = 20;
		((ModItem)this).item.maxStack = 1;
		((ModItem)this).item.rare = 11;
		((ModItem)this).item.value = Item.buyPrice(0, 10);
		((ModItem)this).item.useAnimation = 45;
		((ModItem)this).item.useTime = 45;
		((ModItem)this).item.useStyle = 4;
		((ModItem)this).item.UseSound = SoundID.Item44;
		((ModItem)this).item.consumable = false;
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		tooltips[0].overrideColor = new Color(241, 166, 0);
	}

	public override bool CanUseItem(Player player)
	{
		if (!NPC.AnyNPCs(((ModItem)this).mod.NPCType("Ultrum")))
		{
			return player.ZoneOverworldHeight;
		}
		return false;
	}

	public override bool UseItem(Player player)
	{
		NPC.NewNPC((int)player.Center.X, (int)player.Center.Y - 150, ((ModItem)this).mod.NPCType("Ultrum"), 0, 0f, 0f, 0f, 0f, 255);
		Main.PlaySound(15, player.position, 0);
		return true;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		ModRecipe val = new ModRecipe(((ModItem)this).mod);
		val.AddIngredient(3467, 5);
		val.AddIngredient((Mod)null, "BrokenUltrumSummon", 1);
		val.AddTile(412);
		val.SetResult((ModItem)(object)this, 1);
		val.AddRecipe();
	}
}
