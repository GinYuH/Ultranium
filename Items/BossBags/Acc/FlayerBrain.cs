using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.BossBags.Acc;

public class FlayerBrain : ModItem
{
	public override void SetStaticDefaults()
	{
		// ((ModItem)this).DisplayName.SetDefault("Mind of the Mindflayer");
		// ((ModItem)this).Tooltip.SetDefault("10% increased damage and critical strike chance chance\nYou have a chance to dodge enemy attacks and gain longer invincibility when hit\nCreates an eldritch aura around the player that damages nearby enemies\nDisabling the visibility will disable the aura\nhowever the other bonuses will become buffed");
	}

	public override void SetDefaults()
	{
		((Entity)(object)((ModItem)this).Item).width = 32;
		((Entity)(object)((ModItem)this).Item).height = 32;
		((ModItem)this).Item.rare = 4;
		((ModItem)this).Item.value = Item.buyPrice(0, 45);
		((ModItem)this).Item.accessory = true;
		((ModItem)this).Item.expert = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.longInvince = true;
		player.blackBelt = true;
		if (!hideVisual)
		{
			player.GetDamage(DamageClass.Melee) += 0.1f;
			player.GetDamage(DamageClass.Ranged) += 0.1f;
			player.GetDamage(DamageClass.Magic) += 0.1f;
			player.GetDamage(DamageClass.Summon) += 0.1f;
			player.GetCritChance(DamageClass.Magic) += 10;
			player.GetCritChance(DamageClass.Melee) += 10;
			player.GetCritChance(DamageClass.Ranged) += 10;
			if (player.ownedProjectileCounts[((ModItem)this).Mod.Find<ModProjectile>("EldritchAuraBase").Type] < 1)
			{
				Projectile.NewProjectile(player.position.X, player.position.Y, 0f, 0f, ((ModItem)this).Mod.Find<ModProjectile>("EldritchAuraBase").Type, 150, 10f, player.whoAmI, 0f, 0f);
			}
		}
		if (hideVisual)
		{
			player.GetDamage(DamageClass.Melee) += 0.15f;
			player.GetDamage(DamageClass.Ranged) += 0.15f;
			player.GetDamage(DamageClass.Magic) += 0.15f;
			player.GetDamage(DamageClass.Summon) += 0.15f;
			player.GetCritChance(DamageClass.Magic) += 15;
			player.GetCritChance(DamageClass.Melee) += 15;
			player.GetCritChance(DamageClass.Ranged) += 15;
		}
	}
}
