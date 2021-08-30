DebugConsole is a tool in C# for debugging during gameplay.
## Architecture
DebugConsole is applied by Clean Architecture, which is written by Robert C. Martin. The core concepts as follow.
* Decoupling business rules and user interfaces
* An inner circle (entities, policies) knows nothing about an outer circle (user interfaces, Devices)

The benefit of a clean architecture is that the policies are entirely independent, you can replace any user interfaces arbitrarily. To achieve this architecture, there are some layers that must be implemented.
*	Controller: an adaptor which is used to transfer outer information to a use case input. In DebugConsole, `DebugConsoleController` get the information by subscribing `IEventBus`.
*	UseCase: this layer applies the input from a controller to manipulate entities and output the data given from entities to a presenter.
*	Entity: contains business rules. In this case, the entity is a command generator. Note that a command generator knows nothing about outer information.
*	Presenter: another adaptor which transfers data from an entity to a user interface. Since a presenter only knows `IDisplay`, users can alter any user interfaces which implement `IDisplay`.
## Usage
To drive `DebugConsoleController`, you must publish the event, `OnInputCommand`, with the command word.
```c#
public void SubmitCommand()
{
    _eventBus.Publish(new OnInputCommand(inputField.text));
}
``` 
On the other hand, to create a command, the class’s name should be the command word with suffix, `DebugCommand`. The class should inherit `DebugCommandBase`. In both examples, I call the command by class name. You can also call it with `CommandToken` by implement another `IFactory<string, IDebugCommand>`.
## Example
I provide two examples for implementing different user interfaces. To call the DebugConsole, type the back quote key.
*	Command logs display on the scroll view: press the button to submit a command, and the logs display above.
![image](https://github.com/benlee0202/DebugConsole/blob/main/Images/DebugConsoleScrollView.png)
*	The command input field and related log display in one line: after inputting a command, press the enter key to submit, and the command log will display on the right.
![image](https://github.com/benlee0202/DebugConsole/blob/main/Images/DebugConsoleOneLine.png)
