# RGBtoGrey

RGBtoGrey is a WPF C# app that allows to convert an image to grayscale and save it to disk.

## Getting started
App should work on Windows 7 and newer.

#### Prerequisites for development:
Only required thing to start modyfing and devloping this project is [.NET Framework 4.7.2 Developer Pack](https://dotnet.microsoft.com/download/dotnet-framework/thank-you/net472-developer-pack-offline-installer).

RGBtoGrey project should be set up as a startup project.

#### Prerequisites for using realease:
If you only want to use compiled version of the app, you need to download and install [.NET Framework 4.7.2 Runtime](https://dotnet.microsoft.com/download/dotnet-framework/thank-you/net472-web-installer).

## Overview of technologies used in the project

#### WPF Framework
To simplify creating loosely coupled app I used [Prism Framework](https://prismlibrary.com/docs/).
I took advantage of existing mechanisms that implement popular design patterns in MVVM.
#### IoC Container Framework
Since Prism is integrated with [Unity Container](http://unitycontainer.org/) I decided to use it in this project.
#### Test Framework
For unit testing this project uses one of the most popular framworks - [NUnit](https://nunit.org/). For mocking I used [Moq](https://github.com/Moq/moq4/wiki/Quickstart).

## Contributing
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

Please make sure to update tests as appropriate.

## License
[MIT](https://choosealicense.com/licenses/mit/)
