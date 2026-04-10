using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.BossBags.Acc;

public class FlayerBrain : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Mind of the Mindflayer");
		((ModItem)this).Tooltip.SetDefault("10% increased damage and critical strike chance chance\nYou have a chance to dodge enemy attacks and gain longer invincibility when hit\nCreates an eldritch aura around the player that damages nearby enemies\nDisabling the visibility will disable the aura\nhowever the other bonuses will become buffed");
	}

	public override void SetDefaults()
	{
		((Entity)(object)((ModItem)this).item).width = 32;
		((Entity)(object)((ModItem)this).item).height = 32;
		((ModItem)this).item.rare = 4;
		((ModItem)this).item.value = Item.buyPrice(0, 45);
		((ModItem)this).item.accessory = true;
		((ModItem)this).item.expert = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.longInvince = true;
		player.blackBelt = true;
		if (!hideVisual)
		{
			player.meleeDamage += 0.1f;
			player.rangedDamage += 0.1f;
			player.magicDamage += 0.1f;
			player.minionDamage += 0.1f;
			player.magicCrit += 10;
			player.meleeCrit += 10;
			player.rangedCrit += 10;
			if (player.ownedProjectileCounts[((ModItem)this).mod.ProjectileType("EldritchAuraBase")] < 1)
			{
				Projectile.NewProjectile(player.position.X, player.position.Y, 0f, 0f, ((ModItem)this).mod.ProjectileType("EldritchAuraBase"), 150, 10f, player.whoAmI, 0f, 0f);
			}
		}
		if (hideVisual)
		{
			player.meleeDamage += 0.15f;
			player.rangedDamage += 0.15f;
			player.magicDamage += 0.15f;
			player.minionDamage += 0.15f;
			player.magicCrit += 15;
			player.meleeCrit += 15;
			player.rangedCrit += 15;
		}
	}
}
