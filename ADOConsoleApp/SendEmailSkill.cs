using Microsoft.SemanticKernel.Orchestration;
using Microsoft.SemanticKernel.SkillDefinition;
using System.ComponentModel;
using System.Text;

namespace SkFromZero.Skills.EmailSkill
{
    public class SendEmailSkill
    {
        [SKFunction, Description("Send an email to the receivers")]
        public async Task<string> SendEmail(SKContext context)
        {
            var receivers = context["Receivers"].Split(',');
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"Subject: {context["Subject"]}");
            stringBuilder.AppendLine($"Content: {context["Content"]}");
            foreach (var receiver in receivers)
            {
                stringBuilder.AppendLine($"email sent to {receiver}");
            }
            return await Task.FromResult(stringBuilder.ToString());
        }
    }
}
