using System;
using System.Collections.Generic;
using UnityEngine;

namespace GB_Platformer
{
    internal sealed class SpriteAnimator : IExecute, IDisposable
    {
        private readonly SpriteAnimationsData _data;
        private readonly Dictionary<SpriteRenderer, CustomAnimation> _activeAnimations = new();

        public SpriteAnimator(SpriteAnimationsData data)
        {
            _data = data;
        }

        public void StartAnimation(SpriteRenderer spriteRenderer, Track track, bool loop, float speed)
        {
            if (_activeAnimations.TryGetValue(spriteRenderer, out CustomAnimation animation))
            {
                animation.Loop = loop;
                animation.Speed = speed;
                animation.IsActive = false;

                if (animation.Track == track)
                {
                    return;
                }

                animation.Track = track;
                animation.Sprites = _data.Sequences.Find(sequence => sequence.Track == track).Sprites;
                animation.Counter = 0;
            }
            else
            {
                _activeAnimations.Add(spriteRenderer, new CustomAnimation
                {
                    Track = track,
                    Sprites = _data.Sequences.Find(sequence => sequence.Track == track).Sprites,
                    Loop = loop,
                    Speed = speed
                });
            }
        }

        public void StopAnimation(SpriteRenderer sprite)
        {
            if (_activeAnimations.ContainsKey(sprite))
                _activeAnimations.Remove(sprite);
        }

        public void Execute()
        {
            foreach (KeyValuePair<SpriteRenderer, CustomAnimation> animation in _activeAnimations)
            {
                animation.Value.Execute();
                animation.Key.sprite = animation.Value.Sprites[(int)animation.Value.Counter];
            }
        }

        public void Dispose()
        {
            _activeAnimations.Clear();
        }
    }
}
