using System;
using System.Collections;

namespace Patterns.GameLoop
{
    public interface IGameLoop
    {
        public class TimeUpdatedData
        {
            public float GameLoopTime = 0f;
            public float DeltaTime = 0f;
        }
        
        public event EventHandler<IGameLoop.TimeUpdatedData> TimeUpdated;
        
        public IEnumerator DoGameLoop();
    }
}