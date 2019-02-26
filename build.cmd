dotnet build -c Release
dotnet test Roadrunner.Test/Roadrunner.Test.csproj
start cmd.exe /c "dotnet run -c Release -p Roadrunner.Web/Roadrunner.Web.csproj"
start cmd.exe /c "dotnet run -c Release -p Roadrunner.PassengersSimulator/Roadrunner.PassengersSimulator.csproj"
start cmd.exe /c "dotnet run -c Release -p Roadrunner.DriversSimulator/Roadrunner.DriversSimulator.csproj"