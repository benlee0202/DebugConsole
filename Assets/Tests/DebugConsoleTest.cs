using DebugConsole.Controllers;
using DebugConsole.Entities;
using DebugConsole.Interfaces;
using DebugConsole.Presenters;
using DebugConsole.Repositories;
using DebugConsole.Utilities;
using EventHandler.Events;
using EventHandler.Interfaces;
using NUnit.Framework;

namespace Tests
{
    public class DebugConsoleTest
    {
        private readonly IEventBus _eventBus = new EventHandler.EventBus();
        private bool _isInvoke;
        private const int ViewID = 1;
        
        [SetUp]
        public void SetUp()
        {
            _isInvoke = false;
            _eventBus.Subscribe<OnTestCommandExecute>(SetIsInvoke);
        }

        [Test]
        public void Create_Debug_Console()
        {
            // Arrange
            var debugConsoleDisplay = new FakeDisplay(ViewID);
            var commandGeneratorRepository = new CommandGeneratorRepository();
            var displayRepository = new DisplayRepository();
            var presenter = new DebugConsolePresenter(displayRepository);
            var controller = new DebugConsoleController(_eventBus,
                new UseCaseFactory<CommandGenerator>(commandGeneratorRepository, presenter),
                new CommandFactory(_eventBus));
            
            // Act
            controller.Enable();
            _eventBus.Publish(new OnCreateDebugConsole(debugConsoleDisplay));
            controller.Disable();
            
            // Assert
            Assert.AreEqual(debugConsoleDisplay, displayRepository.GetItem(ViewID));
            Assert.IsNotNull(commandGeneratorRepository.GetItem(ViewID));
        }

        [Test]
        public void Execute_Command_Without_Arguments()
        {
            // Arrange
            var debugConsoleDisplay = new FakeDisplay(ViewID);
            var commandGeneratorRepository = new CommandGeneratorRepository();
            var presenter = new DebugConsolePresenter(new DisplayRepository());
            var controller = new DebugConsoleController(_eventBus,
                new UseCaseFactory<CommandGenerator>(commandGeneratorRepository, presenter),
                new CommandFactory(_eventBus));
            
            // Act
            controller.Enable();
            _eventBus.Publish(new OnCreateDebugConsole(debugConsoleDisplay));
            _eventBus.Publish(new OnInputCommand("Test"));
            controller.Disable();

            // Assert
            Assert.IsTrue(_isInvoke);
        }

        [Test]
        [TestCase("TestArgument test 10 0.9 true", "System.String: test, System.Int32: 10, System.Single: 0.9, System.Boolean: True")]
        [TestCase("TestArgument test 10 1 true", "System.String: test, System.Int32: 10, System.Single: 1, System.Boolean: True")]
        [TestCase("TestArgument test 10 0.9 true", "System.String: test, System.Int32: 10, System.Single: 0.9, System.Boolean: True")]
        [TestCase("TestArgument test 10 0.9 false", "System.String: test, System.Int32: 10, System.Single: 0.9, System.Boolean: False")]
        public void Execute_Command_With_Arguments(string command, string expected)
        {
            // Arrange
            var debugConsoleDisplay = new FakeDisplay(ViewID);
            var commandGeneratorRepository = new CommandGeneratorRepository();
            var presenter = new DebugConsolePresenter(new DisplayRepository());
            var controller = new DebugConsoleController(_eventBus,
                new UseCaseFactory<CommandGenerator>(commandGeneratorRepository, presenter),
                new CommandFactory(_eventBus));
            
            // Act
            controller.Enable();
            _eventBus.Publish(new OnCreateDebugConsole(debugConsoleDisplay));
            _eventBus.Publish(new OnInputCommand(command));
            controller.Disable();

            // Assert
            Assert.AreEqual(expected, debugConsoleDisplay.Log);
        }

        [Test]
        [TestCase("TestArgument test 10.1 0.9 true", "Cannot convert the argument to int")]
        [TestCase("TestArgument test 10 0.9 t", "Cannot convert the argument to bool")]
        public void Show_Error_Massage_If_Wrong_Arguments(string command, string expected)
        {
            // Arrange
            var debugConsoleDisplay = new FakeDisplay(ViewID);
            var commandGeneratorRepository = new CommandGeneratorRepository();
            var presenter = new DebugConsolePresenter(new DisplayRepository());
            var controller = new DebugConsoleController(_eventBus,
                new UseCaseFactory<CommandGenerator>(commandGeneratorRepository, presenter),
                new CommandFactory(_eventBus));
            
            // Act
            controller.Enable();
            _eventBus.Publish(new OnCreateDebugConsole(debugConsoleDisplay));
            _eventBus.Publish(new OnInputCommand(command));
            controller.Disable();

            // Assert
            Assert.AreEqual(expected, debugConsoleDisplay.Log);
        }

        [Test]
        public void Input_Wrong_Command_Will_Not_Execute_Anything()
        {
            // Arrange
            var debugConsoleDisplay = new FakeDisplay(ViewID);
            var commandGeneratorRepository = new CommandGeneratorRepository();
            var presenter = new DebugConsolePresenter(new DisplayRepository());
            var controller = new DebugConsoleController(_eventBus,
                new UseCaseFactory<CommandGenerator>(commandGeneratorRepository, presenter),
                new CommandFactory(_eventBus));
            
            // Act
            controller.Enable();
            _eventBus.Publish(new OnCreateDebugConsole(debugConsoleDisplay));
            _eventBus.Publish(new OnInputCommand("WrongTest"));
            controller.Disable();

            // Assert
            Assert.IsFalse(_isInvoke);
        }

        [Test]
        public void Display_Error_Message_If_Input_Wrong_Command()
        {
            // Arrange
            var debugConsoleDisplay = new FakeDisplay(ViewID);
            var commandGeneratorRepository = new CommandGeneratorRepository();
            var presenter = new DebugConsolePresenter(new DisplayRepository());
            var controller = new DebugConsoleController(_eventBus,
                new UseCaseFactory<CommandGenerator>(commandGeneratorRepository, presenter),
                new CommandFactory(_eventBus));
            
            // Act
            controller.Enable();
            _eventBus.Publish(new OnCreateDebugConsole(debugConsoleDisplay));
            _eventBus.Publish(new OnInputCommand("WrongTest"));
            controller.Disable();
            
            // Assert
            Assert.AreEqual("Command not found", debugConsoleDisplay.Log);
        }

        [TearDown]
        public void TearDown()
        {
            _eventBus.Unsubscribe<OnTestCommandExecute>(SetIsInvoke);
        }

        private void SetIsInvoke(OnTestCommandExecute obj)
        {
            _isInvoke = true;
        }
    }

    internal class FakeDisplay : IDisplay
    {
        public FakeDisplay(int viewID)
        {
            ViewID = viewID;
        }

        public int ViewID { get; }
        public string Log { get; private set; }
        public void Display(IDebugCommand debugCommand)
        {
            Log = debugCommand.CommandLog;
        }
    }
}