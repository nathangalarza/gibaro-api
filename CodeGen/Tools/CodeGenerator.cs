using CodeGen.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CodeGen.Tools
{
    public class CodeGenerator
    {
        public CodeGenerator()
        {

        }
        public string? model { get; set; }

        public string? filePath = VisualStudioProvider.TryGetSolutionDirectoryInfo().FullName;
        public string? titleCaseModel { get; set; }
        public async Task<int> Prompt()
        {
            var modelClasses = AppDomain.CurrentDomain.GetAssemblies()
                                   .SelectMany(t => t.GetTypes())
                                   .Where(t => t.IsClass && t.Namespace == "Entities.Models");

            Console.WriteLine("Enter Model Name snakeCase");
            model = Console.ReadLine();
            titleCaseModel = ToTitleCase(model);
            var result = modelClasses.FirstOrDefault(x => x.Name == titleCaseModel);

            if (result == null)
            {
                throw new Exception("No modesl found with that name");
            }


            return 1;
        }
        public async Task<int> UpdateFiles()
        {
            string contents = File.ReadAllText($"{filePath}\\Service\\ServiceManager.cs");
            var split = contents.Split("\r\n");
            var list = new List<string>();
            bool[] flags = { false, false, false };

            foreach (var line in split)
            {

                if (line.Contains("//{{Lazy}}//") && flags[0] != true)
                {
                    string linetoAdd = $"         private readonly Lazy<I{titleCaseModel}Service> _{model}Service;\t\n";
                    if (contents.Contains(linetoAdd.Trim()))
                    {
                        list.Add(line);

                        continue;
                    }
                    flags[0] = true;
                    list.Add(linetoAdd);
                }
                if (line.Contains("//{{constructor}}//") && flags[1] != true)
                {
                    string linetoAdd = $"            _{model}Service = new Lazy<I{titleCaseModel}Service>(() => new\r\n                {titleCaseModel}Service(mapper, repositoryManager));             \r\n";
                    if (contents.Contains(linetoAdd.Trim()))
                    {
                        list.Add(line);

                        continue;
                    }
                    flags[1] = true;
                    list.Add($"            _{model}Service = new Lazy<I{titleCaseModel}Service>(() => new\r\n                {titleCaseModel}Service(mapper, repositoryManager));             \r\n");
                }
                if (line.Contains("//{service}//") && flags[2] != true)
                {
                    string linetoAdd = $"        public I{titleCaseModel}Service {titleCaseModel}Service => _{model}Service.Value;\r\n";
                    if (contents.Contains(linetoAdd.Trim()))
                    {
                        list.Add(line);

                        continue;
                    }
                    flags[2] = true;
                    list.Add(linetoAdd);
                }
                list.Add(line);
            }

            await File.WriteAllLinesAsync(filePath + $"\\Service\\ServiceManager.cs", list);


            contents = File.ReadAllText($"{filePath}\\Repository\\RepositoryManager.cs");
            split = contents.Split("\r\n");
            list = new List<string>();
            flags = new bool[] { false, false, false };

            foreach (var line in split)
            {
                if (line.Contains("//{{Lazy}}//") && flags[0] != true)
                {
                    string linetoAdd = $"        private readonly Lazy<I{titleCaseModel}Repository> _{model}Repository;\r\n";
                    if (contents.Contains(linetoAdd.Trim()))
                    {
                        list.Add(line);

                        continue;
                    }
                    flags[0] = true;
                    list.Add(linetoAdd);
                }
                if (line.Contains("//{{constructor}}//") && flags[1] != true)
                {
                    string linetoAdd = $"            _{model}Repository = new Lazy<I{titleCaseModel}Repository>(() => new\r\n                {titleCaseModel}Repository(repositoryContext));             \r\n";
                    if (contents.Contains(linetoAdd.Trim()))
                    {
                        list.Add(line);

                        continue;
                    }
                    flags[1] = true;
                    list.Add(linetoAdd);
                }
                if (line.Contains("//{service}//") && flags[2] != true)
                {
                    string linetoAdd = $"                public I{titleCaseModel}Repository {titleCaseModel} => _{model}Repository.Value;\r\n";
                    if (contents.Contains(linetoAdd.Trim()))
                    {
                        list.Add(line);

                        continue;
                    }
                    flags[2] = true;
                    list.Add(linetoAdd);
                }
                list.Add(line);


            }
            await File.WriteAllLinesAsync(filePath + $"\\Repository\\RepositoryManager.cs", list);


            contents = File.ReadAllText($"{filePath}\\Gibaro-API\\MappingProfile.cs");
            split = contents.Split("\r\n");
            list = new List<string>();
            flags = new bool[] { false, false };


            foreach (var line in split)
            {
                if (line.Contains(" //{{Mapper}}//") && flags[0] != true)
                {
                    string linetoAdd = $"            CreateMap<{titleCaseModel}, {titleCaseModel}Dto>().ReverseMap();\r\n            CreateMap<{titleCaseModel}, {titleCaseModel}CreationDto>().ReverseMap();\r\n            CreateMap<{titleCaseModel}, {titleCaseModel}UpdateDto>().ReverseMap();";
                    if (contents.Contains(linetoAdd.Trim()))
                    {
                        list.Add(line);
                        continue;
                    }

                    flags[0] = true;
                    list.Add(linetoAdd);
                }
                if (line.Contains("//{{Using}}//") && flags[1] != true)
                {
                    flags[1] = true;
                    string linetoAdd = $"using Shared.DataTransferObjects.{titleCaseModel};";
                    if (contents.Contains(linetoAdd.Trim()))
                    {
                        list.Add(line);
                        continue;
                    }
                    list.Add(linetoAdd);



                }
                list.Add(line);

            }
            await File.WriteAllLinesAsync(filePath + $"\\Gibaro-API\\MappingProfile.cs", list);

            contents = File.ReadAllText($"{filePath}\\Contracts\\IRepositoryManager.cs");
            split = contents.Split("\r\n");
            list = new List<string>();
            flags = new bool[] { false };


            foreach (var line in split)
            {
                if (line.Contains("//{{Interface}}//") && flags[0] != true)
                {
                    string linetoAdd = $"        I{titleCaseModel}Repository {titleCaseModel} {{ get; }}";
                    if (contents.Contains(linetoAdd.Trim()))
                    {
                        list.Add(line);
                        continue;
                    }

                    flags[0] = true;
                    list.Add(linetoAdd);
                }
                list.Add(line);

            }
            await File.WriteAllLinesAsync(filePath + $"\\Contracts\\IRepositoryManager.cs", list);

            contents = File.ReadAllText($"{filePath}\\Service.Contracts\\IServiceManager.cs");
            split = contents.Split("\r\n");
            list = new List<string>();
            flags = new bool[] { false };


            foreach (var line in split)
            {
                if (line.Contains("//{{Interface}}//") && flags[0] != true)
                {
                    string linetoAdd = $"        I{titleCaseModel}Service {titleCaseModel}Service {{ get; }}";
                    if (contents.Contains(linetoAdd.Trim()))
                    {
                        list.Add(line);
                        continue;
                    }

                    flags[0] = true;
                    list.Add(linetoAdd);
                }
                list.Add(line);

            }
            await File.WriteAllLinesAsync(filePath + $"\\Service.Contracts\\IServiceManager.cs", list);

            return 1;
        }
        public async Task<int> GenerateFiles()
        {
            string[] service =
                          {
                    //Imports
                    $"#warning This is generated Code. Please check and update {titleCaseModel} Model in Entities.Models. Once you do the check remove this line. \r\n",
                    "using AutoMapper;\r\nusing Contracts;\r\nusing Microsoft.AspNetCore.Identity;\r\nusing Microsoft.EntityFrameworkCore;\r\nusing Entities.Models;\r\nusing Service.Contracts;\r\nusing Shared.DataTransferObjects.User;\r\nusing Shared.RequestFeatures;" +
                    $"using Shared.DataTransferObjects.{titleCaseModel};\r\n",
                    "namespace Service\r\n{\r\n",
                    "    public class " + titleCaseModel + "Service: I" + titleCaseModel + "Service\r\n    {",
                    "        private readonly IMapper _mapper;\r\n        private readonly IRepositoryManager _repository;",
                    $" public {titleCaseModel}Service(IMapper mapper, IRepositoryManager repository)\r\n        " +
                    $"{{\r\n            " +
                    $"_mapper = mapper;\r\n            " +
                    $"\r\n            " +
                    $"_repository = repository;\r\n        " +
                    $"}}",
                    //GET
                    $"    public async Task<{titleCaseModel}Dto?> Get{titleCaseModel}(Guid id, bool trackChanges)",
                    $"    {{",
                    $"           {titleCaseModel} {model} = await Get{titleCaseModel}ById(id, trackChanges);\r\n            return _mapper.Map<{titleCaseModel}Dto>({model});",
                    $"     }}",
                    $"\r\n",
                    //UPDATE
                    $"        public async Task Update{titleCaseModel}(Guid id, {titleCaseModel}UpdateDto {model}UpdateDto)\r\n",
                    $"        {{",
                    $"            {titleCaseModel} {model} = await Get{titleCaseModel}ById(id, false);\r\n",
                    $"\r\n",
                    $"            _mapper.Map({model}UpdateDto,{model});\r\n",
                    $"            _repository.{titleCaseModel}.Update{titleCaseModel}({model});\r\n",
                    $"            await _repository.SaveAsync();\r\n",
                    $"        }}",
                    $"\r\n",
                    //CREATE
                    $"        public async Task<{titleCaseModel}Dto> Create{titleCaseModel}({titleCaseModel}CreationDto {model}CreationDto)\r\n",
                    $"        {{\r\n",
                    $"            {titleCaseModel} {model} = _mapper.Map<{titleCaseModel}>({model}CreationDto);\r\n",
                    $"            _repository.{titleCaseModel}.Create{titleCaseModel}({model});\r\n",
                    $"            await _repository.SaveAsync();\r\n",
                    $"\r\n",
                    $"            return _mapper.Map<{titleCaseModel}Dto>({model});\r\n",
                    $"        }}\r\n",
                    $"\r\n",
                    //DELETE
                    $"        public async Task Delete{titleCaseModel}(Guid id)\r\n",
                    $"        {{\r\n",
                    $"            {titleCaseModel} {model} = await Get{titleCaseModel}ById(id, false);\r\n",
                    $"            _repository.{titleCaseModel}.Delete{titleCaseModel}({model});\r\n",
                    $"        }}\r\n",
                    $"\r\n",
                    $"",
                    //GET MANY
                    $"        public async Task<(IEnumerable<{titleCaseModel}Dto> {model}s, MetaData metaData)> Get{titleCaseModel}s(RequestParameters requestParameters, bool trackChanges)\r\n",
                    $"        {{\r\n",
                    $"            PagedList<{titleCaseModel}?> {model}s = await _repository.{titleCaseModel}.Get{titleCaseModel}s(requestParameters, trackChanges);\r\n",
                    $"            return ({model}s: _mapper.Map<IEnumerable<{titleCaseModel}Dto>>({model}s), metaData: {model}s.MetaData);\r\n",
                    $"        }}\r\n",
                    $"private async Task<{titleCaseModel}> Get{titleCaseModel}ById(Guid id, bool trackChanges)" +
                    $"\r\n" +
                    $"        {{" +
                    $"\r\n" +
                    $"            {titleCaseModel}? {model} = await _repository.{titleCaseModel}.Get{titleCaseModel}(id, trackChanges);\r\n" +
                    $"\r\n            if ({model} == null)\r\n                throw new Exception(\"{model} not found\");\r\n" +
                    $"\r\n            return {model};" +
                    $"\r\n        }}",
                    $"}}",
                    $"}}",
                    $"",
                    $"",
              };

            await File.WriteAllLinesAsync(filePath + $"\\Service\\{titleCaseModel}Service.cs", service);

            string[] serviceContract =
            {
            $"#warning This is generated Code. Please check and update {titleCaseModel} Model in Entities.Models. Once you do the check remove this line. \r\n",
            $"using Shared.DataTransferObjects.{titleCaseModel};\r\n",
            $"using Shared.RequestFeatures;\r\n",
            $"namespace Service.Contracts\r\n",
            $"{{\r\n",
            $"    public interface I{titleCaseModel}Service\r\n",
            $"    {{\r\n",
            $"        Task<(IEnumerable<{titleCaseModel}Dto> {model}s, MetaData metaData)> Get{titleCaseModel}s(RequestParameters requestParameters, bool trackChanges);\r\n",
            $"        Task<{titleCaseModel}Dto> Get{titleCaseModel}(Guid id, bool trackChanges);\r\n",
            $"        Task<{titleCaseModel}Dto> Create{titleCaseModel}({titleCaseModel}CreationDto {model}CreationDto);\r\n",
            $"        Task Delete{titleCaseModel}(Guid id);\r\n",
            $"        Task Update{titleCaseModel}(Guid id, {titleCaseModel}UpdateDto {model}UpdateDto);\r\n",
            $"    }}\r\n",
            $"}}\r\n",

        };

            await File.WriteAllLinesAsync(filePath + $"\\Service.Contracts\\I{titleCaseModel}Service.cs", serviceContract);
            string[] RepositoryContract =
            {
            $"#warning This is generated Code. Please check and update {titleCaseModel} Model in Entities.Models. Once you do the check remove this line. \r\n",
            $"using Entities.Models;",
            $"using Shared.RequestFeatures;\r\n",
            $"namespace Contracts\r\n",
            $"{{\r\n",
            $"    public interface I{titleCaseModel}Repository\r\n",
            $"    {{\r\n",
            $"        Task<PagedList<{titleCaseModel}?>> Get{titleCaseModel}s(RequestParameters requestParameters, bool trackChanges);\r\n",
            $"        Task<{titleCaseModel}?> Get{titleCaseModel}(Guid id, bool trackChanges);\r\n",
            $"        void Create{titleCaseModel}({titleCaseModel} {model});\r\n",
            $"        void Delete{titleCaseModel}({titleCaseModel} {model});\r\n",
            $"        void Update{titleCaseModel}({titleCaseModel} {model});\r\n",
            $"    }}\r\n",
            $"}}\r\n",

        };

            await File.WriteAllLinesAsync(filePath + $"\\Contracts\\I{titleCaseModel}Repository.cs", RepositoryContract);

            string[] repository =
            {
            $"#warning This is generated Code. Please check and update {titleCaseModel} Model in Entities.Models. Once you do the check remove this line. \r\n",

            //IMPORTS
            $"using Repository.Context;\r\nusing Contracts;\r\nusing Entities;\r\nusing Entities.Models;\r\nusing Microsoft.EntityFrameworkCore;\r\nusing Shared.RequestFeatures;",
            //NAMESPACE
            $"\r\nnamespace Repository\r\n{{",
            $"    internal sealed class {titleCaseModel}Repository : RepositoryBase<{titleCaseModel}>, I{titleCaseModel}Repository\r\n",
            $"    {{\r\n",
            $"        public {titleCaseModel}Repository(RepositoryContext repositoryContext)\r\n",
            $"            : base(repositoryContext)\r\n",
            $"        {{\r\n",
            $"        }}\r\n",
            $"\r\n",
            $"        public void Create{titleCaseModel}({titleCaseModel} {model}) => Create({model});\r\n",
            $"        public void Delete{titleCaseModel}({titleCaseModel} {model}) => Delete({model});\r\n",
            $"        public void Update{titleCaseModel}({titleCaseModel} {model}) => Update({model});\r\n",
            $"        public async Task<{titleCaseModel}?> Get{titleCaseModel}(Guid id, bool trackChanges)\r\n",
            //TODO: ADD LOGIC
            $"        {{\r\n",
            $"            throw new Exception(\"{titleCaseModel} not found\");\r\n",
            $"        }}\r\n",
            $"        public async Task<PagedList<{titleCaseModel}?>> Get{titleCaseModel}s(RequestParameters requestParameters,\r\n",
            $"            bool trackChanges)\r\n",
            $"        {{\r\n",
            $" return PagedList<{titleCaseModel}?>.ToPagedList(null, requestParameters.PageNumber, requestParameters.PageSize);",
            $"        }}\r\n",
            $"    }}\r\n",
            $"}}",

        };

            await File.WriteAllLinesAsync(filePath + $"\\Repository\\{titleCaseModel}Repository.cs", repository);

            return 1;

        }
        public async Task<int> GenerateDTOs()
        {

            var models = AppDomain.CurrentDomain.GetAssemblies()
                                   .SelectMany(t => t.GetTypes())
                                   .Where(t => t.IsClass && t.Namespace == "Entities.Models");

            var result = models.FirstOrDefault(x => x.Name == titleCaseModel);

            var classItems = result.GetRuntimeProperties();

            List<string> itemsInClass = new List<string>();


            foreach (var item in classItems)
            {
                //Nullable`1 == Int?
                var isNullable = item.PropertyType.CustomAttributes.Any(x => x.AttributeType.Name.Contains("Nullable"));
                var isInClass = models.Any(x => x.Name == item.PropertyType.Name);
                itemsInClass.Add($"    public {(isInClass == true ? "Entities.Models." : "")}{(item.PropertyType.Name == "Nullable`1" ? "int?" : item.PropertyType.Name == "String" ? "string" : item.PropertyType.Name)} {(isNullable == true ? "?" : "")} {item.Name} {{ get;set;}}");


            }



            bool exists = Directory.Exists(filePath + "\\Shared\\DataTransferObjects\\" + titleCaseModel);

            if (!exists)
                Directory.CreateDirectory(filePath + "\\Shared\\DataTransferObjects\\" + titleCaseModel);


            List<string> Dto = new List<string>()
        {
           $"#warning This is generated Code. Please check and update {titleCaseModel} Model in Entities.Models. Once you do the check remove this line. \r\n",
           $"using Shared.DataTransferObjects.User;\r\n",
           $"namespace Shared.DataTransferObjects.{titleCaseModel}\r\n",
           $"{{\r\n",
           $"    public record {titleCaseModel}Dto {{\r\n",
           //$"    }}\r\n",
           //$"}}\r\n",

        };

            Dto.AddRange(itemsInClass);
            Dto.Add($"    }}\r\n");
            Dto.Add($"}}\r\n");

            List<string> CreateDto = new List<string>()
        {
               $"#warning This is generated Code. Please check and update {titleCaseModel} Model in Entities.Models. Once you do the check remove this line. \r\n",
              $"",
           $"namespace Shared.DataTransferObjects.{titleCaseModel}\r\n",
           $"{{\r\n",
           $"    public record {titleCaseModel}CreationDto : {titleCaseModel}ManipulationDto{{ \r\n",
                 $"",
               $"    }}\r\n",
               $"}}\r\n",

        };

            List<string> UpdateDto = new List<string>()
        {   $"#warning This is generated Code. Please check and update {titleCaseModel} Model in Entities.Models. Once you do the check remove this line. \r\n",
              $"",
           $"namespace Shared.DataTransferObjects.{titleCaseModel}\r\n",
           $"{{\r\n",
           $"    public record {titleCaseModel}UpdateDto  : {titleCaseModel}ManipulationDto{{\r\n",
                 $"",
               $"    }}\r\n",
               $"}}\r\n",

        };

            List<string> ManipulationDto = new List<string>()
        {
            $"#warning This is generated Code. Please check and update {titleCaseModel} Model in Entities.Models. Once you do the check remove this line. \r\n",
              $"using Shared.DataTransferObjects.User;\r\n",
           $"namespace Shared.DataTransferObjects.{titleCaseModel}\r\n",
           $"{{\r\n",
           $"    public record {titleCaseModel}ManipulationDto {{\r\n",
           $"",

        };

            ManipulationDto.AddRange(itemsInClass);
            ManipulationDto.Add($"    }}\r\n");
            ManipulationDto.Add($"}}\r\n");


            await File.WriteAllLinesAsync(filePath + $"\\Shared\\DataTransferObjects\\{titleCaseModel}\\{titleCaseModel}Dto.cs", Dto);
            await File.WriteAllLinesAsync(filePath + $"\\Shared\\DataTransferObjects\\{titleCaseModel}\\{titleCaseModel}CreateDto.cs", CreateDto);
            await File.WriteAllLinesAsync(filePath + $"\\Shared\\DataTransferObjects\\{titleCaseModel}\\{titleCaseModel}UpdateDto.cs", UpdateDto);
            await File.WriteAllLinesAsync(filePath + $"\\Shared\\DataTransferObjects\\{titleCaseModel}\\{titleCaseModel}ManipulationDto.cs", ManipulationDto);

            return 1;

        }

        string ToTitleCase(string str)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(str.ToLower());
        }

    }
}
