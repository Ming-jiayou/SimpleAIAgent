[简体中文](./README.zh.md) | English

# SimpleTranslationAIAgent

## An AI Agent exploration application based on C# Semantic Kernel and WPF ✨

SimpleAIAgent is an AI Agent exploration application built on C# Semantic Kernel and WPF. It is primarily used for exploring and learning how to build AI Agent applications using domestic large language models or open-source large language models, with the hope of assisting interested friends.

Next, I would like to share my practical experience in developing AI Agent applications.

## Translate Text and Save It to a File

The first example is translating text and saving it to a specified file.

Enter the following content:

![image-20240925113714519](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240925113714519.png)

**Execution Process**

Step 1: The LLM determines the function and parameters to be called as follows:

![image-20240925113837225](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240925113837225.png)

Step 2: The LLM assists us in calling this function and returns the result:

![image-20240925113939862](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240925113939862.png)

Step 3: The LLM re-evaluates the functions and parameters that need to be invoked:

![image-20240925114202861](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240925114202861.png)

Step 4: The LLM calls this function and returns the function's return value.

![image-20240925114250823](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240925114250823.png)

Step 5: The LLM determines that the task is complete and calls the end function:

![image-20240925114350284](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240925114350284.png)

Step 6: Return the final response:

![image-20240925114503461](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240925114503461.png)

**View Results**

![image-20240925114554332](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240925114554332.png)

You will find that a new file has appeared on the desktop, opened as follows:

![image-20240925114623548](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240925114623548.png)

The above AI Agent application can be implemented using glm-4-flash, although other models can also be attempted. The stronger the model, the higher the probability of success.

## Implementing Translation from File to File  

Input:  

![image-20240925114853823](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240925114853823.png)

The content of file1.txt is as follows:

![image-20240925115006964](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240925115006964.png)

is a description about WPF in Chinese, and now I want the LLM to help me translate it into English and then save it to another file.

Still using the free glm-4-flash.

**Execution Process**

Step 1: The LLM determines the function to be called and its parameters as follows:

![image-20240925115631597](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240925115631597.png)

Step 2: Have the LLM call this function for us and return the result:

![image-20240925120033177](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240925120033177.png)

Step 3: The LLM determines that the task is complete and calls the end function.

![image-20240925115856804](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240925115856804.png)

Step 4: Return the final response:

![image-20240925115922792](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240925115922792.png)

**View Results**

![image-20240925120115600](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240925120115600.png)

![image-20240925120135716](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240925120135716.png)

## Key Points for Implementation

You may notice that the key to implementation is to enable the LLM to automatically call functions, which is essentially achieving automatic function calling.

After that, the task is to write plugins based on what you want the LLM to automate, and then import these plugins accordingly.

It's best to keep the number of functions in the plugin minimal, as too many functions can lead to confusion and incorrect calls for models with weaker capabilities. Depending on your specific needs, it's advisable to implement different characters with different plugins.

Plugins can be written as follows, using the translation plugin as an example:

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

It's just adding some descriptions to help the LLM understand the purpose of the function, which shouldn't be a problem for programmers. Now you can start building your own AI Agent application.

I hope this share is helpful for those interested in building AI Agent applications using LLM.

For those interested in this application, pull the code, rename `appsettings.example.json` to `appsettings.json`, and fill in your API Key and model name, or use Ollma to fill in the address and model name for a quick experience.

GitHub link: https://github.com/Ming-jiayou/SimpleAIAgent

## Quick Start🚀

GitHub Address: https://github.com/Ming-jiayou/SimpleTranslationAIAgent

Noticed there is a 'Releases' here:

![image-20240831114112502](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240831114112502.png)

Click, there are two zip files: 

![](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240831114112502.png)

One depends on the .NET 8.0-windows framework, and the other is standalone. 

If you have installed the .NET 8.0-windows framework, you can choose the smaller one. I have already installed the .NET 8.0-windows framework, so I selected the smaller one, clicked to start the download, and after downloading, extract it as shown:

![image-20240831114515700](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240831114515700.png)

Now, just open appsettings and enter your API KEY to use it, very simple!! 

Open the appsettings.json file as follows: 

![image-20240831114609377](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240831114609377.png)

After entering the information, click on SimpleTranslationAIAgent.exe to run: 

![](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240831114609377.png)

Test whether the configuration is successful: 

![](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240831114609377.png)

The configuration has been successful, Test if the Function Calling is normal: 

![image-20240831114920726](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240831114920726.png)

Function Calling is normal, now you can start using the Translation AI Agent!!

## Source Code Build Guide🚀
After cloning the repository locally with .git clone, follow the steps as shown below:

![image-20240830160422435](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240830160422435.png)

Open the appsettings.example.json file as follows:

![](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240830160422435.png)

Zhipu AI's glm-4-flash is now free. Taking this LLM as an example, after entering the API KEY, rename this file to appsettings.json or create a new appsettings.json and copy the content into it:

![](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240830160422435.png)

IDE: vs2022
.NET version: .NET 8
Open the solution:

![image-20240830162920242](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240830162920242.png)

Upon running, an error occurs:

![](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240830162920242.png)

Right-click on the sppsettings.json file, click on properties, and change it to an embedded resource:

![](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240830162920242.png)

Run again and verify the configuration is successful through dialogue:

![](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240830162920242.png)

The configuration is successful.
Test if Function Calling is working properly:

![image-20240830164818771](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240830164818771.png)

Function Calling is normal, and now you can start using the Translation AI Agent!

