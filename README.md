# Mutation Testing with Stryker.NET

## Introduction

According to Wikipedia

> Mutation testing involves modifying a program in small ways.[1] Each mutated version is called a mutant and tests detect and reject mutants by causing the behaviour of the original version to differ from the mutant.
> (Source: [Wikipedia](https://en.wikipedia.org/wiki/Mutation_testing))

In a way this has similarities to _Fuzz testing_ which does not alter the code under test but the parameters used during execution.

> [...] Fuzz testing is an automated software testing technique that involves providing invalid, unexpected, or random data as inputs to a computer program.
> (Source: [Wikipedia](https://en.wikipedia.org/wiki/Fuzzing))

Enter Stryker.NET

> Stryker.NET offers you mutation testing for your .NET Core and .NET Framework projects. It allows you to test your tests by temporarily inserting bugs. Stryker.NET is installed using NuGet.

## Running Tests

The demo project contains a single class `ShoppingCart` implementing the basic functionalities of a shopping cart.

To run all _basic_ tests from the console navigate to the test project `tests/StrykerNetDemo.Tests` folder and call 

```sh
dotnet test --filter "Category=basic"
```

These tests should provide a 100% code coverage. Next, install Stryker.NET by calling  

```sh
dotnet tool restore
```

inside the test project folder. As an alternative Stryker.NET can also be installed as a global too. Consult the [getting started guide](https://stryker-mutator.io/docs/stryker-net/getting-started/) for more information.

Stryker.NET can now be started by calling

```sh
dotnet stryker -o
```

Once finished the HTML report will open in a browser window. The overall mutation score will be way below 100%, meaning that certain mutations survived and were not detected/killed by any unit test.

Next, modify `stryker-config.json` of the test project and remove the line containing the `test-case-filter` expression. This will include additional unit tests, resulting in a higher mutation score. Rerun Stryker.NET with the same command as before.

## Considerations

### Runtime

A larger code base results in a higher number of generated mutations, affecting the execution time of Stryker.NET.

```
Processor: Intel Core i7-4470K CPU 4.00GHz (Haswell)
ProcessorCount: 8
LinesOfExecutableCodeCount: 712
TestCount: 274
MutationCount: 881

dotnet build --no-restore --no-incremental
~2.5 sec

dotnet test --no-restore
~2.0 sec

dotnet stryker
~16 min
```

### Coding Style

Some mutations are difficult to fix if you are used or bound to a specific coding style. 

In the following example Stryker.NET will create a mutation that removes the `return` statement. This will cause the remaining code to be executed. Even if it is guarded against side effects, the mutation will still survive.

```csharp
public void DoSomething()
{
    if (!condition)
        return;

    ...
}
```

One possible solution is to execute the remaining code in the `else` branch of the `if` statement.

```csharp
public void DoSomething()
{
    if (!condition)
    {
        return;
    }
    else
    {
        ...
    }
}

```

## References

- [Stryker.NET](https://stryker-mutator.io/docs/stryker-net/introduction/)
- [Mutation Testing (Wikipedia)](https://en.wikipedia.org/wiki/Mutation_testing)
- [Fuzz Testing (Wikipedia)](https://en.wikipedia.org/wiki/Fuzzing)