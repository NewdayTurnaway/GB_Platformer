namespace GB_Platformer
{
    internal sealed class GameInitialization
    {
        public GameInitialization(Controllers controllers, GameData gameData)
        {
            CameraController cameraController = new(gameData.Camera.transform, gameData.PlayerInfo.PlayerLevelObjectView.transform);
            BackgroundLayerManager backgroundLayerManager = new(gameData.Camera, gameData.Paralax.Background, gameData.Paralax.BackCoefficient,
                gameData.Paralax.Midground, gameData.Paralax.MidCoefficient,
                gameData.Paralax.Foreground, gameData.Paralax.ForeCoefficient,
                gameData.Paralax.BackgroundColor1, gameData.Paralax.BackgroundColor2);

            SpriteAnimator spriteAnimator = new(gameData.SpriteAnimationsData);
            PlayerController playerController = new(gameData.PlayerInfo, spriteAnimator);
            CannonController cannonController = new(gameData.CannonInfo, gameData.PlayerInfo.HeadTransform, spriteAnimator);
            //EnemiesSimpleController enemiesSimpleController = new(gameData.EnemiesInfo, spriteAnimator);
            EnemiesController enemiesController = new(gameData.EnemiesInfo, gameData.PlayerInfo.PlayerLevelObjectView,spriteAnimator);
            QuestsConfigurator questsConfigurator = new(gameData.QuestStoriesData, gameData.QuestObjects, spriteAnimator);

            new CoinsManager(gameData.PlayerInfo.PlayerLevelObjectView, gameData.Coins, spriteAnimator);
            new ElevatorManager(gameData.PlayerInfo.PlayerLevelObjectView, gameData.Elevators);

            controllers.Add(cameraController);
            controllers.Add(backgroundLayerManager);
            controllers.Add(spriteAnimator);
            controllers.Add(playerController);
            controllers.Add(cannonController);
            //controllers.Add(enemiesSimpleController);
            controllers.Add(enemiesController);
            controllers.Add(questsConfigurator);
        }
    }
}