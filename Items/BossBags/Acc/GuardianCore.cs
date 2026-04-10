using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.BossBags.Acc;

public class GuardianCore : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Guardian's Insignia");
		((ModItem)this).Tooltip.SetDefault("15% increased damage and 10% increased critical strike chance\nincreases max life and max mana by 40\n+3 max minions\n20% reduced mana usage");
	}

	public override void SetDefaults()
	{
		((Entity)(object)((ModItem)this).item).width = 32;
		((Entity)(object)((ModItem)this).item).height = 32;
		((ModItem)this).item.rare = 12;
		((ModItem)this).item.value = Item.buyPrice(1);
		((ModItem)this).item.accessory = true;
		((ModItem)this).item.expert = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.statManaMax2 += 40;
		player.statLifeMax2 += 40;
		player.maxMinions += 3;
		player.manaCost += -0.2f;
		player.minionDamage += 0.15f;
		player.thrownDamage += 0.15f;
		player.magicDamage += 0.15f;
		player.meleeDamage += 0.15f;
		player.rangedDamage += 0.15f;
		player.thrownCrit += 10;
		player.magicCrit += 10;
		player.meleeCrit += 10;
		player.rangedCrit += 10;
		player.meleeSpeed += 0.08f;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		ModRecipe val = new ModRecipe(((ModItem)this).mod);
		val.AddIngredient((Mod)null, "UltrumRelic", 1);
		val.AddIngredient((Mod)null, "IgnodiumRelic", 1);
		val.AddTile(412);
		val.SetResult((ModItem)(object)this, 1);
		val.AddRecipe();
	}
}
