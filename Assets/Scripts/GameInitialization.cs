namespace GB_Platformer
{
    internal sealed class GameInitialization
    {
        public GameInitialization(Controllers controllers, GameData gameData)
        {
            BackgroundLayerManager backgroundLayerManager = new(gameData.Camera, gameData.Background, gameData.BackCoefficient,
                gameData.Midground, gameData.MidCoefficient,
                gameData.Foreground, gameData.ForeCoefficient,
                gameData.BackgroundColor1, gameData.BackgroundColor2);

            SpriteAnimator spriteAnimator = new(gameData.PlayerAnimationsData);
            spriteAnimator.StartAnimation(gameData.PlayerSpriteRenderer, Track.Idle, true, 10);
            controllers.Add(backgroundLayerManager);
            controllers.Add(spriteAnimator);
        }
    }
}