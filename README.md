# SMCL — Sprig Minecraft Launcher
[中文](https://github.com/LeSnow-Ye/SMCL/blob/master/README_CN.md)

## Notice
As it's originally developed for modpacks and servers, the features are very limited now.

So by now, you can not select the game version by GUI or download the game through SMCL itself.

## Features
* Automatically update your client files with a specified **Update Source**
* Customizable random background pictures
* ~~And so on~~
  
## Screenshots
![](https://p5.toutiaoimg.com/origin/pgc-image/f77d8f7b56bb44288f563ec03cd4c4bd)
![](https://p5.toutiaoimg.com/origin/pgc-image/4417a282424246d690c9490f252721e1)
![](https://p5.toutiaoimg.com/origin/pgc-image/6250b325baae49a0ad500341074e787a)

## Usage
### Specify the Game Version
As was stated above, you need to specify the game version manually.
To do this, edit the `SMCL.xml` file and add `<Version>#Version#</Version>` to it.

In most cases, the `#Version#` is the name of a directory under `.minecraft\versions`.
> If you didn't fint it. Start SMCL and you'll get one.
``` xml
<?xml version="1.0" encoding="utf-8"?>
<Config xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
  <Username>LeSnow_Ye</Username>
  <Version>1.12.2</Version>
  <Memory>Auto</Memory>
  <JavaPath>Auto</JavaPath>
</Config>
```

### Memory Configuration
Edit `<Memory>Auto</Memory>` to specify the MaxMemory of the game.The unit of integer value is `MB`.

### Automatically Update
1. Add `<UpdateSource>#Your update source here#</UpdateSource>` to the `SMCL.xml`
> The update source must be a **full URI** link
> 
> In this way, you **mustn't** leave out `http://` , `http://` or something else.

Now the `SMCL.xml` should be like:

``` xml
<?xml version="1.0" encoding="utf-8"?>
<Config xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
  <Username>LeSnow_Ye</Username>
  <Version>1.12.2</Version>
  <Memory>Auto</Memory>
  <JavaPath>Auto</JavaPath>
  <UpdateSource>http://localhost:12121/manifest.xml</UpdateSource>
</Config>
```

2. The update source should lead to a xml file like

``` xml
<ArrayOfManifestItem xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <ManifestItem>
    <FilePath>.minecraft\mods\mod1.jar</FilePath>
    <Url>https://*.*/mod1.jar</Url>
    <Level>Required</Level>
  </ManifestItem>
  <ManifestItem>
    <FilePath>.minecraft\config\mod1.cfg</FilePath>
    <Url>https://*.*/mod1.cfg</Url>
    <Level>Required</Level> 
  </ManifestItem>
  <ManifestItem>
    <FilePath>.minecraft\mods\xray.jar</FilePath>
    <Level>Banned</Level>
  </ManifestItem>
</ArrayOfManifestItem>
```

Each `ManifestItem` should contain a `FilePath` and a `Level`.

When the `Level` is set as `Required`, the laucher would download the file from the specified `Url`.

When the `Level` is set as `Banned`, the laucher would automatically disable the file specified through `FilePath`.


### Backgrounds
You just need to add directory `bg` and put your backgrounds there.
The recommended resolution ratio is 2 : 1.

### Automatically Joining Server
Add `<ServerIp>#Your Server IP Address here#</ServerIp>` to the `SMCL.xml`.

## Dependencies
* [MaterialDesignInXAML](https://github.com/MaterialDesignInXAML/MaterialDesignInXamlToolkit)
* [Newtonsoft.Json](https://github.com/JamesNK/Newtonsoft.Json)
* [KMCCC](https://github.com/MineStudio/KMCCC)

## Enjoy!
