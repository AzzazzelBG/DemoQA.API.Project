dotnet test src\DemoQA.API.Tests\DemoQA.API.Tests.csproj -l:"trx;LogFileName=TestResults.trx"
Start-Process -NoNewWindow "./tools/TrxReporter.exe" -ArgumentList "-i ./src/DemoQA.API.Tests/TestResults/TestResults.trx -o ./src/DemoQA.API.Tests/TestResults/TestResults.html" -Wait
Invoke-Expression ./src/DemoQA.API.Tests/TestResults/TestResults.html

Read-Host -Prompt "Press Enter to exit"
