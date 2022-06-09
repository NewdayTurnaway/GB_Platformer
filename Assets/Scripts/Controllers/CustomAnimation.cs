using System.Collections.Generic;
using UnityEngine;

namespace GB_Platformer
{
    internal sealed class CustomAnimation : IExecute
    {
        public Track Track;
        public List<Sprite> Sprites;
        public bool Loop;
        public float Speed = 10.0f;
        public float Counter;
        public bool IsActive;

        public void Execute()
        {
            if (IsActive)
            {
                return;
            }

            Counter += Time.deltaTime * Speed;

            if (Loop)
            {
                while (Counter > Sprites.Count)
                {
                    Counter -= Sprites.Count;
                }
            }
            else if (Counter > Sprites.Count)
            {
                Counter = Sprites.Count - 1;
                IsActive = true;
            }
        }
    }
}