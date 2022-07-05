using UnityEngine;

namespace GB_Platformer
{
    internal sealed class CoinView : LevelObjectView
    {
        protected override void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.gameObject.TryGetComponent(out PlayerView playerView))
            {
                playerView.AddCoin();
            }
        }
    } 
}
