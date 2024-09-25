简体中文|[English](./README.md) 

# Simple AI Agent

## 基于C# Semantic Kernel 与 WPF构建的一款AI Agent探索应用✨

SimpleAIAgent是基于C# Semantic Kernel 与 WPF构建的一款AI Agent探索应用。主要用于使用国产大语言模型或开源大语言模型构建AI Agent应用的探索学习，希望能够帮助到感兴趣的朋友。

接下来我想分享一下我的AI Agent应用实践。

## 翻译文本并将文本存入文件

第一个例子是翻译文本，并将文本存入指定的文件。

输入如下内容：

![image-20240925113714519](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240925113714519.png)

**执行过程**

第一步，LLM判断应该调用的函数与参数如下：

![image-20240925113837225](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240925113837225.png)

第二步，LLM帮我们调用这个函数，并返回结果：

![image-20240925113939862](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240925113939862.png)

第三步，LLM再次判断需要调用的函数与参数：

![image-20240925114202861](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240925114202861.png)

第四步，LLM调用这个函数，并返回函数返回值：

![image-20240925114250823](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240925114250823.png)

第五步，LLM判断任务已经完成，调用结束函数：

![image-20240925114350284](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240925114350284.png)

第六步，返回最终的回应：

![image-20240925114503461](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240925114503461.png)

**查看结果**

![image-20240925114554332](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240925114554332.png)

会发现桌面多了一个文件，打开如下所示：

![image-20240925114623548](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240925114623548.png)

以上AI Agent应用使用glm-4-flash即可实现，当然也可以尝试其他模型，模型越强，成功概率越高。

## 实现文件到文件的翻译

输入：

![image-20240925114853823](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240925114853823.png)

文件1.txt的内容如下：

![image-20240925115006964](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240925115006964.png)

是一段关于WPF的中文描述，现在我想让LLM帮我翻译成英文之后再保存到另一个文件。

同样还是使用免费的glm-4-flash

**执行过程**

第一步，LLM判断应该调用的函数与参数如下：

![image-20240925115631597](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240925115631597.png)

第二步，LLM帮我们调用这个函数，并返回结果：

![image-20240925120033177](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240925120033177.png)

第三步，LLM判断任务已经完成，调用结束函数：

![image-20240925115856804](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240925115856804.png)

第四步，返回最终的回应：

![image-20240925115922792](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240925115922792.png)

**查看结果**

![image-20240925120115600](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240925120115600.png)

![image-20240925120135716](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240925120135716.png)

## 实现要点

大家可能会注意到实现的要点其实就是要让LLM自动调用函数，也就是实现自动函数调用的功能。

之后要做的就是根据你想让LLM自动做的事去写插件，然后导入这个插件罢了。

插件中函数最好不要太多，太多模型能力弱的就会乱调用。根据你的需求，实现不同人物导入不同的插件比较好。

插件可以这样写，以上面的翻译插件为例：

```csharp
#pragma warning disable SKEXP0050
    internal class TranslationFunctions
    {
        private readonly Kernel _kernel;
        public TranslationFunctions()
        {
            var handler = new OpenAIHttpClientHandler();
            var builder = Kernel.CreateBuilder()
            .AddOpenAIChatCompletion(
               modelId: ChatAIOption.ChatModel,
               apiKey: ChatAIOption.Key,
               httpClient: new HttpClient(handler));
            _kernel = builder.Build();
        }
        [KernelFunction, Description("选择用户想要的语言翻译文本")]
        public async Task<string> TranslateText(
            [Description("要翻译的文本")] string text,
            [Description("要翻译成的语言，从'中文'、'英文'中选一个")] string language
 )
        {
            string skPrompt = """
                            {{$input}}

                            将上面的文本翻译成{{$language}}，无需任何其他内容
                            """;
            var result = await _kernel.InvokePromptAsync(skPrompt, new() { ["input"] = text, ["language"] = language });
            var str = result.ToString();
            return str;
        }

        [KernelFunction, Description("实现文件到文件的翻译")]
        public async Task<string> TranslateTextFileToFile(
           [Description("要翻译的文件路径")] string path1,
           [Description("保存翻译结果的文件路径")] string path2,
           [Description("要翻译成的语言，从'中文'、'英文'中选一个")] string language
)
        {
            string fileContent = File.ReadAllText(path1);
            var lines = TextChunker.SplitPlainTextLines(fileContent,100);
            var paragraphs = TextChunker.SplitPlainTextParagraphs(lines, 1000);
            string result = "";
            string skPrompt = """
                            {{$input}}

                            将上面的文本翻译成{{$language}}，无需任何其他内容
                            """;
            foreach (var paragraph in paragraphs)
            {
                var result1 = await _kernel.InvokePromptAsync(skPrompt, new() { ["input"] = paragraph, ["language"] = language });
                result += result1.ToString() + "\r\n";
            }        
           
            var str = result.ToString();

            // 使用 StreamWriter 将文本写入文件
            using (StreamWriter writer = new StreamWriter(path2, true))
            {
                writer.WriteLine(str);
            }

            string message = $"已成功实现文件{path1}到文件{path2}的翻译";
            return message;
        }

        [KernelFunction, Description("将文本保存到文件")]
        public string SaveTextToFile(
           [Description("要保存的文本")] string text,
           [Description("要保存到的文件路径")] string filePath
)
        {
            // 使用 StreamWriter 将文本写入文件
            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine(text);
            }
            return "已成功写入文件";
        }

        [KernelFunction, Description("从文件中读取文本")]
        public string GetTextFromFile(
           [Description("要读取的文件路径")] string filePath
)
        {
            string fileContent = File.ReadAllText(filePath);
            return fileContent;
        }

    }
```

就是加上了一些描述用于帮助LLM理解函数的用途罢了，相信对程序员朋友来说不是什么问题，现在就可以动手构建自己的AI Agent应用了。

希望这次的分享对使用LLM构建AI Agent应用感兴趣的朋友有所帮助。

对这个应用感兴趣的朋友，拉一下代码，将appsettings.example.json改为appsettings.json，填入你的API Key与模型名或者使用Ollma填入地址，填入模型名即可快速体验。

GitHub地址：https://github.com/Ming-jiayou/SimpleAIAgent

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

Function Calling正常，现在就可以开始使用Simple AI Agent啦！！
