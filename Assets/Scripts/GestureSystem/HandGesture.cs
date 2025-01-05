using System.Linq;
using Rosambo.GestureSystem.Data;

namespace Rosambo.GestureSystem.Model
{
    public class HandGesture : IGesture
    {
        public GestureType GestureType => Data.GestureType;
        public int AnimationHash => Data.AnimationHash;
        public readonly HandGestureData Data;

        public HandGesture(HandGestureData data)
        {
            Data = data;
        }

        public bool CanWinAgainst(GestureType gestureType)
        {
            return Data.GoodAgainst.Contains(gestureType);
        }
    }
}