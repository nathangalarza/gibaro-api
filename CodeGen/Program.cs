using System.Globalization;
using System.Reflection;
using Entities.Models;
using System.Linq;
using System.Collections.Immutable;
using System.Reflection.Metadata.Ecma335;
using System;
using CodeGen.Tools;
using CodeGen.Helpers;

User AssembyRefference = new User();
CodeGenerator obj = new CodeGenerator();

var task = obj.Prompt();
var task1 = obj.GenerateFiles();
var task2 = obj.GenerateDTOs();
var task3 = obj.UpdateFiles();

await Task.WhenAll(task, task1,task2, task3);

