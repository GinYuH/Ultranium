using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.BossSummon;

public class IceFood : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Frozen Food");
		//Tooltip.SetDefault("Attracts the ice dragon\nCan only be used in the snow biome");
	}

	public override void SetDefaults()
	{
		Item.width = 20;
		Item.height = 20;
		Item.maxStack = 20;
		Item.rare = 3;
		Item.useAnimation = 45;
		Item.useTime = 45;
		Item.useStyle = 4;
		Item.UseSound = SoundID.Item44;
		Item.consumable = true;
	}

	public override bool CanUseItem(Player player)
	{
		if (!NPC.AnyNPCs(Mod.Find<ModNPC>("IceDragon").Type))
		{
			return player.ZoneSnow;
		}
		return false;
	}

	public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
	{
		NPC.SpawnOnPlayer(player.whoAmI, Mod.Find<ModNPC>("IceDragon").Type);
		SoundEngine.PlaySound(SoundID.Roar, player.position);
		return true;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create(Type, 1);
		val.AddIngredient(664, 50);
		val.AddIngredient(154, 15);
		val.AddRecipeGroup("Ultranium:RottenChunk/Vetebrae", 6);
		val.AddTile(16);
		val.Register();
	}
}
