// See https://aka.ms/new-console-template for more information
using ConsoleApp;

            
var apiURL = "https://localhost:7222/api";

await (new TaskWhenAll(apiURL)).DemoTaskWhenAllAsync();

Console.ReadLine();


