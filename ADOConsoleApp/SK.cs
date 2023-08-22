using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.SemanticFunctions;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using Microsoft.VisualStudio.Services.Common;

public class SK: ISKExecutor
{
    private KernelBuilder builder;
    private IKernel kernel;

    private PromptTemplateConfig promptConfig = new PromptTemplateConfig
    {
        Completion =
    {
        MaxTokens = 1000,
        Temperature = 0.9,
        TopP = 0.0,
        PresencePenalty = 0.0,
        FrequencyPenalty = 0.0,
    }
    };

    public SK()
    {
        builder = new KernelBuilder();
        builder.WithAzureChatCompletionService(
             "chat",                  // Azure OpenAI Deployment Name
             "https://servicename.openai.azure.com/", // Azure OpenAI Endpoint
             "903a364b68574c2188a1e15572de739d");      // Azure OpenAI Key

        kernel = builder.Build();
    }

    public async Task<String> generateReport(IEnumerable<WorkItem> items)
    {
        Console.WriteLine("Mirror mirror, generate weekly report for me");

        var prompt = @"{{$input}}

            These are work items separated by comma. For each work item, generate 
            a one line description. 

            The format shall be: Task and url: ...Description...";

        var summarize = kernel.CreateSemanticFunction(prompt);

        var relevantFields = new List<String>()
        {
            "System.Title",
            "System.Description",
            "url"
        };

        var itemContents = items.Select(x => String.Join(",", relevantFields.Select(f => f + ": " + x.Fields[f].ToString())));

        var itemString = String.Join("; ", itemContents);
        var result = await summarize.InvokeAsync(itemString);
        return result.ToString();

        // Output:
        //   Energy conserved, entropy increases, zero entropy at 0K.
        //   Objects move in response to forces.
    }

    public async Task<String> generateContext() 
    {
        string text1 = @"
            1st Law of Thermodynamics - Energy cannot be created or destroyed.
            2nd Law of Thermodynamics - For a spontaneous process, the entropy of the universe increases.
            3rd Law of Thermodynamics - A perfect crystal at zero Kelvin has zero entropy.";
   
        Console.WriteLine("Oh, I'm console log");

        var prompt = @"{{$input}}

            One line TLDR with the fewest words.";

        var summarize = kernel.CreateSemanticFunction(prompt);

        var result = await summarize.InvokeAsync(text1);
        return result.ToString();

        // Output:
        //   Energy conserved, entropy increases, zero entropy at 0K.
        //   Objects move in response to forces.
    }

    public async void registerFunction()
    {
        string skPrompt = @"WRITE EXACTLY ONE JOKE or HUMOROUS STORY ABOUT THE TOPIC BELOW

            JOKE MUST BE:
            - G RATED
            - WORKPLACE/FAMILY SAFE
            NO SEXISM, RACISM OR OTHER BIAS/BIGOTRY

            BE CREATIVE AND FUNNY. I WANT TO LAUGH.
            +++++

            {{$input}}
            +++++

            ";

        var promptTemplate = new PromptTemplate(
            skPrompt,
            promptConfig,
            kernel
        );

        var functionConfig = new SemanticFunctionConfig(promptConfig, promptTemplate);
        var jokeFunction = kernel.RegisterSemanticFunction("FunSkill", "Joke", functionConfig);
        var result = await jokeFunction.InvokeAsync("time travel to dinosaur age");

        Console.WriteLine(result);
    }

}
