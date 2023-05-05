using System;
using System.Linq;

namespace Assets.src.IO.Models
{
    public class TileDto
    {
        public int X { get; set; }

        public int Y { get; set; }

        public int Floor { get; set; }

        public int Creature { get; set; }

        public int Item { get; set; }

        public int Effect { get; set; }

        public bool IsCharacterLocation { get; set; }

        public byte[] ToByteArray()
        {
            var x = BitConverter.GetBytes(X);
            var y = BitConverter.GetBytes(Y);
            var floor = BitConverter.GetBytes(Floor);
            var creature = BitConverter.GetBytes(Creature);
            var item = BitConverter.GetBytes(Item);
            var effect = BitConverter.GetBytes(Effect);
            var isCharacterLocation = BitConverter.GetBytes(IsCharacterLocation);

            return x.Concat(y).Concat(floor).Concat(creature).Concat(item).Concat(effect).Concat(isCharacterLocation).ToArray();
        }

        public static TileDto FromByteArray(byte[] bytes, int startIndex)
        {
            var x = BitConverter.ToInt32(bytes, startIndex + 0);
            var y = BitConverter.ToInt32(bytes, startIndex + 4);
            var floor = BitConverter.ToInt32(bytes, startIndex + 8);
            var creature = BitConverter.ToInt32(bytes, startIndex + 12);
            var item = BitConverter.ToInt32(bytes, startIndex + 16);
            var effect = BitConverter.ToInt32(bytes, startIndex + 20);
            var isCharacterLocation = BitConverter.ToBoolean(bytes, startIndex + 24);

            return new()
            {
                X = x,
                Y = y,
                Floor = floor,
                Creature = creature,
                Item = item,
                Effect = effect,
                IsCharacterLocation = isCharacterLocation
            };
        }
    }
}