namespace GB_Platformer
{
    internal sealed class GameInitialization
    {
        public GameInitialization(Controllers controllers, GameData gameData)
        {
            CameraController cameraController = new(gameData.Camera.transform, gameData.PlayerInfo.PlayerSpriteRenderer.transform);
            BackgroundLayerManager backgroundLayerManager = new(gameData.Camera, gameData.Paralax.Background, gameData.Paralax.BackCoefficient,
                gameData.Paralax.Midground, gameData.Paralax.MidCoefficient,
                gameData.Paralax.Foreground, gameData.Paralax.ForeCoefficient,
                gameData.Paralax.BackgroundColor1, gameData.Paralax.BackgroundColor2);

            SpriteAnimator spriteAnimator = new(gameData.PlayerInfo.PlayerAnimationsData);
            PlayerController playerController = new(gameData.PlayerInfo, spriteAnimator);
            CannonController cannonController = new(gameData.CannonInfo, gameData.PlayerInfo.PlayerSpriteRenderer.transform);

            controllers.Add(cameraController);
            controllers.Add(backgroundLayerManager);
            controllers.Add(spriteAnimator);
            controllers.Add(playerController);
            controllers.Add(cannonController);
        }
    }
}