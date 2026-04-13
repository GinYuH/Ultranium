using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Ultranium.ShadowEvent;

namespace Ultranium.Items.BossSummon;

public class BloodMoonSummon : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Blood Moon Idol");
		//Tooltip.SetDefault("Summons the Blood moon if used at night");
	}

	public override void SetDefaults()
	{
		Item.width = 20;
		Item.height = 20;
		Item.maxStack = 20;
		Item.rare = ItemRarityID.LightRed;
		Item.useAnimation = 45;
		Item.useTime = 45;
		Item.useStyle = ItemUseStyleID.HoldUp;
		Item.UseSound = SoundID.Item44;
		Item.consumable = true;
	}

	public override bool CanUseItem(Player player)
	{
		if (!Main.bloodMoon && !Main.dayTime)
		{
			return !ShadowEventWorld.ShadowEventActive;
		}
		return false;
	}

	public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
	{
		Main.bloodMoon = true;
		SoundEngine.PlaySound(SoundID.Roar, player.position);
		return true;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create(Type, 1);
		val.AddIngredient((Mod)null, "BloodClot", 15);
		val.AddTile(TileID.Anvils);
		val.Register();
	}
}
