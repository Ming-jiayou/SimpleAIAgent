# SimpleTranslationAIAgent

## 基于C#与LLM通过简单对话即可实现文件到文件的翻译任务

该软件是MIT协议完全开源免费的，但是调用LLM的API可能需要费用，但是没关系，赛博菩萨硅基流动与智谱AI等都有免费的模型可调了。

这个Translation AI Agent只是一个简单的AI Agent示例应用，可能很多人都不需要它。

开源出来主要是为了感兴趣的同学可以在看源码之后，也可以使用C#+LLM构建出更有意思更能提高自己工作效率的自己专属的AI Agent应用！！

可以根据自己构建的AI Agent应用的复杂度，选择合适的模型。当应用比较简单时，可能免费的模型就可以了，但是当应用比较复杂时，可能需要更强的模型才行了。现在各大平台几乎都有送一些token体验，可以先拿这些token试一试。

现在glm-4-flash免费了，经过测试可以完成一些简单的AI Agent任务。

首先来一个简单的任务，将内容翻译完之后，自动写入一个文件：

![image-20240830164931643](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240830164931643.png)

![image-20240830165003575](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240830165003575.png)

创建了这个文件，并将内容写入了：

![image-20240901115707800](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240901115707800.png)

整个过程录屏看看：

![](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/TranslationAIAgent1.gif)

md文件也是可以的：

![image-20240830165653037](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240830165653037.png)

![image-20240830165717751](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240830165717751.png)

录屏看下整个过程：

![](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/TranslationAIAgent2.gif)

现在尝试一下更难的任务，将一个文件里的文本取出来翻译之后写入另一个文本。

比如我有一个test1.txt文件，如下所示：

![image-20240830170813739](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240830170813739.png)

我想要让Translation AI Agent 帮我翻译成中文，然后存入另一个文件中，如果不存在这个文件就新建一个文件，就可以这么写，只要提供文件路径即可：

![image-20240830171542144](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240830171542144.png)

失败了：

![image-20240830172736359](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240830172736359.png)

换成更强的glm-4模型试试：

![image-20240830172933040](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240830172933040.png)

成功完成这个任务了：

![image-20240830173048479](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240830173048479.png)

现在试一下将这个文件：

![image-20240830180636766](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240830180636766.png)

翻译成英文之后写入另一个文件：

![image-20240830174100940](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240830174100940.png)

查看效果：

![image-20240830174157728](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240830174157728.png)

自动省略了...

可以调试看看这个过程。

第一步先获取文件的内容：

![image-20240830175134230](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240830175134230.png)

成功获取到文件内容：

![image-20240830175207798](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240830175207798.png)

第二步出错了：

![image-20240830175246128](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240830175246128.png)

我该用硅基流动提供的Qwen/Qwen2-72B-Instruct再试试：

![image-20240830175603881](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240830175603881.png)

现在没错了。

成功获取翻译结果：

![image-20240830175648519](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240830175648519.png)

第三步，将翻译之后的结果写入文件：

![image-20240830175745941](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240830175745941.png)

已成功写入：

![image-20240830175809502](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240830175809502.png)

第四步，返回完成信息：

![image-20240830175845567](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240830175845567.png)

![image-20240830175910947](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240830175910947.png)

查看效果：

![image-20240830180006034](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240830180006034.png)

如果一个模型返回出错，可以重试，重试不行就可以换个模型试试了，越强的模型，成功的几率越高。

## 快速开始🚀

GitHub地址：https://github.com/Ming-jiayou/SimpleTranslationAIAgent

注意到这里有个Releases：

![image-20240831114112502](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240831114112502.png)

点击，有两个压缩包：

![image-20240831114150976](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240831114150976.png)

一个依赖.net8.0-windows框架，一个独立。

安装了.net8.0-windows框架的就可以选体积小的那个，我已经安装了.net8.0-windows框架就选择体积小的那一个，点击就在下载了，下载之后解压缩，如下所示：

![image-20240831114515700](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240831114515700.png)

现在只要打开appsettings填入你的API KEY即可使用，非常简单！！

打开appsettings.json文件如下所示：

![image-20240831114609377](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240831114609377.png)

填入之后，点击SimpleTranslationAIAgent.exe即可运行：

![image-20240831114749577](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240831114749577.png)

测试是否配置成功：

![](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240831114836888.png)

配置已经成功，测试Function Calling是否正常：

![image-20240831114920726](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240831114920726.png)

Function Calling正常，现在就可以开始使用Translation AI Agent啦！！

## 源码构建指南🚀

git clone到本地后，如下所示：

![image-20240830160422435](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240830160422435.png)

打开appsettings.example.json文件，如下所示：

![image-20240830160550389](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240830160550389.png)

智谱AI glm-4-flash免费了，以这个LLM为例，填入API KEY之后，将该文件名字改为appsettings.json或者新建一个appsettings.json，将文件内容复制进去即可：

![image-20240830162839622](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240830162839622.png)

IDE：vs2022

.net版本：.net 8

打开解决方案：

![image-20240830162920242](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240830162920242.png)

运行报错：

![image-20240830163038688](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240830163038688.png)

右键sppsettings.json文件，点击属性，改为嵌入的资源：

![image-20240830163211284](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240830163211284.png)

再次运行，通过对话验证是否配置成功：

![image-20240830164734788](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240830164734788.png)

配置已经成功，测试Function Calling是否正常：

![image-20240830164818771](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240830164818771.png)

Function Calling正常，现在就可以开始使用Translation AI Agent啦！！
