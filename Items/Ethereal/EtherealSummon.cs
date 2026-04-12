using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Ethereal;

public class EtherealSummon : ModItem
{
	public override void SetStaticDefaults()
	{
		DisplayName.SetDefault("Ethereal Staff");
		Tooltip.SetDefault("Summons ethereal wisps that shoots ethereal blasts at enemies");
	}

	public override void SetDefaults()
	{
		Item.DamageType = DamageClass.Summon;
		Item.mana = 30;
		Item.damage = 75;
		Item.width = 26;
		Item.height = 26;
		Item.useTime = 30;
		Item.useAnimation = 30;
		Item.useStyle = 1;
		Item.noMelee = true;
		Item.knockBack = 0f;
		Item.rare = 9;
		Item.value = Item.buyPrice(0, 30);
		Item.UseSound = SoundID.Item44;
		Item.shoot = Mod.Find<ModProjectile>("EtherealWisp").Type;
		Item.shootSpeed = 10f;
		Item.buffType = Mod.Find<ModBuff>("WispBuff").Type;
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
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create(Type, 1);
		val.AddIngredient((Mod)null, "XenanisFlesh", 10);
		val.AddIngredient((Mod)null, "ShadowFlame", 5);
		val.AddTile(134);
		val.Register();
	}
}
