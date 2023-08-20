using Microsoft.SemanticKernel;

public class SK: ISKExecutor
{
    private KernelBuilder builder;
    private IKernel kernel;

    public SK()
    {
        builder = new KernelBuilder();
        builder.WithAzureChatCompletionService(
             "chat",                  // Azure OpenAI Deployment Name
             "https://servicename.openai.azure.com/", // Azure OpenAI Endpoint
             "903a364b68574c2188a1e15572de739d");      // Azure OpenAI Key

        kernel = builder.Build();
    }
    
    public async Task<String> generateContext() 
    {
        var prompt = @"{{$input}}

            One line TLDR with the fewest words.";

        var summarize = kernel.CreateSemanticFunction(prompt);

        string text1 = @"
            1st Law of Thermodynamics - Energy cannot be created or destroyed.
            2nd Law of Thermodynamics - For a spontaneous process, the entropy of the universe increases.
            3rd Law of Thermodynamics - A perfect crystal at zero Kelvin has zero entropy.";
   
        Console.WriteLine("Oh, I'm console log");

        var result = await summarize.InvokeAsync(text1);
        return result.ToString();

        // Output:
        //   Energy conserved, entropy increases, zero entropy at 0K.
        //   Objects move in response to forces.
    }

}
