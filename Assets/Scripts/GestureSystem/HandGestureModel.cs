using System.Collections.Generic;
using Rosambo.GestureSystem.Data;

namespace Rosambo.GestureSystem.Model
{
    public class HandGestureModel
    {
        public IReadOnlyList<HandGesture> HandGestures => _handGestures;
        private readonly List<HandGesture> _handGestures = new List<HandGesture>();

        public void Add(HandGestureData handGestureData)
        {
            _handGestures.Add(new HandGesture(handGestureData));
        }

        public HandGesture GetGesture(GestureType gestureType)
        {
            foreach (var handGesture in _handGestures)
            {
                if (handGesture.Data.GestureType == gestureType)
                {
                    return handGesture;
                }
            }

            return null;
        }

        public HandGesture GetGesture(int gestureIndex)
        {
            return _handGestures[gestureIndex];
        }
    }
}