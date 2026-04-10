using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.BossBags.Acc;

public class ShadowHeart : ModItem
{
	public override void SetStaticDefaults()
	{
		// ((ModItem)this).DisplayName.SetDefault("Heart of Erebus");
		// ((ModItem)this).Tooltip.SetDefault("+60 max life and +30 max mana\nGrants immunity to the Eldritch Decay debuff\nGives bonuses as your health gets lower\nAt 75% health, you gain 15% increased critical strike chance\nAt 50% health, you gain 15% increased damage\nAt 25% health, you gain 15 defense\nThese buffs will stack with each other");
	}

	public override void SetDefaults()
	{
		((Entity)(object)((ModItem)this).Item).width = 32;
		((Entity)(object)((ModItem)this).Item).height = 32;
		((ModItem)this).Item.value = Item.buyPrice(1, 50);
		((ModItem)this).Item.rare = 4;
		((ModItem)this).Item.accessory = true;
		((ModItem)this).Item.expert = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.statManaMax2 += 30;
		player.statLifeMax2 += 60;
		player.buffImmune[((ModItem)this).Mod.Find<ModBuff>("DarkDebuff").Type] = true;
		if (player.statLife < (int)((float)player.statLifeMax2 * 0.75f))
		{
			player.GetCritChance(DamageClass.Magic) += 15;
			player.GetCritChance(DamageClass.Melee) += 15;
			player.GetCritChance(DamageClass.Ranged) += 15;
		}
		if (player.statLife < (int)((float)player.statLifeMax2 * 0.5f))
		{
			player.GetDamage(DamageClass.Summon) += 0.15f;
			player.GetDamage(DamageClass.Magic) += 0.15f;
			player.GetDamage(DamageClass.Melee) += 0.15f;
			player.GetDamage(DamageClass.Ranged) += 0.15f;
		}
		if (player.statLife < (int)((float)player.statLifeMax2 * 0.25f))
		{
			player.statDefense += 15;
		}
	}
}
