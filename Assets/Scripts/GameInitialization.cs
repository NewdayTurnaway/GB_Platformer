namespace GB_Platformer
{
    internal sealed class GameInitialization
    {
        public GameInitialization(Controllers controllers, GameData gameData)
        {
            CameraController cameraController = new(gameData.Camera.transform, gameData.PlayerInfo.PlayerView.transform);
            BackgroundLayerManager backgroundLayerManager = new(gameData.Camera, gameData.Paralax.Background, gameData.Paralax.BackCoefficient,
                gameData.Paralax.Midground, gameData.Paralax.MidCoefficient,
                gameData.Paralax.Foreground, gameData.Paralax.ForeCoefficient,
                gameData.Paralax.BackgroundColor1, gameData.Paralax.BackgroundColor2);
            UIController uIController = new(gameData.UIView, gameData.PlayerInfo.PlayerView, gameData.QuestItemsData, gameData.QuestObjects, gameData.Coins.Count);
            SpriteAnimator spriteAnimator = new(gameData.SpriteAnimationsData);
            AbilitiesController abilitiesController = new(gameData.PlayerInfo, gameData.QuestObjects);

            PlayerController playerController = new(gameData.PlayerInfo, spriteAnimator);
            CannonController cannonController = new(gameData.CannonInfo, gameData.PlayerInfo.PlayerView.HeadTransform, spriteAnimator);
            //EnemiesSimpleController enemiesSimpleController = new(gameData.EnemiesInfo, spriteAnimator);
            EnemiesController enemiesController = new(gameData.EnemiesInfo, gameData.PlayerInfo.PlayerView,spriteAnimator);
            QuestsConfigurator questsConfigurator = new(gameData.QuestStoriesData, gameData.QuestObjects, spriteAnimator);

            new CoinsManager(gameData.PlayerInfo.PlayerView, gameData.Coins, spriteAnimator);
            new ElevatorManager(gameData.PlayerInfo.PlayerView, gameData.Elevators);

            controllers.Add(cameraController);
            controllers.Add(backgroundLayerManager);
            controllers.Add(uIController);
            controllers.Add(spriteAnimator);
            controllers.Add(abilitiesController);
            controllers.Add(playerController);
            controllers.Add(cannonController);
            //controllers.Add(enemiesSimpleController);
            controllers.Add(enemiesController);
            controllers.Add(questsConfigurator);
        }
    }
}