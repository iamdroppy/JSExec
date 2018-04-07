# JSExec

JSExec is a runtime C# interactor with JavaScript. It makes use of DynamicObjects to dynamically create objects.

## Installation

I haven't created Nuget a nuget package yet, but you can simply download and compile.

The usage itself isn't only for JavaScript, but I have made it for JavaScript (to help web automation tools, like Selenium).

Our Tests/Example projects have Selenium WebDriver ( https://www.seleniumhq.org/ ) and you can install it through Nuget:

```Install-Package Selenium.WebDriver -Version 3.11.1```

## Usage

Please, check this example: https://github.com/iamdroppy/JSExec/blob/master/JSExec.Example/Program.cs
It contains most information you'll need to write the code in C# that binds to JavaScript.

## Limitations

### Binary Operations
For now, binary operations are limited to Add/Subtract/Multiply/Divide, and it retrieves the data and store locally (including the result), and it may be better to store and make them in Javascript (and return the data to c#).
As of logical operations, you must use ExecuteReturn() to return the data, then cast the data to a boolean, so you can use.
I plan on moving all the binary operations to JavaScript, so I must analyze the impact.

### Unary Operations
It must be developed.

### MethodExecutor
For now, we use class MethodExecutor, so when you're calling a method, it'll try, through reflection, find a method, and invoke. It's unsafe for now.

The reason I use a MethodExecutor instead of returning every function is because we don't want to get the data every time it's executed.

For instance, if I use in JS:

```return getBigData```

considering getBigData would be a huge amount of data, it would break selenium and wouldn't return anything, so I gave more freedom to the developer to choose what they want to return to C#, and what they want to literally write to JS. I may rewrite it later if I find any workaround.

What happens if you write:
```js.name = "Lucca";
js.alert(js.name).Execute();
```

It will set name as Lucca, and then call alert. Since the "name" variable is defined in javascript, it will pass to alert as follow:
```alert(name);```

Now if you use 
```js.name = "Lucca";
js.alert(js.name.ExecuteResult()).Execute();
```

It will execute:
```alert("Lucca");```
