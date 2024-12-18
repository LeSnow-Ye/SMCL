# SMCL — Sprig Minecraft Launcher
## 注意
由于 SMCL 最初是为整合包和服务器开发，所以目前功能十分有限。

因此现在仍不能通过 GUI 界面选择游戏版本或者通过 SMCL 下载游戏。

## ~~特性~~ 功能
* 通过指定的 **Update Source** 更新客户端
* 自定义随机背景
* ~~And so on~~
  
## 有图没真相
![](https://p5.toutiaoimg.com/origin/pgc-image/f77d8f7b56bb44288f563ec03cd4c4bd)
![](https://p5.toutiaoimg.com/origin/pgc-image/4417a282424246d690c9490f252721e1)
![](https://p5.toutiaoimg.com/origin/pgc-image/6250b325baae49a0ad500341074e787a)

## 使用
### 指定游戏版本
如上文所述，你需要手动指定游戏版本。
编辑 `SMCL.xml` 并向其中添加 `<Version>#Version#</Version>`。

多数情况下, `#Version#` 就是 `.minecraft\versions` 目录下的文件夹名。
> I如果不存在 `SMCL.xml`，启动一次启动器即可。
``` xml
<?xml version="1.0" encoding="utf-8"?>
<Config xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
  <Username>LeSnow_Ye</Username>
  <Version>1.12.2</Version>
  <Memory>Auto</Memory>
  <JavaPath>Auto</JavaPath>
</Config>
```

### 内存设置
编辑 `<Memory>Auto</Memory>` 来指定游戏最大内存。整数值的单位是 `MB`。

### 自动更新
1. 向 `SMCL.xml` 中添加 `<UpdateSource>#Your update source here#</UpdateSource>`。
> 更新源必须是完整的 URI 链接
> 
> 所以不应省略 `http://` , `http://` 或其他前缀。

示例如下：

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

2. 更新源指定到一个 XML 文档，大概长这样：

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

每个 `ManifestItem` 都应包含 `FilePath` 和 `Level`。

若指定 `Level` 为 `Required`, 启动器会自动从 `Url` 下载所需的文件至 `FilePath`。

若指定 `Level` 为 `Banned`, 启动器会自动禁用 `FilePath` 指定的文件。


### 背景图
添加 `bg` 文件夹，把背景图丢进去就行了。
推荐比例为 2 : 1。

### 自动进入服务器
向 `SMCL.xml` 中加入 `<ServerIp>#服务器 IP 地址#</ServerIp>` 即可.

## Dependencies
* [MaterialDesignInXAML](https://github.com/MaterialDesignInXAML/MaterialDesignInXamlToolkit)
* [Newtonsoft.Json](https://github.com/JamesNK/Newtonsoft.Json)
* [KMCCC](https://github.com/MineStudio/KMCCC)

## Enjoy!
