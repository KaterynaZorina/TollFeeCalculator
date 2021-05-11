# Toll fee calculator
#### This app calculates toll fee for specific vehicle on specific date. Calculations are based on vehicle type and time of a day, when toll pass was generated.

We support next vehicle types: car, diplomat, emergency, foreign, military, motorbike, tractor. All previously listed vehicles, except cars, are toll free.

# Instructions
## Prerequisites
To be able to run the console app or unit tests, included in this repo, next steps should be completed:
1. Install .NET 5 SDK. Follow next link for more details: https://dotnet.microsoft.com/download/dotnet/5.0
2. Install `dotnet` tools. Follow next link for more details: https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-tool-install

## How to run the console app
1. Navigate to the root folder.
2. Run `dotnet restore` using cmd tool.
3. Run `dotnet build --no-restore` using cmd tool.
4. Navigate to the `src\TollFeeCalculator.App` folder and run next command: `dotnet run` using cmd tool. Follow the instructions, written to the console.

## How to run unit tests
1. Navigate to the root folder.
2. Run `dotnet restore` using cmd tool.
3. Run `dotnet build --no-restore` using cmd tool.
4. Run `dotnet test --no-build` using cmd tool.
