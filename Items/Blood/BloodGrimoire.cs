using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Blood;

public class BloodGrimoire : ModItem
{
	public override void SetStaticDefaults()
	{
		DisplayName.SetDefault("Bloody Grimoire");
		Tooltip.SetDefault("Conjures a blood dripper minion to fight with you");
	}

	public override void SetDefaults()
	{
		Item.damage = 20;
		Item.mana = 20;
		Item.width = 26;
		Item.height = 26;
		Item.useTime = 30;
		Item.useAnimation = 30;
		Item.useStyle = 5;
		Item.noMelee = true;
		Item.DamageType = DamageClass.Summon;
		Item.knockBack = 0f;
		Item.value = Item.buyPrice(0, 1, 35);
		Item.rare = 2;
		Item.UseSound = SoundID.Item44;
		Item.shoot = Mod.Find<ModProjectile>("BloodMinion").Type;
		Item.shootSpeed = 10f;
		Item.buffType = Mod.Find<ModBuff>("BloodBuff").Type;
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
		return UseItem(player);
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create(Type, 1);
		val.AddIngredient((Mod)null, "BloodClot", 12);
		val.AddRecipeGroup("Ultranium:Silver/Tungsten", 8);
		val.AddTile(16);
		val.Register();
	}
}
