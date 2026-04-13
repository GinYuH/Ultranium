using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Minion;

public class AuroraStaff : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Aurora Crystal Staff");
		//Tooltip.SetDefault("Summons a spacial star to float above you\nThe star will shoot blasts at the cursor when enemies are near");
	}

	public override void SetDefaults()
	{
		Item.mana = 20;
		Item.damage = 25;
		Item.width = 42;
		Item.height = 42;
		Item.useTime = 30;
		Item.useAnimation = 30;
		Item.useStyle = 1;
		Item.noMelee = true;
		Item.DamageType = DamageClass.Summon;
		Item.knockBack = 0f;
		Item.value = Item.buyPrice(0, 0, 80);
		Item.rare = 1;
		Item.UseSound = SoundID.Item44;
		Item.shoot = Mod.Find<ModProjectile>("StarMinion").Type;
		Item.shootSpeed = 0f;
		Item.buffType = Mod.Find<ModBuff>("StarMinionBuff").Type;
		Item.buffTime = 3600;
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
			player.MinionNPCTargetAim(false);
		}
		return null;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create(Type, 1);
		val.AddIngredient((Mod)null, "AuroraBar", 5);
		val.AddTile(16);
		val.Register();
	}
}
