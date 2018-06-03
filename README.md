# Fable.Elmish.React Minesweeper App
This is a web app developed with F# + Fable + Elmish React template. I follow up one youtube video to manual create this app step by step, it help me grasp F# related knowledge a lot after complete this app.

And I add in `additional functions to allow end user can set the minefields size and mines count by themselves`. Program will have validation rule to fine tune teh size and mines count at backend.

I personally do feel this F# based solution is the most comfortable way to myself to create JS related web till now (Jun 2, 2018).

Hope you enjoy it too, thanks.

## Sample Pictures
![Sample Picture - Running](https://github.com/ScottHuangZL/Minesweeper/blob/master/src/sample-picture1.png)
![Sample Picture - Lost](https://github.com/ScottHuangZL/Minesweeper/blob/master/src/sample-picture2.png)
![Sample Picture - Won](https://github.com/ScottHuangZL/Minesweeper/blob/master/src/sample-picture3.png)

## Requirements

* [dotnet SDK](https://www.microsoft.com/net/download/core) 2.0.0 or higher
* [node.js](https://nodejs.org) 4.8.2 or higher
* npm5: JS package manager

Although is not a Fable requirement, on macOS and Linux you'll need [Mono](http://www.mono-project.com/) for other F# tooling like Paket or editor support.

## Editor

The project can be used by editors compatible with the new .fsproj format, like VS Code + [Ionide](http://ionide.io/), Emacs with [fsharp-mode](https://github.com/fsharp/emacs-fsharp-mode) or [Rider](https://www.jetbrains.com/rider/). **Visual Studio for Mac** is also compatible but in the current version the package auto-restore function conflicts with Paket so you need to disable it: `Preferences > Nuget > General`.

I personally use visual code for this project. 

## Installing guide

You can direct leverage this project with below steps:

In a terminal, run below commands:
* Just clone the project or download this project from github as first step, and then use below commands.
* `yarn install` or `npm install` to add node related packages
* `cd src`
* `dotnet restore` to restore related .net packages
* `code ..` to open visual code editor in parent folder
* `dotnet fable yarn-start` or `dotnet fable npm-start`

Or else, if you want to create app from beginning to end, try below:
* `mkdir Minesweeper`
* `cd Minesweeper`
* `dotnet new -u Fable.Template.Elmish.React` to uninstall template
* `dotnet new -i Fable.Template.Elmish.React` to install latest template
* `dotnet new fable-elmish-react -lang F#` to create project in current folder
* `git init`
* `git add -A`
* `git commit -am "Initial template"` to setup git related matter
* `yarn install` or `npm install` to add node related packages
* `cd src`
* `dotnet restore` to restore related .net packages
* `code ..` to open visual code editor in parent folder
* `dotnet fable yarn-start` or `dotnet fable npm-start` to start the app with HMR.
* Then you can go to http://localhost:8080 to view the app result.


I do not use `Fulma` for this project at very beginning. But I decide add this package at last.
The steps as below FYI. I have done it for you:)

* in the project root `paket.dependencies` add in one line `nuget Fulma`
* in the src folder `paket.references` add in one line `Fumla`
* in the project root run command `.\paket\paket.exe update`
* in the src folder run command `dotnet restore`
* And then you should be able `open Fulma` to use Fulma in your view file now.


> In some shells you many need quotations: `dotnet new -i "Fable.Template.Elmish.React::*"`. If you use dotnet SDK 2, you should only need to type `dotnet new -i Fable.Template.Elmish.React`.

If you are using VS Code + [Ionide](http://ionide.io/), you can also use the key combination: Ctrl+Shift+B (Cmd+Shift+B on macOS) instead of typing the `dotnet fable yarn-start` command. This also has the advantage that Fable-specific errors will be highlighted in the editor along with other F# errors.

Any modification you do to the F# code will be reflected in the web page after saving. When you want to output the JS code to disk, run `dotnet fable yarn-build` (or `npm-build`) and you'll get a minified JS bundle in the `public` folder.

## --------------------Below FYI only-------------------

### Paket

[Paket](https://fsprojects.github.io/Paket/) is the package manager used for F# dependencies. It doesn't need a global installation, the binary is included in the `.paket` folder. Other Paket related files are:

- **paket.dependencies**: contains all the dependencies in the repository.
- **paket.references**: there should be one such a file next to each `.fsproj` file.
- **paket.lock**: automatically generated, but should committed to source control, [see why](https://fsprojects.github.io/Paket/faq.html#Why-should-I-commit-the-lock-file).
- **Nuget.Config**: prevents conflicts with Paket in machines with some Nuget configuration.

> Paket dependencies will be installed in the `packages` directory. See [Paket website](https://fsprojects.github.io/Paket/) for more info.

### npm

- **package.json**: contains the JS dependencies together with other info, like development scripts.
- **package-lock.json**: is the lock file created by npm5.

> JS dependencies will be installed in `node_modules`. See [npm](https://www.npmjs.com/) website for more info.

### Webpack

[Webpack](https://webpack.js.org) is a bundler, which links different JS sources into a single file making deployment much easier. It also offers other features, like a static dev server that can automatically refresh the browser upon changes in your code or a minifier for production release. Fable interacts with Webpack through the `fable-loader`.

- **webpack.config.js**: is the configuration file for Webpack. It allows you to set many things: like the path of the bundle, the port for the development server or [Babel](https://babeljs.io/) options. See [Webpack website](https://webpack.js.org) for more info.

