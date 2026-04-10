using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Shade;

public class ShadeWispStaff : ModItem
{
	public override void SetStaticDefaults()
	{
		// ((ModItem)this).DisplayName.SetDefault("Shadow Wisp Staff");
		// ((ModItem)this).Tooltip.SetDefault("Summons a Shade Wisp to fight with you");
	}

	public override void SetDefaults()
	{
		((ModItem)this).Item.DamageType = DamageClass.Summon;
		((ModItem)this).Item.mana = 20;
		((ModItem)this).Item.damage = 20;
		((Entity)(object)((ModItem)this).Item).width = 26;
		((Entity)(object)((ModItem)this).Item).height = 26;
		((ModItem)this).Item.useTime = 30;
		((ModItem)this).Item.useAnimation = 30;
		((ModItem)this).Item.useStyle = 1;
		((ModItem)this).Item.noMelee = true;
		((ModItem)this).Item.knockBack = 0f;
		((ModItem)this).Item.value = Item.buyPrice(0, 2, 50);
		((ModItem)this).Item.rare = 1;
		((ModItem)this).Item.UseSound = SoundID.Item44;
		((ModItem)this).Item.shoot = ((ModItem)this).Mod.Find<ModProjectile>("ShadeWisp").Type;
		((ModItem)this).Item.shootSpeed = 10f;
		((ModItem)this).Item.buffType = ((ModItem)this).Mod.Find<ModBuff>("ShadeWispBuff").Type;
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

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create((ModItem)(object)this.Type, 1);
		val.AddIngredient((Mod)null, "NightmareBar", 12);
		val.AddTile(16);
		val.Register();
	}
}
