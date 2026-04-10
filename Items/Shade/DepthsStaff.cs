using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Shade;

public class DepthsStaff : ModItem
{
	public override void SetStaticDefaults()
	{
		// ((ModItem)this).DisplayName.SetDefault("Ny-Rakath");
		// ((ModItem)this).Tooltip.SetDefault("Summons a shade demon that shoots demon scythes at nearby enemies");
	}

	public override void SetDefaults()
	{
		((ModItem)this).Item.DamageType = DamageClass.Summon;
		((ModItem)this).Item.mana = 20;
		((ModItem)this).Item.damage = 65;
		((Entity)(object)((ModItem)this).Item).width = 42;
		((Entity)(object)((ModItem)this).Item).height = 42;
		((ModItem)this).Item.useTime = 30;
		((ModItem)this).Item.useAnimation = 30;
		((ModItem)this).Item.useStyle = 1;
		((ModItem)this).Item.noMelee = true;
		((ModItem)this).Item.knockBack = 0f;
		((ModItem)this).Item.value = Item.buyPrice(0, 68);
		((ModItem)this).Item.rare = 7;
		((ModItem)this).Item.UseSound = SoundID.Item44;
		((ModItem)this).Item.shoot = ((ModItem)this).Mod.Find<ModProjectile>("DemonMinion").Type;
		((ModItem)this).Item.shootSpeed = 10f;
		((ModItem)this).Item.buffType = ((ModItem)this).Mod.Find<ModBuff>("DemonBuff").Type;
		((ModItem)this).Item.buffTime = 3600;
	}

	public override bool AltFunctionUse(Player player)
	{
		return true;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		return player.altFunctionUse != 2;
	}

	public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
	{
		if (player.altFunctionUse == 2)
		{
			player.MinionNPCTargetAim();
		}
		return ((ModItem)this).UseItem(player);
	}
}
