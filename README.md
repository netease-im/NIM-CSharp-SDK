# C# SDK

当前C# SDK基于[云信WindowsPC SDK V6.3.0](https://netease.im/im-sdk-demo)输出的C接口封装而成，方便C#开发者方便直观地调用接口使用云信的服务。
目前C# SDK支持 .NET Framework 3.5及以上版本开发。

## 开始

克隆项目到你的磁盘中

```bash
git clone https://github.com/netease-im/NIM-CSharp-SDK
```

使用 Visual Studio 2010或以上版本 IDE 打开 `nim.sln`，解决方案包括一个可视化测试应用程序工程。正式编译和调试解决方案之前，建议开发者简单看下编译配置、平台配置以及工程配置，合理的选择编译条件以及了解并按需自定义和选择工程的输出目录等设置项。

## 文档

- [Windows(PC) SDK 开发手册(C# 封装层)](https://dev.yunxin.163.com/docs/product/%E9%80%9A%E7%94%A8/Demo%E6%BA%90%E7%A0%81%E5%AF%BC%E8%AF%BB/PC%E9%80%9A%E7%94%A8/CSharp%E5%B0%81%E8%A3%85%E5%B1%82)

## 目录结构

├─`config`    编译配置文件    
├─`demos`     配套可视化测试程序    
│   └─`winform` winform测试程序    
└─`sdk`         SDK封装源码      
│   ├─`libs`    云信WindowsPC SDK库文件（包含x86和x64平台）  
│   ├─`Src`     源码    
│   └─`ThirdParty`    依赖的第三方库

## 交流

- 遇到问题：欢迎查看网易云信提供的更详细的[API文档](https://dev.yunxin.163.com/docs/interface/%E5%8D%B3%E6%97%B6%E9%80%9A%E8%AE%AFWindows%E7%AB%AF/NIMSDKAPI_CSharp/index.html)，网易云信的客户也可以通过技术支持更快的找到我们。
- 提交缺陷：在确保使用最新版本依然存在问题时请尽量以简洁的语言描述清楚复现该问题的步骤并提交 issue。网易云信的客户也可以通过技术支持更快的将问题反馈给我们。
- `关于更新`：C# SDK将随[网易云信WindowsPC SDK](https://netease.im/im-sdk-demo)发生重大更新后同步更新，开发者如需更新日常版本，可以参考[网易云信WindowsPC IM SDK 版本历史](https://dev.yunxin.163.com/docs/product/IM%E5%8D%B3%E6%97%B6%E9%80%9A%E8%AE%AF/%E6%9B%B4%E6%96%B0%E6%97%A5%E5%BF%97/Windows%E7%AB%AF%E6%9B%B4%E6%96%B0%E6%97%A5%E5%BF%97)和[云信WindowsPC 实时音 SDK 版本历史](https://dev.yunxin.163.com/docs/product/%E9%9F%B3%E8%A7%86%E9%A2%91%E9%80%9A%E8%AF%9D/%E6%9B%B4%E6%96%B0%E6%97%A5%E5%BF%97/Windows%E7%AB%AF%E6%9B%B4%E6%96%B0%E6%97%A5%E5%BF%97)自行修改和更新C# SDK代码。

## TODO

- 