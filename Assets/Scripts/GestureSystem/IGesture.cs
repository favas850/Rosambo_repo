using Rosambo.GestureSystem.Data;

namespace Rosambo.GestureSystem
{
    public interface IGesture
    {
        bool CanWinAgainst(GestureType gestureType);
        GestureType GestureType { get; }
        int AnimationHash { get; }
    }
}