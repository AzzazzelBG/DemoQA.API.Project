<p align="center">
  <h3 align="center">DemoQA Integration Tests</h3>

  <p align="center">
    This project runs integration tests for https://www.demoqa.com/ Web API and generates a report with the tests results afterwards.
    <br />
    <br />
    <a href="https://github.com/AzzazzelBG/DemoQA.API.Project/issues">Request New Test</a>
    ·
    <a href="https://github.com/AzzazzelBG/DemoQA.API.Project/issues">Report Bug</a>
  </p>
</p>



<!-- TABLE OF CONTENTS -->
  <summary><h2 style="display: inline-block">Table of Contents</h2></summary>
  <ol>
    <li><a href="#about-the-project">About The Project</a></li>
    <li><a href="#getting-started">Getting Started</a></li>
    <li><a href="#contributing">Contributing</a></li>
    <li><a href="#license">License</a></li>
    <li><a href="#contact">Contact</a></li>
  </ol>

<!-- ABOUT THE PROJECT -->
## About The Project

### Built With

* [ASP.NET Core 3.1](https://docs.microsoft.com/en-us/aspnet/core/whats-new/?view=aspnetcore-3.1)
* [RestSharp](https://github.com/restsharp/RestSharp)
* [NUnit](https://nunit.org/)

<!-- GETTING STARTED -->
## Getting Started

To get a local copy up and running follow these simple steps.

### Powershell Script

1. Clone the repo:
   ```sh
   git clone https://github.com/github_username/repo_name.git
   ```
2. Run the RunTests.ps1 script with admin priviliges.
3. Enjoy the generated report.

#### Prerequisites

[.NET Core 3.1 SDK](https://dotnet.microsoft.com/download/dotnet/3.1)

### Manual Tests Run

1. Clone the repo:
   ```sh
   git clone https://github.com/github_username/repo_name.git
   ```
2. Open your shell of choice (i.e. cmd, powershel etc.) and navigate to the root directory of the repository.
3. Run the following command, which will generate a TestResults folder in **src\DemoQA.API.Tests\TestResults** with TestResults.trx report inside:
```
dotnet test src\DemoQA.API.Tests\DemoQA.API.Tests.csproj -l:"trx;LogFileName=TestResults.trx"
```
4. Execute the **/tools/TrxReporter.exe** from the shell of your choice. Here is an example with Powershell and CMD
* Powershell:
```powershell
Start-Process -NoNewWindow "./tools/TrxReporter.exe" -ArgumentList "-i ./src/DemoQA.API.Tests/TestResults/TestResults.trx -o ./src/DemoQA.API.Tests/TestResults/TestResults.html" -Wait
```
* CMD:
```
"./tools/TrxReporter.exe" -i "./src/DemoQA.API.Tests/TestResults/TestResults.trx" -o "./src/DemoQA.API.Tests/TestResults/TestResults.html"
```
5. Open the generated HTML file in **/src/DemoQA.API.Tests/TestResults/TestResults.html** using your favorite browser.
6. Enjoy the report!

<!-- CONTRIBUTING -->
## Contributing

Contributions are what make the open-source community such an amazing place to be learn, inspire, and create. Any contributions you make are **greatly appreciated**.

1. Fork the Project
2. Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3. Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the Branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## License

Distributed under the MIT License. See `LICENSE` for more information.

## Contact

[Aleksandar Drinkov](https://github.com/AzzazzelBG)
