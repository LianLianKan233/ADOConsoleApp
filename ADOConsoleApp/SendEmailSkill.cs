using Microsoft.SemanticKernel.Orchestration;
using Microsoft.SemanticKernel.SkillDefinition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkFromZero.Skills.EmailSkill
{
    public class SendEmailSkill
    {
        [SKFunction("Send an email to the receivers")]
        [SKFunctionContextParameter(Name = "Receivers", Description = "The list of email addresses of the receivers, separated by ','")]
        [SKFunctionContextParameter(Name = "Subject", Description = "The subject of the email")]
        [SKFunctionContextParameter(Name = "Content", Description = "The content of the email")]
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
