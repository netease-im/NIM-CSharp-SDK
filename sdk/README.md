# Windows(PC) SDK 开发手册(C\# 封装层)

## <span id="概述">概述</span>

Windows(PC) SDK对外暴露的是C接口，为了让桌面开发者更加方便快捷的接入SDK，我们基于C接口封装了C\# SDK，可以让开发者方便直观的调用接口使用云信的服务。

## <span id="开发准备">开发准备</span>

- SDK包含对C接口的封装，Demo提供了简单的接口使用实例。 C\# 应用程序在运行时依赖于C SDK提供的Dll
(nim.dll, nim\_tools\_http.dll是必须的，其他按需).
- 在编译解决方案之前需要通过运行$(SolutionDir\config\copy_win32_native_dlls.bat将这些Dll文件拷贝到Demo文件夹Dependents\dllimport。

### <span id="SDK内容">SDK内容</span>

#### <span id="C\#程序集">C\#程序集</span>

* Src/Core(NIMCore): NIM C\# SDK功能亮点模块的源代码文件及工程文件，生成NimCore.dll。
* Src/Audio(NIMAudio): NIM C\# Audio模块的源代码文件及工程文件，负责语音录制和播放，非SDK功能模块，可作为APP开发者的辅助模块使用，生成NimAudio.dll。
* Src/Http(NIMHttp): NIM C\# Http模块的源代码文件及工程文件，负责通用的http传输，非SDK功能模块，可作为APP开发者的辅助模块使用,生成NimHttp.dll。
* Src/ChatRoom(NIMChatRoom): NIM C\# 聊天室模块的源代码文件及工程文件，负责聊天室功能，生成NIMChatRoom.dll
* Src/Utility(NIMUtility): NIM C\# 工具库，为其他程序集提供支持，生成NimUtility.dll
* ThirdParty：SDK 依赖的第三方程序集。
* libs/win32：在运行时依赖的 Native DLL(x86&x64)。

#### <span id="依赖的Native DLL">依赖的Native DLL</span>

* nim.dll： SDK功能亮点模块；放在用户程序目录下
* nim\_audio.dll： 负责语音录制和播放；放在用户程序目录下
* nim\_tools\_http.dll： 负责通用的http传输；nim.dll模块使用了此功能，用户自己的程序也可以使用；放在用户程序目录下
* nrtc.dll： 负责视频聊天功能；放在用户程序目录下
* nrtc\_audio\_process.dll： 负责视频聊天功能；放在用户程序目录下
* nim\_audio\_hook.dll： 负责语音hook功能；放在用户程序目录下（x64没有）
* nim\_chatroom.dll： 负责聊天室功能；放在用户程序目录下
* nim\_conf： SDK版本相关；放在用户程序目录下 
* msvcp120.dll： SDK依赖的运行时库；放在用户程序目录下
* msvcr120.dll： SDK依赖的运行时库；放在用户程序目录下

### <span id="SDK C\#封装层类讲解">SDK C\#封装层类讲解</span>

SDK C\#封装层代码在Src/Core子目录下，主要封装了以下核心类（XXXAPI.cs主要封装该模块的API，XXXDef.cs主要封装该模块的数据结构定义，有些模块拆分了几个独立的文件来定义）：

* NIMClientAPI.cs + NIMClientDef.cs + NIMSDKConfig.cs: 全局管理功能：主要包括SDK初始化/清理、客户端登录/注销/重连/掉线/多点登录/把其他端踢下线等功能
* NIMGlobalAPI.cs + NIMGlobalDef.cs: 全局接口：提供SDK分配的内存的释放的相关接口
* NIMDataSyncAPI.cs + NIMDataSyncDef.cs: 数据同步接口：提供注册监听数据同步结果的接口 
* NIMFriendAPI.cs + NIMFriendDef.cs: 好友功能：主要包括添加、删除好友、通过验证，拒绝好友请求、获取好友列表等功能 
* NIMUserAPI.cs + NIMUserDef.cs: 用户信息托管：提供基础的用户信息托管服务；用户特殊关系管理功能：主要包括设置对方消息静音和取消静音，把对方加入黑名单和从黑名单移除
* NIMTeamAPI.cs + NIMTeamDef.cs等: 群功能：主要包括查询群信息、查询群成员信息、加人、踢人、转移群主、设置管理员、接受入群邀请等功能
* NIMTalkAPI.cs + NIMMessage.cs等: 聊天功能：主要包括发送消息、接受消息等功能
* NIMVChatAPI.cs + NIMVChatDef.cs: 音视频功能：主要包括初始化/清理、发起会话、设置通话模式、会话控制、结束会话等功能
* NIMDeviceAPI.cs + NIMDeviceDef.cs: 音视频设备相关功能：设备相关接口，及音视频数据相关的接口及回调，依赖VChat中的初始化/清理* 
* NIMMsgLogAPI.cs + NIMMsgLogDef.cs: 消息历史功能（不包含系统消息）：主要包括查询消息，设置消息读取状态，删除消息和导出消息到本地等功能。
* NIMSysMsgAPI.cs + NIMSysMsgDef.cs: 系统消息和自定义通知功能：主要包括注册接收系统消息、发送自定义通知，删除查询系统消息等功能
* NIMSessionAPI.cs + NIMSessionDef.cs: 会话列表管理功能：主要包括查询会话列表，删除会话列表等功能
* NIMNosAPI.cs + NIMNosDef.cs: NOS云服务功能：主要包括资源文件的上传和下载功能，支持断点续传。
* NIMRtsAPI.cs + NIMRtsDef.cs:	RTS会话功能：主要包括发起RTS会话，接收会话请求，监听会话，会话控制和结束会话等功能。
* NIMToolsAPI.cs + NIMToolsDef.cs: 提供的一些工具接口，主要包括获取SDK里app account对应的app data目录、计算md5、语音转文字等
* NIMResponseCode.cs: NIM SDK通用错误码定义。

此外，还包含一些工具类，如JsonParser.cs提供了JSON系列化相关的辅助方法。

### <span id="Audio C\#封装层类讲解">Audio C\#封装层类讲解</span>

代码位于Src/Audio子目录下，详见NIMAudioAPI.cs和NIMAudioDef.cs。

### <span id="HTTP C\#封装层类讲解">HTTP C\#封装层类讲解</span>

代码位于Src/Http子目录下，详见NIMHttpAPI.cs和NIMHttpDef.cs。

## <span id="接入SDK C\#封装层">接入SDK C\#封装层</span>

### <span id="添加SDK C\#封装层">添加SDK C\#封装层</span>

- 开发者自行编译NIM C\# SDK，并将生产的C\# DLL文件添加到自己项目的“引用”里，即可方便地调用NIM C\# SDK的接口。
- 开发者自行编译NIM C\# SDK生成的DLL文件及其依赖的所有Native DLL文件必须放在用户程序（以NIM Demo程序为例）生成的可执行程序目录下，
确保可以成功加载依赖的DLL。
- NIM Demo工程里引用的NIM模块（即NIM C\# SDK DLL）需要确保路径正确，如果提示出错，需要用户自己重新添加该模块的引用！
- 开发者需要在条件编译符号里添加NIMAPI_UNDER_WIN_DESKTOP_ONLY宏定义，以保障引用到正确平台的接口。

### <span id="添加所需的第三方库">添加所需的第三方库</span>

SDK C\#项目文件依赖第三方库Newtonsoft.Json，这是一款.NET中开源的Json序列化和反序列化类库([下载地址](https://github.com/JamesNK/Newtonsoft.Json "target=_blank"))。我们随包提供了Newtonsoft.Json.dll(开发者也可以自行下载相应的DLL或使用源代码进行编译)。

当然，开发者也可以使用自己熟悉的json有关的第三方库，如果使用其他第三方库，需要手工调整C\# SDK源码中引用到Newtonsoft.Json相关的代码。