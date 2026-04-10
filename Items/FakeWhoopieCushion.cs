using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items;

public class FakeWhoopieCushion : ModItem
{
	public override void SetStaticDefaults()
	{
		// ((ModItem)this).DisplayName.SetDefault("Whoopie Cushion?");
		// ((ModItem)this).Tooltip.SetDefault("'May annoy others'\nLegends say that it's farts can be harvested...");
		ItemID.Sets.ExtractinatorMode[((ModItem)this).Item.type] = ((ModItem)this).Item.type;
	}

	public override void SetDefaults()
	{
		((Entity)(object)((ModItem)this).Item).width = 16;
		((Entity)(object)((ModItem)this).Item).height = 14;
		((ModItem)this).Item.maxStack = 1;
		((ModItem)this).Item.useStyle = 4;
		((ModItem)this).Item.useTime = 30;
		((ModItem)this).Item.useAnimation = 30;
		((ModItem)this).Item.UseSound = SoundID.Item16;
		((ModItem)this).Item.rare = 2;
		((ModItem)this).Item.useTurn = true;
		((ModItem)this).Item.autoReuse = false;
	}

	public override void ExtractinatorUse(int extractinatorBlockType, ref int resultType, ref int resultStack)
	{
		string[] array = new string[1] { "TheFart" };
		int num = Main.rand.Next(array.Length);
		resultType = ((ModItem)this).Mod.Find<ModItem>(array[num]).Type;
		resultStack = 1;
		if (!UltraniumWorld.TheFart)
		{
			UltraniumWorld.TheFart = true;
			if (Main.netMode == 2)
			{
				NetMessage.SendData(7);
			}
		}
		int num2 = Main.LocalPlayer.FindItem(((ModItem)this).Mod.Find<ModItem>("FakeWhoopieCushion").Type);
		Main.LocalPlayer.inventory[num2].TurnToAir();
	}
}
