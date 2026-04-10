using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.BossSummon;

public class EtherealLantern : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Ethereal Lantern");
		((ModItem)this).Tooltip.SetDefault("Calls forth the ethereal demon\nCan only be used at night");
	}

	public override void SetDefaults()
	{
		((Entity)(object)((ModItem)this).item).width = 20;
		((Entity)(object)((ModItem)this).item).height = 20;
		((ModItem)this).item.maxStack = 20;
		((ModItem)this).item.rare = 5;
		((ModItem)this).item.useAnimation = 45;
		((ModItem)this).item.useTime = 45;
		((ModItem)this).item.useStyle = 4;
		((ModItem)this).item.UseSound = SoundID.Item44;
		((ModItem)this).item.consumable = true;
	}

	public override bool CanUseItem(Player player)
	{
		if (!NPC.AnyNPCs(((ModItem)this).mod.NPCType("Xenanis")))
		{
			return !Main.dayTime;
		}
		return false;
	}

	public override bool UseItem(Player player)
	{
		NPC.SpawnOnPlayer(player.whoAmI, ((ModItem)this).mod.NPCType("Xenanis"));
		Main.PlaySound(15, player.position, 0);
		return true;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		ModRecipe val = new ModRecipe(((ModItem)this).mod);
		val.AddIngredient(1508, 10);
		val.AddIngredient((Mod)null, "ShadowFlame", 10);
		val.AddIngredient(154, 30);
		val.AddTile(134);
		val.SetResult((ModItem)(object)this, 1);
		val.AddRecipe();
	}
}
