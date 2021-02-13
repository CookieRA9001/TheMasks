using Terraria.ModLoader;

namespace TheMasks
{
	public class TheMasks : Mod
	{
		public static TheMasks Instance;

		public TheMasks(){
			Instance = this;
		}

		public override void Load(){
			if(Instance == null || Instance != this){
				Instance = this;
			}
		}
	}
}