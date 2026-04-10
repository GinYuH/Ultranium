using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.BossBags.Acc;

public class ShadowHeart : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Heart of Erebus");
		((ModItem)this).Tooltip.SetDefault("+60 max life and +30 max mana\nGrants immunity to the Eldritch Decay debuff\nGives bonuses as your health gets lower\nAt 75% health, you gain 15% increased critical strike chance\nAt 50% health, you gain 15% increased damage\nAt 25% health, you gain 15 defense\nThese buffs will stack with each other");
	}

	public override void SetDefaults()
	{
		((Entity)(object)((ModItem)this).item).width = 32;
		((Entity)(object)((ModItem)this).item).height = 32;
		((ModItem)this).item.value = Item.buyPrice(1, 50);
		((ModItem)this).item.rare = 4;
		((ModItem)this).item.accessory = true;
		((ModItem)this).item.expert = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.statManaMax2 += 30;
		player.statLifeMax2 += 60;
		player.buffImmune[((ModItem)this).mod.BuffType("DarkDebuff")] = true;
		if (player.statLife < (int)((float)player.statLifeMax2 * 0.75f))
		{
			player.magicCrit += 15;
			player.meleeCrit += 15;
			player.rangedCrit += 15;
		}
		if (player.statLife < (int)((float)player.statLifeMax2 * 0.5f))
		{
			player.minionDamage += 0.15f;
			player.magicDamage += 0.15f;
			player.meleeDamage += 0.15f;
			player.rangedDamage += 0.15f;
		}
		if (player.statLife < (int)((float)player.statLifeMax2 * 0.25f))
		{
			player.statDefense += 15;
		}
	}
}
