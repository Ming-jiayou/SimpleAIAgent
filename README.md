[简体中文](./README.zh.md) | English

# SimpleTranslationAIAgent

## Based on C# and LLM, a file-to-file translation task can be achieved through simple dialogue.✨

This software is completely open-source and free under the MIT license. However, calling the LLM API may incur costs. But don't worry, there are now free models available for use, such as those from SiliconCloud, and Zhipu AI.

This Translation AI Agent is just a simple example application of an AI Agent, and many people may not need it. The main reason for open-sourcing it is to allow those who are interested to study the source code and use C# plus LLM to build their own unique AI Agent applications that are more interesting and can improve their own work efficiency!!

You can choose an appropriate model based on the complexity of your self-built AI Agent application. For simpler applications, free models may suffice, but for more complex ones, you might require a stronger model. Nowadays, almost all platforms offer some tokens for free trial, so you can give them a try first.

Now GLM-4-Flash is free, and after testing, it can complete some simple AI Agent tasks.

First, a simple task: after translating the content, automatically write it into a file:

![image-20240830164931643](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240830164931643.png)

Created this file and wrote the content into it:

![image-20240901115707800](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240901115707800.png)

Check the entire recording of the screen:

![](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/TranslationAIAgent1.gif)

MD files are also acceptable:

![image-20240830165653037](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240830165653037.png)

![image-20240830165717751](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240830165717751.png)

Capture the entire process on screen:

![](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/TranslationAIAgent2.gif)

For instance, I have a file named test1.txt as follows:

![image-20240830170813739](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240830170813739.png)

Translate the text above into English and save it to another file. If the file does not exist, create a new one.

![image-20240830171542144](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240830171542144.png)

The task has been successfully completed:

![image-20240830173048479](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240830173048479.png)

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

