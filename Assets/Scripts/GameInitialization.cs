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
            UIController uIController = new(gameData.UIView, gameData.PlayerInfo.PlayerView, gameData.QuestItemsData, gameData.QuestObjects);
            SpriteAnimator spriteAnimator = new(gameData.SpriteAnimationsData);

            PlayerController playerController = new(gameData.PlayerInfo, spriteAnimator);
            AbilitiesController abilitiesController = new(gameData.PlayerInfo, gameData.QuestObjects);
            
            QuestsConfigurator questsConfigurator = new(gameData.QuestStoriesData, gameData.QuestObjects, spriteAnimator);

            CannonController cannonController = new(gameData.CannonInfo, gameData.PlayerInfo.PlayerView.HeadTransform, spriteAnimator);
            EnemiesController enemiesController = new(gameData.EnemiesInfo, gameData.PlayerInfo.PlayerView,spriteAnimator);

            RespawnController respawnController = new(gameData.QuestObjects, gameData.GetQuestStory(QuestStoryType.Resettable), 
                gameData.PlayerInfo.PlayerView, enemiesController);
            ExitZone exitZone = new(gameData.ExitDoor, gameData.Exit, gameData.PlayerInfo, spriteAnimator);

            new CoinsManager(gameData.PlayerInfo.PlayerView, gameData.Coins, spriteAnimator);
            new ElevatorManager(gameData.PlayerInfo.PlayerView, gameData.Elevators);

            controllers.Add(cameraController);
            controllers.Add(backgroundLayerManager);
            controllers.Add(uIController);
            controllers.Add(spriteAnimator);
            controllers.Add(playerController);
            controllers.Add(abilitiesController);
            controllers.Add(questsConfigurator);
            controllers.Add(cannonController);
            controllers.Add(enemiesController);
            controllers.Add(respawnController);
            controllers.Add(exitZone);
        }
    }
}