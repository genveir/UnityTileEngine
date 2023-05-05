using System;
using System.Linq;

namespace Assets.src.IO.Models
{
    internal class CommandDto
    {
        public int OrderType { get; set; }

        public int[] Parameters { get; set; }

        public byte[] ToByteArray()
        {
            byte[] orderType = BitConverter.GetBytes(OrderType);
            byte[] numParams = BitConverter.GetBytes(Parameters.Length);
            byte[] parameters = Parameters
                .Select(p => BitConverter.GetBytes(p))
                .Aggregate((a, b) => a.Concat(b).ToArray());

            return orderType.Concat(numParams).Concat(parameters).ToArray();
        }

        public static CommandDto FromByteArray(byte[] bytes, int startIndex)
        {
            int orderType = BitConverter.ToInt32(bytes, startIndex + 0);
            int numParams = BitConverter.ToInt32(bytes, startIndex + 4);

            int[] parameters = new int[numParams];
            for (int n = 0; n < numParams; n++)
            {
                parameters[n] = BitConverter.ToInt32(bytes, startIndex + 8 + 4 * n);
            }

            return new()
            {
                OrderType = orderType,
                Parameters = parameters
            };
        }
    }
}