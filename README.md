
# Logger CSharp

Logger library for .NET Core & Framework Application




## Installation

Download latest release and add the Logger.dll on project reference in your project.
    
## Example

Action on specific severity level ( HIGH, MEDIUM, LOW )

```
Logger logger = new Logger()
            {
                lowSeverityAction = new Action(( )=> {
                   //your stuff
                })
            };
            logger.error(" ", Logger.LOW_SEVERITY);
        }

```

Basic use

```
Logger logger = new Logger();
logger.log("");
logger.error("");
logger.error("", Logger.LOW_SEVERITY)
logger.info("");
logger.debug("");

```

Custom prefix and colour

```
Logger logger = new Logger(){
    errorPrefix = "[ERROR] - ",
    errorColor = ConsoleColor.RED,
    ...
};

```

Enable file logs

```
Logger logger = new Logger(){

                enableSaveLogs = true,
                logFileBaseName = "myapp_",
                logsDirectory = @"C:\"
};

```

## Authors

- [@melv1no](https://www.github.com/melv1no)


