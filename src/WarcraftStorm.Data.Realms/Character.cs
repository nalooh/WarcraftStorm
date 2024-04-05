using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WarcraftStorm.Data.Realms
{
	[Table("Characters", Schema = "Realms")]
	public class Character
	{
		[Key, Column("CharacterID")]
		public int Id { get; protected set; }

		[Column("AccountID")]
		public int AccountId { get; set; }

		[Column("CharacterName")]
		public string Name { get; set; }

		[Column("CharacterRace")]
		public Race Race { get; set; }

		[Column("CharacterClass")]
		public Class Class { get; set; }

		[Column("CharacterGender")]
		public Gender Gender { get; set; }

		[Column("CharacterSkin")]
		public byte Skin { get; set; }

		[Column("CharacterFace")]
		public byte Face { get; set; }

		[Column("CharacterHairStyle")]
		public byte HairStyle { get; set; }

		[Column("CharacterHairColor")]
		public byte HairColor { get; set; }

		[Column("CharacterFacialHair")]
		public byte FacialHair { get; set; }

		[Column("CharacterOutfit")]
		public byte Outfit { get; set; }

		[Column("CharacterLevel")]
		public byte Level { get; set; }

		[Column("CharacterPositionX")]
		public double PositionX { get; set; }

		[Column("CharacterPositionY")]
		public double PositionY { get; set; }

		[Column("CharacterPositionZ")]
		public double PositionZ { get; set; }

		[Column("CharacterZone")]
		public int Zone { get; set; }

		[Column("CharacterMap")]
		public int Map { get; set; }
	}
}