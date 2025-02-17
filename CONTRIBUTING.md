Contributing
============

Please read [.NET Core Guidelines](https://github.com/dotnet/runtime/blob/master/CONTRIBUTING.md) for more general information about coding styles, source structure, making pull requests, and more.
While this project is in the early phases of development, some of the guidelines in this document do not yet apply as strongly.

## Developer guide

This project can be developed on any platform. To get started, follow instructions for your OS.

### Prerequisites

This project depends on .NET Core 5.0. Before working on the project, check that .NET Core prerequisites have been met.

 - [Prerequisites for .NET Core on Windows](https://docs.microsoft.com/en-us/dotnet/core/windows-prerequisites?tabs=net50)
 - [Prerequisites for .NET Core on Linux](https://docs.microsoft.com/en-us/dotnet/core/linux-prerequisites?tabs=net50)
 - [Prerequisites for .NET Core on macOS](https://docs.microsoft.com/en-us/dotnet/core/macos-prerequisites?tabs=net50)

### Visual Studio

This project supports [Visual Studio 2019](https://visualstudio.com) and [Visual Studio for Mac](https://www.visualstudio.com/vs/visual-studio-mac/). Any version, including the free Community Edition, should be sufficient so long as you install Visual Studio support for .NET Core development.

This project also supports using
[Visual Studio Code](https://code.visualstudio.com). Install the [C# extension](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csharp) and install the [.NET Core CLI](https://get.dot.net/core) to get started.


## Architecture


### Nuget Dependencies

Package                          | Version                                                                                                                                     | Description
---------------------------------| ------------------------------------------------------------------------------------------------------------------------------------------- | -----------------------------
`System.CommandLine`             | [![Nuget](https://img.shields.io/nuget/v/System.CommandLine.svg)](https://nuget.org/packages/System.CommandLine)                            | Command line parser, model binding, invocation, shell completions
`System.CommandLine.Hosting`     | [![Nuget](https://img.shields.io/nuget/v/System.CommandLine.Hosting.svg)](https://nuget.org/packages/System.CommandLine.Hosting)            | support for using System.CommandLine with [Microsoft.Extensions.Hosting](https://www.nuget.org/packages/Microsoft.Extensions.Hosting/)
`Corgibytes.Freshli.Lib`             | [![Nuget](https://img.shields.io/nuget/v/Corgibytes.Freshli.Lib.svg)](https://nuget.org/packages/Corgibytes.Freshli.Lib)                            | Core library for collecting historical metrics about a project's dependencies
`YamlDotNet`             | [![Nuget](https://img.shields.io/nuget/v/YamlDotNet.svg)](https://nuget.org/packages/YamlDotNet)                            | A .NET library for YAML. YamlDotNet provides low level parsing and emitting of YAML as well as a high level object model similar to XmlDocument.
`Newtonsoft.Json`             | [![Nuget](https://img.shields.io/nuget/v/Newtonsoft.Json.svg)](https://nuget.org/packages/Newtonsoft.Json)                            | Json.NET is a popular high-performance JSON framework for .NET
`NamedServices.Microsoft.Extensions.DependencyInjection`             | [![Nuget](https://img.shields.io/nuget/v/NamedServices.Microsoft.Extensions.DependencyInjection.svg)](https://nuget.org/packages/NamedServices.Microsoft.Extensions.DependencyInjection)                            | Named Services for Microsoft.Extensions.DependencyInjection


### Main Entities

#### Command Options and Commands and Command Runners

* Freshli CLI **Commands** are implemented using the [Command Line Api](https://github.com/dotnet/command-line-api) library. All the commands are located under the **Commands** folder and are needed for the library to know how to parse user input and transform it into CommandOptions. 
This type of entity is where you configure the structure of your command.
* Freshli CLI **Command Options** represents all the options allowed for a particular Command. The input will be transformed into this object and send to the command runner. All the commands options for all the different commands are located inside the **CommandOptions** folder.
* Freshli CLI **Command Runners** are responsible for receiving a Command Options and execute the Run Command logic. It is used by the Command classes to delegate it's execution.

So far, the following commands have been implemented:

* **scan** - Collects historical metrics about a project's dependencies - Implemented in _Commands/ScanCommand.cs_, _CommandOptions/ScanCommandOptions.cs_ and _CommandRunners/ScanCommandRunner.cs_

Follow below steps if you want to contribute with a new command:

1) Add a new class called _**YourNewCommandNameCommandOptions**_ into the **CommandOptions folder** and inherit from the base class **_CommandOptions_**. You do not need to implement anything  to allow the **format** and **output** options. These are inherited from the base class. 

Example: CustomCommandOptions

```
    public class ScanCommandOptions : CommandOptions
    {                
        public string YourArgument { get ; set; }
        public string YourOption { get ; set; }

        // Define all the arguments and options as Properties in this class.
    }
```

2) Add a new class called _**YourNewCommandNameCommand**_ into the **Commands folder** and inherit it from the base class _**BaseCommand**_. You do not need to implement anything  to allow the **format** and **output** options. These are inherited from the base class.

    Example: CustomCommand

```
    public class CustomCommand : BaseCommand<CustomCommandOptions>
    {
        public CustomCommand() : base("custom", "Custom command description")
        {
          // add your arguments and/or options definitioins here. For detailed information
          // you have to reference the command-line-api library documentation. Below are just
          // examples. See the Commands/ScanCommand.cs for an example or go to the Command Line Api (https://github.com/dotnet/command-line-api) repository for detailed information.

           Option<string> yourOption= new(new[] { "--alias1", "alias2" }, description: "Option  Description");    
           AddOption(yourOption);

           Argument<string> yourArgument = new("yourargumentname", "Argument Description")
           AddArgument(yourArgument);
        }
    }
```

3) Add a new class called _**YourNewCommandNameCommandRunner**_ into the **CommandRunners** folder and inherit it from **CommandRunner**. 
Implement the Run method.

Example: CustomCommandRunners

```
 public class CustomCommandRunner : CommandRunner<CustomCommandOptions>
 {        
        public ScanCommandRunner(IServiceProvider serviceProvider, Runner runner): base(serviceProvider,runner)
        {
            
        }

        public override int Run(CustomCommandOptions options)
        {
           // Implement the Run logic
        }
}

```

4) Configure your dependencies in the IoC Container. Go to the IoC folder, open the **FreshliServiceBuilder.cs** class and create a new method called

```
        public void RegisterCustomCommand()
        {
            Services.AddScoped<ICommandRunner<CustomCommandOptions>, CustomCommandRunner>();
            Services.AddOptions<YourNewCommandNameCommandOptions>().BindCommandLine();

            // Add additional services as needed
        }
```

Update the **FreshliServiceBuilder Register** method in order to add the invokation to this new method.

5) Go to Main and Add your new Command in the _**CreateCommandLineBuilder**_ method. This will allow the main program to identify you added
a new command.
 
6) Go to **Corgibytes.Freshli.Cli.Test** and add unit tests.

7) Open a Pull Request with your changes

#### Formatters

A serialization formatter is responsible for encoding objects into a particular format. The formatted output will be sent to all the selected output strategies
Available formatters are **json, yaml, and csv**. These formatters can be found under the _**Formatters**_ folder as follows.

* **CsvOutputFormatter**  - Encodes cli response Csv into csv format.
* **JsonOutputFormatter** - Encodes cli response into Json format.
* **YamlOutputFormatter** - Encodes cli response into Yaml format.

If you want to contribute with a formatter, you have to follow below instructions:

1) Add the new format type into the _**FormatType**_ enum. Example: _**Custom**_
 
2) Add a new class called _**YourNewFormatOutputFormatter**_ into the _**Formatters folder**_ and inherit it from OutputFormatter. Implement requred methods 
  Example: CustomOutputFormatter

```
    public class CustomOutputFormatter : OutputFormatter
    {
        public override FormatType Type => FormatType.Custom;

        protected override string Build<T>(T entity)
        {
            // Implement object serialization
        }

        protected override string Build<T>(IList<T> entities)
        {
            // Implement object list serialization
        }
    }
```

5) Register your new formatter class in the IoC Container. Open the _**FreshliServiceBuilder.cs**_ file, search for the **RegisterBaseCommand** 
method and add a new registration line for your formatter as follows

```     
    Example: Services.AddNamedScoped<IOutputFormatter,CustomOutputFormatter>(FormatType.Custom);

```

6) In order to test your new formatter, build the solution and run a command (for example the scan command), and specofy your new format as input for the format option. Example:

        ```
        freshli scan repository-path -f custom

        ```
7) Go to Corgibytes.Freshli.Cli.Test and add unit tests.
8) Open a Pull Request with your changes

#### Output Strategies

An Output Strategy is reponsable for sending the serialized response of a command to a configured output. The formatted output will be sent to all the selected output strategies
Available outputs are **console, file**. These formatters can be found under the _**OutputStrategies**_ folder as follows.

* **ConsoleOutputStrategy**  - Sends serialized data by a formatter to the standard output.
* **FileOutputStrategy**     - Sends serialized data by a formatter to a file. 

If you want to contribute with a new output strategy, you have to follow below instructions:

1) Add the new format type into the _**OutputStrategyType**_ enum. Example: _**Custom**_
 
2) Add a new class called _**YourNewOutputStrategy**_ into the _**OutputStrategies folder**_ and implement the _**IOutputStrategy**_ interface.
  Example: CustomOutputStrategy

```
   public class CustomOutputStrategy : IOutputStrategy
    {
        public OutputStrategyType Type => OutputStrategyType.Custom;

        public virtual void Send(IList<MetricsResult> results, IOutputFormatter formatter, CustomCommandOptions options)
        {
            // Implement your logic here.
        }
    }
```

5) Register your new output strategy class in the IoC Container. Open the _**FreshliServiceBuilder.cs**_ file, search for the **RegisterBaseCommand** 
method and add a new registration line for your strategy as follows

```     
    Example: Services.AddNamedScoped<IOutputStrategy, CustomOutputStrategy>(OutputStrategyType.Custom);
```

6) In order to test your new output strategy, build the solution and run a command (for example the scan command), and specify your new format as input for the output option. Example:

        ```
        freshli scan repository-path -o custom

        ```
7) Go to Corgibytes.Freshli.Cli.Test and add unit tests.
8) Open a Pull Request with your changes
