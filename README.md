# AegisubHelper
Aegisub辅助听译

## 提示
本程序使用了内存读取功能，需要给予管理员权限，可能会有杀软报错.
使用到了[memory.dll](https://github.com/erfg12/memory.dll)第三方内存读写模块，安全性未知

内存读取用于获取播放状态，以此来自动录取音频片段并请求网络接口

## 使用到的网络接口
- [有道翻译-短音频识别](https://ai.youdao.com/product-asr.s) (需个人认证，赠送50元，每次调用0.05元)
- [百度翻译-通用翻译](https://fanyi-api.baidu.com/product/11) (需个人认证，每月200万字符免费)

请自行通过审核并获取对应的`APPId`与`APPKey`，并在右上角的设置中填写

## 使用流程
- 打开Aegisub
- 打开程序
- 假如下方提示`内存读取成功`，则可使用`自动捕获模式`
- 按`Ctrl+F9`来切换自动捕获模式
- 在自动捕获模式下，程序会自动录取Aegisub播放的音频，并请求网络接口来获取听译结果与翻译

## 结果示例
![image](https://user-images.githubusercontent.com/50934714/171083491-74ce7c1a-e830-442c-bc22-f49f0e8fbdab.png)
