using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.BossBags.Acc;

public class FlayerBrain : ModItem
{
	public override void SetStaticDefaults()
	{
		DisplayName.SetDefault("Mind of the Mindflayer");
		Tooltip.SetDefault("10% increased damage and critical strike chance chance\nYou have a chance to dodge enemy attacks and gain longer invincibility when hit\nCreates an eldritch aura around the player that damages nearby enemies\nDisabling the visibility will disable the aura\nhowever the other bonuses will become buffed");
	}

	public override void SetDefaults()
	{
		Item.width = 32;
		Item.height = 32;
		Item.rare = 4;
		Item.value = Item.buyPrice(0, 45);
		Item.accessory = true;
		Item.expert = true;
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
			if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("EldritchAuraBase").Type] < 1)
			{
				Projectile.NewProjectile(null, player.position.X, player.position.Y, 0f, 0f, Mod.Find<ModProjectile>("EldritchAuraBase").Type, 150, 10f, player.whoAmI, 0f, 0f);
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
