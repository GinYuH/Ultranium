using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Pumpkin;

public class PumpkinSummon : ModItem
{
	public override void SetStaticDefaults()
	{
		DisplayName.SetDefault("Pumpkin Scepter");
		Tooltip.SetDefault("Summons a small pumpkin to fight with you");
	}

	public override void SetDefaults()
	{
		Item.damage = 12;
		Item.mana = 15;
		Item.DamageType = DamageClass.Summon;
		((Entity)(object)Item).width = 26;
		((Entity)(object)Item).height = 26;
		Item.useTime = 30;
		Item.useAnimation = 30;
		Item.useStyle = 1;
		Item.noMelee = true;
		Item.knockBack = 0f;
		Item.value = Item.buyPrice(0, 0, 50);
		Item.rare = 1;
		Item.UseSound = SoundID.Item44;
		Item.shoot = Mod.Find<ModProjectile>("PumpSlime").Type;
		Item.shootSpeed = 10f;
		Item.buffType = Mod.Find<ModBuff>("PumpBuff").Type;
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
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Expected O, but got Unknown
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create(Type, 1);
		val.AddIngredient(1725, 20);
        val.AddRecipeGroup(RecipeGroupID.Wood, 20);
        val.AddTile(18);
		val.Register();
	}
}
